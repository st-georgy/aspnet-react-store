using System.ComponentModel.DataAnnotations;

namespace aspnet_react_store.API.Contracts.Requests.UserProfiles
{
    public record PasswordsRequest(
        [Required] string OldPassword,
        [Required] string NewPassword);
}