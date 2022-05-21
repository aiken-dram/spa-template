namespace Application.R.RScript.Commands.DeleteRScript;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class DeleteRScriptCommand : IRequest
{
    /// <summary>
    /// Id of R script
    /// </summary>
    public long Id { get; set; }
}

public class DeleteRScriptCommandHandler : IRequestHandler<DeleteRScriptCommand>
{
    private readonly ISPADbContext _context;

    public DeleteRScriptCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRScriptCommand request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.RScripts
            .FindIdAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.RScript), request.Id);

        //changed foreign key to SET_NULL on delete, so tree should remain intact
        _context.RScripts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
