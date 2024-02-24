using Microsoft.AspNetCore.Mvc;
using aspnet_react_store.API.Contracts.Responses;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Exceptions;

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

                var response = products.Select(p => new ProductsResponse(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Quantity,
                    p.Discount,
                    p.Description,
                    p.Categories?
                        .Select(c => new CategoriesResponse(c.Id, c.Name))
                        .ToArray(),
                    p.Images?
                        .Select(i => new ImagesResponse(i.Id, i.FilePath))
                        .ToArray()));

                return Ok(response);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest($"Invalid argument: {ex.ParamName}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid argument: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsResponse>> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest("Id is invalid");
            try
            {
                var product = await _productsService.GetProductById(id);

                var response = new ProductsResponse(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Quantity,
                    product.Discount,
                    product.Description,
                    product.Categories?
                        .Select(c => new CategoriesResponse(c.Id, c.Name))
                        .ToArray(),
                    product.Images?
                        .Select(i => new ImagesResponse(i.Id, i.FilePath))
                        .ToArray());

                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}