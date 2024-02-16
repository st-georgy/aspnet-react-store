using Microsoft.AspNetCore.Mvc;
using aspnet_react_store.API.Contracts.Responses;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Exceptions;

namespace aspnet_react_store.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductsService productsService, IImageUrlProvider imageUrlProvider) : ControllerBase
    {

        private readonly IProductsService _productsService = productsService;
        private readonly IImageUrlProvider _imageUrlProvider = imageUrlProvider;

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
                    p.Description,
                    p.Images?
                        .Select(i => new ImagesResponse(i.Id, _imageUrlProvider.GetImageUrl(i.FilePath)))
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
    }
}