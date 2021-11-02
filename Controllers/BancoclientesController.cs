using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AudioTel.Models;
using System.Web;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc;



namespace AudioTel.Controllers
{
    public class BancoclientesController : Controller
    {
        private readonly AudioTelRecDbContext _context;

        public BancoclientesController(AudioTelRecDbContext context)
        {
            _context = context;
        }

        // GET: Bancoclientes
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pagina)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.First = String.IsNullOrEmpty(sortOrder) ? "First" : "";
            ViewBag.Second = sortOrder == "Second" ? "Second_desc" : "Second";

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

            var entrevistador = from s in _context.Bancoclientes
                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                entrevistador = entrevistador.Where(s => s.Entrevistador.ToUpper().Contains(searchString.ToUpper())
                    || s.Ddd1.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "First":
                    entrevistador = entrevistador.OrderByDescending(s => s.Entrevistador);
                        break;
                case "Second":
                    entrevistador = entrevistador.OrderBy(s => s.HoraStatus);
                    break;
                case "Second_desc":
                    entrevistador = entrevistador.OrderByDescending(s => s.HoraStatus);
                    break;
                default:
                    entrevistador = entrevistador.OrderBy(s => s.Entrevistador);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (pagina ?? 1);
            //return View(entrevistador.ToListAsync(pageNumber, pageSize));
                        return View(entrevistador.ToPagedList(numeroPagina, tamanhoPagina));

        }

        // GET: Bancoclientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancocliente = await _context.Bancoclientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bancocliente == null)
            {
                return NotFound();
            }

            return View(bancocliente);
        }

        // GET: Bancoclientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bancoclientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Entrevistador,DataStatus,HoraStatus,TelFeito,NumTratado,Gravacao,Ddd1,Fone1,Ddd2,Fone2,Ddd3,Fone3,Ddd4,Fone4")] Bancocliente bancocliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bancocliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bancocliente);
        }

        // GET: Bancoclientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancocliente = await _context.Bancoclientes.FindAsync(id);
            if (bancocliente == null)
            {
                return NotFound();
            }
            return View(bancocliente);
        }

        // POST: Bancoclientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Entrevistador,DataStatus,HoraStatus,TelFeito,NumTratado,Gravacao,Ddd1,Fone1,Ddd2,Fone2,Ddd3,Fone3,Ddd4,Fone4")] Bancocliente bancocliente)
        {
            if (id != bancocliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bancocliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BancoclienteExists(bancocliente.Id))
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
            return View(bancocliente);
        }

        // GET: Bancoclientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancocliente = await _context.Bancoclientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bancocliente == null)
            {
                return NotFound();
            }

            return View(bancocliente);
        }

        // POST: Bancoclientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bancocliente = await _context.Bancoclientes.FindAsync(id);
            _context.Bancoclientes.Remove(bancocliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BancoclienteExists(int id)
        {
            return _context.Bancoclientes.Any(e => e.Id == id);
        }
    }
}
