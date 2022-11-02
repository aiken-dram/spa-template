namespace Application.Common.Interfaces;

public interface IRScriptService
{
    /// <summary>
    /// Processes R script request from message query with provided Id
    /// </summary>
    /// <param name="Id">Id of request from message query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task ProcessRequestAsync(long Id, CancellationToken cancellationToken);

    /// <summary>
    /// Runs an R script
    /// </summary>
    /// <param name="scriptFile">Name of file with R script to run</param>
    /// <param name="guid">GUID of request</param>
    /// <param name="args">Arguments to pass into R script</param>
    void RunRScript(string scriptFile, string guid, string[] args);
}
