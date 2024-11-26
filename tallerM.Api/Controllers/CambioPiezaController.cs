using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Route("/api/cambio-piezas")]
    public class CambioPiezaController : ControllerBase
    {
        private readonly DataContext dataContext;

        public CambioPiezaController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.CambioPiezas.Include(cp => cp.Refaccion).ToListAsync());
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cambioPieza = await dataContext.CambioPiezas.Include(cp => cp.Refaccion).FirstOrDefaultAsync(x => x.Id == id);
            if (cambioPieza == null)
            {
                return NotFound();
            }
            return Ok(cambioPieza);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CambioPieza cambioPieza)
        {
            dataContext.CambioPiezas.Add(cambioPieza);
            await dataContext.SaveChangesAsync();
            return Ok(cambioPieza);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(CambioPieza cambioPieza)
        {
            dataContext.CambioPiezas.Update(cambioPieza);
            await dataContext.SaveChangesAsync();
            return Ok(cambioPieza);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.CambioPiezas.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
