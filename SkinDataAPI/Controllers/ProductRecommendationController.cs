using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkinData.Application;
using SkinData.Domain;
using System.ComponentModel.DataAnnotations;

namespace SkinDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRecommendationController : ControllerBase
    {
        private readonly IProductRecommendationService _service;

        public ProductRecommendationController(IProductRecommendationService service)
        {
            _service = service;
        }

        [HttpGet("GetProductRecommendations")]
        public async Task<IActionResult> GetProductRecommendationData(int userId)
        {
            var data = await _service.GetProductRecommendationAsync(userId);

            if (data == null || !data.Any())
            {
                return NotFound(new { message = "Product recommendation data not found." });
            }

            return Ok(data);
        }

        [HttpPost("PostProductRecommendations")]
        public async Task<IActionResult> CreateProductRecommendationsData(List<ProductRecommendation> data)
        {
            if (data == null || data.Count == 0)
            {
                return BadRequest(new { message = "Product recommendation data is required." });
            }

            try
            {
                await _service.AddProductRecommendationsAsync(data);
                var response = new { Message = "Product recommendations data created successfully." };
                return CreatedAtAction(nameof(GetProductRecommendationData), response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
