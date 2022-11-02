namespace Application.Services;

public class AuditService : IAuditService
{
    private readonly ISPADbContext _context;
    private IAuditBuilder _audit => _context.AuditBuilder;

    public AuditService(ISPADbContext context)
    {
        _context = context;
    }

#warning SAMPLE, remove in actual application
    public async Task<Audit> SampleBatchUpdateAsync(Domain.Entities.Sample entity)
    {
        var res = new Audit(
            entity,
            (int)eSampleAuditAction.BatchUpdate,
            null);

        // AuditData
        res.Add(await _audit.PropertyValueAsync(entity, p => p.IdType));

        return res;
    }
}
