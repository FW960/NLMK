using Microsoft.EntityFrameworkCore;
using NLMK.Models;

namespace NLMK.DBContext;

public class ProjectsDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<ProjectObject> ProjectObjects { get; set; }

    public ProjectsDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectObject>().ToTable("objects");
        modelBuilder.Entity<Project>().ToTable("projects");
        modelBuilder.Entity<Project>().HasKey("Id");
        modelBuilder.Entity<ProjectObject>().HasKey("Id");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*var configManager = new ConfigurationManager();

        configManager.SetBasePath(@"C:\Users\windo\RiderProjects\NLMK\NLMK\NLMK\appsettings.json");*/
        
        optionsBuilder.UseSqlServer(@"Server=LAPTOP-0BLC0UDE\MSSQLSERVER01;Database=Projects;Trusted_Connection=True;");
    }
}