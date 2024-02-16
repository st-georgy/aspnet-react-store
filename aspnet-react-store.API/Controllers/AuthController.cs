using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aspnet_react_store.API.Contracts.Requests.Users;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Exceptions;

namespace aspnet_react_store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUsersService usersService) : Controller
    {
        private readonly IUsersService _usersService = usersService;

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserRequest request)
        {
            try
            {
                await _usersService.Register(request.Username, request.Email, request.Password);

                return Ok("Successefully registered!");
            }
            catch (UserExistsException ex)
            {
                return BadRequest($"User with the provided username or email already exists: {ex.Message}");
            }
            catch (RegisterFailedException ex)
            {
                return BadRequest($"Failed to register user: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserRequest request)
        {
            try
            {
                var token = await _usersService.Login(request.Email, request.Password);

                HttpContext.Response.Cookies.Append("tasty-cookies", token);

                return Ok("Successefully logged in!");
            }
            catch (AuthorizationFailedException ex)
            {
                return Unauthorized($"Authorization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("validate")]
        public ActionResult ValidateToken() => Ok();

        [Authorize]
        [HttpPost("logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("tasty-cookies"))
                Response.Cookies.Delete("tasty-cookies");

            return Ok();
        }
    }
}
