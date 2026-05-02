using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BateriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BateriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bateria>>> GetAll()
        {
            return await _context.Baterias.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bateria>> GetById(int id)
        {
            var bateria = await _context.Baterias.FindAsync(id);

            if (bateria == null)
                return NotFound("Bateria não encontrada.");

            return bateria;
        }

        [HttpPost]
        public async Task<ActionResult<Bateria>> Create(Bateria bateria)
        {
            if (string.IsNullOrWhiteSpace(bateria.NumeroSerie))
                return BadRequest("O número de série é obrigatório.");

            if (bateria.SaudeBateria < 0 || bateria.SaudeBateria > 100)
                return BadRequest("A saúde da bateria deve estar entre 0 e 100.");

            _context.Baterias.Add(bateria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bateria.Id }, bateria);
        }

        [HttpPatch("{id}/saude")]
        public async Task<IActionResult> AtualizarSaude(int id, [FromBody] int novaSaude)
        {
            var bateria = await _context.Baterias.FindAsync(id);

            if (bateria == null)
                return NotFound("Bateria não encontrada.");

            if (novaSaude < 0 || novaSaude > 100)
                return BadRequest("A saúde da bateria deve estar entre 0 e 100.");

            if (bateria.SaudeBateria <= 10 && novaSaude > bateria.SaudeBateria)
            {
                return Conflict("Fraude de dados detectada: uma bateria inativa não pode ter sua saúde aumentada.");
            }

            bateria.SaudeBateria = novaSaude;

            await _context.SaveChangesAsync();

            return Ok(bateria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bateria = await _context.Baterias.FindAsync(id);

            if (bateria == null)
                return NotFound("Bateria não encontrada.");

            _context.Baterias.Remove(bateria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}