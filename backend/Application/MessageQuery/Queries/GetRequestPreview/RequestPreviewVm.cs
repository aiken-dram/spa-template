namespace Application.MessageQuery.Queries.GetRequestPreview;

/// <summary>
/// Data transfer object for previewing table of request response
/// </summary>
public class PreviewTableDto
{
    /// <summary>
    /// List of headers for table
    /// </summary>
    public string[]? Headers { get; set; }

    /// <summary>
    /// Data for table
    /// </summary>
    public IEnumerable<string[]>? Data { get; set; }
}


/// <summary>
/// Types of previews
/// </summary>
public enum eRequestPreviewType
{
    //data in table format
    Table,

    //file directly
    File
}

/// <summary>
/// View model for previewing content of request result
/// </summary>
public class RequestPreviewVm
{
    /// <summary>
    /// Type of preview:
    /// Table - data in table form with Content as TablePreview
    /// File - image as file link with Content as FileVm
    /// </summary>
    public eRequestPreviewType Type { get; set; }

    /// <summary>
    /// Content of preview as object
    /// </summary>
    public object Content { get; set; } = null!;
}
