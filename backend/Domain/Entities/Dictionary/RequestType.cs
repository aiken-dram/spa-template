using Domain.Common.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Request type dictionary
/// </summary>
public partial class RequestType : IDictionaryType
{
    public RequestType()
    {
        Requests = new HashSet<Domain.Entities.Request>();
    }

    /// <summary>
    /// Id of type in database
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Name of type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Description of type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of requests with this type
    /// </summary>
    public virtual ICollection<Domain.Entities.Request> Requests { get; set; }
}
