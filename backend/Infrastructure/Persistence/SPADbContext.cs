using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Shared.Application.Models.DB2;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Attributes;
using Shared.Application.Interfaces;
using Infrastructure.Services;

namespace Infrastructure.Persistence;

public class SQLResult
{
    public int RES { get; set; }
}

#region COMMAND CONFIGURATION
public class SQLResultConfiguration : IEntityTypeConfiguration<SQLResult>
{
    public void Configure(EntityTypeBuilder<SQLResult> builder)
    {
        builder.HasNoKey();
    }
}

public class LoadResultConfiguration : IEntityTypeConfiguration<LoadResult>
{
    public void Configure(EntityTypeBuilder<LoadResult> builder)
    {
        builder.HasNoKey();
    }
}

public class ExportResultConfiguration : IEntityTypeConfiguration<ExportResult>
{
    public void Configure(EntityTypeBuilder<ExportResult> builder)
    {
        builder.HasNoKey();
    }
}

public class SqlMessageConfiguration : IEntityTypeConfiguration<SqlMessage>
{
    public void Configure(EntityTypeBuilder<SqlMessage> builder)
    {
        builder.HasNoKey();
    }
}
#endregion

public partial class SPADbContext : DbContext, ISPADbContext
{
    private readonly IDomainEventService _domainEventService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuditBuilder _audit;

    public SPADbContext(
        DbContextOptions<SPADbContext> options,
        IDomainEventService domainEventService,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _domainEventService = domainEventService;
        _currentUserService = currentUserService;

        // constructor is here since we'll be calling AuditBuilde from this context
        // cant use DI cause of circular dependency (well i havent researched this yet)
        // option configuration goes here, if necessary
        _audit = new AppAuditBuilder(this, options => { });
    }

    public IAuditBuilder AuditBuilder => _audit;

    #region SYSTEM
    public virtual DbSet<SQLResult> SQLResult { get; set; } = null!;
    public virtual DbSet<LoadResult> LoadResult { get; set; } = null!;
    public virtual DbSet<ExportResult> ExportResult { get; set; } = null!;
    public virtual DbSet<SqlMessage> SqlMessage { get; set; } = null!;
    #endregion

