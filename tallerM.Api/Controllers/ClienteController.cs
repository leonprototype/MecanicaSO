using tallerM.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tallerM.Api;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Route("/api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ClientesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Clientes.ToListAsync());
        }
        [HttpGet("id:int")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cliente = await dataContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(Cliente cliente)
        {
            dataContext.Clientes.Add(cliente);
            await dataContext.SaveChangesAsync();
            return Ok(cliente);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(Cliente cliente)
        {
            dataContext.Clientes.Update(cliente);
            await dataContext.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var afectedRows = await dataContext.Clientes.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (afectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}