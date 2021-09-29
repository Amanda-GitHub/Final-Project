using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Projeto_CLOUD_45_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Azure.Storage.Blobs;


namespace WebApp_BackOffice.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Produto> produtos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Produtos");

                //HTTP GET
                var responseTask = client.GetAsync("produtos");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    produtos = JsonConvert.DeserializeObject<IList<Produto>>(readTask.Result);
                }
                else
                {
                    produtos = Enumerable.Empty<Produto>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contacte o Suporte.");
                }
            }


            return View(produtos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            IEnumerable<Categoria> categorias = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Categorias");

                var responseTask = client.GetAsync("Categorias");
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

                ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(Produto produtos)
        {
            if (produtos == null)
            {
                return BadRequest();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Produtos");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Produto>("produtos", produtos);
                postTask.Wait();
                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Erro no servidor. Contacte o Suporte.");


            return View(produtos);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            IEnumerable<Categoria> categorias = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Categorias");

                var responseTask = client.GetAsync("Categorias");
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

                ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            }


            Produto produto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("produtos/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    produto = JsonConvert.DeserializeObject<Produto>(readTask.Result);

                }

                ViewData["Categoria"] = new SelectList(categorias, "Nome", "Nome", produto.Categoria);
                return View(produto);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, Produto produto)
        {
            if (produto == null)
            {
                return BadRequest();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<Produto>("produtos/" + id.ToString(), produto);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(produto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Produto produto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("produtos/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(produto);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Produto produto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP GET
                var responseTask = client.GetAsync("produtos/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    produto = JsonConvert.DeserializeObject<Produto>(readTask.Result);

                }
            }

            return View(produto);
        }

       


    }
}
