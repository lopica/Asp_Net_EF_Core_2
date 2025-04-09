namespace Asp_Net_EF_Core_1.Domains
{
    public class ProjectEmployee
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
