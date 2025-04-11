using Asp_Net_EF_Core_1.Domains;
using Asp_Net_EF_Core_1.DTOs;
using Asp_Net_EF_Core_1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_Net_EF_Core_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalariesController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetSalaries()
        {
            var salaries = await _context.Salaries
        .Include(s => s.Employee)
        .ToListAsync();

            var result = salaries.Select(s => new SalaryDto
            {
                Id = s.Id,
                Amount = s.Amount,
                EmployeeId = (Guid)s.EmployeeId!,
                EmployeeName = s.Employee?.Name
            });

            return Ok(result);
        }

        // GET salary by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalary(Guid id)
        {
            var salary = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (salary == null) return NotFound();

            var result = new SalaryDto
            {
                Id = salary.Id,
                Amount = salary.Amount,
                EmployeeId = (Guid)salary.EmployeeId!,
                EmployeeName = salary.Employee?.Name
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalary([FromBody] CreateSalaryDto dto)
        {
            var employee = await _context.Employees.FindAsync(dto.EmployeeId);
            if (employee == null)
                return NotFound("Employee not found.");

            var existingSalary = await _context.Salaries
                .FirstOrDefaultAsync(s => s.EmployeeId == dto.EmployeeId);
            if (existingSalary != null)
                return Conflict("Salary already exists for this employee.");

            var salary = new Salary
            {
                Id = Guid.NewGuid(),
                Amount = dto.Amount,
                EmployeeId = dto.EmployeeId
            };

            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();

            var result = new SalaryDto
            {
                Id = salary.Id,
                Amount = salary.Amount,
                EmployeeId = (Guid)salary.EmployeeId,
                EmployeeName = employee.Name
            };

            return CreatedAtAction(nameof(GetSalary), new { id = salary.Id }, result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalary(Guid id, [FromBody] UpdateSalaryDto dto)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return NotFound();

            salary.Amount = dto.Amount;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return NotFound();

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
