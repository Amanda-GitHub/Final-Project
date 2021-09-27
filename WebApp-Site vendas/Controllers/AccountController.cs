using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Projeto_CLOUD_45_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Site_vendas.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataBaseContext _context;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utilizador model)
        {
            if (ModelState.IsValid)
            {
                
                    Utilizador user = _context.Utilizadores
                                       .Where(u => u.Email == model.Email && u.Password == model.Password)
                                       .FirstOrDefault();

                    if (user != null)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nome de utilizador ou Password inválida!");
                        return View(model);
                    }
                
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {            
            return RedirectToAction("Index", "Home");
        }
    }
}
