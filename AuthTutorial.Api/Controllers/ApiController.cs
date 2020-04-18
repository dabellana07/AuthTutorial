using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTutorial.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [Route("text/welcome")]
        [Authorize]
        public IActionResult GetWelcomeText()
        {
            return Content("Welcome " + User.Identity.Name);
        }

        [Route("user")]
        [Authorize]
        public IActionResult GetUser()
        {
            return Content("User: " + User.Identity.Name);
        }
    }
}