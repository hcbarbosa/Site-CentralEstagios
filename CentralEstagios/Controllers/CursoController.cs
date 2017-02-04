using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Controllers
{
    public class CursoController : Controller
    {
        private readonly Repositorio<Curso> db = new Repositorio<Curso>(); 

        //
        // GET: /Curso/

        public ActionResult Index()
        {
            if (Session["usuario"] != null)
            {
                return View(listarCursosValidos());
            }
            else
            {
                return Redirect("~/Home");
            }
            
        }

        //
        // GET: /Curso/Details/5

        public ActionResult Details(int id)
        {
            return View(obterCursoPorId(id));
        }

        //
        // GET: /Curso/Create

        public ActionResult Create()
        {
            return View(new Curso());
        }

        //
        // POST: /Curso/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Curso curso)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    curso.Status = 1;

                    db.Criar(curso);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Curso/Edit/5

        public ActionResult Edit(int id)
        {
            return View(obterCursoPorId(id));
        }

        //
        // POST: /Curso/Edit/5

        [HttpPost]
        public ActionResult Edit(Curso curso)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    db.Atualizar(curso);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Curso/Delete/5

        public ActionResult Delete(int id)
        {
            return View(obterCursoPorId(id));
        }

        //
        // POST: /Curso/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Curso curso)
        {
            try
            {
                // TODO: Add delete logic here

                curso = obterCursoPorId(curso.Id);
                curso.Status = 0;

                db.Atualizar(curso);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Curso obterCursoPorId(int id)
        {
            return db.Obter().Where(p => p.Id == id).FirstOrDefault();
        }

        private IEnumerable<Curso> listarCursosValidos()
        {
            return db.Obter().Where(p => p.Status == 1);
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
