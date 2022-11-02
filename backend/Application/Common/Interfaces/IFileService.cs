using Application.Account.User.Queries.GetUserTable;
using Application.MessageQuery.Queries.GetRequestPreview;
using Application.Sample.Queries.GetSampleTable;

namespace Application.Common.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Export user table to csv
    /// </summary>
    /// <param name="records">User table items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<byte[]> BuildUserTableFileAsync(IEnumerable<UserTableDto>? records, CancellationToken cancellationToken);

    /// <summary>
    /// Export sample table to csv
    /// </summary>
    /// <param name="records">Sample table items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<byte[]> BuildSampleTableFileAsync(IEnumerable<SampleTableDto>? records, CancellationToken cancellationToken);

    /// <summary>
    /// Preview request response file as table
    /// </summary>
    /// <param name="guid">Guid of request response file</param>
    /// <param name="contentType">Type of file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Preview content of request response file</returns>
    Task<PreviewTableDto> PreviewRequestResponseTable(string guid, string contentType, CancellationToken cancellationToken);

    /// <summary>
    /// Delete request response file
    /// </summary>
    /// <param name="guid">Guid of request response file</param>
    void DeleteRequestResponseFile(string guid);

    /// <summary>
    /// Reads R script file
    /// </summary>
    /// <param name="fname">Name of R script file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>R script content as string</returns>
    Task<string> ReadRScriptFileAsync(string fname, CancellationToken cancellationToken);

    /// <summary>
    /// Saves content to R script file
    /// </summary>
    /// <param name="fname">Name of R script file</param>
    /// <param name="content">R script content as string</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SaveRScriptFileAsync(string fname, string? content, CancellationToken cancellationToken);
}
