using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiNetCore.Controllers.Models;

namespace WebApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }




        #region Create JWT token.


        // POST: /Account/Token
        [HttpPost]
        public async Task<IActionResult> TokenAsync([FromBody]LoginApiModel model)
        {
            IdentityUser appUser = await _userManager.FindByEmailAsync(model.Email);


            if (appUser != null)
            {

                var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);

                if (result.Succeeded)
                {
                    JwtSecurityTokenHandler hundler = new JwtSecurityTokenHandler();

                    ClaimsPrincipal cp = await _signInManager.CreateUserPrincipalAsync(appUser);


                    // The secret key every token will be signed with.
                    // In production, you should store this securely in environment variables
                    // or a key management tool. Don't hardcode this into your application!
                    string SecretKey = _configuration["SecretKey"]; // "needtogetthisfromenvironment";
                    SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

                    var creds = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

                    string token = hundler.CreateEncodedJwt(_configuration["TokenIssuer"], _configuration["TokenAudience"],
                       (ClaimsIdentity)cp.Identity, DateTime.UtcNow, DateTime.UtcNow.AddDays(30), DateTime.UtcNow,
                       new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256));


                    return Ok(token);

                }

            }

            return BadRequest("Bad login or password");
        }

        #endregion
    }
}