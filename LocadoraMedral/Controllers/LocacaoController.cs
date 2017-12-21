using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraMedral.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly postgresContext _context;

        public LocacaoController(postgresContext context)
        {
            _context = context;
        }

        // GET: Locacao
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TbLocacao.Include(t => t.TbCliente).Include(t => t.TbFilme);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Locacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLocacao = await _context.TbLocacao
                .Include(t => t.TbCliente)
                .Include(t => t.TbFilme)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tbLocacao == null)
            {
                return NotFound();
            }

            return View(tbLocacao);
        }

        // GET: Locacao/Create
        public IActionResult Create()
        {
            ViewData["TbClienteId"] = new SelectList(_context.TbCliente, "Id", "Cpf");
            ViewData["TbFilmeId"] = new SelectList(_context.TbFilme, "Id", "Filme");
            return View();
        }

        // POST: Locacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TbClienteId,TbFilmeId,DataLocacao,DataDevolucao,Id")] TbLocacao tbLocacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbLocacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TbClienteId"] = new SelectList(_context.TbCliente, "Id", "Cpf", tbLocacao.TbClienteId);
            ViewData["TbFilmeId"] = new SelectList(_context.TbFilme, "Id", "Filme", tbLocacao.TbFilmeId);
            return View(tbLocacao);
        }

        // GET: Locacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLocacao = await _context.TbLocacao.SingleOrDefaultAsync(m => m.Id == id);
            if (tbLocacao == null)
            {
                return NotFound();
            }
            ViewData["TbClienteId"] = new SelectList(_context.TbCliente, "Id", "Cpf", tbLocacao.TbClienteId);
            ViewData["TbFilmeId"] = new SelectList(_context.TbFilme, "Id", "Filme", tbLocacao.TbFilmeId);
            return View(tbLocacao);
        }

        // POST: Locacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TbClienteId,TbFilmeId,DataLocacao,DataDevolucao,Id")] TbLocacao tbLocacao)
        {
            if (id != tbLocacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbLocacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbLocacaoExists(tbLocacao.Id))
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
            ViewData["TbClienteId"] = new SelectList(_context.TbCliente, "Id", "Cpf", tbLocacao.TbClienteId);
            ViewData["TbFilmeId"] = new SelectList(_context.TbFilme, "Id", "Filme", tbLocacao.TbFilmeId);
            return View(tbLocacao);
        }

        // GET: Locacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLocacao = await _context.TbLocacao
                .Include(t => t.TbCliente)
                .Include(t => t.TbFilme)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tbLocacao == null)
            {
                return NotFound();
            }

            return View(tbLocacao);
        }

        // POST: Locacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbLocacao = await _context.TbLocacao.SingleOrDefaultAsync(m => m.Id == id);
            _context.TbLocacao.Remove(tbLocacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbLocacaoExists(int id)
        {
            return _context.TbLocacao.Any(e => e.Id == id);
        }
    }
}