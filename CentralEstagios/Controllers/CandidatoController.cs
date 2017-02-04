using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly Repositorio<Candidato> dbCandidato = new Repositorio<Candidato>();
        private readonly Repositorio<Perfil> dbPerfil = new Repositorio<Perfil>();
        private readonly Repositorio<Vaga> dbVaga = new Repositorio<Vaga>();

        //
        // GET: /Candidato/

        public ActionResult Index(int vagaId)
        {
            List<Candidato> candidatos = new List<Candidato>();

            foreach (var item in dbCandidato.Obter())
            {
                if (item.VagaId == vagaId)
                {
                    item.Vaga = dbVaga.Obter().Where(p => p.Id == item.VagaId).FirstOrDefault();
                    item.Perfil = dbPerfil.Obter().Where(p => p.LoginRm == item.PerfilRm).FirstOrDefault();

                    candidatos.Add(item);
                }
            }

            if (Session["usuario"] != null)
            {
                return View(candidatos);
            }
            else
            {
                return Redirect("~/Home");
            }
        }

        //
        // GET: /Candidato/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Candidato/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Candidato/Create

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
        // GET: /Candidato/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Candidato/Edit/5

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
        // GET: /Candidato/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Candidato/Delete/5

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
