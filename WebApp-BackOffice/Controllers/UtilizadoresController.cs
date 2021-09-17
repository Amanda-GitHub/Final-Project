using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Projeto_CLOUD_45_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace WebApp_BackOffice.Controllers
{
    public class UtilizadoresController : Controller
    {

        public IActionResult Index()
        {
            IEnumerable<Utilizador> utilizadores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/Utilizadores");

                //HTTP GET
                var responseTask = client.GetAsync("utilizadores");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    utilizadores = JsonConvert.DeserializeObject<IList<Utilizador>>(readTask.Result);
                }
                else
                {
                    utilizadores = Enumerable.Empty<Utilizador>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contacte o Suporte.");
                }
            }

            return View(utilizadores);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Utilizador utilizador)
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
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Erro no servidor.Contacte o Suporte.");

            return View(utilizador);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
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

        [HttpPost]
        public ActionResult Edit(int id, Utilizador utilizador)
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
                    return RedirectToAction("Index");
                }
            }

            return View(utilizador);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Utilizador utilizador = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("utilizadores/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(utilizador);
        }

        public ActionResult Details(int? id)
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
            }

            return View(utilizador);

        }

        //public IActionResult Search(string nome)
        //{
        //    IEnumerable<Utilizador> utilizadores = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44365/api/");

        //        //HTTP GET
        //        var responseTask = client.GetAsync("utilizadores?nome=" + nome);
        //        responseTask.Wait();
        //        var result = responseTask.Result;
                
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            readTask.Wait();

        //            utilizadores = JsonConvert.DeserializeObject<IList<Utilizador>>(readTask.Result);
        //        }
        //        else
        //        {
        //            utilizadores = Enumerable.Empty<Utilizador>();
        //            ModelState.AddModelError(string.Empty, "Erro no servidor. Contacte o Suporte.");
        //        }

        //    }
        //    return View(utilizadores);
        //}


    }
}
