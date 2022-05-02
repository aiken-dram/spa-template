using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Interfaces;
using Shared.Application.Models.DB2;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Enums;
using Shared.Domain.Models;
using Shared.Domain.Attributes;
using Infrastructure.Common;

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

    public SPADbContext(
        DbContextOptions<SPADbContext> options,
        IDomainEventService domainEventService,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _domainEventService = domainEventService;
        _currentUserService = currentUserService;
    }

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
    public virtual DbSet<UserEvent> UserEvents { get; set; } = null!;
    public virtual DbSet<UserEventData> UserEventData { get; set; } = null!;
    #endregion

    #region DICTIONARY
    public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
    public virtual DbSet<RequestState> RequestStates { get; set; } = null!;
    public virtual DbSet<EventAction> EventActions { get; set; } = null!;
    public virtual DbSet<EventTarget> EventTargets { get; set; } = null!;
    public virtual DbSet<District> Districts { get; set; } = null!;
    #endregion

    #region QUERY
    public virtual DbSet<Request> Requests { get; set; } = null!;
    #endregion

    #region COMMANDS
    /** these are DB2 funcitons, 2D: change to postgre */
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

    public async Task<ExportResult> ExportSQLAsync(string sql, string file, CancellationToken cancellationToken)
    {
        string SQL = $"CALL SYSPROC.ADMIN_CMD('EXPORT TO \"{file}\" OF DEL MODIFIED BY COLDEL; CODEPAGE=1251 TIMESTAMPFORMAT=\"DD.MM.YYYY\" {sql}')";
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
        //Will create audit events for AuditableEntities that have [AutoAudit] attribute
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            var t = entry.Entity.GetType();
            if (Attribute.IsDefined(t, typeof(AutoAuditAttribute)))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AuditEvents.Add(AutoAuditHelper.Create(entry));
                        break;

                    case EntityState.Modified:
                        entry.Entity.AuditEvents.Add(AutoAuditHelper.Edit(entry));
                        break;

                    case EntityState.Deleted:
                        entry.Entity.AuditEvents.Add(AutoAuditHelper.Delete(entry));
                        break;
                }
            }
        }

        var audits = ChangeTracker.Entries<AuditableEntity>()
            .Select(x => x.Entity.AuditEvents)
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
    /// Save audit event log into database
    /// </summary>
    /// <param name="auditEvents"></param>
    public void Log(IEnumerable<AuditEvent> auditEvents)
    {
        //current user
        var uid = Convert.ToInt64(_currentUserService.UserId);

        // later i would need to split audit into appropriate tables, 
        // but for now i only have logs in ACCOUNT schema
        foreach (var @event in auditEvents)
        {
            @event.IsLogged = true;
            //insert current user for not auth events
            if (@event.IdTarget != (int)eEventTarget.Auth)
                @event.IdUser = uid;
            //add user events
            UserEvents.Add(new UserEvent(@event));
        }
    }
}
