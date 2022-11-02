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
    /// Field and old and new values { Field: "field", Old: "old", New: "new" }
    /// </summary>
    [Dictionary("FIELD_OLD_NEW")]
    FieldOldNew = 3,

    /// <summary>
    /// Field and operation and value { Field: "field", Operation: "operation", Value: "value" }
    /// </summary>
    [Dictionary("FIELD_OPERATION_VALUE")]
    FieldOperationValue = 4,
}

/// <summary>
/// Operations for FIELD_OPERATION_VALUE audit data type
/// </summary>
public static class eAuditDataOperations
{
    /// <summary>
    /// Adding value to list
    /// </summary>
    public const string AddList = "AddList";

    /// <summary>
    /// Removing value from list
    /// </summary>
    public const string RemoveList = "RemoveList";
}