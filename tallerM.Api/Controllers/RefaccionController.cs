using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Route("/api/refacciones")]
    public class RefaccionController : ControllerBase
    {
        private readonly DataContext dataContext;

        public RefaccionController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Refacciones.ToListAsync());
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var refaccion = await dataContext.Refacciones.FirstOrDefaultAsync(x => x.Id == id);
            if (refaccion == null)
            {
                return NotFound();
            }
            return Ok(refaccion);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Refaccion refaccion)
        {
            dataContext.Refacciones.Add(refaccion);
            await dataContext.SaveChangesAsync();
            return Ok(refaccion);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Refaccion refaccion)
        {
            dataContext.Refacciones.Update(refaccion);
            await dataContext.SaveChangesAsync();
            return Ok(refaccion);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.Refacciones.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
