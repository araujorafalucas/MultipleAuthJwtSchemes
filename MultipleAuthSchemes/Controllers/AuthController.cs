using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultipleAuthSchemes.Controllers
{
    [Authorize(AuthenticationSchemes = "AlphaClient")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public ActionResult<IEnumerable<string>> GetAlphaDate()
        {
            return new List<string>() { "Alpha Data 1","Alpha Date 2" };
        }
    }
}