using Asp_Net_EF_Core_1.Domains;
using Asp_Net_EF_Core_1.DTOs;
using Asp_Net_EF_Core_1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_Net_EF_Core_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    JoinedDate = e.JoinedDate != null
                        ? e.JoinedDate.ToString("yyyy-MM-dd")
                        : null,
                    DepartmentId = (Guid)e.DepartmentId
                })
                .ToListAsync();

            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    JoinedDate = e.JoinedDate != null
                        ? e.JoinedDate.ToString("yyyy-MM-dd")
                        : null,
                    DepartmentId = (Guid)e.DepartmentId
                })
                .FirstOrDefaultAsync();

            return employee == null ? NotFound() : Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            // If DepartmentId is provided, validate it
            if (dto.DepartmentId.HasValue)
            {
                var departmentExists = await _context.Departments
                    .AnyAsync(d => d.Id == dto.DepartmentId.Value);

                if (!departmentExists)
                {
                    return BadRequest("Provided DepartmentId does not exist.");
                }
            }

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                JoinedDate = dto.JoinedDate,
                DepartmentId = dto.DepartmentId // may be null, and that's okay
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees
                .Include(e => e.ProjectEmployees)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return NotFound();

            // ✅ Kiểm tra DepartmentId tồn tại
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);
            if (!departmentExists)
            {
                return BadRequest($"Department with Id '{dto.DepartmentId}' does not exist.");
            }

            // Cập nhật thông tin
            employee.Name = dto.Name;
            employee.JoinedDate = dto.JoinedDate;
            employee.DepartmentId = dto.DepartmentId;

            // Cập nhật ProjectEmployees
            _context.ProjectEmployees.RemoveRange(employee.ProjectEmployees);

            employee.ProjectEmployees = dto.ProjectIds.Select(pid => new ProjectEmployee
            {
                EmployeeId = employee.Id,
                ProjectId = pid
            }).ToList();

            await _context.SaveChangesAsync();
            return NoContent();
        }



        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("with-department")]
        public async Task<IActionResult> GetEmployeesWithDepartments()
        {
            var result = await _context.Employees
                .Where(e => e.Department != null)
                .Select(e => new EmployeeWithDepartmentDto
                {
                    EmployeeName = e.Name,
                    DepartmentName = e.Department!.Name
                })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("with-projects")]
        public async Task<IActionResult> GetEmployeesWithProjects()
        {
            var result = await _context.Employees
                .Include(e => e.ProjectEmployees)
                .ThenInclude(pe => pe.Project)
                .Select(e => new EmployeeWithProjectsDto
                {
                    EmployeeName = e.Name,
                    ProjectNames = e.ProjectEmployees
                        .Select(pe => pe.Project.Name)
                        .ToList()
                })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredEmployees()
        {
            var result = await _context.Employees
                .Include(e => e.Salary) // Ensure Salary is loaded
                .Where(e => e.Salary != null &&
                            e.Salary.Amount > 100 &&
                            e.JoinedDate >= new DateTime(2024, 1, 1))
                .Select(e => new FilteredEmployeeDto
                {
                    Name = e.Name,
                    JoinedDate = e.JoinedDate.ToString("yyyy-MM-dd"), // format date only
                    SalaryAmount = e.Salary.Amount
                })
                .ToListAsync();

            return Ok(result);
        }


    }
}
