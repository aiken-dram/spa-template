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
    /// Application users
    /// </summary>
    DbSet<User> Users { get; set; }

    /// <summary>
    /// User authorization events
    /// </summary>
    DbSet<UserAuth> UserAuth { get; set; }
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
    /// Dictionary of authorization event actions
    /// </summary>
    DbSet<AuthAction> AuthActions { get; set; }
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
    /// EXPORT SQL to CSV file
    /// </summary>
    /// <param name="sql">SQL</param>
    /// <param name="file">Name of file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<ExportResult> ExportSQLAsync(string sql, string file, CancellationToken cancellationToken);
    #endregion
}