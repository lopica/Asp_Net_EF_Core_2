namespace Asp_Net_EF_Core_1.Domains
{
    public class Department
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public Guid EmployeeId { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
