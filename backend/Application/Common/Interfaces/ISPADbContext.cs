using Shared.Application.Models.DB2;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

/// <summary>
/// Database context
/// </summary>
public interface ISPADbContext
{
    /// <summary>
    /// Audit builder
    /// </summary>
    /// <remarks>
    /// For implementing manual audit
    /// </remarks>
    IAuditBuilder AuditBuilder { get; }

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
    /// User audit
    /// </summary>
    DbSet<UserAudit> UserAudits { get; set; }

    /// <summary>
    /// User audit data
    /// </summary>
    DbSet<UserAuditData> UserAuditData { get; set; }

    /* might have to change from view to MQT later when audit will become too big */
    /* or maybe even not union them at all, if even bigger */
    /// <summary>
    /// View of all audit
    /// </summary>
    DbSet<VAudit> VAudits { get; set; }

    /// <summary>
    /// View of all audit data
    /// </summary>
    DbSet<VAuditData> VAuditData { get; set; }
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
    /// Dictionary of audit actions
    /// </summary>
    DbSet<AuditAction> AuditActions { get; set; }

    /// <summary>
    /// Dictionary of audit targets
    /// </summary>
    DbSet<AuditTarget> AuditTargets { get; set; }

    /// <summary>
    /// Dictionary of audit data types
    /// </summary>
    DbSet<AuditDataType> AuditDataTypes { get; set; }

    /// <summary>
    /// Dictionary of R script parameter types
    /// </summary>
    DbSet<RScriptParamType> RScriptParamTypes { get; set; }

    /// <summary>
    /// Sample dictionary
    /// </summary>
#warning SAMPLE, remove in actual application
    DbSet<SampleDict> SampleDicts { get; set; }


    /// <summary>
    /// Dictionary of sample types
    /// </summary>
#warning SAMPLE, remove in actual application
    DbSet<SampleType> SampleTypes { get; set; }
    #endregion

    #region MESSAGE QUERY
    /// <summary>
    /// Requests in message query for processing with background service worker
    /// </summary>
    DbSet<Domain.Entities.Request> Requests { get; set; }
    #endregion

    #region R
    /// <summary>
    /// R scripts
    /// </summary>
    DbSet<Domain.Entities.RScript> RScripts { get; set; }

    /// <summary>
    /// R script parameters
    /// </summary>
    DbSet<Domain.Entities.RScriptParam> RScriptParams { get; set; }

    /// <summary>
    /// Statistic menu tree
    /// </summary>
    DbSet<Domain.Entities.RScriptTreeNode> RScriptTree { get; set; }
    #endregion

#warning SAMPLE, remove entire region in actual application
    #region SAMPLE
    /// <summary>
    /// Sample entities
    /// </summary>
    DbSet<Domain.Entities.Sample> Samples { get; set; }

    /// <summary>
    /// Sample children entities
    /// </summary>
    DbSet<SampleChild> SampleChildren { get; set; }

    /// <summary>
    /// Sample audit
    /// </summary>
    DbSet<SampleAudit> SampleAudits { get; set; }

    /// <summary>
    /// Sample audit data
    /// </summary>
    DbSet<SampleAuditData> SampleAuditData { get; set; }
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
    /// Get SQL messages from sql
    /// </summary>
    /// <param name="sql">SQL</param>
    IQueryable<SqlMessage> GetSqlMessages(string sql);

    /// <summary>
    /// Export SQL to CSV file from database
    /// </summary>
    /// <param name="sql">SQL</param>
    /// <param name="file">Name of file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="timestampFormat">DateTime format for export</param>
    Task<ExportResult> ExportSQLAsync(string sql, string file, CancellationToken cancellationToken, string timestampFormat = "DD.MM.YYYY");
    #endregion
}