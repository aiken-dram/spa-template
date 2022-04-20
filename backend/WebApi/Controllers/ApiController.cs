using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[area]/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
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
