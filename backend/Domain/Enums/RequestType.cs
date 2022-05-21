namespace Domain.Enums;

/// <summary>
/// Request types for message query processing
/// </summary>
public enum eRequestType : int
{
    /// <summary>
    /// Export data from audit table
    /// </summary>
    [Dictionary("TABLE_EXPORT_AUDIT")]
    TableExportAudit = 1,

    /// <summary>
    /// Export data from sample table
    /// </summary>
    [Dictionary("TABLE_EXPORT_SAMPLE")]
    TableExportSample = 2,

    /// <summary>
    /// Run R script
    /// </summary>
    [Dictionary("RSCRIPT")]
    RScript = 3
}