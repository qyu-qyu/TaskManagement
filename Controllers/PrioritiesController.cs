using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityService _priorityService;

        public PrioritiesController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var priorities = await _priorityService.GetAllAsync();
            return Ok(priorities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var priority = await _priorityService.GetByIdAsync(id);

            if (priority == null)
                return NotFound(new { message = "Priority not found." });

            return Ok(priority);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Priority priority)
        {
            var createdPriority = await _priorityService.CreateAsync(priority);
            return Ok(createdPriority);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Priority priority)
        {
            var updated = await _priorityService.UpdateAsync(id, priority);

            if (!updated)
                return NotFound(new { message = "Priority not found." });

            return Ok(new { message = "Priority updated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _priorityService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Priority not found." });

            return Ok(new { message = "Priority deleted successfully." });
        }
    }
}