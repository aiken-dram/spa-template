using Shared.Application.Models.DB2;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface ISPADbContext
{
    #region ACCOUNT
    /// <summary>
    /// Links between access groups and access roles 
    /// </summary>
    DbSet<GroupRole> GroupRoles { get; set; }

    /// <summary>
    /// Access groups for application
    /// </summary>
    DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Access modules for application
    /// </summary>
    DbSet<Module> Modules { get; set; }

    /// <summary>
    /// Links between access roles and access modules
    /// </summary>
    DbSet<RoleModule> RoleModules { get; set; }

    /// <summary>
    /// Access roles for application
    /// </summary>
    DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Links between application users and access groups
    /// </summary>
    DbSet<UserGroup> UserGroups { get; set; }

    /// <summary>
    /// Links between application users and access roles
    /// </summary>
    DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Links between application users and districts
    /// </summary>
    DbSet<UserDistrict> UserDistricts { get; set; }

    /// <summary>
    /// Application users
    /// </summary>
    DbSet<User> Users { get; set; }

    /// <summary>
    /// User audit events
    /// </summary>
    DbSet<UserEvent> UserEvents { get; set; }

    /// <summary>
    /// User audit events data
    /// </summary>
    DbSet<UserEventData> UserEventData { get; set; }
    #endregion

    #region DICTIONARY
    /// <summary>
    /// Dictionary of request types
    /// </summary>
    DbSet<RequestType> RequestTypes { get; set; }

    /// <summary>
    /// Dictionary of request states
    /// </summary>
    DbSet<RequestState> RequestStates { get; set; }

    /// <summary>
    /// Dictionary of districts
    /// </summary>
    DbSet<District> Districts { get; set; }

    /// <summary>
    /// Dictionary of audit event actions
    /// </summary>
    DbSet<EventAction> EventActions { get; set; }

    /// <summary>
    /// Dictionary of audit event targets
    /// </summary>
    DbSet<EventTarget> EventTargets { get; set; }
    #endregion

    #region MESSAGE QUERY
    /// <summary>
    /// Requests in message query for processing with background service worker
    /// </summary>
    DbSet<Domain.Entities.Request> Requests { get; set; }
    #endregion

    /// <summary>
    /// Save changes to entities into database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    #region COMMANDS
    /// <summary>
    /// Load file into database table
    /// </summary>
    /// <param name="file">Name of file</param>
    /// <param name="method">Method</param>
    /// <param name="table">Table name</param>
    /// <param name="fields">Fields</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Load result</returns>
    Task<LoadResult> UploadFileAsync(string file, string method, string table, string fields, CancellationToken cancellationToken);

    /// <summary>
    /// Sets integrity on selected table with immediate checked
    /// </summary>
    /// <param name="table">Name of table</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<int> SetIntegrityAsync(string table, CancellationToken cancellationToken);

    /// <summary>
    /// Get SQL messages from sql request
    /// </summary>
    /// <param name="sql">SQL request</param>
    IQueryable<SqlMessage> GetSqlMessages(string sql);

    /// <summary>
    /// EXPORT SQL to CSV file
    /// </summary>
    /// <param name="sql">SQL</param>
    /// <param name="file">Name of file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<ExportResult> ExportSQLAsync(string sql, string file, CancellationToken cancellationToken);
    #endregion
}