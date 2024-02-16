using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aspnet_react_store.API.Contracts.Requests.UserProfiles;
using aspnet_react_store.API.Contracts.Responses;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Exceptions;

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
                var userIdClaim = User.FindFirst("userId");

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    throw new ClaimsException("User ID is invalid or missing");

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
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ClaimsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserProfile(UserProfilesRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    throw new ClaimsException("User ID is invalid or missing");

                await _usersService.UpdateUser(userId, request.UserName, request.Email);
                await _userInfosService.UpdateUserInfo(
                    userId,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid argument: {ex.Message}");
            }
            catch (UserExistsException ex)
            {
                return BadRequest($"User already exists: {ex.Message}");
            }
            catch (ClaimsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("me")]
        public async Task<ActionResult<UsersResponse>> GetUser()
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    throw new ClaimsException("User ID is invalid or missing");

                var user = await _usersService.GetUserById(userId);
                var userInfo = await _userInfosService.GetUserInfo(userId);

                var shortName = "Пользователь";

                if (!string.IsNullOrWhiteSpace(userInfo.FirstName))
                    shortName = string.IsNullOrWhiteSpace(userInfo.LastName) ?
                        userInfo.FirstName : $"{userInfo.FirstName} {userInfo.LastName[0]}.";

                return Ok(new UsersResponse(user.UserName, shortName, user.UserRole.ToString().ToLower()));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ClaimsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("password")]
        public async Task<ActionResult> UpdatePassword(PasswordsRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    throw new ClaimsException("User ID is invalid or missing");

                await _usersService.UpdatePassword(userId, request.OldPassword, request.NewPassword);

                return Ok();
            }
            catch (ClaimsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}