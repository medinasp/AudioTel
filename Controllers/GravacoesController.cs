using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AudioTel.Models;
using X.PagedList;

namespace AudioTel.Controllers
{
    public class GravacoesController : Controller
    {
        private readonly AudioTelDbContext _context;

        public GravacoesController(AudioTelDbContext context)
        {
            _context = context;
        }

        // GET: Gravacoes
/*
        public async Task<IActionResult> Index()
        {
            var audioTelDbContext = _context.Gravacoes.Include(g => g.IdBancoClienteNavigation);
            return View(await audioTelDbContext.ToListAsync());
        }
*/
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pagina)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.First = String.IsNullOrEmpty(sortOrder) ? "First" : "";

            int tamanhoPagina = 2;
            int numeroPagina = pagina ?? 1;

            if (searchString != null)
            {
                pagina = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var numero = from s in _context.Gravacoes
                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                numero = numero.Where(s => s.Numero.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "First":
                    numero = numero.OrderByDescending(s => s.Numero);
                    break;
                default:
                    numero = numero.OrderBy(s => s.Numero);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (pagina ?? 1);
            return View(numero.ToPagedList(numeroPagina, tamanhoPagina));

        }

        // GET: Gravacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravacao = await _context.Gravacoes
                .Include(g => g.IdBancoClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravacao == null)
            {
                return NotFound();
            }

            return View(gravacao);
        }

        // GET: Gravacoes/Create
        public IActionResult Create()
        {
            ViewData["IdBancoCliente"] = new SelectList(_context.Bancoclientes, "Id", "Ddd1");
            return View();
        }

        // POST: Gravacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdBancoCliente,Numero,NomeDoArquivo,FileSize,FilePath,Ramal")] Gravacao gravacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gravacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBancoCliente"] = new SelectList(_context.Bancoclientes, "Id", "Ddd1", gravacao.IdBancoCliente);
            return View(gravacao);
        }

        // GET: Gravacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravacao = await _context.Gravacoes.FindAsync(id);
            if (gravacao == null)
            {
                return NotFound();
            }
            ViewData["IdBancoCliente"] = new SelectList(_context.Bancoclientes, "Id", "Ddd1", gravacao.IdBancoCliente);
            return View(gravacao);
        }

        // POST: Gravacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdBancoCliente,Numero,NomeDoArquivo,FileSize,FilePath,Ramal")] Gravacao gravacao)
        {
            if (id != gravacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gravacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GravacaoExists(gravacao.Id))
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
            ViewData["IdBancoCliente"] = new SelectList(_context.Bancoclientes, "Id", "Ddd1", gravacao.IdBancoCliente);
            return View(gravacao);
        }

        // GET: Gravacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravacao = await _context.Gravacoes
                .Include(g => g.IdBancoClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravacao == null)
            {
                return NotFound();
            }

            return View(gravacao);
        }

        // POST: Gravacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gravacao = await _context.Gravacoes.FindAsync(id);
            _context.Gravacoes.Remove(gravacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GravacaoExists(int id)
        {
            return _context.Gravacoes.Any(e => e.Id == id);
        }
    }
}
