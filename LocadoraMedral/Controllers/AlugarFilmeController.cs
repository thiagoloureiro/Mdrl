﻿using Microsoft.AspNetCore.Mvc;
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