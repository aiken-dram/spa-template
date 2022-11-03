namespace Infrastructure.Common.Interfaces;

public interface IConfigurationService
{
    /// <summary>
    /// Path to where message query results are stored
    /// </summary>
    string RequestStoragePath { get; }

    /// <summary>
    /// Path for exporting data from database, relative to database
    /// </summary>
    string DatabaseExportPath { get; }

    /// <summary>
    /// Path for exporting data from database, relative to application
    /// </summary>
    string DatabaseExportRemotePath { get; }

    /// <summary>
    /// Path to where R scripts are stored
    /// </summary>
    string RScriptPath { get; }

    /// <summary>
    /// Path to where headers for database export are stored
    /// </summary>
    string HeadersPath { get; }

    /// <summary>
    /// Maximum rows for previewing request result tables
    /// </summary>
    int PreviewMaxRows { get; }

    /// <summary>
    /// Number of failed authorizations before user will be locked
    /// </summary>
    int AuthLock { get; }

    /// <summary>
    /// Number of minutes authorization failures are accounted for when resolving timeout and locking
    /// </summary>
    int AuthPeriod { get; }

    /// <summary>
    /// Number of failed authorizations before user timeout will be implemented
    /// </summary>
    int AuthTimeout { get; }

    /// <summary>
    /// Minutes of timeout for one failed authorization
    /// </summary>
    int AuthBaseTimeout { get; }

    /// <summary>
    /// Path to R bin directory
    /// </summary>
    string rPath { get; }

    /// <summary>
    /// Path to R home directory
    /// </summary>
    string rHome { get; }

    /// <summary>
    /// PATH environmental variable for R service
    /// </summary>
    string PATH { get; }

    /// <summary>
    /// Host of Rabbit MQ
    /// </summary>
    string MQ { get; }

    /// <summary>
    /// Port of Rabbit MQ
    /// </summary>
    int MQPort { get; }

    /// <summary>
    /// Username of Rabbit MQ
    /// </summary>
    string MQUser { get; }

    /// <summary>
    /// Password of Rabbit MQ
    /// </summary>
    string MQPass { get; }

    /// <summary>
    /// URI of RabbitMQ
    /// </summary>
    string MQUri { get; }
}
