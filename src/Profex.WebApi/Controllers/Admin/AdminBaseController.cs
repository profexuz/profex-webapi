using Microsoft.AspNetCore.Mvc;

namespace Profex.WebApi.Controllers.Admin;

[ApiController]
//[Authorize(Roles = "Admin")]
public abstract class AdminBaseController : ControllerBase
{ }
