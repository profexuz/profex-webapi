using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.PostImages;
using Profex.Persistance.Validations.Dtos.PostImages;
using Profex.Service.Interfaces.PostImages;

namespace Profex.WebApi.Controllers.Common.Post;

[Route("api/common/post/image")]
[ApiController]
public class CommonPostImageController : ControllerBase
{
    private readonly IPostImagesService _service;
    private readonly int maxPageSize = 30;

    public CommonPostImageController(IPostImagesService service)
    {
        this._service = service;
    }


    [HttpGet("getAll")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
    => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpGet("getById/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long id)
    => Ok(await _service.GetByIdAsync(id));

}

