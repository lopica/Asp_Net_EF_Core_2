using Asp_Net_EF_Core_1.Domains;
using Asp_Net_EF_Core_1.DTOs;
using Asp_Net_EF_Core_1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_Net_EF_Core_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _context.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var proj = await _context.Projects.FindAsync(id);
            return proj == null ? NotFound() : Ok(proj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto dto)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
            };

            await _context.AddAsync(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            project.Name = dto.Name; 

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
