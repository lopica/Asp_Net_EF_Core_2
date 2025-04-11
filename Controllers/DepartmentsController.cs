using Asp_Net_EF_Core_1.Domains;
using Asp_Net_EF_Core_1.DTOs;
using Asp_Net_EF_Core_1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_Net_EF_Core_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            var dept = await _context.Departments.FindAsync(id);
            return dept == null ? NotFound() : Ok(dept);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto dto)
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
            };

            await _context.AddAsync(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentDto dto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            department.Name = dto.Name; // only update the fields you want

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
