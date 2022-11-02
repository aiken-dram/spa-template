namespace Application.MessageQuery.Queries.GetRequestPreview;

/// <summary>
/// Get preview of request result
/// </summary>
public class GetRequestPreviewQuery : IRequest<RequestPreviewVm>
{
    /// <summary>
    /// Id of request
    /// </summary>
    public long Id { get; set; }
}

public class GetRequestPreviewQueryHandler : IRequestHandler<GetRequestPreviewQuery, RequestPreviewVm>
{
    private readonly ISPADbContext _context;
    private readonly IFileService _file;

    public GetRequestPreviewQueryHandler(
        ISPADbContext context,
        IFileService file)
    {
        _context = context;
        _file = file;
    }

    public async Task<RequestPreviewVm> Handle(GetRequestPreviewQuery request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.Requests
            .GetAsync(request.Id, cancellationToken);

        if (entity.IdState is not (eRequestState.Ready or eRequestState.Delivered))
            throw new BadRequestException("Request hasnt been processed!");

        if (entity.Guid == null)
            throw new BadRequestException("No result file for request");

        var vm = new RequestPreviewVm();
        switch (entity.IdType)
        {
            case eRequestType.TableExportAudit:
#warning SAMPLE, remove next line in actual application
            case eRequestType.TableExportSample:
                vm.Type = eRequestPreviewType.Table;
                vm.Content = await _file.PreviewRequestResponseTable(entity.Guid, FileContentType.CSV, cancellationToken);
                break;

            case eRequestType.RScript:
                var id = JsonHelper.GetId(entity.Json);

                var rscript = await _context.RScripts
                    .GetAsync(id, cancellationToken);

                switch (rscript.ContentType)
                {
                    case FileContentType.CSV:
                        vm.Type = eRequestPreviewType.Table;
                        vm.Content = await _file.PreviewRequestResponseTable(entity.Guid, FileContentType.CSV, cancellationToken);
                        break;
                }
                break;
        }
        return vm;
    }
}
