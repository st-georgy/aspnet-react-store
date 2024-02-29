using aspnet_react_store.API.Contracts.Responses;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_react_store.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CartsController(ICartsService cartsService, IProductCartsService productCartsService) : ControllerBase
    {
        private readonly ICartsService _cartsService = cartsService;
        private readonly IProductCartsService _productCartsService = productCartsService;

        [HttpGet]
        public async Task<ActionResult<CartsResponse>> GetProductsFromCart()
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    throw new ClaimsException("User ID is invalid or missing");

                var cart = await _cartsService.GetUserCart(userId);

                var productCarts = await _productCartsService.GetProductsInCart(cart.Id);

                var response = new CartsResponse(
                    productCarts.Sum(pc => pc.Quantity),
                    productCarts.Sum(pc => pc.Product!.Price * (1 - pc.Product!.Discount) * pc.Quantity),
                    cart.Discount,
                    productCarts.Select(pc => new CartProductsResponse(
                        pc.Product!.Id,
                        pc.Product.Name,
                        pc.Product.Price,
                        pc.Product.Quantity,
                        pc.Quantity,
                        pc.Product.Discount,
                        pc.Product.Images.Count > 0 ? new ImagesResponse
                        (
                            pc.Product.Images[0].Id,
                            pc.Product.Images[0].FilePath
                        ) : null)).ToArray());

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