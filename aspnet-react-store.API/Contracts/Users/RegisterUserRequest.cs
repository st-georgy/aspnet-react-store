using System.ComponentModel.DataAnnotations;

namespace aspnet_react_store.API.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string Email,
        [Required] string Username,
        [Required] string Password);
}
