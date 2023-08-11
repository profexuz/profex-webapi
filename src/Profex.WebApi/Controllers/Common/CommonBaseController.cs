﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Profex.WebApi.Controllers.Common;

[ApiController]
[AllowAnonymous]
public class CommonBaseController : ControllerBase
{}