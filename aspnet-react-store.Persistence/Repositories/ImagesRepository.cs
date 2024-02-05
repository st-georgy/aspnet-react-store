using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Repositories
{
    public class ImagesRepository(StoreDbContext context) : IImagesRepository
    {
        private readonly StoreDbContext _context = context;

        public async Task<IEnumerable<Image>> Get(int productId)
        {
            var imageEntities = await _context.Images
                .Where(i => i.ProductId == productId)
                .Include(i => i.Product)
                .AsNoTracking()
                .ToListAsync();

            return EntitiesToImages(imageEntities);
        }

        public async Task<int> Create(Image image)
        {
            var productEntity = new ProductEntity
            {
                Name = image.Product.Name,
                Price = image.Product.Price,
                Description = image.Product.Description,
            };
            var imageEntity = new ImageEntity
            {
                FilePath = image.FilePath,
                Product = productEntity,
            };

            await _context.Images.AddAsync(imageEntity);
            await _context.SaveChangesAsync();

            return imageEntity.Id;
        }

        public async Task<int> Update(int id, string filePath, int productId)
        {
            await _context.Images
                .Where(i => i.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(i => i.FilePath, i => filePath)
                    .SetProperty(i => i.ProductId, i => productId));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Images
                .Where(i => i.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public static List<Image> EntitiesToImages(List<ImageEntity> imageEntities)
        {
            var images = imageEntities
                .Select(i =>
                {
                    if (i.Product is null)
                        return Result.Failure<Image>("Product can not be null");

                    var productResult = Product.Create(
                        i.Product.Id,
                        i.Product.Name,
                        i.Product.Price,
                        i.Product.Description,
                        []
                    );

                    if (productResult.IsSuccess)
                        return Image.Create(
                            i.Id,
                            i.FilePath,
                            productResult.Value
                        );
                    else
                        return Result.Failure<Image>(productResult.Error);
                })
                .ToList();

            foreach (var image in images)
                if (image.IsSuccess)
                    image.Value.Product.Images?.Add(image.Value);

            var validImages = images
                .Where(result => result.IsSuccess)
                .Select(result => result.Value)
                .ToList();

            var errors = images
                .Where(result => result.IsFailure)
                .Select(result => result.Error)
                .ToList();

            if (errors.Count != 0)
                Console.WriteLine($"Failed to create some images: {string.Join(", ", errors)}");

            return validImages;
        }
    }
}