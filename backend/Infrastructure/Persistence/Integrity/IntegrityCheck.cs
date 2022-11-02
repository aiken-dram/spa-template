using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Integrity;

public class IntegrityCheck
{
    private readonly ISPADbContext _context;

    public IntegrityCheck(ISPADbContext context)
    {
        _context = context;
    }

    private static List<T?> GetAllPublicConstantVales<T>(Type type)
    {
        return type
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
            .Select(x => (T?)x.GetRawConstantValue())
            .ToList();
    }

    private static void CheckConstant<T>(Type type, List<T> list, Func<string?, Func<T, bool>> predicate)
    {
        foreach (var m in GetAllPublicConstantVales<string>(type))
            if (!list.Any(predicate(m)))
                throw new Exception("DATA STRUCTURE ERROR");
    }


    public async Task CheckDataIntegrity()
    {
        //need to check that fixed enums in domain and database are equal
        CheckConstant(typeof(eAccountModule),
            await _context.Modules.ToListAsync(),
            m => p => p.Name == m);

        //2D: finish rest, and check all
        var auditActions = await _context.AuditActions.ToListAsync();

        var auditDataTypes = await _context.AuditDataTypes.ToListAsync();

        var auditTargets = await _context.AuditTargets.ToListAsync();

        var requestStates = await _context.RequestStates.ToListAsync();

        var requestTypes = await _context.RequestStates.ToListAsync();

        var rscriptParamTypes = await _context.RScriptParamTypes.ToListAsync();

        var sampleTypes = await _context.SampleTypes.ToListAsync();
    }
}
