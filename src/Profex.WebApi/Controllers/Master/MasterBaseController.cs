using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Profex.WebApi.Controllers.Master;


[ApiController]
[Authorize(Roles = "Master")]
public class MasterBaseController : ControllerBase
{ }
