namespace aspnet_react_store.Domain.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() { }
        public UserExistsException(string message) : base(message) { }
    }
}
