using Application.Dictionary.District.Commands.DeleteDistrict;
using Application.Dictionary.District.Commands.UpsertDistrict;
using Application.Dictionary.District.Queries.GetDistrict;
using Application.Dictionary.District.Queries.GetDistrictList;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin.Dictionary;

[Authorize]
[Area("Admin")]
[ApiController]
public class DistrictController : ApiController
{
    /// <summary>
    /// Get list of districts
    /// </summary>
    /// <response code="200">List of districts</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DistrictListVm>> List()
    {
        var vm = await Mediator.Send(new GetDistrictListQuery());

        return base.Ok(vm);
    }

    /// <summary>
    /// Get info for requested district id
    /// </summary>
    /// <param name="id" example="1">Id of district</param>
    /// <response code="200">District info</response>
    /// <response code="403">District doesnt have dictionary admin role</response>
    /// <response code="404">District with requested id was not found</response>
    [Authorize(Roles = eAccountModule.DictionaryAdmin)]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictVm>> Get(int id)
    {
        var vm = await Mediator.Send(new GetDistrictQuery { Id = id });

        return base.Ok(vm);
    }

    /// <summary>
    /// Creates new district or updates existing district with new data
    /// </summary>
    /// <param name="command">District data</param>
    /// <response code="204">District created or updated with new data</response>
    /// <response code="403">District doesnt have dictionary admin role</response>
    /// <response code="404">District was not found</response>
    [Authorize(Roles = eAccountModule.DictionaryAdmin)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsert(UpsertDistrictCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Delete district with provided id
    /// </summary>
    /// <param name="id" example="1">Id of district to delete</param>
    /// <response code="204">District was deleted</response>
    /// <response code="403">District doesnt have dictionary admin role</response>
    /// <response code="404">District with provided id was not found</response>
    [Authorize(Roles = eAccountModule.DictionaryAdmin)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteDistrictCommand { Id = id });

        return NoContent();
    }
}