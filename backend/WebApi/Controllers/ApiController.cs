using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[area]/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
}

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class DefaultController : ControllerBase
{
    /// <summary>
    /// Redirect from root to frontend app
    /// </summary>
    [Route("/")]
    public IActionResult Index()
    {
        return new RedirectResult("~/app/");
    }
}
