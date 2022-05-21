using System.Globalization;
using System.Text;
using CsvHelper;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using CsvHelper.Configuration;
using System.Linq;
using Application.Account.User.Queries.GetUserTable;

namespace Infrastructure.Files;

public partial class FileService : IFileService
{
    private readonly ILogger _logger;

    public FileService(ILogger<FileService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Builds csv file byte data from list of records using mapping
    /// </summary>
    /// <param name="records">List of records</param>
    /// <typeparam name="T">type of records</typeparam>
    /// <typeparam name="M">mapping class</typeparam>
    /// <returns>Byte data with csv file</returns>
    private byte[] BuildCSV<T, M>(IEnumerable<T>? records)
     where M : ClassMap<T>
     where T : class
    {
        var enc1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251) ?? Encoding.UTF8;
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream, enc1251))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.GetCultureInfo("ru-RU"));
            csvWriter.Context.RegisterClassMap<M>();
            if (records != null)
                csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }

    public byte[] BuildUserTableFile(IEnumerable<UserTableDto>? records)
    {
        return BuildCSV<UserTableDto, UserTableFileMap>(records);
    }

    public void DeleteRequestFile(string guid)
    {
        //2D
        throw new NotImplementedException();
    }

    public string[] ReadRScriptFile(string fname)
    {
        //2D
        throw new NotImplementedException();
    }
}
