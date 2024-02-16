namespace aspnet_react_store.Domain.Exceptions
{
    public class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException() { }
        public AuthorizationFailedException(string message) : base(message) { }
    }
}
