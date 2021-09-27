using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Projeto_CLOUD_45_2021.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;


namespace WebApp_Site_vendas.Controllers
{
    public class UtilizadoresController : Controller
    {
        private readonly DataBaseContext _context;

        public UtilizadoresController(DataBaseContext context)
        {
            _context = context;
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilizadorId,Nome,Contribuinte,DataNascimento,Telefone,Email,DataRegisto,Password,Morada,Numero,Andar,Localidade,CodigoPostal")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Utilizadores.FirstOrDefault(e => e.Email == utilizador.Email);

                if (check == null)
                {
                    _context.Add(utilizador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("MinhaConta");
                }
                else
                {                    
                    return View(); //Tratar Erro
                }
            }

            return View(utilizador);
        }
            

        // GET: Utilizadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);

            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilizadorId,Nome,Contribuinte,DataNascimento,Telefone,Email,DataRegisto,Password,Morada,Numero,Andar,Localidade,CodigoPostal")] Utilizador utilizador)
        {
            if (id != utilizador.UtilizadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.UtilizadorId))
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

            return View(utilizador);
        }      
                

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email)
        {
            var utilizador = _context.Utilizadores.Where(e => e.Email == email).FirstOrDefault();

            if (utilizador == null)
            {
                return View(); //Tratar Erro
            }
            else
            {
                return View("MinhaConta", utilizador);

            }
        }

        public IActionResult MinhaConta(Utilizador utilizador)
        {
           
            return View();
        }


        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.UtilizadorId == id);
        }

    }
}
