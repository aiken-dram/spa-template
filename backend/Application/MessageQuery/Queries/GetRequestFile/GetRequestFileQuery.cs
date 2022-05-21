using Microsoft.EntityFrameworkCore;

namespace Application.Request.Queries.GetRequestFile;

public class GetRequestFileQuery : IRequest<RequestFileVm>
{
    /// <summary>
    /// Id of request
    /// </summary>
    public long Id { get; set; }
}

public class GetRequestFileQueryHandler : IRequestHandler<GetRequestFileQuery, RequestFileVm>
{
    private readonly ISPADbContext _context;

    public GetRequestFileQueryHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<RequestFileVm> Handle(GetRequestFileQuery request, CancellationToken cancellationToken)
    {
        //check access
        var entity = await _context.Requests
            .Include(p => p.IdTypeNavigation)
            .FirstOrDefaultAsync(p => p.IdRequest == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.Request), request.Id);

        if (entity.IdState != eRequestState.Ready &&
            entity.IdState != eRequestState.Delivered)
            throw new Exception(Messages.RequestHasNotBeenProcessed);

        if (entity.Guid == null)
            throw new Exception(Messages.RequestHasNoGuid);

        //if state is ready, set it to delivered
        if (entity.IdState == eRequestState.Ready)
        {
            entity.Delivered = DateTime.Now;
            entity.IdState = eRequestState.Delivered;
            await _context.SaveChangesAsync(cancellationToken);
        }

        var vm = new RequestFileVm { Guid = entity.Guid! };

        switch (entity.IdType)
        {
            case eRequestType.TableExportAudit:
                vm.FileName = Messages.AuditTableFileName(entity.Processed);
                vm.ContentType = "text/csv";
                break;
            case eRequestType.TableExportSample:
                vm.FileName = Messages.SampleTableFileName(entity.Processed);
                vm.ContentType = "text/csv";
                break;
            case eRequestType.RScript:
                //alright, here i need a content type and file name from RScript
                break;
        };

        return vm;
    }
}

