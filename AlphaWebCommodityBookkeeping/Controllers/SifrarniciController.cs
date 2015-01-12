using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlphaWebCommodityBookkeeping.Controllers
{
    public class SifrarniciController : Controller
    {
        //
        // GET: /Sifrarnici/
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Sifrarnici/Details/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Tvrtka()
        {
            return View();
        }

        //
        // GET: /Sifrarnici/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Sifrarnici/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Sifrarnici/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Sifrarnici/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Sifrarnici/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Sifrarnici/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
