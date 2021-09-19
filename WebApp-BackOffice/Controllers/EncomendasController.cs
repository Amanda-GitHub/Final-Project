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
    public class EncomendasController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Encomenda> encomendas = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Encomendas");

                //HTTP GET
                var responseTask = client.GetAsync("encomendas");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    encomendas = JsonConvert.DeserializeObject<IList<Encomenda>>(readTask.Result);
                }
                else
                {
                    encomendas = Enumerable.Empty<Encomenda>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contacte o Suporte.");
                }
            }

            return View(encomendas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Encomenda encomenda)
        {
            if (encomenda == null)
            {
                return BadRequest();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Encomendas");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Encomenda>("encomendas", encomenda);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Erro no servidor.Contacte o Suporte.");

            return View(encomenda);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Encomenda encomenda = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("encomendas/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    encomenda = JsonConvert.DeserializeObject<Encomenda>(readTask.Result);

                }
                return View(encomenda);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Encomenda encomenda)
        {
            if (encomenda == null)
            {
                return BadRequest();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<Encomenda>("encomendas/" + id.ToString(), encomenda);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(encomenda);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Encomenda encomenda = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("encomendas/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(encomenda);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Encomenda encomenda = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("encomendas/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    encomenda = JsonConvert.DeserializeObject<Encomenda>(readTask.Result);

                }
            }

            return View(encomenda);

        }

    }
}
