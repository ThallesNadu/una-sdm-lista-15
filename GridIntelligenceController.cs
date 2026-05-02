using GreenDriveApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/intelligence")]
    public class GridIntelligenceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GridIntelligenceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("carbon-footprint")]
        public async Task<IActionResult> GetCarbonFootprint()
        {
            await Task.Delay(3000);

            var resultado = await _context.OrdensReciclagem
                .Join(
                    _context.EstacoesCarga,
                    ordem => ordem.EstacaoId,
                    estacao => estacao.Id,
                    (ordem, estacao) => new
                    {
                        estacao.Cidade,
                        ordem.CustoProcessamento
                    }
                )
                .GroupBy(x => x.Cidade)
                .Select(g => new
                {
                    Cidade = g.Key,
                    CustoTotalProcessamento = g.Sum(x => x.CustoProcessamento)
                })
                .ToListAsync();

            return Ok(resultado);
        }
    }
}