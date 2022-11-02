using Application.Sample.Commands.BatchUpdateSample;
using Application.Sample.Commands.DeleteSample;
using Application.Sample.Commands.UpsertSample;
using Application.Sample.Queries.GetSample;
using Application.Sample.Queries.GetSampleAuditTable;
using Application.Sample.Queries.GetSampleTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Sample;

#warning SAMPLE, remove entire file in actual application
[Authorize]
[Area("Sample")]
[ApiController]
public class SampleController : ApiController
{
    /// <summary>
    /// Get table of samples
    /// </summary>
    /// <response code="200">Table of samples</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SampleTableVm>> Table([FromQuery] GetSampleTableQuery query)
    {
        var vm = await Mediator.Send(query);

        return base.Ok(vm);
    }

    /// <summary>
    /// Get table of sample audit
    /// </summary>
    /// <response code="200">Table of sample audit</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SampleTableVm>> Audit([FromQuery] GetSampleAuditTableQuery query)
    {
        var vm = await Mediator.Send(query);

        return base.Ok(vm);
    }

    /// <summary>
    /// Get info for requested sample id
    /// </summary>
    /// <param name="id" example="1">Id of sample</param>
    /// <response code="200">Sample info</response>
    /// <response code="404">Sample with requested id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SampleVm>> Get(long id)
    {
        var vm = await Mediator.Send(new GetSampleQuery { Id = id });

        return base.Ok(vm);
    }

    /// <summary>
    /// Creates new sample or updates existing sample with new data
    /// </summary>
    /// <param name="command">Sample data</param>
    /// <response code="204">Sample created or updated with new data</response>
    /// <response code="403">User doesnt have access to creating or editing selected sample</response>
    /// <response code="404">Sample was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsert(UpsertSampleCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Updates samples in batch with new data
    /// </summary>
    /// <param name="command">Sample data</param>
    /// <response code="204">Samples updated with new data</response>
    /// <response code="403">User doesnt have access to updating samples</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Batch(BatchUpdateSampleCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Delete sample with provided id
    /// </summary>
    /// <param name="id" example="1">Id of sample to delete</param>
    /// <response code="204">Sample was deleted</response>
    /// <response code="403">User doesnt have access to delete selected sample</response>
    /// <response code="404">Sample with provided id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteSampleCommand { Id = id });

        return NoContent();
    }
}
