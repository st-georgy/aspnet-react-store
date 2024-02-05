namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface IImageUrlProvider
    {
        public string GetImageUrl(string imagePath);
    }
}