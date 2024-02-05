using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface IImagesService
    {
        Task<int> CreateImage(Image image);
        Task<int> DeleteImage(int id);
        Task<IEnumerable<Image>> GetProductImages(int productId);
        Task<int> UpdateImage(int id, string filePath);
    }
}