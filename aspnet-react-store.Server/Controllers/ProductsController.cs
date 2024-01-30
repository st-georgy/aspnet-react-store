using aspnet_react_store.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_react_store.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(StoreDbContext context) : ControllerBase
    {
        private readonly StoreDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int? startId, [FromQuery] int? count, [FromQuery] string? searchText)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var searchTerms = searchText.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(p =>
                    p.Name.ToLower().Equals(searchText)
                        || searchTerms.Any(term =>
                            p.Name.ToLower().Contains(term)));
            }

            if (!startId.HasValue || !count.HasValue)
                return Ok(await query.ToListAsync());

            if (startId < 1 || count < 1)
                return BadRequest("Invalid arguments (startId or count)");

            var maxProductId = await _context.Products.MaxAsync(p => (int?)p.Id) ?? 0;

            if (startId > maxProductId)
                return BadRequest("startId exceeds the maximum Id in the database");

            query = query
                .OrderBy(p => p.Id)
                .Where(p => p.Id >= startId)
                .Take(count.Value);

            var result = await query.ToListAsync();

            return Ok(result);
        }
    }
}