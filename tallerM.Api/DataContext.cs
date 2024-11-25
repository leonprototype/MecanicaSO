using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api
{
    public class DataContext : DbContext
    {
        public DbSet <Cliente> Clientes { get; set; }
        public DbSet <Automovil> Automoviles { get; set; }

        public DbSet <Servicio> Servicios { get; set; }
        public DataContext(DbContextOptions<DataContext> dbContext):base(dbContext) 
        { 
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Automovil>().HasIndex(x => x.Plate).IsUnique();

        }
    }
}

