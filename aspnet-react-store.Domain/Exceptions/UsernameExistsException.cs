namespace aspnet_react_store.Domain.Exceptions
{
    public class UsernameExistsException : UserExistsException
    {
        public UsernameExistsException() { }
        public UsernameExistsException(string message) : base(message) { }
    }
}
