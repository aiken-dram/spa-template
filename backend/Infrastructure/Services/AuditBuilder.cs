using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Services;
using Shared.Domain.Attributes;

namespace Infrastructure.Services;

public class AppAuditBuilder : AuditBuilder
{
    private readonly ISPADbContext _context;

    public AppAuditBuilder(ISPADbContext context, Action<AuditOptionsBuilder> options)
    : base(options)
    {
        _context = context;
    }

    public override async Task<Dictionary<object, string?>> GetDictionary(string dictionary)
    {
        //return dictionary from dictionary name
        switch (dictionary)
        {
            case "RequestTypes":
                var d = await _context.RequestTypes.ToListAsync();
                return d.ToDictionary(k => (object)k.IdType, t => t.Description);
        }
        return new Dictionary<object, string?>();
    }

    public override async Task<string> PropertyToString(object? val, AuditAttribute attr)
    {
        //place custom property conversions to string here, if any
        return await base.PropertyToString(val, attr);
    }
}
