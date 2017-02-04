using CentralEstagios.Models;
using CentralEstagios.Models.Administracao;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Controllers
{
    public class VagaController : Controller
    {
        private readonly Repositorio<Vaga> dbVagas = new Repositorio<Vaga>();
        private readonly Repositorio<Beneficio> dbBeneficios = new Repositorio<Beneficio>();
        private readonly Repositorio<Curso> dbCurso = new Repositorio<Curso>();
        private readonly Repositorio<Conhecimento> dbConhecimento = new Repositorio<Conhecimento>();
        private readonly Repositorio<VagaConhecimento> dbVagaConhecimento = new Repositorio<VagaConhecimento>();
        private readonly Repositorio<VagaCurso> dbVagaCurso = new Repositorio<VagaCurso>();
        private readonly Repositorio<Notificacoes> dbNotificacao = new Repositorio<Notificacoes>();
        private readonly Repositorio<Perfil> dbPerfil = new Repositorio<Perfil>();
        private readonly Repositorio<PerfilNotificacao> dbPerfilNotificacao = new Repositorio<PerfilNotificacao>();

        //
        // GET: /Vaga/

        public ActionResult Index()
        {
            if (Session["usuario"] != null)
            {
                return View(obterListaVagas());
            }
            else
            {
                return Redirect("~/Home");
            }
        }

        //
        // GET: /Vaga/Details/5

        public ActionResult Details(int id)
        {
            //return View(obterVagaPorId(id));
            return View(obterVagaCompletaPorId(id));
        }

        //
        // GET: /Vaga/Create

        public ActionResult Create()
        {
            return View(obterNovaVaga());
        }

        //
        // POST: /Vaga/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VagaCompleta vagaCompleta)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    dbBeneficios.Criar(vagaCompleta.Vaga.Beneficio);

                    vagaCompleta.Vaga.DataCriacao = DateTime.Now;
                    vagaCompleta.Vaga.Status = 1;

                    dbVagas.Criar(vagaCompleta.Vaga);
                    
                    vagaCompleta.Vaga = dbVagas.Obter().OrderByDescending(p => p.Id).FirstOrDefault();

                    foreach (var item in vagaCompleta.VagaConhecimento.Conhecimentos)
                    {
                        VagaConhecimento auxiliar = new VagaConhecimento();
                        auxiliar.Conhecimento = new Conhecimento();

                        auxiliar.VagaId = vagaCompleta.Vaga.Id;

                        if (item.EstaSelecionado)
                        {
                            auxiliar.ConhecimentoId = item.Id;

                            dbVagaConhecimento.Criar(auxiliar);
                        }
                    }

                    foreach (var item in vagaCompleta.VagaCurso.Cursos)
                    {
                        VagaCurso auxiliar = new VagaCurso();
                        auxiliar.Curso = new Curso();

                        auxiliar.VagaId = vagaCompleta.Vaga.Id;

                        if (item.EstaSelecionado)
                        {
                            auxiliar.CursoId = item.Id;

                            dbVagaCurso.Criar(auxiliar);
                        }
                    }

                    Notificacoes notificacao = new Notificacoes();

                    notificacao.Titulo = "Nova vaga: " + vagaCompleta.Vaga.Descricao;
                    notificacao.Conteudo = "Clique aqui para ver esta vaga!";

                    dbNotificacao.Criar(notificacao);

                    notificacao = dbNotificacao.Obter().OrderByDescending(p => p.Id).FirstOrDefault();

                    foreach (var itemPerfil in dbPerfil.Obter())
                    {
                        foreach (var itemVagaCurso in dbVagaCurso.Obter())
                        {
                            if(itemVagaCurso.VagaId == vagaCompleta.Vaga.Id)
                            {
                                if (itemPerfil.CursoId == itemVagaCurso.CursoId)
                                {
                                    PerfilNotificacao perfilNotificacao = new PerfilNotificacao();

                                    perfilNotificacao.NotificacaoId = notificacao.Id;
                                    perfilNotificacao.PerfilId = itemPerfil.LoginRm;
                                    perfilNotificacao.Status = 1;

                                    dbPerfilNotificacao.Criar(perfilNotificacao);
                                }
                            }
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
        // GET: /Vaga/Edit/5

        public ActionResult Edit(int id)
        {
            return View(obterVagaPorId(id));
        }

        //
        // POST: /Vaga/Edit/5

        [HttpPost]
        public ActionResult Edit(Vaga vaga)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    dbVagas.Atualizar(vaga);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Vaga/Delete/5

        public ActionResult Delete(int id)
        {
            return View(obterVagaPorId(id));
        }

        //
        // POST: /Vaga/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Vaga vaga)
        {
            try
            {
                // TODO: Add delete logic here

                vaga = dbVagas.Obter().Where(p => p.Id == vaga.Id).FirstOrDefault();
                vaga.Status = 0;

                dbVagas.Atualizar(vaga);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Vaga obterVagaPorId(int id)
        {
            return dbVagas.Obter().Where(p => p.Id == id).FirstOrDefault();
        }

        private VagaCompleta obterVagaCompletaPorId(int id)
        {
            VagaCompleta vagaCompleta = obterNovaVaga();

            IList<Curso> cursos = dbCurso.Obter().ToList();
            IList<Conhecimento> conhecimentos = dbConhecimento.Obter().ToList();

            vagaCompleta.Vaga = dbVagas.Obter().Where(p => p.Id == id).FirstOrDefault();
            vagaCompleta.Vaga.Beneficio = dbBeneficios.Obter().Where(p => p.Id == vagaCompleta.Vaga.BeneficioId).FirstOrDefault();

            vagaCompleta.VagaConhecimento.Conhecimentos.Clear();
            IList<VagaConhecimento> vagaConhecimento = dbVagaConhecimento.Obter().ToList().Where(p => p.VagaId == id).ToList();

            foreach (var itemConhecimento in conhecimentos)
            {
                foreach (var objetoConhecimento in vagaConhecimento)
                {
                    if (itemConhecimento.Id == objetoConhecimento.ConhecimentoId)
                    {
                        vagaCompleta.VagaConhecimento.Conhecimentos.Add(itemConhecimento);
                    }
                }
            }
                
            vagaCompleta.VagaCurso.Cursos.Clear();
            IList<VagaCurso> vagaCurso = dbVagaCurso.Obter().ToList().Where(p => p.VagaId == id).ToList();

            foreach (var itemCurso in cursos)
            {
                foreach (var objetoCurso in vagaCurso)
                {
                    if (itemCurso.Id == objetoCurso.CursoId)
                    {
                        vagaCompleta.VagaCurso.Cursos.Add(itemCurso);
                    }
                }
            }

            return vagaCompleta;
        }

        private IList<Vaga> obterListaVagas()
        {
            var vagas = dbVagas.Obter().Where(p => p.Status != 0);
            List<Vaga> vagasBeneficios = new List<Vaga>();

            foreach (var item in vagas)
            {
                item.Beneficio = dbBeneficios.Obter().Where(p => p.Id == item.BeneficioId).FirstOrDefault();
                vagasBeneficios.Add(item);
            }

            return vagasBeneficios;
        }

        private VagaCompleta obterNovaVaga()
        {
            VagaCompleta vagaCompleta = new VagaCompleta();

            vagaCompleta.Vaga = new Vaga();
            vagaCompleta.Vaga.Beneficio = new Beneficio();

            vagaCompleta.VagaConhecimento = new VagaConhecimento();
            vagaCompleta.VagaConhecimento.Conhecimento = new Conhecimento();
            vagaCompleta.VagaConhecimento.Conhecimentos = dbConhecimento.Obter().Where(p => p.Status != 0).ToList();

            vagaCompleta.VagaCurso = new VagaCurso();
            vagaCompleta.VagaCurso.Curso = new Curso();
            vagaCompleta.VagaCurso.Cursos = dbCurso.Obter().Where(p => p.Status != 0).ToList();

            return vagaCompleta;
        }

        public void Dispose()
        {
            dbVagas.Dispose();
            dbConhecimento.Dispose();
            dbBeneficios.Dispose();
            dbCurso.Dispose();
            dbVagaConhecimento.Dispose();
            dbVagaCurso.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
