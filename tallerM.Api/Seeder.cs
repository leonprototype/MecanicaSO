using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api
{
    public class Seeder
    {
        private readonly DataContext dataContext;

        public Seeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await CheckClientesAsync();
            await CheckAutomovilesAsync();
            await CheckServiciosAsync();
        }

        private async Task CheckClientesAsync()
        {
            if (!dataContext.Clientes.Any())
            {
                // Crear cliente sin la necesidad de la colección de Automoviles
                var cliente = new Cliente
                {
                    Name = "Cliente101",
                    Email = "cliente101@g.com",
                };

                // Agregar cliente a la base de datos
                dataContext.Clientes.Add(cliente);
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckAutomovilesAsync()
        {
            if (!dataContext.Automoviles.Any())
            {
                var cliente = await dataContext.Clientes.FirstOrDefaultAsync(c => c.Email == "cliente101@g.com");
                if (cliente != null)
                {
                    // Crear los automóviles y asignar el cliente correspondiente
                    dataContext.Automoviles.AddRange(new List<Automovil>
                    {
                        new Automovil
                        {
                            Brand = "Toyota",
                            Year = 2020,
                            Plate = "ABC123",
                            Cliente = cliente // Asignar cliente
                        },
                        new Automovil
                        {
                            Brand = "Honda",
                            Year = 2018,
                            Plate = "XYZ789",
                            Cliente = cliente // Asignar cliente
                        },
                        new Automovil
                        {
                            Brand = "Ford",
                            Year = 2022,
                            Plate = "FORD456",
                            Cliente = cliente // Asignar cliente
                        }
                    });

                    await dataContext.SaveChangesAsync();
                }
            }
        }

        private async Task CheckServiciosAsync()
        {
            if (!dataContext.Servicios.Any())
            {
                var automovil = await dataContext.Automoviles.FirstOrDefaultAsync(a => a.Plate == "ABC123");
                if (automovil != null)
                {
                    // Crear un servicio para el automóvil
                    dataContext.Servicios.Add(new Servicio
                    {
                        Automovil = automovil // Asignar automóvil al servicio
                    });

                    await dataContext.SaveChangesAsync();
                }
            }
        }
    }
}
