using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusesController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _statusService.GetAllAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var status = await _statusService.GetByIdAsync(id);

            if (status == null)
                return NotFound(new { message = "Status not found." });

            return Ok(status);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] StatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdStatus = await _statusService.CreateAsync(dto);
            return Ok(createdStatus);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _statusService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new { message = "Status not found." });

            return Ok(new { message = "Status updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statusService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Status not found." });

            return Ok(new { message = "Status deleted successfully." });
        }
    }
}