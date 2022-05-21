using Domain.Entities;

namespace Infrastructure.MessageQuery;

public partial class QueryResponseBuilder
{
    private async Task TableExportSampleAsync(Request req, CancellationToken cancellationToken)
    {
        string guid = Guid.NewGuid().ToString(); //generate guid
        req.Guid = guid;

    }
}
