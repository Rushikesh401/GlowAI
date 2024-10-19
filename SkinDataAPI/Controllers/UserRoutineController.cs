using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SkinData.Infrastructure;
using SkinData.Domain;

namespace SkinDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoutineController : ControllerBase
    {
        private readonly IUserRoutineRepository _repository;

        public UserRoutineController(IUserRoutineRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetUserRoutines")]
        public async Task<IActionResult> GetUserRoutinesData(int userId)
        {
            var data = await _repository.GetUserRoutinesAsync(userId);

            if (data == null || !data.Any())
            {
                return NotFound(new { message = "User routine data not found." });
            }

            return Ok(data);
        }

        [HttpPost("PostUserRoutines")]
        public async Task<IActionResult> CreateUserRoutinesData(List<UserRoutine> data)
        {
            if (data == null || !data.Any())
            {
                return BadRequest(new { message = "User routine data is required." });
            }

            try
            {
                await _repository.AddUserRoutinesAsync(data);

                var response = new
                {
                    Message = "User routines created successfully."
                };

                return CreatedAtAction(nameof(GetUserRoutinesData), response);
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
