using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PriorityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPriority = await _priorityService.CreateAsync(dto);
            return Ok(createdPriority);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] PriorityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _priorityService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new { message = "Priority not found." });

            return Ok(new { message = "Priority updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _priorityService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Priority not found." });

            return Ok(new { message = "Priority deleted successfully." });
        }
    }
}