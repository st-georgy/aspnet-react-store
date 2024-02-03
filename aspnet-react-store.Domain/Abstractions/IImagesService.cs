using aspnet_react_store.Core.Models;

namespace aspnet_react_store.Core.Abstractions
{
    public interface IImagesService
    {
        Task<int> CreateImage(Image image);
        Task<int> DeleteImage(int id);
        Task<IEnumerable<Image>> GetProductImages(int productId);
        Task<int> UpdateImage(int id, string filePath, int productId);
    }
}