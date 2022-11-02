using Application.R.RScript.Commands.DeleteRScript;
using Application.R.RScript.Commands.UpsertRScript;
using Application.R.RScript.Queries.GetRScript;
using Application.R.RScript.Queries.GetRScriptForm;
using Application.R.RScript.Queries.GetRScriptList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.R;

[Authorize]
[Area("R")]
[ApiController]
public class RScriptController : ApiController
{
    /// <summary>
    /// Get list of R scripts
    /// </summary>
    /// <response code="200">List of R scripts</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<RScriptListVm>> List()
    {
        var vm = await Mediator.Send(new GetRScriptListQuery());

        return base.Ok(vm);
    }

    /// <summary>
    /// Get info for requested R script id
    /// </summary>
    /// <param name="id" example="1">Id of R script</param>
    /// <response code="200">R script info</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    /// <response code="404">R script with requested id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RScriptVm>> Get(long id)
    {
        var vm = await Mediator.Send(new GetRScriptQuery { Id = id });

        return base.Ok(vm);
    }

    /// <summary>
    /// Get form for requested R script id
    /// </summary>
    /// <param name="id" example="1">Id of R script</param>
    /// <response code="200">R script form</response>
    /// <response code="404">R script form with requested id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RScriptVm>> Form(long id)
    {
        var vm = await Mediator.Send(new GetRScriptFormQuery { Id = id });

        return base.Ok(vm);
    }

    /// <summary>
    /// Creates new R script or updates existing R script with new data
    /// </summary>
    /// <param name="command">R script data</param>
    /// <response code="204">R script created or updated with new data</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    /// <response code="404">R script was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsert(UpsertRScriptCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Delete R script with provided id
    /// </summary>
    /// <param name="id" example="1">Id of R script to delete</param>
    /// <response code="204">R script was deleted</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    /// <response code="404">R script with provided id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteRScriptCommand { Id = id });

        return NoContent();
    }
}
