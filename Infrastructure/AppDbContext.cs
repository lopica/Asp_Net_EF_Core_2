using Asp_Net_EF_Core_1.Domains;
using Microsoft.EntityFrameworkCore;

namespace Asp_Net_EF_Core_1.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // constraints
            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(d => d.Name)
                .HasMaxLength(50) 
                .IsRequired();

            // relationships
            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.Salary)
            //    .WithOne(s => s.Employee)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Department>()
            //    .HasMany(d => d.Employees)
            //    .WithOne(e => e.Department)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Project>()
            //    .HasMany(p => p.Employees)
            //    .WithMany(e => e.Projects);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // seeding data
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"),
                    Name = "Software Development",
                    EmployeeId = Guid.Empty
                },
                new Department
                {
                    Id = Guid.Parse("22222222-bbbb-cccc-dddd-222222222222"),
                    Name = "Finance",
                    EmployeeId = Guid.Empty
                },
                new Department
                {
                    Id = Guid.Parse("33333333-cccc-dddd-eeee-333333333333"),
                    Name = "Accountant",
                    EmployeeId = Guid.Empty
                },
                new Department
                {
                    Id = Guid.Parse("44444444-dddd-eeee-ffff-444444444444"),
                    Name = "HR",
                    EmployeeId = Guid.Empty
                }
            );
        }

        public virtual DbSet<Department> Departments { get; set; }
         public virtual DbSet<Project> Projects { get; set; }
         public virtual DbSet<Employee> Employees { get; set; }
         public virtual DbSet<Salary> Salaries { get; set; }
         public virtual DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
