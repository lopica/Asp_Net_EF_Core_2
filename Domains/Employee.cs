namespace Asp_Net_EF_Core_1.Domains
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime JoinedDate { get; set; }

        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid SalaryId { get; set; }
        public Salary Salary { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }

    }
}
