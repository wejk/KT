using Microsoft.AspNetCore.Mvc;
using WebApiCore.Services;

namespace WebApiCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class WordsController : ControllerBase
{
    private readonly IHelperService _helperService;
    public WordsController(IHelperService helperService)
    {
        _helperService = helperService;
    }

    [HttpGet("{category:Categories}/{count:int}")]
    public ActionResult<string[]> Get([FromRoute] Categories category, [FromRoute] int count)
    {
        var result = _helperService.Get(count, category);
        return Ok(result);
    }

    [HttpGet("samples")]
    public ActionResult<Dictionary<string, object>> GetSampleData()
    {
        var result = _helperService.GetSamples();
        return Ok(result);
    }
}
