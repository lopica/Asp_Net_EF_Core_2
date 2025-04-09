namespace Asp_Net_EF_Core_1.Domains
{
    public class Salary
    {
        public Guid Id { get; set; }
        public required decimal Amount { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
