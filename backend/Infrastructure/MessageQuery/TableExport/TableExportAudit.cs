using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Application.Account.User.Queries.GetAuditTable;

namespace Infrastructure.MessageQuery;

/// <summary>
/// Json for export audit
/// </summary>
public class ExportAuditJson
{
    /// <summary>
    /// Id of user
    /// </summary>
    public long? IdUser { get; set; }

    /// <summary>
    /// List of filters
    /// </summary>
    public IList<string>? Filters { get; set; }
}

public partial class QueryResponseBuilder
{
    private async Task TableExportAuditAsync(Request request, CancellationToken cancellationToken)
    {
        //start processing request
        var guid = request.Start();
        await _context.SaveChangesAsync(cancellationToken);
        _mq.SendRequestSignalR(request);

        try
        {
            _logger.LogInformation($"json: {request.Json}");
            //parse jsoon
            var par = JsonConvert.DeserializeObject<ExportAuditJson>(request.Json ?? string.Empty) ?? new ExportAuditJson();

            //SQL
            //hey this works, niiice!
            var _query = GetAuditTableQueryHandler.Query(_context, _mapper, par.IdUser, null, par.Filters);
            //list of fields
            var query = _query.Select(p => new
            {
                source = (p.source == "USER" ? "Пользователь" : (p.source == "SAMPLE" ? "Пример" : p.source)),
                p.login,
                p.stamp,
                p.targetDesc,
                p.action,
                p.targetName
            });
            //not sure how list property would translate, prolly wont?
            //well i'm not gonna export those anyway, this is just activity log, without data
            var sql = query.ToQueryString();

            //if there're string values inside query we'll need to double em for the command
            sql = sql.Replace("'", "''");

            string fname = Path.Combine(_configuration.DatabaseExportPath, guid);
            _logger.LogInformation($"SQL: {sql}");
            await _context.ExportSQLAsync(sql, fname, cancellationToken, "DD.MM.YYYY HH:MM:SS");
            _logger.LogInformation("Data exported to csv");

            //copy file from (remote) database server to application server
            System.IO.File.Copy(Path.Combine(_configuration.DatabaseExportRemotePath, guid), Path.Combine(_configuration.RequestStoragePath, guid + "_data"), true);

            //delete file from (remote) database server
            System.IO.File.Delete(Path.Combine(_configuration.DatabaseExportRemotePath, guid));

            //add column headers
            using (var output = System.IO.File.Create(Path.Combine(_configuration.RequestStoragePath, guid)))
            {
                foreach (var file in new[]
                    {
                        Path.Combine(_configuration.HeadersPath, "audit_table.txt"),
                        Path.Combine(_configuration.RequestStoragePath, guid + "_data")
                    })
                    using (var input = System.IO.File.OpenRead(file))
                    {
                        input.CopyTo(output);
                    }
            }

            //delete _data file
            System.IO.File.Delete(Path.Combine(_configuration.RequestStoragePath, guid + "_data"));

            _logger.LogInformation("Csv file processed");
            request.Success();
        }
        catch (Exception err)
        {
            _logger.LogError(err, "Error while processing request");
            request.Error(err);
        }

        _logger.LogInformation($"Processing request {request.IdRequest} is finished");
        //save request changes in database
        await _context.SaveChangesAsync(cancellationToken);
        _mq.SendRequestSignalR(request);
    }
}
