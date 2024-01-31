using Microsoft.AspNetCore.Mvc;
using aspnet_react_store.Domain.Abstractions;
using aspnet_react_store.API.Contracts;

namespace aspnet_react_store.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductsService productsService) : ControllerBase
    {

        private readonly IProductsService _productsService = productsService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsResponse>>> GetProducts(int? startId, int? count, string? searchText)
        {
            try
            {
                var products = await _productsService.GetProducts(startId, count, searchText);

                var response = products.Select(p => new ProductsResponse(p.Id, p.Name, p.Price, p.Description));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}