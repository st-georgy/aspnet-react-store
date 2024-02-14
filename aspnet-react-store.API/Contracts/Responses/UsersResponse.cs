namespace aspnet_react_store.API.Contracts.Responses
{
    public record UsersResponse(
        string UserName,
        string ShortName,
        string UserRole);
}