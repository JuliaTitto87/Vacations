using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vacations.DataTransferObjects.Identity;
using Vacations_DomainModel.Models.Identity;
using ILogger = Serilog.ILogger;
namespace Vacations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        public LoginController(
            SignInManager<User> signInManager,
            ILogger logger)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginData)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(loginData.Email, loginData.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.Information($@"User {loginData.Email} has logged in.");

                // todo get the user's role and put it into Claims, so it will be easier for filters to check access

                return Ok();
            }

            if (result.IsLockedOut)
            {
                return Unauthorized("The user has been locked out, please contact Your administrator");
            }

            if (result.IsNotAllowed)
            {
                return Unauthorized("The user is not allowed to log in at the moment");
            }

            return Unauthorized("Email or/and Password are incorrect, please try again");
        }
    }
}
