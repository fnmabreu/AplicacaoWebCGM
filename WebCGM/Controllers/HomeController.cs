using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCGM.Models;

namespace WebCGM.Controllers
{
    public class HomeController : Controller
    {
        private EntidadeCGM db = new EntidadeCGM();

        public ActionResult Index()
        {
            ViewBag.Especialidades = new SelectList(db.Especialidades, "IDEspecialidade", "Nome");
            ViewBag.Cidades = new SelectList(db.Cidades, "IDCidade", "Nome");

            return View();
        }

        public ActionResult Pesquisar(Pesquisa pesquisa)
        {
            var medicos = from m in db.Medicos
                          where m.IDCidade == pesquisa.IdCidade && m.IDEspecialidade == pesquisa.IdEspecialidade
                          select new ResultadoPesquisa { Nome = m.Nome, Crm = m.CRM };

            return Json(medicos, JsonRequestBehavior.AllowGet);
        }
    }
}