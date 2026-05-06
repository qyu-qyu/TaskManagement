using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Status status)
        {
            var createdStatus = await _statusService.CreateAsync(status);
            return Ok(createdStatus);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Status status)
        {
            var updated = await _statusService.UpdateAsync(id, status);

            if (!updated)
                return NotFound(new { message = "Status not found." });

            return Ok(new { message = "Status updated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statusService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Status not found." });

            return Ok(new { message = "Status deleted successfully." });
        }
    }
}