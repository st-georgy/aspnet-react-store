namespace aspnet_react_store.Domain.Exceptions
{
    public class EmailExistsException : UserExistsException
    {
        public EmailExistsException() { }
        public EmailExistsException(string message) : base(message) { }
    }
}
