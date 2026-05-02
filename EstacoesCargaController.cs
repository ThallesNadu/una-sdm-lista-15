using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacoesCargaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstacoesCargaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstacaoCarga>>> GetAll()
        {
            return await _context.EstacoesCarga.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstacaoCarga>> GetById(int id)
        {
            var estacao = await _context.EstacoesCarga.FindAsync(id);

            if (estacao == null)
                return NotFound("Estação de carga não encontrada.");

            return estacao;
        }

        [HttpPost]
        public async Task<ActionResult<EstacaoCarga>> Create(EstacaoCarga estacao)
        {
            if (string.IsNullOrWhiteSpace(estacao.Cidade))
                return BadRequest("A cidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(estacao.TipoCarga))
                return BadRequest("O tipo de carga é obrigatório.");

            _context.EstacoesCarga.Add(estacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = estacao.Id }, estacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estacao = await _context.EstacoesCarga.FindAsync(id);

            if (estacao == null)
                return NotFound("Estação de carga não encontrada.");

            _context.EstacoesCarga.Remove(estacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}