namespace Application.R.RScriptTree.Commands.UpsertRScriptTreeNode;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class UpsertRScriptTreeNodeCommand : IRequest<long>
{
    /// <summary>
    /// Id of statistic menu tree node in database
    /// </summary>
    public long? id { get; set; }

    /// <summary>
    /// Id of parent node, or null if this is a top level node
    /// </summary>
    public long? idParent { get; set; }

    /// <summary>
    /// Id of R script, if this node linked to R script
    /// </summary>
    public long? idRScript { get; set; }

    /// <summary>
    /// Name of node
    /// </summary>
    public string name { get; set; } = null!;

    /// <summary>
    /// Modules with access to the tree node, comma separated
    /// </summary>
    public string? modules { get; set; }

    /// <summary>
    /// Icon of node (FontAwesome)
    /// </summary>
    public string? icon { get; set; }

    /// <summary>
    /// Color of node
    /// </summary>
    public string? color { get; set; }

    /// <summary>
    /// Description of node
    /// </summary>
    public string? description { get; set; }
}

public class UpsertRScriptTreeNodeCommandHandler : IRequestHandler<UpsertRScriptTreeNodeCommand, long>
{
    private readonly ISPADbContext _context;

    public UpsertRScriptTreeNodeCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpsertRScriptTreeNodeCommand request, CancellationToken cancellationToken)
    {
        //check access

        RScriptTreeNode? entity;

        if (request.id.HasValue)
        {
            //edit
            entity = await _context.RScriptTree
                .FindIdAsync(request.id.Value, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(RScriptTreeNode), request.id);
        }
        else
        {
            //create
            entity = new RScriptTreeNode();
        }

        return entity.Id;
    }
}