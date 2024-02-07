using aspnet_react_store.Domain.Abstractions.Auth;

namespace aspnet_react_store.Infrastructure
{
    public class PasswordHashProvider : IPasswordHashProvider
    {
        public string Generate(string password) =>
            CreateMD5(password);

        public bool Verify(string password, string hashedPassword) =>
            CreateMD5(password) == hashedPassword;

        private static string CreateMD5(string input)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
}
