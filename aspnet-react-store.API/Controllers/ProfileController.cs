using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aspnet_react_store.API.Contracts.Requests.UserProfiles;
using aspnet_react_store.API.Contracts.Responses;
using aspnet_react_store.Domain.Abstractions.Services;

namespace aspnet_react_store.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProfileController(IUsersService usersService, IUserInfosService userInfosService) : Controller
    {
        private readonly IUsersService _usersService = usersService;
        private readonly IUserInfosService _userInfosService = userInfosService;

        [HttpGet]
        public async Task<ActionResult<UserProfilesResponse>> GetUserProfile()
        {
            try
            {
                var userId = int.Parse((User.FindFirst("userId")?.Value)
                    ?? throw new Exception("User is invalid"));

                var user = await _usersService.GetUserById(userId);
                var userInfo = await _userInfosService.GetUserInfo(userId);

                return Ok(new UserProfilesResponse(
                    user.UserName,
                    user.Email,
                    userInfo.FirstName,
                    userInfo.MiddleName,
                    userInfo.LastName,
                    userInfo.JoinDate
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserProfile(UserProfilesRequest request)
        {
            try
            {
                var userId = int.Parse((User.FindFirst("userId")?.Value)
                    ?? throw new Exception("User is invalid"));

                await _usersService.UpdateUser(userId, request.UserName, request.Email);
                await _userInfosService.UpdateUserInfo(
                    userId,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("me")]
        public async Task<ActionResult<UsersResponse>> GetUser()
        {
            try
            {
                var userId = int.Parse((User.FindFirst("userId")?.Value)
                        ?? throw new Exception("User is invalid"));

                var user = await _usersService.GetUserById(userId);
                var userInfo = await _userInfosService.GetUserInfo(userId);

                var shortName = "Пользователь";

                if (!string.IsNullOrWhiteSpace(userInfo.FirstName))
                    shortName = string.IsNullOrWhiteSpace(userInfo.LastName) ?
                        userInfo.FirstName : $"{userInfo.FirstName} {userInfo.LastName[0]}.";

                return Ok(new UsersResponse(user.UserName, shortName, user.UserRole.ToString().ToLower()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("password")]
        public async Task<ActionResult> UpdatePassword(PasswordsRequest request)
        {
            try
            {
                var userId = int.Parse((User.FindFirst("userId")?.Value)
                        ?? throw new Exception("User is invalid"));

                await _usersService.UpdatePassword(userId, request.OldPassword, request.NewPassword);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}