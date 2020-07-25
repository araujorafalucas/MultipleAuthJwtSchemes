using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultipleAuthSchemes.Models;
using MultipleAuthSchemes.Services;

namespace MultipleAuthSchemes.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("alpha/login")]
        [AllowAnonymous]
        public ActionResult<ClientToken> AlphaClientLogin([FromBody] User user)
        {
            if (user.Username == "userAlpha" && user.Password == "123")
            {
                return AlphaTokenService.GenerateToken(user);
            }
            else
            {
                return Unauthorized(new { message = "Invalid Username or password" });
            }
        }

        [HttpPost("beta/login")]
        public ActionResult<ClientToken> BetaClientLogin([FromBody] User user)
        {
            if (user.Username == "userBeta" && user.Password == "456")
            {
                return BetaTokenService.GenerateToken(user);
            }
            else
            {
                return Unauthorized(new { message = "Invalid Username or password" });
            }
        }
    }
}