namespace Asp_Net_EF_Core_1.DTOs
{
    public class CreateSalaryDto
    {
        public required decimal Amount { get; set; }
        public required Guid EmployeeId { get; set; }
    }
}
