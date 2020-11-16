using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProtectedApi.Controllers
{

    [ApiController]
    [Route("Identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(User.Claims.Select(c => new {c.Type, c.Value})); //  new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}