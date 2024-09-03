using Microsoft.EntityFrameworkCore;
using Тестовое_Системы_управления;

namespace Тестовое_Системы_управления
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Uparticipation> Uparticipation { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<Cabinet> Cabinet { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }

}
