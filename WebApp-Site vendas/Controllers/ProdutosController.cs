using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Projeto_CLOUD_45_2021.Models;
using WebApp_Site_vendas.Models;
using Newtonsoft.Json;
using WebApp_Site_vendas.Data;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp_Site_vendas.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly DataBaseCarrinhoContext _carrinhocontext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public ProdutosController(DataBaseContext context, DataBaseCarrinhoContext carrinhoContext, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _carrinhocontext = carrinhoContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Produtos

        public async Task<IActionResult> Index(int? id)
        {
            var dataBaseContext = _context.Produtos.Include(p => p.Categoria).Where(c => c.CategoriaId == id);
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

        [HttpPost]
        public async Task<IActionResult> AddCarrinho(int? id)
        {
            var itens = _context.Produtos.Include(c => c.Categoria).FirstOrDefault(m => m.ProdutoId == id);

            
            CarrinhoCompras cart = new CarrinhoCompras();           
            cart.CarrinhoId = Guid.NewGuid();
            cart.ProdutoId = itens.ProdutoId;
            cart.Quantidade = 1;
            cart.ValorTotal = 1;
            cart.PrecoUnit = 1;
            cart.Nome = itens.NomeComum;

            _carrinhocontext.Add(cart);
            _carrinhocontext.SaveChanges();


            return View(_carrinhocontext.Carrinho.ToList());

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCarrinho2(int? id)
        {
           
            var produtos = _context.Produtos.Include(c => c.Categoria).FirstOrDefault(p => p.ProdutoId == id);


            var utilizador = _context.Utilizadores.FirstOrDefault(e => e.Email == User.Identity.Name);

            Encomenda novaEnc = new Encomenda();
            novaEnc.DataEncomenda = DateTime.Now;
            novaEnc.ProdutoId = produtos.ProdutoId;
            novaEnc.Quantidade = 1;
            novaEnc.ValorTotal = 1;
            novaEnc.UtilizadorId = utilizador.UtilizadorId;

            _context.Add(novaEnc);
            _context.SaveChanges();


            return View(_context.Encomendas.ToList());
        }



        [HttpPost]
        public async Task<IActionResult> FechamentoCompra(Guid id)
        {
            var cart = _carrinhocontext.Carrinho.FirstOrDefault(c => c.CarrinhoId == id);

            var produto = _context.Produtos.Include(c => c.Categoria).Where(p => p.ProdutoId.Equals(cart.ProdutoId));
            
            return View("CarrinhoCompras", await produto.ToListAsync());
        }

        
    }
}
