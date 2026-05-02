using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosTelemetriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrosTelemetriaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroTelemetria>>> GetAll()
        {
            return await _context.RegistrosTelemetria.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroTelemetria>> GetById(int id)
        {
            var registro = await _context.RegistrosTelemetria.FindAsync(id);

            if (registro == null)
                return NotFound("Registro de telemetria não encontrado.");

            return registro;
        }

        [HttpPost]
        public async Task<ActionResult<RegistroTelemetria>> Create(RegistroTelemetria registro)
        {
            var bateria = await _context.Baterias.FindAsync(registro.BateriaId);

            if (bateria == null)
                return BadRequest("A bateria informada não existe.");

            if (registro.Temperatura > 85)
            {
                Console.WriteLine(
                    $"ALERTA DE SEGURANÇA: Risco térmico detectado na bateria {bateria.NumeroSerie}! Registro bloqueado para investigação."
                );

                return BadRequest(
                    $"ALERTA DE SEGURANÇA: Risco térmico detectado na bateria {bateria.NumeroSerie}! Registro bloqueado para investigação."
                );
            }

            registro.DataLeitura = DateTime.Now;

            _context.RegistrosTelemetria.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = registro.Id }, registro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registro = await _context.RegistrosTelemetria.FindAsync(id);

            if (registro == null)
                return NotFound("Registro de telemetria não encontrado.");

            _context.RegistrosTelemetria.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}