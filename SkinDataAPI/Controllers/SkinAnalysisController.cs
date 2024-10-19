using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkinData.Domain;
using SkinData.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace SkinDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinAnalysisController : ControllerBase
    {
        private readonly ISkinAnalysisRepository _repository;

        public SkinAnalysisController(ISkinAnalysisRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetSkinData")]
        public async Task<IActionResult> GetSkinAnalysisData(int userId)
        {
            var data = await _repository.GetSkinDataAsync(userId);

            if (data == null)
            {
                return NotFound(new { message = "Skin analysis data not found." });
            }

            return Ok(data);
        }

        [HttpPost("PostSkinData")]
        public async Task<IActionResult> CreateSkinAnalysisData(UserSkin data)
        {
            if (data == null)
            {
                return BadRequest(new { message = "Skin analysis data is required." });
            }

            try
            {
                await _repository.AddSkinDataAsync(data);

                var response = new
                {
                    UserId = data.UserId,
                    Message = "Skin analysis data created successfully."
                };

                return CreatedAtAction(nameof(GetSkinAnalysisData), new { userId = data.UserId }, response);
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
