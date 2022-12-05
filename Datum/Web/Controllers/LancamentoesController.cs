using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Web.Data;
using System.Net.Http;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class LancamentoesController : Controller
    {
        private readonly WebContext _context;

        public LancamentoesController(WebContext context)
        {
            _context = context;
        }

        // GET: Lancamentoes
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> message = client.GetAsync("http://localhost:57728/api/Lancamentoes");            
            List<Domain.Lancamento> lancamentos = JsonConvert.DeserializeObject<List<Domain.Lancamento>>(message.Result.Content.ToString());
            return View(lancamentos);
        }

        // GET: Lancamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> message = client.GetAsync("http://localhost:57728/api/Lancamentoes/" + id);
            Domain.Lancamento lancamento = JsonConvert.DeserializeObject<Domain.Lancamento>(message.Result.Content.ToString());
            return View(lancamento);
        }

        // GET: Lancamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lancamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Tipo,Valor,Data")] Lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lancamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lancamento);
        }

        // GET: Lancamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamento = await _context.Lancamento.FindAsync(id);
            if (lancamento == null)
            {
                return NotFound();
            }
            return View(lancamento);
        }

        // POST: Lancamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Tipo,Valor,Data")] Lancamento lancamento)
        {
            if (id != lancamento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lancamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancamentoExists(lancamento.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lancamento);
        }

        // GET: Lancamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamento = await _context.Lancamento
                .FirstOrDefaultAsync(m => m.id == id);
            if (lancamento == null)
            {
                return NotFound();
            }

            return View(lancamento);
        }

        // POST: Lancamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lancamento = await _context.Lancamento.FindAsync(id);
            _context.Lancamento.Remove(lancamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancamentoExists(int id)
        {
            return _context.Lancamento.Any(e => e.id == id);
        }
    }
}
