namespace Application.R.RScript.Commands.UpsertRScript;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class UpsertRScriptCommand : IRequest<long>
{
    /// <summary>
    /// Id of R script
    /// </summary>
    public long? id { get; set; }
}

public class UpsertRScriptCommandHandler : IRequestHandler<UpsertRScriptCommand, long>
{
    private readonly ISPADbContext _context;

    public UpsertRScriptCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpsertRScriptCommand request, CancellationToken cancellationToken)
    {
        //check access

        Domain.Entities.RScript? entity;

        if (request.id.HasValue)
        {
            //edit
            entity = await _context.RScripts
                .FindIdAsync(request.id, cancellationToken);

            if (entity == null)
                new NotFoundException(nameof(Domain.Entities.RScript), request.id);
        }
        else
        {
            //create
            entity = new Domain.Entities.RScript();
        }

        return entity.IdRScript;
    }
}


