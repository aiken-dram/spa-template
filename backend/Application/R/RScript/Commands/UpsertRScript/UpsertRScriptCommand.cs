using Microsoft.EntityFrameworkCore;
using Shared.Application.Extensions;

namespace Application.R.RScript.Commands.UpsertRScript;

/// <summary>
/// Data transfer object for R script parameter
/// </summary>
public class UpsertRScriptParamDto
{
    /// <summary>
    /// Is new parameter
    /// </summary>
    public bool? IsNew { get; set; }

    /// <summary>
    /// Id of parameter in database
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// Id of parameter type
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Name of parameter
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Hint displayed for parameter in form
    /// </summary>
    public string? Hint { get; set; }

    /// <summary>
    /// Description of parameter
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Updates existing or creates new R script
/// </summary>
[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class UpsertRScriptCommand : IRequest<long>
{
    /// <summary>
    /// Id of R script in database
    /// </summary>
    public long? IdRScript { get; set; }

    /// <summary>
    /// Name of file with R script
    /// </summary>
    public string ScriptFile { get; set; } = null!;

    /// <summary>
    /// Name of R script
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Content type of result file
    /// </summary>
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Result file name (timestamp will be added at the end)
    /// </summary>
    public string ResultFile { get; set; } = null!;

    /// <summary>
    /// Description of R script
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// List of R script parameters
    /// </summary>
    public IEnumerable<UpsertRScriptParamDto> ScriptParams { get; set; } = null!;

    /// <summary>
    /// Content of R script file
    /// </summary>
    public string? ScriptContent { get; set; } = null!;
}

public class UpsertRScriptCommandHandler : IRequestHandler<UpsertRScriptCommand, long>
{
    private readonly ISPADbContext _context;
    private readonly IFileService _file;

    public UpsertRScriptCommandHandler(
        ISPADbContext context,
        IFileService file)
    {
        _context = context;
        _file = file;
    }

    public async Task<long> Handle(UpsertRScriptCommand request, CancellationToken cancellationToken)
    {
        //check access

        Domain.Entities.RScript? entity;

        if (request.IdRScript.HasValue)
        {
            //edit
            entity = await _context.RScripts.Include(p => p.RScriptParams)
                .GetAsync(p => p.IdRScript == request.IdRScript, cancellationToken);
        }
        else
        {
            //create
            entity = new Domain.Entities.RScript();
            _context.RScripts.Add(entity);
        }

        //set fields
        entity.ScriptFile = request.ScriptFile;
        entity.Name = request.Name;
        entity.ContentType = request.ContentType;
        entity.ResultFile = request.ResultFile;
        entity.Description = request.Description;

        //check if this works (need compilation and debug):
        //seems to work fine
        _context.RScriptParams.UpdateCollection(
            entity.RScriptParams,
            request.ScriptParams,
            (p, r) =>
            {
                p.IdType = (eRScriptParamType)r.IdType;
                p.Name = r.Name;
                p.Hint = r.Hint;
                p.Description = r.Description;
            },
            r => new RScriptParam()
            {
                IdType = (eRScriptParamType)r.IdType,
                Name = r.Name,
                Hint = r.Hint,
                Description = r.Description
            },
            e => (p => p.Id == e.Id),
            r => r.IsNew.HasValue && r.IsNew.Value);

        await _context.SaveChangesAsync(cancellationToken);

        //file content
        await _file.SaveRScriptFileAsync(entity.ScriptFile, request.ScriptContent, cancellationToken);

        return entity.IdRScript;
    }
}


