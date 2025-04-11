namespace Asp_Net_EF_Core_1.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // 👇 Format date to only show the date part (yyyy-MM-dd)
        public string? JoinedDate { get; set; }

        public Guid? DepartmentId { get; set; }
    }
}
