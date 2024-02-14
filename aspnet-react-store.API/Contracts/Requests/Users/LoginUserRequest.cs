using System.ComponentModel.DataAnnotations;

namespace aspnet_react_store.API.Contracts.Requests.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
