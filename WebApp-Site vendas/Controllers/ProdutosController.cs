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
    public class ProdutosController : Controller
    {
        private readonly DataBaseContext _context;

        public ProdutosController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Produtos.Include(p => p.Categoria).Where(c => c.CategoriaId == 4);


            return View(await dataBaseContext.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            var dataBaseContext = _context.Produtos.Include(p => p.Categoria).Where(c => c.CategoriaId == 3);


            return View(await dataBaseContext.ToListAsync());
        }

        public async Task<IActionResult> Index3()
        {
            var dataBaseContext = _context.Produtos.Include(p => p.Categoria).Where(c => c.CategoriaId == 5);


            return View(await dataBaseContext.ToListAsync());
        }

        public async Task<IActionResult> Index4()
        {
            var dataBaseContext = _context.Produtos.Include(p => p.Categoria).Where(c => c.CategoriaId == 1);


            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public async Task<IActionResult> CarrinhoCompras(int? id)
        {
            var produtos = new List<Produto>();

            var produto = await _context.Produtos.FindAsync(id);

            if (id == null)
            {
                return View("CarrinhoVazio");
            }
            else
            {
                produtos.Add(produto);
                
            }          
           
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriaId);

            return View(produtos);
            
        }

        public async Task<IActionResult> FechamentoCompra(int? id)
        {
            return View();
        }

        // GET: Produtos/Create
        //public IActionResult Create()
        //{
        //    ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId");
        //    return View();
        //}

        //// POST: Produtos/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProdutoId,NomeComum,NomeCientífico,Descricao,Preco,Foto,CategoriaId")] Produto produto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(produto);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriaId);
        //    return View(produto);
        //}

        //    // GET: Produtos/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var produto = await _context.Produtos.FindAsync(id);
        //        if (produto == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriaId);
        //        return View(produto);
        //    }

        //    // POST: Produtos/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,NomeComum,NomeCientífico,Descricao,Preco,Foto,CategoriaId")] Produto produto)
        //    {
        //        if (id != produto.ProdutoId)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(produto);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProdutoExists(produto.ProdutoId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriaId);
        //        return View(produto);
        //    }

        //    // GET: Produtos/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var produto = await _context.Produtos
        //            .Include(p => p.Categoria)
        //            .FirstOrDefaultAsync(m => m.ProdutoId == id);
        //        if (produto == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(produto);
        //    }

        //    // POST: Produtos/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var produto = await _context.Produtos.FindAsync(id);
        //        _context.Produtos.Remove(produto);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ProdutoExists(int id)
        //    {
        //        return _context.Produtos.Any(e => e.ProdutoId == id);
        //    }
    }
}
