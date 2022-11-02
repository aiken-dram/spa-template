namespace Infrastructure.MessageQuery;

#warning SAMPLE, remove entire file in actual application
public partial class QueryResponseBuilder
{
    private async Task TableExportSampleAsync(Request req, CancellationToken cancellationToken)
    {
        string guid = Guid.NewGuid().ToString(); //generate guid
        req.Guid = guid;
    }
}
