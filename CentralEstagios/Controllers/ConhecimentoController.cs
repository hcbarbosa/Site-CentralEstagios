using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Controllers
{
    public class ConhecimentoController : Controller
    {
        private readonly Repositorio<Conhecimento> dbConhecimento = new Repositorio<Conhecimento>();
        private readonly Repositorio<Curso> dbCurso = new Repositorio<Curso>();
        private readonly Repositorio<ConhecimentoCurso> dbConhecimentoCurso = new Repositorio<ConhecimentoCurso>();

        //
        // GET: /Conhecimento/

        public ActionResult Index()
        {
            if (Session["usuario"] != null)
            {
                List<Conhecimento> validos = new List<Conhecimento>();

                foreach (var item in dbConhecimento.Obter())
                {
                    if (item.Status == 1)
                    {
                        validos.Add(item);
                    }
                }

                return View(validos);
            }
            else
            {
                return Redirect("~/Home");
            }
        }

        //
        // GET: /Conhecimento/Details/5

        public ActionResult Details(int id)
        {
            ConhecimentoCurso conhecimentoCurso = obterInstancia();

            conhecimentoCurso.Conhecimento = obterConhecimentoPorId(id);
            conhecimentoCurso.Cursos.Clear();

            IList<Curso> cursos = dbCurso.Obter().ToList();
            IList<ConhecimentoCurso> conhecimentoCursos = dbConhecimentoCurso.Obter().ToList().Where(p => p.ConhecimentoId == id).ToList();

            foreach (var itemCurso in cursos)
            {
                foreach (var cursosObjeto in conhecimentoCursos)
                {
                    if (itemCurso.Id == cursosObjeto.CursoId)
                    {
                        //if (cursosObjeto.Conhecimento.Id == id)
                        //{
                            conhecimentoCurso.Cursos.Add(itemCurso);
                        //}
                    }
                }
            }

            return View(conhecimentoCurso);
        }

        //
        // GET: /Conhecimento/Create

        public ActionResult Create()
        {
            return View(obterInstancia());
        }

        //
        // POST: /Conhecimento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConhecimentoCurso conhecimentoCurso)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    conhecimentoCurso.Conhecimento.Status = 1;

                    dbConhecimento.Criar(conhecimentoCurso.Conhecimento);

                    conhecimentoCurso.Conhecimento = dbConhecimento.Obter().OrderByDescending(p => p.Id).FirstOrDefault();

                    foreach (var item in conhecimentoCurso.Cursos)
                    {
                        ConhecimentoCurso auxiliar = new ConhecimentoCurso();
                        auxiliar.Curso = new Curso();

                        auxiliar.ConhecimentoId = conhecimentoCurso.Conhecimento.Id;

                        if (item.EstaSelecionado)
                        {
                            auxiliar.CursoId = item.Id;

                            dbConhecimentoCurso.Criar(auxiliar);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Conhecimento/Edit/5

        public ActionResult Edit(int id)
        {
            ConhecimentoCurso conhecimentoCurso = obterInstancia();

            conhecimentoCurso.Conhecimento = obterConhecimentoPorId(id);
            conhecimentoCurso.Cursos.Clear();

            IList<Curso> cursos = dbCurso.Obter().ToList();
            IList<ConhecimentoCurso> conhecimentoCursos = dbConhecimentoCurso.Obter().ToList();

            foreach (var item in cursos)
            {
                foreach (var cursosObjeto in conhecimentoCursos)
                {
                    if (item.Id == cursosObjeto.CursoId)
                    {
                        item.EstaSelecionado = true;
                    }
                }

                conhecimentoCurso.Cursos.Add(item);
            }

            return View(conhecimentoCurso);
        }

        //
        // POST: /Conhecimento/Edit/5

        [HttpPost]
        public ActionResult Edit(ConhecimentoCurso conhecimentoCurso)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    dbConhecimento.Atualizar(conhecimentoCurso.Conhecimento);

                    IList<Curso> cursos = dbCurso.Obter().ToList();
                    IList<Curso> conhecimentoCursos = conhecimentoCurso.Cursos;

                    foreach (var item in cursos)
                    {
                        foreach (var cursosObjeto in conhecimentoCursos)
                        {
                            if (cursosObjeto.EstaSelecionado)
                            {

                            }
                        }
                    }

                    foreach (var item in conhecimentoCurso.Cursos)
                    {
                        ConhecimentoCurso auxiliar = new ConhecimentoCurso();
                        auxiliar.Curso = new Curso();

                        auxiliar.ConhecimentoId = conhecimentoCurso.Conhecimento.Id;
                        auxiliar.CursoId = item.Id;

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Conhecimento/Delete/5

        public ActionResult Delete(int id)
        {
            return View(obterConhecimentoPorId(id));
        }

        //
        // POST: /Conhecimento/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Conhecimento conhecimento)
        {
            try
            {
                // TODO: Add delete logic here

                conhecimento = dbConhecimento.Obter().Where(p => p.Id == conhecimento.Id).FirstOrDefault();

                conhecimento.Status = 0;

                dbConhecimento.Atualizar(conhecimento);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /* ---------- Métodos de apoio ----------*/

        /// <summary>
        /// Retorna o conhecimento de acordo com o id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Conhecimento obterConhecimentoPorId(int id)
        {
            return dbConhecimento.Obter().Where(p => p.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Obtem uma instancia completa de conhecimentoCurso
        /// </summary>
        /// <returns></returns>
        private ConhecimentoCurso obterInstancia()
        {
            ConhecimentoCurso instancia = new ConhecimentoCurso();

            instancia.Cursos = dbCurso.Obter().ToList();

            instancia.Conhecimento = new Conhecimento();
            instancia.Curso = new Curso();

            return instancia;
        }

        /// <summary>
        /// Exclui em cascata conhcimentoCurso e conhecimento
        /// </summary>
        /// <param name="conhecimentoId"></param>
        private void excluirEmCascata(int conhecimentoId)
        {
            IList<ConhecimentoCurso> query = dbConhecimentoCurso.Obter().Where(p => p.ConhecimentoId == conhecimentoId).ToList();

            foreach (var item in query)
            {
                dbConhecimentoCurso.Excluir(item);
            }

            dbConhecimento.Excluir(obterConhecimentoPorId(conhecimentoId));
        }

        /// <summary>
        /// Dispensa todos os "bancos" e passa o GarbageCollector
        /// </summary>
        public void Dispose()
        {
            dbConhecimento.Dispose();
            dbConhecimentoCurso.Dispose();
            dbCurso.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
