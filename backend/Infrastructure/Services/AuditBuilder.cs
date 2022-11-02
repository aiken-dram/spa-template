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

    public override async Task<Dictionary<long, string?>> GetDictionaryAsync(string dictionary)
    {
        //return dictionary from dictionary name
        switch (dictionary)
        {
            case "RequestTypes":
                return (await _context.RequestTypes.ToListAsync())
                    .ToDictionary(k => (long)(k.IdType), t => t.Description);
            case "SampleTypes":
                return (await _context.SampleTypes.ToListAsync())
                    .ToDictionary(k => (long)(k.IdType), t => t.Description);
            case "SampleDicts":
                return (await _context.SampleDicts.ToListAsync())
                    .ToDictionary(k => (long)k.IdDict, t => t.Description);
        }
        return new Dictionary<long, string?>();
    }

    public override async Task<string> PropertyToStringAsync(object? val, AuditAttribute attr)
    {
        //place custom property conversions to string here, if any
        return await base.PropertyToStringAsync(val, attr);
    }
}
