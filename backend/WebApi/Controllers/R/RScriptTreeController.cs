using Application.R.RScriptTree.Commands.DeleteRScriptTreeNode;
using Application.R.RScriptTree.Commands.UpsertRScriptTreeNode;
using Application.R.RScriptTree.Queries.GetRScriptTree;
using Application.R.RScriptTree.Queries.GetRScriptTreeNode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.R;

[Authorize]
[Area("R")]
[ApiController]
public class RScriptTreeController : ApiController
{
    /// <summary>
    /// Get R script tree
    /// </summary>
    /// <response code="200">R script tree</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RScriptTreeVm>> Tree()
    {
        var vm = await Mediator.Send(new GetRScriptTreeQuery());

        return base.Ok(vm);
    }

    /// <summary>
    /// Get info for requested R script tree node id
    /// </summary>
    /// <param name="id" example="1">Id of R script tree node</param>
    /// <response code="200">R script tree node info</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    /// <response code="404">R script tree node with requested id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RScriptTreeNodeVm>> Get(long id)
    {
        var vm = await Mediator.Send(new GetRScriptTreeNodeQuery { Id = id });

        return base.Ok(vm);
    }

    /// <summary>
    /// Creates new R script tree node or updates existing R script tree node with new data
    /// </summary>
    /// <param name="command">R script tree node data</param>
    /// <response code="204">R script tree node created or updated with new data</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    /// <response code="404">R script tree node was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsert(UpsertRScriptTreeNodeCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Delete R script tree node with provided id
    /// </summary>
    /// <param name="id" example="1">Id of R script tree node to delete</param>
    /// <response code="204">R script tree node was deleted</response>
    /// <response code="403">User doesnt have configuration admin role</response>
    /// <response code="404">R script tree node with provided id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteRScriptTreeNodeCommand { Id = id });

        return NoContent();
    }
}
