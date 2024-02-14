namespace aspnet_react_store.API.Contracts.Requests.UserProfiles
{
    public record UserProfilesRequest(
        string? UserName,
        string? Email,
        string? FirstName,
        string? MiddleName,
        string? LastName);
}