using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Threading.Tasks;
using Service;

namespace LocadoraMedral.Controllers
{
    public class AlugarFilmeController : Controller
    {
        private readonly postgresContext _context;
        private IFilmeService _filmeService;

        public AlugarFilmeController(postgresContext context, IFilmeService filmeService)
        {
            _context = context;
            _filmeService = filmeService;
        }

        public IActionResult Erro()
        {
            return View();
        }

        public IActionResult Sucesso()
        {
            return View();
        }

        // GET: AlugarFilme
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbFilme.ToListAsync());
        }

        // GET: AlugarFilme/Details/5
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

        // GET: AlugarFilme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlugarFilme/Create
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

        // GET: AlugarFilme/Edit/5
        public IActionResult Alugar(int? id)
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

            var ret = _filmeService.ChecaDisponibilidade(tbFilme.Id);

            if (ret == false)
                return View("Erro");
            else
                _filmeService.Reservar(tbFilme.Id, 1);

            return View("Sucesso");
        }

        private bool TbFilmeExists(int id)
        {
            return _context.TbFilme.Any(e => e.Id == id);
        }
    }
}