using Application.Account.User.Queries.GetUserTable;

namespace Application.Common.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Export user table to csv
    /// </summary>
    /// <param name="records">user table items</param>
    byte[] BuildUserTableFile(IEnumerable<UserTableDto>? records);

    /// <summary>
    /// Delete requested file
    /// </summary>
    /// <param name="guid">Guid of requested file</param>
    void DeleteRequestFile(string? guid);

    /// <summary>
    /// Read R script file
    /// </summary>
    /// <param name="fname">Name of script file</param>
    /// <returns>Content of file as array of string</returns>
    string[] ReadRScriptFile(string fname);
}
