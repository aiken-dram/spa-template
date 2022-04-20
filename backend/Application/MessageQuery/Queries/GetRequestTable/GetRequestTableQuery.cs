using Shared.Application.Models;
using MediatR;

namespace Application.MessageQuery.Queries.GetRequestTable;

public class GetRequestTableQuery : TableQuery, IRequest<RequestTableVm>
{

}
