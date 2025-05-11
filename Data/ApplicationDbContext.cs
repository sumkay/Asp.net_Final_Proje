using Microsoft.EntityFrameworkCore;
using Dis.Models;



namespace Dis.Data 

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; } 
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
       




    }
}
