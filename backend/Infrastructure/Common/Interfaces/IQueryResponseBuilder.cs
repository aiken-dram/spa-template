namespace Infrastructure.Common.Interfaces;

public interface IQueryResponseBuilder
{
    /// <summary>
    /// Processes request from message query with provided Id
    /// </summary>
    /// <param name="Id">Id of request from message query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task ProcessRequestAsync(long Id, CancellationToken cancellationToken);
}
