using System.Diagnostics;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using RDotNet;
using Shared.Application.Extensions;

namespace Infrastructure.RScript;

public class RScriptService : IRScriptService
{
    private readonly Infrastructure.Common.Interfaces.IConfigurationService _configuration;
    private readonly ILogger _logger;
    private readonly ISPADbContext _context;
    private readonly IMessageQueryService _mq;

    private string _rPath;
    private string _rHome;

    public RScriptService(
        ISPADbContext context,
        Infrastructure.Common.Interfaces.IConfigurationService configuration,
        IMessageQueryService mq,
        ILogger<RScriptService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
        _mq = mq;
    }

    public async Task ProcessRequestAsync(long Id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Processing request with id {Id}...");
        var request = await _context.Requests.GetAsync(Id, cancellationToken);

        //start processing request
        Thread.Sleep(TimeSpan.FromSeconds(5)); //test animation
        string guid = request.Start();
        await _context.SaveChangesAsync(cancellationToken);
        _mq.SendRequestSignalR(request);

        try
        {
            _logger.LogInformation($"json: {request.Json}");

            var id = JsonHelper.GetId(request.Json);

            var rscript = await _context.RScripts.GetAsync(id, cancellationToken);

            //run r script
            string file = rscript.ScriptFile;
            var args = JsonHelper.GetArgs(request.Json);
            RunRScript(file, guid, args);

            _logger.LogInformation("R script finished");
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
        Thread.Sleep(TimeSpan.FromSeconds(5)); //test animation
        _mq.SendRequestSignalR(request);
    }

    public void RunRScript(string scriptFile, string guid, string[] args)
    {
        try
        {
            var info = new ProcessStartInfo
            {
                FileName = Path.Combine(_configuration.rPath, "RScript.exe"),
                WorkingDirectory = Path.GetDirectoryName(_configuration.RScriptPath),
                Arguments = scriptFile,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            using (var proc = new Process { StartInfo = info })
            {
                if (false == proc.Start())
                    throw new Exception("Didn't start R");

                proc.WaitForExit();
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
        }
    }

    /// <summary>
    /// There're some problems with running scripts via REngine:
    /// dll not loading, odbc driver error, crashes on library(xlsx)
    /// Solved dll issue with setting up environmental variables 
    /// but no idea what to do with crash and odbc driver
    /// </summary>
    public void _RunRScript(string scriptFile, string guid, string[] args)
    {
        REngine.SetEnvironmentVariables(_configuration.rPath, _configuration.rHome);
        var en = REngine.GetInstance();

        // Workaround - explicitly include R libs in PATH so R environment can find them.  Not sure why R can't find them when
        // we set this via Environment.SetEnvironmentVariable
        en.Evaluate($"Sys.setenv(PATH = paste(\"{_configuration.PATH}\", Sys.getenv(\"PATH\"), sep=\";\"))");

        //fix path for R command
        var script_path = _configuration.RScriptPath.Replace("\\", "\\\\");
        var out_path = _configuration.RequestStoragePath.Replace("\\", "\\\\");

        var cmd = $"source('{script_path + scriptFile}', encoding='UTF-8')";
        _logger.LogInformation($"RScript command: {cmd}");

        var par = String.Join("','", args);

        //set up variables?
        en.Evaluate($"file <- c('{out_path + guid}')");
        en.Evaluate($"params <- c('{par}')");
        en.Evaluate(cmd);
        en.Evaluate("rm(file, params)");
    }
}
