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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int? startId, [FromQuery] int? count)
        {
            if (!startId.HasValue || !count.HasValue)
            {
                var allProducts = await _context.Products.ToListAsync();
                return Ok(allProducts);
            }

            if (startId < 1 || count < 1)
                return BadRequest("Invalid arguments (startId or count)");

            var products = await _context.Products
                                .Where(p => p.Id >= startId)
                                .Take(count.Value)
                                .ToListAsync();

            return Ok(products);
        }
    }
}