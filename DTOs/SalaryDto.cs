namespace Asp_Net_EF_Core_1.DTOs
{
    public class SalaryDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }

}
