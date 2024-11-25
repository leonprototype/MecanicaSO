using tallerM.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Api;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Route("/api/automoviles")]
    public class AutomovilesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public AutomovilesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Automoviles.Include(c => c.Cliente).ToListAsync());
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var automovil = await dataContext.Automoviles.FirstOrDefaultAsync(x => x.Id == id);
            if (automovil == null)
            {
                return NotFound();
            }
            return Ok(automovil);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Automovil automovil)
        {
            dataContext.Automoviles.Add(automovil);
            await dataContext.SaveChangesAsync();
            return Ok(automovil);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Automovil automovil)
        {
            dataContext.Automoviles.Update(automovil);
            await dataContext.SaveChangesAsync();
            return Ok(automovil);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var afectedRows = await dataContext.Automoviles.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (afectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
