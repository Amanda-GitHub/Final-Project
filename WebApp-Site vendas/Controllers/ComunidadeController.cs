using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Site_vendas.Controllers
{
    public class ComunidadeController : Controller
    {
        // GET: ComunidadeController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Desafio(string nome)
        {
            string nomeplanta = "verbena";

            if (nome == nomeplanta)
            {
                return View("Acertou");
            }
            else
            {
                return View("Errou");
            }
           
        }


        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net");

            string containername = "amandaimages";

            var blobContainers = blobServiceClient.GetBlobContainers();

            var s = from bc in blobContainers where bc.Name.Equals(containername) select bc;

            BlobContainerClient blobContainerClient = null;

            if (s != null)
            {
                blobContainerClient = blobServiceClient.GetBlobContainerClient(containername);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Impossível fazer upload no momento!");
            }

            var filename = Path.GetFileName(file.FileName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(filename);
            FileStream filestream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            blobClient.Upload(filestream, true);
            filestream.Flush();

            var url = blobClient.Uri.AbsoluteUri;

            
            return View("Index");

        }
        
    }
}
