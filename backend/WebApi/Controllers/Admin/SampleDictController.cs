using Application.Dictionary.SampleDict.Commands.DeleteSampleDict;
using Application.Dictionary.SampleDict.Commands.UpsertSampleDict;
using Application.Dictionary.SampleDict.Queries.GetSampleDictList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

#warning SAMPLE, remove entire file in actual application
[Authorize]
[Area("Admin")]
[ApiController]
public class SampleDictController : ApiController
{
    /// <summary>
    /// Get list of sample dictionaries
    /// </summary>
    /// <response code="200">List of sample dictionaries</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SampleDictListVm>> List()
    {
        var vm = await Mediator.Send(new GetSampleDictListQuery());

        return base.Ok(vm);
    }

    /// <summary>
    /// Creates new sample dictionary or updates existing sample dictionary with new data
    /// </summary>
    /// <param name="command">Sample dictionary data</param>
    /// <response code="204">Sample dictionary created or updated with new data</response>
    /// <response code="403">User doesnt have dictionary admin role</response>
    /// <response code="404">Sample dictionary was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsert(UpsertSampleDictCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Delete sample dictionary with provided id
    /// </summary>
    /// <param name="id" example="1">Id of sample dictionary to delete</param>
    /// <response code="204">Sample dictionary was deleted</response>
    /// <response code="403">User doesnt have dictionary admin role</response>
    /// <response code="404">Sample dictionary with provided id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteSampleDictCommand { Id = id });

        return NoContent();
    }
}
