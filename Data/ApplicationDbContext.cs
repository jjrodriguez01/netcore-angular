using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using spa.Models;

namespace spa.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Timeslip> Timeslips {get; set;}
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasKey(x => x.DepartmentId);
            modelBuilder.Entity<Department>().Property(x => x.DepartmentId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Models.Task>().HasKey(x => x.TaskId);
            modelBuilder.Entity<Models.Task>().Property(x => x.TaskId).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Timeslip>().HasKey(x => x.TimeslipId);
            modelBuilder.Entity<Timeslip>().Property(x => x.TimeslipId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Timeslip>().HasOne(x => x.ApplicationUser)
                .WithMany().HasForeignKey(ts => ts.ApplicationUserId);
            //seed data
            var depto1 = new Department()
            {
                DepartmentId = 1,
                Name = "Credivalores CV Desarrollo"
            };
            modelBuilder.Entity<Department>().HasData(depto1);

            modelBuilder.Entity<Models.Task>().HasData(new Models.Task()
            {
                TaskId = 1,
                Name = "Daily six",
                DepartmentId = 1
            });
        }
}
