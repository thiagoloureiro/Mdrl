using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraMedral.Model;

namespace LocadoraMedral.Controllers
{
    public class FilmeController : Controller
    {
        private readonly postgresContext _context;

        public FilmeController(postgresContext context)
        {
            _context = context;
        }

        // GET: Filme
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbFilme.ToListAsync());
        }

        // GET: Filme/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbFilme = await _context.TbFilme
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tbFilme == null)
            {
                return NotFound();
            }

            return View(tbFilme);
        }

        // GET: Filme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Filme,Genero")] TbFilme tbFilme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbFilme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbFilme);
        }

        // GET: Filme/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbFilme = await _context.TbFilme.SingleOrDefaultAsync(m => m.Id == id);
            if (tbFilme == null)
            {
                return NotFound();
            }
            return View(tbFilme);
        }

        // POST: Filme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Filme,Genero")] TbFilme tbFilme)
        {
            if (id != tbFilme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbFilme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbFilmeExists(tbFilme.Id))
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
            return View(tbFilme);
        }

        // GET: Filme/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbFilme = await _context.TbFilme
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tbFilme == null)
            {
                return NotFound();
            }

            return View(tbFilme);
        }

        // POST: Filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbFilme = await _context.TbFilme.SingleOrDefaultAsync(m => m.Id == id);
            _context.TbFilme.Remove(tbFilme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbFilmeExists(int id)
        {
            return _context.TbFilme.Any(e => e.Id == id);
        }
    }
}