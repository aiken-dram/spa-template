using Domain.Common.Interfaces;

namespace Domain.Entities;

public partial class RequestState : IDictionaryState
{
    public RequestState()
    {
        Requests = new HashSet<Request>();
    }

    public int IdState { get; set; }
    public string State { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
}
