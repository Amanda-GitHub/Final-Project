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
using Newtonsoft.Json;
using System.Text;

namespace WebApp_BackOffice.Controllers
{
    public class UtilizadoresTesteController : Controller
    {

        HttpClientHandler _clientHandler = new HttpClientHandler();

        List<Utilizador> _utilizadores = new List<Utilizador>();

        private Utilizador _utilizador;

        public UtilizadoresTesteController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]        
        public async Task<List<Utilizador>> GetUtilizadores()
        {
            _utilizadores = new List<Utilizador>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44365/api/Utilizadores"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _utilizadores = JsonConvert.DeserializeObject<List<Utilizador>>(apiResponse);

                }

            }

            return _utilizadores;

        }

        [HttpGet]        
        public async Task<Utilizador> GetUtilizadoresbyId(int utilizadorId)
        {
            _utilizador = new Utilizador();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44365/api/Utilizadores/" + utilizadorId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _utilizador = JsonConvert.DeserializeObject<Utilizador>(apiResponse);

                }

            }

            return _utilizador;

        }

        [HttpPost]
        public async Task<Utilizador> AtualizarUtilizador(Utilizador utilizador)
        {
            _utilizador = new Utilizador();

            using (var httpClient = new HttpClient(_clientHandler))
            { 
                StringContent content = new StringContent(JsonConvert.SerializeObject(utilizador), Encoding.UTF8, "application/json");
            
                using (var response = await httpClient.PostAsync("https://localhost:44365/api/Utilizadores", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _utilizador = JsonConvert.DeserializeObject<Utilizador>(apiResponse);

                }

            }

            return _utilizador;

        }


        [HttpDelete]
        public async Task<String> Delete(int utilizadorId)
        {
            string message = "";

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44365/api/Utilizadores/" + utilizadorId))
                {
                    message = await response.Content.ReadAsStringAsync();
                    

                }

            }

            return message;

        }
    }
}