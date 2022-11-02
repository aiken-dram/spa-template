using System.Globalization;
using Application.MessageQuery.Queries.GetRequestPreview;
using CsvHelper;
using Shared.Application.Models;

namespace Infrastructure.Files;

public partial class FileService
{
    public async Task<PreviewTableDto> PreviewRequestResponseTable(string guid, string contentType, CancellationToken cancellationToken)
    {
        //read file with guid and save output of N rows to PreviewTableDto
        var res = new PreviewTableDto();
        res.Data = new List<string[]>();
        switch (contentType)
        {
            case FileContentType.CSV:
                //2D: check if this works
                using (var stream = File.OpenRead(Path.Combine(_configuration.RequestStoragePath, guid)))
                using (var reader = new StreamReader(stream))
                using (var parser = new CsvParser(reader, CultureInfo.GetCultureInfo("ru-RU")))
                {
                    if (await parser.ReadAsync())
                    {
                        //header
                        res.Headers = parser.Record;

                        //data
                        int i = 0;
                        while ((await parser.ReadAsync()) && (i++ < _configuration.PreviewMaxRows))
                            res.Data.Append(parser.Record);
                    }
                }
                break;
        }
        return res;
    }

    public void DeleteRequestResponseFile(string guid)
    {
        File.Delete(Path.Combine(_configuration.RequestStoragePath, guid));
    }
}
