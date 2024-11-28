using tallerM.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/servicio")]
    public class ServicioController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ServicioController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Servicios.Include(a => a.Automovil).ToListAsync());
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var servicio = await dataContext.Servicios.FirstOrDefaultAsync(x => x.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }
            return Ok(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Servicio servicio)
        {
            dataContext.Servicios.Add(servicio);
            await dataContext.SaveChangesAsync();
            return Ok(servicio);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Servicio servicio)
        {
            dataContext.Servicios.Update(servicio);
            await dataContext.SaveChangesAsync();
            return Ok(servicio);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var afectedRows = await dataContext.Servicios.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (afectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
