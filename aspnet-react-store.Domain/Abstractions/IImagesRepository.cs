using aspnet_react_store.Core.Models;

namespace aspnet_react_store.Core.Abstractions
{
    public interface IImagesRepository
    {
        Task<int> Create(Image image);
        Task<int> Delete(int id);
        Task<IEnumerable<Image>> Get(int productId);
        Task<int> Update(int id, string filePath, int productId);
    }
}