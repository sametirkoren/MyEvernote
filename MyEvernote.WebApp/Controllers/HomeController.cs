using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            // CategoryController üzerinden gelen view talebi ve model...
            //if(TempData["mm"] != null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}
            NoteManager nm = new NoteManager();
            return View(nm.GetAllNote());
        }

        public ActionResult ByCategory(int? id)
        {
            if(id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);

            if(cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes);
        }
    }
}