using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Application.R.RScriptTree.Queries.GetRScriptTree;

public class GetRScriptTreeQuery : IRequest<RScriptTreeVm>
{ }

public class GetRScriptTreeQueryHandler : IRequestHandler<GetRScriptTreeQuery, RScriptTreeVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _user;

    private List<RScriptTreeNode> _nodes = null!;
    private string[] _modules = null!;

    public GetRScriptTreeQueryHandler(
        ISPADbContext context,
        IMapper mapper,
        IUserService user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    /// <summary>
    /// Recursive funtion to fill node tree for statistics menu
    /// </summary>
    /// <param name="node">R script menu tree node</param>
    /// <returns>RScriptNode</returns>
    private RScriptNode GetNode(RScriptTreeNode node)
    {
        var res = _mapper.Map<RScriptNode>(node);
        if (_nodes.Any(p => p.IdParent == res.id))
        {
            res.children = new List<RScriptNode>();
            foreach (var n in _nodes.Where(p => p.IdParent == res.id))
                if (CheckNodeModule(n))
                    res.children.Add(GetNode(n));
        }
        return res;
    }

    /// <summary>
    /// Check if user has access to this node
    /// </summary>
    /// <param name="node">R script menu tree node</param>
    /// <returns>true if user can access the node, otherwise false</returns>
    private bool CheckNodeModule(RScriptTreeNode node)
    {
        //if modules arent defined, allow node
        if (string.IsNullOrEmpty(node.Modules))
            return true;

        var modules = node.Modules.Split(',');

        //user will need to have all listed modules to access node
        foreach (var m in modules)
            if (!_modules.Contains(m))
                return false;

        return true;
    }

    public async Task<RScriptTreeVm> Handle(GetRScriptTreeQuery request, CancellationToken cancellationToken)
    {
        //check access

        //current user
        var user = await _user.GetCurrentUserAsync(cancellationToken);
        _modules = user.Modules;

        //all nodes in tree from context
        _nodes = await _context.RScriptTree
            .ToListAsync(cancellationToken);

        var vm = new RScriptTreeVm();
        vm.Items = new List<RScriptNode>();

        foreach (var n in _nodes.Where(p => !p.IdParent.HasValue))
            if (CheckNodeModule(n))
                vm.Items.Add(GetNode(n));

        return vm;
    }
}