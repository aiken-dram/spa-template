using System.Reflection;
using System.Text;
using Application;
using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Hubs;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Common;
using WebApi.Services;
using Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    if (builder.Configuration["Logging:SaveToFile"] == "True")
        logging.AddFile("Logs/spa-{Date}.log", outputTemplate: "begin{NewLine}{Message}{NewLine}end{NewLine}");
});

// Add services to the container.

//configuration
builder.Services.AddConfiguration(builder.Configuration);

//application DI
builder.Services.AddApplication();

//infrastructure DI
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApiInfrastructure();

//current user from httpcontext service
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

//database health check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<SPADbContext>();

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ISPADbContext>());

// Customise default API behaviour
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// In production, the vue files will be served from this directory
//builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot"; });
builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = builder.Configuration["SpaRootPath"]; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SPA", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Id = "Bearer", //The name of the previously defined security scheme.
                    Type = ReferenceType.SecurityScheme
                }
            },new List<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    xmlPath = Path.Combine(AppContext.BaseDirectory, "Application.xml");
    c.IncludeXmlComments(xmlPath);
});

// configure jwt authentication
var key = Encoding.ASCII.GetBytes("key_which_i_think_should_not_be_here_2022");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//configure SignalR
builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromSeconds(5 * 60);
    options.EnableDetailedErrors = true;
});

builder.Services.AddHostedService<SignalRWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCustomExceptionHandler();
app.UseHealthChecks("/health");
if (builder.Configuration["UseHttpsRedirection"] == "True")
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPA V1");
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/hubs/notification");
});

app.UseSpa(spa =>
{
    string src = builder.Configuration["SpaSourcePath"];
    if (!string.IsNullOrEmpty(src))
        spa.Options.SourcePath = src;
    if (app.Environment.IsDevelopment())
        spa.UseProxyToSpaDevelopmentServer(baseUri: "http://localhost:8080");
});

app.Run();
