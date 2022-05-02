using System.Text;
using Shared.Application.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Query;

public class UserExportJson
{
    public long? type { get; set; }
}

public partial class QueryResponseBuilder
{
    private async Task TableExportAuditAsync(Request req, CancellationToken cancellationToken)
    {
        string guid = Guid.NewGuid().ToString(); //lets generate guid
        req.Guid = guid;

        //set state of request to processing
        req.IdState = (int)eRequestState.Processing;
        await _context.SaveChangesAsync(cancellationToken);

        try
        {
            _logger.LogInformation($"json: {req.Json}");
            //parse jsoon
            var par = JsonConvert.DeserializeObject<UserExportJson>(req.Json ?? string.Empty);

            //export file from DB2
            //SQL
            StringBuilder sql = new StringBuilder("SELECT * ");

            //2D - WIP
            //2D could use query.ToQueryString(); to get SQL from some query in some domain class or something?
            sql.Append("FROM ACCOUNT.USERS U ");
            //if (par.type.HasValue)
            //    sql.Append($"AND U.ID_TYPE = {par.type} ");

            string fname = $"{_databaseExportPath}{guid}";
            _logger.LogInformation($"SQL: {sql}");
            await _context.ExportSQLAsync(sql.ToString(), fname, cancellationToken);
            _logger.LogInformation("Data exported to csv");

            //copy file from server to documents folder
            System.IO.File.Copy(Path.Combine(_exportPath, guid), Path.Combine(_localPath, guid + "_data"), true);

            //delete file from export
            System.IO.File.Delete(Path.Combine(_exportPath, guid));

            //add column headers
            using (var output = System.IO.File.Create(Path.Combine(_localPath, guid)))
            {
                foreach (var file in new[] { "user_headers.txt", guid + "_data" })
                    using (var input = System.IO.File.OpenRead(Path.Combine(_localPath, file)))
                    {
                        input.CopyTo(output);
                    }
            }

            //delete _data file
            System.IO.File.Delete(Path.Combine(_localPath, guid + "_data"));

            _logger.LogInformation("Csv file processed");
            //set request state as ready
            req.Processed = DateTime.Now;
            req.IdState = (int)eRequestState.Ready;
        }
        catch (Exception err)
        {
            _logger.LogError(err, "Error while processing request");
            //set request state as error
            req.Message = err.Message.Truncate(500);
            req.IdState = (int)eRequestState.Error;
        }

        _logger.LogInformation($"Processing document with id {req.IdRequest} is finished");
        //save request changes in database
        await _context.SaveChangesAsync(cancellationToken);
    }
}
