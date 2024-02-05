using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Mapping;

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

            return imageEntities.Select(Mapper.Map);
        }

        public async Task<int> Create(Image image)
        {
            var imageEntity = Mapper.Map(image);

            await _context.Images.AddAsync(imageEntity);
            await _context.SaveChangesAsync();

            return imageEntity.Id;
        }

        public async Task<int> Update(int id, string filePath)
        {
            await _context.Images
                .Where(i => i.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(i => i.FilePath, i => filePath));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Images
                .Where(i => i.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}