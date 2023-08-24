using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Profex.WebApi.Controllers.User;


[ApiController]
[Authorize(Roles = "User,Admin")]
public class UserBaseController : ControllerBase
{ }
