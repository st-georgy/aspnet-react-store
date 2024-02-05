using aspnet_react_store.Domain.Abstractions.Services;

namespace aspnet_react_store.Application.Services
{
    public class ImageUrlProvider(string baseUrl) : IImageUrlProvider
    {
        private readonly string _baseUrl = baseUrl;

        public string GetImageUrl(string imagePath) => $"{_baseUrl}{imagePath}";
    }
}