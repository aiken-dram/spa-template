using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.MessageQuery.Queries.GetRequestToolbar;
using Application.Request.Commands.CreateRequest;
using Application.Request.Commands.DeleteRequest;
using Application.Request.Queries.GetRequestFile;
using Application.Request.Queries.GetRequestTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.MessageQuery;

[Authorize]
[Area("MessageQuery")]
[ApiController]
public class RequestController : ApiController
{
    private IConfiguration _configuration;
    private IMessageService _message;

    public RequestController(
        IConfiguration configuration,
        IMessageService message)
    {
        this._configuration = configuration;
        _message = message;
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
        string fname = _configuration.GetValue<string>("SiteSettings:ExportPath");
        fname += vm.Guid;
        var content = await System.IO.File.ReadAllBytesAsync(fname);
        return File(content, vm.ContentType, vm.FileName);
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
        //2D: delete document here????

        return NoContent();
    }

    /// <summary>
    /// Create new request
    /// </summary>
    /// <param name="command">Request parameters</param>
    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// State of queue for requests
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<int> Queue()
    {
        int queue = _message.Queue(eQueue.QueryService);
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
}

