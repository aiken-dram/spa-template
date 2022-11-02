using System.Globalization;
using System.Text;
using Application.Account.User.Queries.GetUserTable;
using Application.Sample.Queries.GetSampleTable;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.Files;

public partial class FileService
{
    /// <summary>
    /// Builds csv file byte data from list of records using mapping
    /// </summary>
    /// <param name="records">List of records</param>
    /// <typeparam name="T">type of records</typeparam>
    /// <typeparam name="M">mapping class</typeparam>
    /// <returns>Byte data with csv file</returns>
    private async Task<byte[]> BuildCSVAsync<T, M>(IEnumerable<T>? records, CancellationToken cancellationToken)
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
                await csvWriter.WriteRecordsAsync(records, cancellationToken);
        }

        return memoryStream.ToArray();
    }

    public Task<byte[]> BuildUserTableFileAsync(IEnumerable<UserTableDto>? records, CancellationToken cancellationToken)
    => BuildCSVAsync<UserTableDto, UserTableFileMap>(records, cancellationToken);

    public Task<byte[]> BuildSampleTableFileAsync(IEnumerable<SampleTableDto>? records, CancellationToken cancellationToken)
    => BuildCSVAsync<SampleTableDto, SampleTableFileMap>(records, cancellationToken);
}
