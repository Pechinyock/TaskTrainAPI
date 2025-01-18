using Microsoft.AspNetCore.Mvc;

namespace TT.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Ping() 
    {
        return Ok();
    }
}
