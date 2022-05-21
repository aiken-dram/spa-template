namespace Application.R.RScriptTree.Queries.GetRScriptTree;

public class RScriptNode : IMapFrom<RScriptTreeNode>
{
    /// <summary>
    /// Id of statistic menu tree node in database
    /// </summary>
    public long id { get; set; }

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

    /// <summary>
    /// List of children nodes, or null if there're no children nodes
    /// </summary>
    public List<RScriptNode>? children { get; set; }
}


public class RScriptTreeVm : ListVm<RScriptNode>
{

}
