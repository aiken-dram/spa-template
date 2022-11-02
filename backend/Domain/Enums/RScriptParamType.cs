namespace Domain.Enums;

/// <summary>
/// Type of R script parameters
/// </summary>
public enum eRScriptParamType : int
{
    /// <summary>
    /// Value from district dictionary
    /// </summary>
    [Dictionary("DICT.DISTRICT")]
    DistrictDictionary = 1,
}
