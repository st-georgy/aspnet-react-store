using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;

namespace aspnet_react_store.Application.Services
{
    public class ImagesService(IImagesRepository imagesRepository) : IImagesService
    {
        private readonly IImagesRepository _imagesRepository = imagesRepository;

        public async Task<IEnumerable<Image>> GetProductImages(int productId) =>
            await _imagesRepository.Get(productId);

        public async Task<int> CreateImage(Image image) =>
            await _imagesRepository.Create(image);

        public async Task<int> UpdateImage(int id, string filePath, int productId) =>
            await _imagesRepository.Update(id, filePath, productId);

        public async Task<int> DeleteImage(int id) =>
            await _imagesRepository.Delete(id);
    }
}