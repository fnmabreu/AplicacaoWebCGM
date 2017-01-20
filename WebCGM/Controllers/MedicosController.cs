using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCGM.Models;

namespace WebCGM.Controllers
{
    public class MedicosController : Controller
    {
        /*Criar uma instância do objeto EntidadeCGM 
        de modo a possibilitar a comunicação dos modelos com a base de dados da aplicação */
        private EntidadeCGM db = new EntidadeCGM();

        // GET: Medicos
        public ActionResult Index()
        {
            var medicos = db.Medicos.Include("Cidade").Include("Especialidade").ToList();
            return View(medicos);
        }

        //Retorna a view para registar o medico
        public ActionResult Adicionar()
        {
            //ViewBag - transferir dados do Controller para a View
            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome");
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Medico medico)
        {
            if (ModelState.IsValid) //Verifica atributos de validação criados no Model (MedicoMetadado)
            {
                db.Medicos.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);

            return View(medico);
        }

        /* Processamento do pedido GET
         * Este método recebe o id do Medico a editar. Caso não receba este parâmetro, id=0, o que evita erro de execução.
         * Se o método Find() encontra o medico, retorna o objecto Medico para o template vista.
         * Se não encontra o método, HttpNotFound() é invocado.
        */
        public ActionResult Editar(long id=0)
        {
            Medico medico = db.Medicos.Find(id);

            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);

            if (medico == null)
            {
                return HttpNotFound();
            }

            return View(medico);
        }

        /* Processamento do pedido POST 
         * O mecanismo Model Binder do framework ASP.NET recebe os valores do formulário enviados por POST, 
         * cria um objecto Medico, que é passado como parâmetro ao método de acção Edit. 
         * ModelState.IsValid verifica se todos os validadores tiveram sucesso. 
         * O objecto Medico já existe na base de dados mas sofreu modificações. 
         * O método Entry() retorna o objecto DBEntityEntry desta entidade, fornecendo acesso a informação sobre a entidade e à capacidade de alterar essa informação.
         * O estado do objecto é mudado para modificado. 
         * SaveChanges() guarda na base de dados todas as modificações feitas no contexto, executando o comando SQL Update para as entidades com a flag Modified.
         * Todas as colunas da correspondente linha da tabela da base de dados são actualizadas, incluindo as que o utilizador não modificou. 
         * Em seguida o controlo é redirigido para o método de acção Index(), que mostra uma listagem dos medicos já com este medico actualizado. 
         * Se a validação não teve sucesso o objecto medico que contém os valores recebidos do formulário é retorna para o template vista Edit.cshtml.
        */
        [HttpPost]
        public ActionResult Editar(Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified; //Estado do objeto é mudado para modificado
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);

            return View(medico);
        }

        [HttpPost]
        public string Remover(long id)
        {
            try
            {
                Medico medico = db.Medicos.Find(id);
                db.Medicos.Remove(medico);
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