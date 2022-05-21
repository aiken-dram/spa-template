namespace Application.Services;

public class AuditService : IAuditService
{
    private readonly ISPADbContext _context;
    private IAuditBuilder _audit => _context.AuditBuilder;

    public AuditService(ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Audit> UserUpdatePassword(Domain.Entities.User entity)
    {
        var res = new Audit(
            entity,
            (int)eUserAuditAction.UpdatePassword,
            null);

        // AuditData
        res.Add(await _audit.PropertyValue(entity, p => p.PassDate));

        return res;
    }
}
