using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCGM.Models;

namespace WebCGM.Controllers
{
    public class EspecialidadesController : Controller
    {
        private EntidadeCGM db = new EntidadeCGM();

        // GET: Especialidades
        public ActionResult Index()
        {
            var especialidades = db.Especialidades;
            return View(especialidades);
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Especialidades.Add(especialidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        public ActionResult Editar(long id = 0)
        {
            Especialidade especialidade = db.Especialidades.Find(id);

            if(especialidade == null)
            {
                return HttpNotFound();
            }

            return View(especialidade);
        }

        [HttpPost]
        public ActionResult Editar(Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        [HttpPost]
        public string Remover(long id)
        {
            try
            {
                Especialidade especialidade = db.Especialidades.Find(id);
                db.Especialidades.Remove(especialidade);
                db.SaveChanges();
                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
            }
        }
    }
}