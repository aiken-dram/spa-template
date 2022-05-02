using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Shared.Application.Services;
using Shared.Domain.Attributes;

namespace Application.Services;

public class AppAuditService : AuditService, IAppAuditService
{
    private ISPADbContext _context;

    public AppAuditService(IOptions<AuditOptions> options, ISPADbContext context)
    : base(options)
    {
        _context = context;
    }

    public override async Task<Dictionary<object, string>> GetDictionary(string dictionary)
    {
        //return dictionary from dictionary name
        //well, dont have any dictionaries now
        return new Dictionary<object, string>();
    }

    public override async Task<string> PropertyToString(object? val, AuditAttribute attr)
    {
        //custom property conversion to string here
        return await base.PropertyToString(val, attr);
    }
}
