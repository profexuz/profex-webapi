using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.PostImages;

namespace Profex.WebApi.Controllers.Common.Post;

[Route("api/common/images")]
[ApiController]
public class CommonPostImageController : ControllerBase
{
    private readonly IPostImagesService _service;
    private readonly int maxPageSize = 30;

    public CommonPostImageController(IPostImagesService service)
    {
        this._service = service;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
    => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpGet("{imageId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long imageId)
    => Ok(await _service.GetByIdAsync(imageId));

}

