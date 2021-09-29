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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace WebApp_Site_vendas.Controllers
{
    public class UtilizadoresController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UtilizadoresController(DataBaseContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

       
        public ActionResult Create()
        {
            return View();
        }

        
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

                    var userIdentity = new IdentityUser
                    {
                        UserName = utilizador.Email,
                        Email = utilizador.Email
                    };
                    var result = await userManager.CreateAsync(userIdentity, utilizador.Password);                    

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(userIdentity, isPersistent: false);
                        return RedirectToAction("MinhaConta", utilizador);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Dados inválidos!");
                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Email já registado, por favor registe um novo email!");
                }
            }

            return View(utilizador);
        }


        
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = _context.Utilizadores.FirstOrDefault(e => e.Email == User.Identity.Name);
            var userId = user.UtilizadorId;

            var utilizador = await _context.Utilizadores.FindAsync(userId);

            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        [Authorize]
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

                return RedirectToAction("MinhaConta", utilizador);
            }

            return View(utilizador);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
                    
        }

        [HttpPost]
        public async Task<IActionResult> Login(Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    utilizador.Email, utilizador.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("MinhaConta", utilizador);
                }
                ModelState.AddModelError(string.Empty, "Login Inválido");
            }
            return View(utilizador);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        
        [Authorize]
        public IActionResult MinhaConta(Utilizador utilizador)
        {
            var user = _context.Utilizadores.FirstOrDefault(e => e.Email == User.Identity.Name);

            return View(user);
        }

       
        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.UtilizadorId == id);
        }

    }
}
