using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Security;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.R;

[Authorize]
[Area("R")]
[ApiController]
public class RScriptController : ApiController
{

}
