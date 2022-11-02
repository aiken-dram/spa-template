namespace Domain.Enums;

/// <summary>
/// Audit targets in application
/// </summary>
public enum eAuditTarget : int
{
    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("AUTH")]
    Auth = 1,

    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("ACCOUNT.USERS")]
    AccountUser = 2,

    /// <summary>
    /// Domain/MessageQuery/Request entity
    /// </summary>
    [Dictionary("MQ.REQUESTS")]
    MessageQueryRequest = 3,

    /// <summary>
    /// Domain/Dictionary/District entity
    /// </summary>
    [Dictionary("DICT.DISTRICTS")]
    DictionaryDistrict = 4,

    /// <summary>
    /// Domain/R/RScript entity
    /// </summary>
    [Dictionary("R.RSCRIPT")]
    RScript = 5,

    /// <summary>
    /// Domain/R/RScriptTree entity
    /// </summary>
    [Dictionary("R.RSCRIPT_TREE")]
    RScriptTree = 6,

#warning SAMPLE, remove in actual application
    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("DICT.SAMPLE_DICTS")]
    DictionarySample = 7,

#warning SAMPLE, remove in actual application    
    /// <summary>
    /// Domain/Sample/Sample entity
    /// </summary>
    [Dictionary("SAMPLE.SAMPLE")]
    Sample = 8
}