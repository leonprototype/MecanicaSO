using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api
{
    public class DataContext : DbContext
    {
        // Define DbSet properties for all entities
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Automovil> Automoviles { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<DetalleServicio> DetalleServicios { get; set; }
        public DbSet<CambioPieza> CambioPiezas { get; set; }
        public DbSet<Mecanico> Mecanicos { get; set; }
        public DbSet<Refaccion> Refacciones { get; set; }

        public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique constraints
            modelBuilder.Entity<Cliente>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Automovil>().HasIndex(x => x.Plate).IsUnique();

            // Configure relationships and additional constraints
            modelBuilder.Entity<DetalleServicio>()
                .HasOne(ds => ds.CambioPieza) // Detail references CambioPieza
                .WithMany() // No navigation property in CambioPieza
                .HasForeignKey(ds => ds.IdCambioPieza)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Automovil) // Servicio references Automovil
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Mecanico) // Servicio references Mecanico
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CambioPieza>()
                .HasOne(cp => cp.Refaccion) // CambioPieza references Refaccion
                .WithMany()
                .HasForeignKey(cp => cp.IdRefaccion)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
        }
    }
}
