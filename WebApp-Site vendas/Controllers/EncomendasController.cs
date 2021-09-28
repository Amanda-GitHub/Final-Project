using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Projeto_CLOUD_45_2021.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp_Site_vendas.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly DataBaseContext _context;

        public EncomendasController(DataBaseContext context)
        {
            _context = context;
        }

         [Authorize]
        public async Task<IActionResult> MinhasEncomendas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }           
            
            var dataBaseContext = _context.Encomendas.Include(e => e.Produto).Include(e => e.Utilizador).Where(c => c.UtilizadorId == id);
            return View(await dataBaseContext.ToListAsync());
        }


       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encomenda = await _context.Encomendas
                .Include(e => e.Produto)
                .Include(e => e.Utilizador)
                .FirstOrDefaultAsync(m => m.EncomendaId == id);
            if (encomenda == null)
            {
                return NotFound();
            }

            return View(encomenda);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId");
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "UtilizadorId", "UtilizadorId");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EncomendaId,DataEncomenda,Quantidade,ValorTotal,UtilizadorId,ProdutoId")] Encomenda encomenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encomenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", encomenda.ProdutoId);
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "UtilizadorId", "UtilizadorId", encomenda.UtilizadorId);
            return View(encomenda);
        }



       
        

    }
}
