namespace aspnet_react_store.API.Contracts.Responses
{
    public record UserProfilesResponse(
        string UserName,
        string Email,
        string? FirstName,
        string? MiddleName,
        string? LastName,
        DateTime JoinDate);
}