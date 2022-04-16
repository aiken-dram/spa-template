using Shared.Application.Models;
using MediatR;

namespace Application.Request.Queries.GetRequestTable;

public class GetRequestTableQuery : TableQuery, IRequest<RequestTableVm>
{

}
