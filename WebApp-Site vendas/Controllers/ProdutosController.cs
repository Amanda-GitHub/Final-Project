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
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCarrinho(int? id)
        {
            var itens = _context.Produtos.Include(c => c.Categoria).FirstOrDefault(m => m.ProdutoId == id);
            var utilizador = _context.Utilizadores.FirstOrDefault(e => e.Email == User.Identity.Name);

            CarrinhoCompras cart = new CarrinhoCompras();           
            cart.CarrinhoId = Guid.NewGuid();
            cart.ProdutoId = itens.ProdutoId;
            cart.ClienteId = utilizador.UtilizadorId;
            cart.Foto = itens.Foto;
            cart.Quantidade = 1;
            cart.ValorTotal = itens.Preco;
            cart.PrecoUnit = itens.Preco;
            cart.Nome = itens.NomeComum;

            _carrinhocontext.Add(cart);
            _carrinhocontext.SaveChanges();


            var itensUtilizador = _carrinhocontext.Carrinho.Where(c => c.ClienteId == utilizador.UtilizadorId);

            ViewBag.CarrinhoTotal = itensUtilizador.Sum(s => s.PrecoUnit).ToString();

            return View(itensUtilizador.ToList());

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Encomendar()
        {
            var utilizador = _context.Utilizadores.FirstOrDefault(e => e.Email == User.Identity.Name);

            var carrinho = _carrinhocontext.Carrinho.Where(c => c.ClienteId == utilizador.UtilizadorId).FirstOrDefault();


            Encomenda novaEnc = new Encomenda();
            novaEnc.DataEncomenda = DateTime.Now;
            novaEnc.ProdutoId = carrinho.ProdutoId;
            novaEnc.Quantidade = carrinho.Quantidade;
            novaEnc.ValorTotal = carrinho.ValorTotal;
            novaEnc.UtilizadorId = carrinho.ClienteId;

            _context.Add(novaEnc);
            _context.SaveChanges();

            
            ////envio da Queue na concusão da encomenda 
            ///
            //QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net", "amandaqueue");
            //string mensagem = novaEnc.EncomendaId.ToString();
            //string mensagem1 = novaEnc.Utilizador.Nome;

            //queueClient.SendMessage("Encomenda nº: " + mensagem + " - Cliente: " + mensagem1);

            return RedirectToAction("ConfirmacaoEncomenda", "Encomendas", novaEnc);
        }

    }
}
