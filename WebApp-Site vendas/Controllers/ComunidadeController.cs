using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET: ComunidadeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComunidadeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComunidadeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComunidadeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComunidadeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComunidadeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComunidadeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
