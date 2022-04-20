using Application.Dictionary.Queries.GetDictionary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

[Authorize]
[Area("Admin")]
[ApiController]
public class DictionaryController : ApiController
{
    /// <summary>
    /// Get dictionary with provided name
    /// </summary>
    /// <remarks>
    /// List of available dictionaries:
    /// 
    ///     AccessGroups
    ///     AccessRoles
    ///     AuthActions
    ///     Raions
    ///     UserRaions
    /// 
    /// </remarks>
    /// <param name="Dictionary" example="Raions">Name of dictionary</param>
    /// <response code="200">Dictionary with requested name</response>
    /// <response code="404">Dictionary with requested name was not found</response>
    [HttpGet("{Dictionary}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IList<DictionaryDto>>> Get(string Dictionary)
    {
        var vm = await Mediator.Send(new GetDictionaryQuery() { Dictionary = Dictionary });

        return base.Ok(vm);
    }
}