using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Projeto_CLOUD_45_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp_BackOffice.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Categoria> categorias = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Categorias");

                //HTTP GET
                var responseTask = client.GetAsync("categorias");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    categorias = JsonConvert.DeserializeObject<IList<Categoria>>(readTask.Result);
                }
                else
                {
                    categorias = Enumerable.Empty<Categoria>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contacte o Suporte.");
                }
            }

            return View(categorias);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Categorias");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Categoria>("categorias", categoria);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Erro no servidor.Contacte o Suporte.");

            return View(categoria);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Categoria categoria = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("categorias/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    categoria = JsonConvert.DeserializeObject<Categoria>(readTask.Result);

                }
                return View(categoria);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<Categoria>("categorias/" + id.ToString(), categoria);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(categoria);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Categoria categoria = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("categorias/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(categoria);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Categoria categoria = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("categorias/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    categoria = JsonConvert.DeserializeObject<Categoria>(readTask.Result);

                }
            }

            return View(categoria);
        }

    }
}
