using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.MessageQuery.Queries.GetRequestToolbar;
using Application.MessageQuery.Commands.CreateRequest;
using Application.MessageQuery.Commands.DeleteRequest;
using Application.MessageQuery.Queries.GetRequestFile;
using Application.MessageQuery.Queries.GetRequestTable;
using Application.MessageQuery.Queries.GetRequestPreview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.MessageQuery.Commands.BatchProcessRequest;

namespace WebApi.Controllers.MessageQuery;

[Authorize]
[Area("MessageQuery")]
[ApiController]
public class RequestController : ApiController
{
    private Infrastructure.Common.Interfaces.IConfigurationService _configuration;
    private IMessageQueryService _mq;

    public RequestController(
        Infrastructure.Common.Interfaces.IConfigurationService configuration,
        IMessageQueryService mq)
    {
        this._configuration = configuration;
        _mq = mq;
    }

    /// <summary>
    /// Get list of requests
    /// </summary>
    /// <param name="query">Request parameters</param>
    /// <response code="200">List of requests</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RequestTableVm>> Table([FromQuery] GetRequestTableQuery query)
    {
        var vm = await Mediator.Send(query);

        return base.Ok(vm);
    }

    /// <summary>
    /// Downloads request file
    /// </summary>
    /// <param name="id">Id of request</param>
    /// <response code="200">request file</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FileResult), 200)]
    public async Task<FileResult> Download(long id)
    {
        var vm = await Mediator.Send(new GetRequestFileQuery() { Id = id });
        string fname = _configuration.RequestStoragePath;
        fname += vm.Guid;
        FileStream stream = System.IO.File.Open(fname, FileMode.Open);
        return File(stream, vm.ContentType, vm.FileName);
    }

    /// <summary>
    /// Preview file of request result
    /// </summary>
    /// <param name="id">Id of request</param>
    [HttpGet]
    public async Task<IActionResult> Preview(long id)
    {
        var vm = await Mediator.Send(new GetRequestPreviewQuery() { Id = id });
        switch (vm.Type)
        {
            case eRequestPreviewType.File:
                var file = (Shared.Application.Models.FileVm)vm.Content;
                string fname = _configuration.RequestStoragePath;
                //fname += vm.Guid;
                //2D: e nani? or is it finished already?
                FileStream stream = System.IO.File.Open(fname, FileMode.Open);
                return File(stream, file.ContentType, file.FileName);
            default:
                return base.Ok(vm);
        }
    }

    /// <summary>
    /// Delete request with provided id
    /// </summary>
    /// <param name="id" example="1">Id of request to delete</param>
    /// <response code="204">Request was deleted</response>
    /// <response code="404">Request with provided id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteRequestCommand { Id = id });

        return NoContent();
    }

    /// <summary>
    /// Create new request
    /// </summary>
    /// <param name="command">Request parameters</param>
    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestCommand command)
    {
        var vm = await Mediator.Send(command);

        return base.Ok(vm);
    }

    /// <summary>
    /// State of queue for requests
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<int> QueueLength()
    {
        int queue = _mq.QueueLength(eQueue.QueryService);
        return base.Ok(queue);
    }

    /// <summary>
    /// Requests for current user to display in toolbar
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RequestToolbarVm>> Toolbar()
    {
        var vm = await Mediator.Send(new GetRequestToolbarQuery());

        return base.Ok(vm);
    }

    /// <summary>
    /// Processes requests in batch
    /// </summary>
    /// <param name="command">Request data</param>
    /// <response code="204">Requests processed</response>
    /// <response code="403">User doesnt have access to process requests</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BatchProcessRequestVm>> Batch(BatchProcessRequestCommand command)
    {
        var vm = await Mediator.Send(command);

        return base.Ok(vm);
    }
}

