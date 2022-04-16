using Domain.Common.Interfaces;

namespace Domain.Entities;

public partial class RequestType : IDictionaryType
{
    public RequestType()
    {
        Requests = new HashSet<Request>();
    }

    public int IdType { get; set; }
    public string Type { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
}

