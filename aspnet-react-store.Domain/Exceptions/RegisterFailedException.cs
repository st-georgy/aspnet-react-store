namespace aspnet_react_store.Domain.Exceptions
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() { }
        public RegisterFailedException(string message) : base(message) { }
    }
}
