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

        public IActionResult MinhaConta(int? id)
        {
            return View();
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
            if (utilizador == null)
            {
                return BadRequest();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Utilizadores");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Utilizador>("utilizadores", utilizador);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return View("MinhaConta");
                }
            }

            ModelState.AddModelError(string.Empty, "Erro no servidor.Contacte o Suporte.");

            return View(utilizador);
        }

        // GET: Utilizadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Utilizador utilizador = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("utilizadores/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    utilizador = JsonConvert.DeserializeObject<Utilizador>(readTask.Result);

                }
                return View(utilizador);
            }
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilizadorId,Nome,Contribuinte,DataNascimento,Telefone,Email,DataRegisto,Password,Morada,Numero,Andar,Localidade,CodigoPostal")] Utilizador utilizador)
        {
            if (utilizador == null)
            {
                return BadRequest();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<Utilizador>("utilizadores/" + id.ToString(), utilizador);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return View("MinhaContaAlterada");
                }
            }

            return View(utilizador);
        }      

        

        public async Task<IActionResult> Login()
        { 
            return View();
        }

    }
}
