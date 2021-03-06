namespace Domain.Enums;

/// <summary>
/// Standard audit data types
/// </summary>
public enum eAuditDataType : int
{
    /// <summary>
    /// Name { Value: "value" }
    /// </summary>
    [Dictionary("VALUE")]
    Value = 1,

    /// <summary>
    /// Field and value { Field: "field", Value: "value" }
    /// </summary>
    [Dictionary("FIELD_VALUE")]
    FieldValue = 2,

    /// <summary>
    /// Field and old nad new values { Field: "field", Old: "old", New: "new" }
    /// </summary>
    [Dictionary("FIELD_OLD_NEW")]
    FieldOldNew = 3
}