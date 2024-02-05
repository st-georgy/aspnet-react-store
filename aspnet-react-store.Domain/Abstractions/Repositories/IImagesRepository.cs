using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IImagesRepository
    {
        Task<int> Create(Image image);
        Task<int> Delete(int id);
        Task<IEnumerable<Image>> Get(int productId);
        Task<int> Update(int id, string filePath);
    }
}