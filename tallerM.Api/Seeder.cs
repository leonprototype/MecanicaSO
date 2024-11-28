using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using tallerM.Api.Helpers;
using tallerM.Shared.Entities;

namespace tallerM.Api
{
    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public Seeder(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await CheckClientesAsync();
            await CheckAutomovilesAsync();
            await CheckMecanicosAsync();
            await CheckRefaccionesAsync();
            await CheckCambioPiezasAsync();
            await CheckDetalleServiciosAsync();
            await CheckServiciosAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Diego", "Rod", "2212068093", "diego@gmail.com", UserType.Admin);
            await CheckUserAsync("Ale", "Rod", "2212068094", "ale@gmail.com", UserType.User);
        }

        private async Task<User> CheckUserAsync(string firstname, string lastName, string phoneNumber, string email, UserType userType)
        {
            var user = await userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstname,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    UserType = userType,
                    UserName = email
                };
                await userHelper.AddUserAsync(user, "123456");
                await userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckClientesAsync()
        {
            if (!dataContext.Clientes.Any())
            {
                var cliente = new Cliente
                {
                    Name = "Juan Pérez",
                    Email = "juan.perez@gmail.com",
                };

                dataContext.Clientes.Add(cliente);
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckAutomovilesAsync()
        {
            if (!dataContext.Automoviles.Any())
            {
                var cliente = await dataContext.Clientes.FirstOrDefaultAsync(c => c.Email == "juan.perez@gmail.com");
                if (cliente != null)
                {
                    dataContext.Automoviles.AddRange(new List<Automovil>
                    {
                        new Automovil
                        {
                            Brand = "Toyota",
                            Year = 2020,
                            Plate = "ABC123",
                            Cliente = cliente
                        },
                        new Automovil
                        {
                            Brand = "Honda",
                            Year = 2018,
                            Plate = "XYZ789",
                            Cliente = cliente
                        },
                        new Automovil
                        {
                            Brand = "Ford",
                            Year = 2022,
                            Plate = "FORD456",
                            Cliente = cliente
                        }
                    });

                    await dataContext.SaveChangesAsync();
                }
            }
        }

        private async Task CheckMecanicosAsync()
        {
            if (!dataContext.Mecanicos.Any())
            {
                dataContext.Mecanicos.AddRange(new List<Mecanico>
                {
                    new Mecanico
                    {
                        Nombre = "Carlos García",
                        Email = "carlos.garcia@taller.com"
                    },
                    new Mecanico
                    {
                        Nombre = "María López",
                        Email = "maria.lopez@taller.com"
                    }
                });

                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckRefaccionesAsync()
        {
            if (!dataContext.Refacciones.Any())
            {
                dataContext.Refacciones.AddRange(new List<Refaccion>
                {
                    new Refaccion
                    {
                        Tipo = "Pastilla de Freno"
                    },
                    new Refaccion
                    {
                        Tipo = "Filtro de Aceite"
                    }
                });

                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckCambioPiezasAsync()
        {
            if (!dataContext.CambioPiezas.Any())
            {
                var refaccion = await dataContext.Refacciones.FirstOrDefaultAsync(r => r.Tipo == "Pastilla de Freno");
                if (refaccion != null)
                {
                    dataContext.CambioPiezas.Add(new CambioPieza
                    {
                        IdRefaccion = refaccion.Id
                    });

                    await dataContext.SaveChangesAsync();
                }
            }
        }

        private async Task CheckDetalleServiciosAsync()
        {
            if (!dataContext.DetalleServicios.Any())
            {
                var cambioPieza = await dataContext.CambioPiezas.FirstOrDefaultAsync();
                if (cambioPieza != null)
                {
                    var detalleServicio = new DetalleServicio
                    {
                        Descripcion = "Reemplazo de pastillas de freno"
                    };

                    // Use the method to set usoRefaccion
                    detalleServicio.SetUsoRefaccion(true, cambioPieza);

                    dataContext.DetalleServicios.Add(detalleServicio);
                    await dataContext.SaveChangesAsync();
                }
            }
        }


        private async Task CheckServiciosAsync()
        {
            if (!dataContext.Servicios.Any())
            {
                var automovil = await dataContext.Automoviles.FirstOrDefaultAsync(a => a.Plate == "ABC123");
                var mecanico = await dataContext.Mecanicos.FirstOrDefaultAsync();
                if (automovil != null && mecanico != null)
                {
                    var detalleServicio = await dataContext.DetalleServicios.FirstOrDefaultAsync();
                    var servicio = new Servicio
                    {
                        Automovil = automovil,
                        Mecanico = mecanico,
                        Descripcion = detalleServicio?.Descripcion ?? "Mantenimiento general"
                    };

                    // Set usoRefaccion and CambioPieza using the method
                    if (detalleServicio != null && detalleServicio.CambioPieza != null)
                    {
                        servicio.SetUsoRefaccion(detalleServicio.usoRefaccion, detalleServicio.CambioPieza);
                    }

                    dataContext.Servicios.Add(servicio);

                    await dataContext.SaveChangesAsync();
                }
            }
        }
    }
}
