using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AudioTel.Models;

namespace AudioTel.Controllers
{
    public class GravacoesController : Controller
    {
        private readonly DbContextAudioTel _context;

        public GravacoesController(DbContextAudioTel context)
        {
            _context = context;
        }

        // GET: Gravacoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gravacoes.ToListAsync());
        }

        // GET: Gravacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravacoes = await _context.Gravacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravacoes == null)
            {
                return NotFound();
            }

            return View(gravacoes);
        }

        // GET: Gravacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gravacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,NomeDoArquivo,FileSize,FilePath,Ramal")] Gravacoes gravacoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gravacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gravacoes);
        }

        // GET: Gravacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravacoes = await _context.Gravacoes.FindAsync(id);
            if (gravacoes == null)
            {
                return NotFound();
            }
            return View(gravacoes);
        }

        // POST: Gravacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,NomeDoArquivo,FileSize,FilePath,Ramal")] Gravacoes gravacoes)
        {
            if (id != gravacoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gravacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GravacoesExists(gravacoes.Id))
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
            return View(gravacoes);
        }

        // GET: Gravacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravacoes = await _context.Gravacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravacoes == null)
            {
                return NotFound();
            }

            return View(gravacoes);
        }

        // POST: Gravacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gravacoes = await _context.Gravacoes.FindAsync(id);
            _context.Gravacoes.Remove(gravacoes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GravacoesExists(int id)
        {
            return _context.Gravacoes.Any(e => e.Id == id);
        }
    }
}
