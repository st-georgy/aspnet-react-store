using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Repositories
{
    public class ImagesRepository(StoreDbContext context, IMapper mapper) : IImagesRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;


        public async Task<IEnumerable<Image>> Get(int productId)
        {
            var imageEntities = await _context.Images
                .Where(i => i.ProductId == productId)
                .Include(i => i.Product)
                .AsNoTracking()
                .ToListAsync();

            return imageEntities.Select(i => _mapper.Map<Image>(i)!);
        }

        public async Task<int> Create(Image image)
        {
            var imageEntity = _mapper.Map<ImageEntity>(image)!;

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