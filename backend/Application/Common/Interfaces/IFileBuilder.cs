using Application.Account.User.Queries.GetUserTable;

namespace Application.Common.Interfaces;

public interface IFileBuilder
{
    /// <summary>
    /// Export user table to csv
    /// </summary>
    /// <param name="records">user table items</param>
    byte[] BuildUserTableFile(IEnumerable<UserTableDto>? records);
}
