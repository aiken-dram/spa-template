using Microsoft.EntityFrameworkCore;

namespace Application.R.RScriptTree.Commands.DeleteRScriptTreeNode;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class DeleteRScriptTreeNodeCommand : IRequest
{
    /// <summary>
    /// Id of tree node in database
    /// </summary>
    public long Id { get; set; }
}

public class DeleteRScriptTreeNodeCommandHandler : IRequestHandler<DeleteRScriptTreeNodeCommand>
{
    private readonly ISPADbContext _context;

    public DeleteRScriptTreeNodeCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRScriptTreeNodeCommand request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.RScriptTree
            .FindIdAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(RScriptTreeNode), request.Id);

        //only delete empty branches
        var hasChildren = await _context.RScriptTree.AnyAsync(p => p.IdParent == entity.Id, cancellationToken);

        if (hasChildren)
            throw new BadRequestException(Messages.CanOnlyDeleteEmptyBranches);

        _context.RScriptTree.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}