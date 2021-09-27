using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Projeto_CLOUD_45_2021.Models;

namespace WebApp_Site_vendas.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly DataBaseContext _context;

        public EncomendasController(DataBaseContext context)
        {
            _context = context;
        }

         
        public async Task<IActionResult> MinhasEncomendas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }           
            
            var dataBaseContext = _context.Encomendas.Include(e => e.Produto).Include(e => e.Utilizador).Where(c => c.UtilizadorId == id);
            return View(await dataBaseContext.ToListAsync());
        }


        // GET: Encomendas/Details/5
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

        // GET: Encomendas/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId");
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "UtilizadorId", "UtilizadorId");
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Encomendas/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var encomenda = await _context.Encomendas.FindAsync(id);
        //    if (encomenda == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", encomenda.ProdutoId);
        //    ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "UtilizadorId", "UtilizadorId", encomenda.UtilizadorId);
        //    return View(encomenda);
        //}

        //// POST: Encomendas/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("EncomendaId,DataEncomenda,Quantidade,ValorTotal,UtilizadorId,ProdutoId")] Encomenda encomenda)
        //{
        //    if (id != encomenda.EncomendaId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(encomenda);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EncomendaExists(encomenda.EncomendaId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", encomenda.ProdutoId);
        //    ViewData["UtilizadorId"] = new SelectList(_context.Utilizadores, "UtilizadorId", "UtilizadorId", encomenda.UtilizadorId);
        //    return View(encomenda);
        //}

        //private bool EncomendaExists(int id)
        //{
        //    return _context.Encomendas.Any(e => e.EncomendaId == id);
        //}
    }
}