    #region ACCOUNT
    public virtual DbSet<GroupRole> GroupRoles { get; set; } = null!;
    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<Module> Modules { get; set; } = null!;
    public virtual DbSet<RoleModule> RoleModules { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
    public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
    public virtual DbSet<UserDistrict> UserDistricts { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UserAudit> UserAudits { get; set; } = null!;
    public virtual DbSet<UserAuditData> UserAuditData { get; set; } = null!;
    public virtual DbSet<VAudit> VAudits { get; set; } = null!;
    public virtual DbSet<VAuditData> VAuditData { get; set; } = null!;
    #endregion

    #region DICTIONARY
    public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
    public virtual DbSet<RequestState> RequestStates { get; set; } = null!;
    public virtual DbSet<AuditAction> AuditActions { get; set; } = null!;
    public virtual DbSet<AuditTarget> AuditTargets { get; set; } = null!;
    public virtual DbSet<District> Districts { get; set; } = null!;
    public virtual DbSet<AuditDataType> AuditDataTypes { get; set; } = null!;
    public virtual DbSet<RScriptParamType> RScriptParamTypes { get; set; } = null!;

#warning SAMPLE, remove next 2 lines in actual application
    public DbSet<SampleDict> SampleDicts { get; set; } = null!;
    public DbSet<SampleType> SampleTypes { get; set; } = null!;
    #endregion

    #region QUERY
    public virtual DbSet<Request> Requests { get; set; } = null!;
    #endregion

    #region R
    public DbSet<Domain.Entities.RScript> RScripts { get; set; } = null!;
    public DbSet<RScriptParam> RScriptParams { get; set; } = null!;
    public DbSet<RScriptTreeNode> RScriptTree { get; set; } = null!;
    #endregion

#warning SAMPLE, remove entire region in actual application
    #region SAMPLE
    public virtual DbSet<Sample> Samples { get; set; } = null!;
    public DbSet<SampleChild> SampleChildren { get; set; } = null!;
    public DbSet<SampleAudit> SampleAudits { get; set; } = null!;
    public DbSet<SampleAuditData> SampleAuditData { get; set; } = null!;
    #endregion

    #region COMMANDS
    public async Task<LoadResult> UploadFileAsync(string file, string method, string table, string fields, CancellationToken cancellationToken)
    {
        string sql = $"CALL SYSPROC.ADMIN_CMD('LOAD FROM \"{file}\" OF DEL MODIFIED BY COLDEL; CODEPAGE=1251 DATEFORMAT=\"DD.MM.YYYY\" METHOD P({method}) MESSAGES ON SERVER INSERT INTO {table} ({fields})')";
        var res = await this.LoadResult.FromSqlRaw(sql).ToListAsync(cancellationToken);
        return res.First();
    }

    public async Task<int> SetIntegrityAsync(string table, CancellationToken cancellationToken)
    {
        var res = await this.Database.ExecuteSqlRawAsync($"SET INTEGRITY FOR {table} IMMEDIATE CHECKED", cancellationToken);
        return res;
    }

    public IQueryable<SqlMessage> GetSqlMessages(string sql)
    {
        var res = this.SqlMessage.FromSqlRaw(sql);
        return res;
    }

    public async Task<ExportResult> ExportSQLAsync(string sql, string file, CancellationToken cancellationToken, string timestampFormat)
    {
        string SQL = $"CALL SYSPROC.ADMIN_CMD('EXPORT TO \"{file}\" OF DEL MODIFIED BY COLDEL; CODEPAGE=1251 TIMESTAMPFORMAT=\"{timestampFormat}\" {sql}')";
        var res = await this.ExportResult.FromSqlRaw(SQL).ToListAsync(cancellationToken);
        return res.First();
    }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //use configuration from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SPADbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //Will create audit for AuditableEntities that have [AutoAudit] attribute
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            var t = entry.Entity.GetType();
            if (Attribute.IsDefined(t, typeof(AutoAuditAttribute)))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Audits.Add(await _audit.CreateAsync(entry));
                        break;

                    case EntityState.Modified:
                        entry.Entity.Audits.Add(await _audit.EditAsync(entry));
                        break;

                    case EntityState.Deleted:
                        entry.Entity.Audits.Add(_audit.Delete(entry));
                        break;
                }
            }
        }

        var audits = ChangeTracker.Entries<AuditableEntity>()
            .Select(x => x.Entity.Audits)
            .SelectMany(x => x)
            .Where(audit => !audit.IsLogged)
            .ToList().ToArray();

        Log(audits);

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    /// <summary>
    /// Dispatch domain events through domain event service
    /// </summary>
    /// <param name="events">Domain events</param>
    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }

    /// <summary>
    /// Save audit log into database
    /// </summary>
    /// <param name="audits">List of audits</param>
    public void Log(IEnumerable<Audit> audits)
    {
        //current user
        var uid = Convert.ToInt64(_currentUserService.UserId);

        // split audit into appropriate tables
        foreach (var audit in audits)
        {
            audit.IsLogged = true;
            //insert current user for not auth audit
            if (audit.IdTarget != (int)eAuditTarget.Auth)
                audit.IdUser = uid;
            switch (audit.IdTarget)
            {
#warning SAMPLE, remove in actual application
                case (int)eAuditTarget.Sample:
                    //add audit for sample to SampleAudit table
                    SampleAudits.Add(new SampleAudit(audit));
                    break;

                default:
                    //default is adding audit to UserAudits table
                    UserAudits.Add(new UserAudit(audit));
                    break;
            }

        }
    }
}
