namespace Asp_Net_EF_Core_1.DTOs
{
    public class CreateEmployeeDto
    {
        public required string Name { get; set; }
        public DateTime JoinedDate { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
