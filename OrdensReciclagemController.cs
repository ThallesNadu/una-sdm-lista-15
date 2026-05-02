using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdensReciclagemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdensReciclagemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemReciclagem>>> GetAll()
        {
            return await _context.OrdensReciclagem.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemReciclagem>> GetById(int id)
        {
            var ordem = await _context.OrdensReciclagem.FindAsync(id);

            if (ordem == null)
                return NotFound("Ordem de reciclagem não encontrada.");

            return ordem;
        }

        [HttpPost]
        public async Task<ActionResult<OrdemReciclagem>> Create(OrdemReciclagem ordem)
        {
            var bateria = await _context.Baterias.FindAsync(ordem.BateriaId);

            if (bateria == null)
                return BadRequest("A bateria informada não existe.");

            var estacao = await _context.EstacoesCarga.FindAsync(ordem.EstacaoId);

            if (estacao == null)
                return BadRequest("A estação de carga informada não existe.");

            if (bateria.SaudeBateria > 60)
            {
                return BadRequest(
                    "Bateria com saúde superior a 60%. Encaminhe para o programa de Reuso Doméstico (Second Life) em vez de reciclagem."
                );
            }

            if (estacao.TipoCarga.Equals("Ultra-Rapida", StringComparison.OrdinalIgnoreCase))
            {
                ordem.CustoProcessamento += 250.00m;
            }

            _context.OrdensReciclagem.Add(ordem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ordem.Id }, ordem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ordem = await _context.OrdensReciclagem.FindAsync(id);

            if (ordem == null)
                return NotFound("Ordem de reciclagem não encontrada.");

            _context.OrdensReciclagem.Remove(ordem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}