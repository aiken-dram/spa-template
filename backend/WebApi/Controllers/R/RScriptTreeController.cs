using Application.Common.Security;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.R;

[Authorize]
[Area("R")]
[ApiController]
public class RScriptTreeController : ApiController
{

}
