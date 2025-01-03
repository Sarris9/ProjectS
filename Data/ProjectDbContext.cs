using Microsoft.EntityFrameworkCore;
using ProjectS.Models.Entities;

namespace ProjectS.Data
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
            
        }

       public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TypeOfService> TypeOfServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
