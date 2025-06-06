using GamePal.Models.AuthContracts;
using GamePal.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamePal.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                const string role = "User";
                var result = await _userService.RegisterAsync(registrationRequest, role);

                if (!result.Success)
                {
                    AddErrors(result);
                    return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var result = await _userService.LoginAsync(request);


                if (result.Success)
                {
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddHours(1)
                    };

                    //Response.Cookies.Append("AuthToken", result.Token, cookieOptions);

                    // temporary sample data
                    return Ok(new
                    {
                        id = "123",
                        name = "John Doe",
                        email = "john@example.com",
                        token = result.Token
                    });
                }



                AddErrors(result);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("upsert")]
        public async Task<IActionResult> UpsertUser([FromBody] ExternalAuthRequest extAuthReq)
        {
            try
            {
                var result = await _userService.UpsertUserAsync(extAuthReq);
                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        token = result.Token,
                    });
                }
                else
                {
                    AddErrors(result);
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // for testing
        [Authorize]
        [HttpGet("/protected")]
        public async Task<IActionResult> ProtectedEndpoint()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader.Substring("Bearer ".Length).Trim();

            return Ok();
        }


        private void AddErrors(AuthResult result)
        {
            foreach (var error in result.ErrorMessages)
                ModelState.AddModelError(error.Key, error.Value);
        }
    }
}
