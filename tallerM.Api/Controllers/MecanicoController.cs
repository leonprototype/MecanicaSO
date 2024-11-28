using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Shared.Entities;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/mecanicos")]
    public class MecanicoController : ControllerBase
    {
        private readonly DataContext dataContext;

        public MecanicoController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Mecanicos.ToListAsync());
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var mecanico = await dataContext.Mecanicos.FirstOrDefaultAsync(x => x.Id == id);
            if (mecanico == null)
            {
                return NotFound();
            }
            return Ok(mecanico);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Mecanico mecanico)
        {
            dataContext.Mecanicos.Add(mecanico);
            await dataContext.SaveChangesAsync();
            return Ok(mecanico);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Mecanico mecanico)
        {
            dataContext.Mecanicos.Update(mecanico);
            await dataContext.SaveChangesAsync();
            return Ok(mecanico);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.Mecanicos.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
