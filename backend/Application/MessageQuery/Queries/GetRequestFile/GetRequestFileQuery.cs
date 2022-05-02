using MediatR;

namespace Application.Request.Queries.GetRequestFile;

public class GetRequestFileQuery : IRequest<RequestFileVm>
{
    public long Id { get; set; }
}
