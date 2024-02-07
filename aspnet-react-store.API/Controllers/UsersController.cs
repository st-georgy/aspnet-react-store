using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using aspnet_react_store.API.Contracts.Users;
using aspnet_react_store.Domain.Abstractions.Services;


namespace aspnet_react_store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUsersService usersService) : Controller
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
            catch (Exception ex)
            {
                return BadRequest("Failed to create new account: " + ex.Message);
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
            catch
            {
                return BadRequest("Failed to login");
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
