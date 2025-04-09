namespace Asp_Net_EF_Core_1.Domains
{
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }

    }
}
