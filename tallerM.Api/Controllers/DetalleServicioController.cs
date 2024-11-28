using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/detalle-servicios")]
    public class DetalleServicioController : ControllerBase
    {
        private readonly DataContext dataContext;

        public DetalleServicioController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.DetalleServicios.Include(ds => ds.CambioPieza).ToListAsync());
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var detalleServicio = await dataContext.DetalleServicios.Include(ds => ds.CambioPieza).FirstOrDefaultAsync(x => x.Id == id);
            if (detalleServicio == null)
            {
                return NotFound();
            }
            return Ok(detalleServicio);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(DetalleServicio detalleServicio)
        {
            dataContext.DetalleServicios.Add(detalleServicio);
            await dataContext.SaveChangesAsync();
            return Ok(detalleServicio);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(DetalleServicio detalleServicio)
        {
            dataContext.DetalleServicios.Update(detalleServicio);
            await dataContext.SaveChangesAsync();
            return Ok(detalleServicio);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.DetalleServicios.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
