using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Controllers
{
    public class ChatController : Controller
    {
        private readonly Repositorio<Perfil> dbPerfil = new Repositorio<Perfil>();
        private readonly Repositorio<Observacao> dbObservacao = new Repositorio<Observacao>();
        private readonly Repositorio<Login> dbLogin = new Repositorio<Login>();
        private readonly Repositorio<Vaga> dbVaga = new Repositorio<Vaga>();

        //
        // GET: /Chat/

        public ActionResult Index(int vagaId)
        {
            List<Observacao> perfis = new List<Observacao>();
            List<Perfil> repetidos = new List<Perfil>();

            foreach (var geral in dbObservacao.Obter())
            {
                geral.PerfilDono = dbPerfil.Obter().Where(p => p.LoginRm == geral.DonoMsg).FirstOrDefault();

                if (perfis.Count <= 0)
                {
                    if (geral.VagaId == vagaId)
                    {
                        perfis.Add(geral);
                        repetidos.Add(geral.PerfilDono);
                    }
                }
                else
                {
                    foreach (var especifico in perfis)
                    {
                        especifico.PerfilDono = dbPerfil.Obter().Where(p => p.LoginRm == especifico.DonoMsg).FirstOrDefault();

                        if (geral.DonoMsg != especifico.DonoMsg)
                        {
                            if (geral.VagaId == vagaId)
                            {
                                if(geral.PerfilDono != null)
                                {
                                    bool encontrou = false;

                                    foreach (var item in repetidos)
                                    {
                                        if (geral.PerfilDono.Nome == item.Nome)
                                        {
                                            encontrou = true;

                                            break;
                                        }
                                    }

                                    if (!encontrou)
                                    {
                                        perfis.Add(geral);
                                        repetidos.Add(geral.PerfilDono);

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Session["usuario"] != null)
            {
                return View(perfis);
            }
            else
            {
                return Redirect("~/Home");
            }
        }

        //
        // GET: /Chat/Details/5

        public ActionResult Details(string admId, string alunoId, string donoMsg, int vagaId)
        {
            List<Observacao> mensagens = new List<Observacao>();
            List<Observacao> query = dbObservacao.Obter().Where(p => p.VagaId == vagaId).ToList();

            foreach (var item in query)
            {
                Login adm = dbLogin.Obter().Where(p => p.Rm == item.DonoMsg).FirstOrDefault();

                if (adm.StatusAdm)
                {
                    if (item.AlunoId == alunoId && adm.StatusAdm)
                    {
                        item.PerfilDono = new Perfil();

                        item.PerfilDono.Ano = 0;
                        item.PerfilDono.Bairro = "";
                        item.PerfilDono.Cep = "";
                        item.PerfilDono.Cidade = "";
                        item.PerfilDono.Complemento = "";
                        item.PerfilDono.CursoId = 0;
                        item.PerfilDono.Email = "";
                        item.PerfilDono.LoginRm = "";
                        item.PerfilDono.Logradouro = "";
                        item.PerfilDono.Nome = "Central de Estágios";
                        item.PerfilDono.Semestre = 0;
                        item.PerfilDono.Telefone = "";
                        item.PerfilDono.Uf = "";

                        item.Vaga = dbVaga.Obter().Where(p => p.Id == vagaId).FirstOrDefault();

                        mensagens.Add(item);
                    }
                }
                else if (item.DonoMsg == alunoId)
                {
                    if (item.AdmId == string.Empty)
                    {
                        Observacao temp = item;

                        temp.AdmId = ((Login)Session["usuario"]).Rm;

                        dbObservacao.Atualizar(temp);
                    }

                    if (item.Status == 1)
                    {
                        Observacao temp = item;

                        temp.Status = 0;

                        dbObservacao.Atualizar(temp);
                    }

                    if (item.AlunoId == alunoId)
                    {
                        item.PerfilDono = dbPerfil.Obter().Where(p => p.LoginRm == donoMsg).FirstOrDefault();
                        item.Vaga = dbVaga.Obter().Where(p => p.Id == vagaId).FirstOrDefault();
                        item.PerfilAluno = dbPerfil.Obter().Where(p => p.LoginRm == alunoId).FirstOrDefault();

                        mensagens.Add(item);
                    }
                }
            }

            return View(mensagens);
        }

        [HttpPost]
        public ActionResult Details(string admId, string alunoId, string donoMsg, int vagaId, FormCollection form)
        {
            try
            {
                Observacao mensagem = new Observacao();

                mensagem.AdmId = admId;
                mensagem.AlunoId = alunoId;
                mensagem.DonoMsg = ((Login)Session["usuario"]).Rm;
                mensagem.VagaId = vagaId;
                mensagem.Status = 1;
                mensagem.Descricao = form["mensagem"];

                dbObservacao.Criar(mensagem);

                List<Observacao> mensagens = new List<Observacao>();
                List<Observacao> query = dbObservacao.Obter().Where(p => p.VagaId == vagaId).ToList();

                foreach (var item in query)
                {
                    Login adm = dbLogin.Obter().Where(p => p.Rm == item.DonoMsg).FirstOrDefault();

                    if (adm.StatusAdm)
                    {
                        if (item.AlunoId == alunoId && adm.StatusAdm)
                        {
                            item.PerfilDono = new Perfil();

                            item.PerfilDono.Ano = 0;
                            item.PerfilDono.Bairro = "";
                            item.PerfilDono.Cep = "";
                            item.PerfilDono.Cidade = "";
                            item.PerfilDono.Complemento = "";
                            item.PerfilDono.CursoId = 0;
                            item.PerfilDono.Email = "";
                            item.PerfilDono.LoginRm = "";
                            item.PerfilDono.Logradouro = "";
                            item.PerfilDono.Nome = "Central de Estágios";
                            item.PerfilDono.Semestre = 0;
                            item.PerfilDono.Telefone = "";
                            item.PerfilDono.Uf = "";

                            mensagens.Add(item);
                        }
                    }
                    else if (item.DonoMsg == alunoId)
                    {
                        if (item.AdmId == string.Empty)
                        {
                            Observacao temp = item;

                            temp.AdmId = ((Login)Session["usuario"]).Rm;

                            dbObservacao.Atualizar(temp);
                        }

                        if (item.Status == 1)
                        {
                            Observacao temp = item;

                            temp.Status = 0;

                            dbObservacao.Atualizar(temp);
                        }

                        if (item.AlunoId == alunoId)
                        {
                            item.PerfilDono = dbPerfil.Obter().Where(p => p.LoginRm == donoMsg).FirstOrDefault();
                            item.Vaga = dbVaga.Obter().Where(p => p.Id == vagaId).FirstOrDefault();
                            item.PerfilAluno = dbPerfil.Obter().Where(p => p.LoginRm == alunoId).FirstOrDefault();

                            mensagens.Add(item);
                        }
                    }
                }

                return View(mensagens);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        //
        // GET: /Chat/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Chat/Create

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
        // GET: /Chat/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Chat/Edit/5

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
        // GET: /Chat/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Chat/Delete/5

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
