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
        public async Task<IActionResult> MinhasEncomendas()
        {
            var utilizador = _context.Utilizadores.FirstOrDefault(e => e.Email == User.Identity.Name);
            var dataBaseContext = _context.Encomendas.Include(e => e.Produto).Include(e => e.Utilizador).Where(c => c.UtilizadorId == utilizador.UtilizadorId);
            return View(await dataBaseContext.ToListAsync());
        }


        [Authorize]
        public async Task<IActionResult> ConfirmacaoEncomenda()
        {
            return View();
        }

       
    }
}
