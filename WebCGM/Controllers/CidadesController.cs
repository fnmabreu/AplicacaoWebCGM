using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCGM.Models;

namespace WebCGM.Controllers
{
    public class CidadesController : Controller
    {
        private EntidadeCGM db = new EntidadeCGM();

        // GET: Cidades
        public ActionResult Index()
        {
            var cidades = db.Cidades;
            return View(cidades);
        }

        //Retorna a view para registar a cidade
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                db.Cidades.Add(cidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }
        /* Processamento do pedido GET
         * Este método recebe o id da Cidade a editar. Caso não receba este parâmetro, id=0, o que evita erro de execução.
         * Se o método Find() encontra a cidade, retorna o objecto cidade para o template vista.
         * Se não encontra o método, HttpNotFound() é invocado.
        */
        public ActionResult Editar(long id=0)
        {
            Cidade cidade = db.Cidades.Find(id);

            if (cidade == null)
            {
                return HttpNotFound();
            }

            return View(cidade);
        }

        [HttpPost]
        public ActionResult Editar(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        public string Remover(long id)
        {
            try
            {
                Cidade cidade = db.Cidades.Find(id);
                db.Cidades.Remove(cidade);
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