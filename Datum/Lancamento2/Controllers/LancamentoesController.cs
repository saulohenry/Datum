using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lancamento2.Data;
using Lancamento2.Model;

namespace Lancamento2.Controllers
{
    public class retornoConsolidado
    {
        public string tipo { set; get; }
        public decimal valor { set; get; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoesController : ControllerBase
    {
        private readonly Lancamento2Context _context;

        public LancamentoesController(Lancamento2Context context)
        {
            _context = context;
        }

        // GET: api/Lancamentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetLancamento()
        {
            return await _context.Lancamento.ToListAsync();
        }


        [HttpGet("consolidado/{data}")]
        public async Task<ActionResult<IEnumerable<retornoConsolidado>>> GetLancamento(DateTime data)
        {
            var sql = from l in _context.Lancamento
                      where l.Data == data
                      group l by l.Tipo into g
                      select new { tipo = g.Key.ToString(), valor = g.Sum(e => e.Valor) };

            List<retornoConsolidado> consolidado = new List<retornoConsolidado>();

            foreach (var s in sql)
            {
                retornoConsolidado rc = new retornoConsolidado();
                rc.tipo = s.tipo;
                rc.valor = s.valor;
                consolidado.Add(rc);
            }
            return consolidado.ToList<retornoConsolidado>();
        }

        // GET: api/Lancamentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lancamento>> GetLancamento(int id)
        {
            var lancamento = await _context.Lancamento.FindAsync(id);

            if (lancamento == null)
            {
                return NotFound();
            }

            return lancamento;
        }

        // PUT: api/Lancamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLancamento(int id, Lancamento lancamento)
        {
            if (id != lancamento.id)
            {
                return BadRequest();
            }

            _context.Entry(lancamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LancamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lancamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lancamento>> PostLancamento(Lancamento lancamento)
        {
            _context.Lancamento.Add(lancamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLancamento", new { id = lancamento.id }, lancamento);
        }

        // DELETE: api/Lancamentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLancamento(int id)
        {
            var lancamento = await _context.Lancamento.FindAsync(id);
            if (lancamento == null)
            {
                return NotFound();
            }

            _context.Lancamento.Remove(lancamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LancamentoExists(int id)
        {
            return _context.Lancamento.Any(e => e.id == id);
        }
    }
}
