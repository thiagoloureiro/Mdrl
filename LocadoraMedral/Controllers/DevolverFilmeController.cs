using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraMedral.Controllers
{
    public class DevolverFilmeController : Controller
    {
        private readonly postgresContext _context;
        private IFilmeService _filmeService;

        public DevolverFilmeController(postgresContext context, IFilmeService filmeService)
        {
            _context = context;
            _filmeService = filmeService;
        }

        // GET: DevolverFilme
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbFilme.ToListAsync());
        }

        public IActionResult Sucesso()
        {
            return View();
        }

        // GET: DevolverFilme/Edit/5
        public IActionResult Devolver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbFilme = _context.TbFilme.SingleOrDefault(m => m.Id == id);
            if (tbFilme == null)
            {
                return NotFound();
            }

            _filmeService.Devolver(tbFilme.Id);

            return View("Sucesso");
        }

        // POST: DevolverFilme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        private bool TbFilmeExists(int id)
        {
            return _context.TbFilme.Any(e => e.Id == id);
        }
    }
}