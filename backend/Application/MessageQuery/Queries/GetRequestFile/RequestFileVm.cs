namespace Application.Request.Queries.GetRequestFile;

public class RequestFileVm : FileVm
{
    /// <summary>
    /// Guid of document file
    /// </summary>
    /// <example>0f8fad5b-d9cb-469f-a165-70867728950e</example>
    public string Guid { get; set; } = null!;
}
