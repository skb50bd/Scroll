using Microsoft.AspNetCore.Mvc;

namespace Scroll.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("pong");
}