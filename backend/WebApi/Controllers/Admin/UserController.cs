using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Account.User.Queries.GetUserTable;
using Application.Account.User.Queries.GetUserTableFile;
using Application.Account.User.Queries.GetUser;
using Application.Account.User.Commands.UpsertUser;
using Application.Account.User.Commands.UpdateCurrentUser;
using Application.Account.User.Commands.ProcessFile;
using Application.Account.User.Queries.GetCurrentUser;
using Application.Account.User.Commands.DeleteUser;
using Application.Account.User.Queries.GetAuditTable;

namespace WebApi.Controllers.Admin;

[Authorize]
[Area("Admin")]
[ApiController]
public class UserController : ApiController
{
    #region TABLE
    /// <summary>
    /// Get table of users
    /// </summary>
    /// <response code="200">Table of users</response>
    /// <response code="403">User doesnt have security admin role</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserTableVm>> Table([FromQuery] GetUserTableQuery query)
    {
        var vm = await Mediator.Send(query);

        return base.Ok(vm);
    }

    /// <summary>
    /// Generates .csv file with list of users
    /// </summary>
    /// <param name="query">Request parameters</param>
    /// <response code="200">.csv file with users data</response>
    /// <response code="403">User doesnt have security admin role</response>
    [HttpGet]
    [ProducesResponseType(typeof(FileResult), 200)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<FileResult> TableCsv([FromQuery] GetUserTableFileQuery query)
    {
        var vm = await Mediator.Send(query);

        return File(vm.Content, vm.ContentType, vm.FileName);
    }

    /// <summary>
    /// Get table of user authorization events
    /// </summary>
    /// <param name="query">Request parameters</param>
    /// <response code="200">List of user activity</response>
    /// <response code="403">User doesnt have access to audit selected user</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AuditTableVm>> AuditTable([FromQuery] GetAuditTableQuery query)
    {
        var vm = await Mediator.Send(query);

        return base.Ok(vm);
    }
    #endregion

    #region CRUD
    /// <summary>
    /// Get info for requested user id
    /// </summary>
    /// <param name="id" example="1">Id of user</param>
    /// <response code="200">User info</response>
    /// <response code="403">User doesnt have security admin role</response>
    /// <response code="404">User with requested id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserVm>> Get(int id)
    {
        var vm = await Mediator.Send(new GetUserQuery { Id = id });

        return base.Ok(vm);
    }

    /// <summary>
    /// Update existing user with new data or create new user with provided data
    /// </summary>
    /// <param name="command">User data for updating/creating</param>
    /// <response code="200">Id of updated or created user</response>
    /// <response code="403">User doesnt have security admin role</response>
    /// <response code="404">User with provided id in user data was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsert(UpsertUserCommand command)
    {
        var id = await Mediator.Send(command);

        return Ok(id);
    }

    /// <summary>
    /// Delete user with provided id
    /// </summary>
    /// <param name="id" example="1">Id of user to delete</param>
    /// <response code="204">User was deleted</response>
    /// <response code="403">User doesnt have security admin role</response>
    /// <response code="404">User with provided id was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await Mediator.Send(new DeleteUserCommand { Id = id });

        return NoContent();
    }
    #endregion

    /// <summary>
    /// Get detailed info for current user
    /// </summary>
    /// <response code="200">Detailed current user info</response>
    /// <response code="404">Current user was not found in database</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CurrentUserVm>> Current()
    {
        var vm = await Mediator.Send(new GetCurrentUserQuery());

        return base.Ok(vm);
    }

    /// <summary>
    /// Update current user profile with provided data
    /// </summary>
    /// <param name="command">User profile data</param>
    /// <response code="204">User profile updated</response>
    /// <response code="403">Current user id does not match provided idUser</response>
    /// <response code="404">Current user was not found in database</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(UpdateCurrentUserCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Upload file with new passwords
    /// </summary>
    /// <param name="file">File with passwords</param>
    /// <param name="idConnection">Id of SignalR connection</param>
    /// <response code="200">Result of processing file</response>
    /// <response code="403">User doesnt have security admin role</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ProcessFileVm>> Upload([FromForm] IFormFile file, [FromForm] string idConnection)
    {
        //read file into memory
        var result = new List<string?>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
                result.Add(reader.ReadLine());
        }

        var vm = await Mediator.Send(new ProcessFileCommand()
        {
            FileContent = result,
            IdConnection = idConnection
        });

        return Ok(vm);
    }
}