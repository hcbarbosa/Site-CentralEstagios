using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Controllers
{
    public class LoginController : Controller
    {
        private readonly Repositorio<Login> db = new Repositorio<Login>(); 

        //
        // GET: /Login/

        public ActionResult Logout()
        {
            Session["usuario"] = null;

            return Redirect("~/Home");
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            string login = form["Login"];
            string senha = form["Password"];
            
            if (ModelState.IsValid)
            {
                if (verificarLogin(login, senha))
                {
                    return Redirect("~/Home");
                }
            }

            return View();
        }

        public ActionResult List()
        {
            return View(db.Obter());
        }

        //
        // GET: /Login/Details/5

        public ActionResult Details(string rm)
        {
            return View(obterLoginPorRm(rm));
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View(new Login());
        }

        //
        // POST: /Login/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login login)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    login.Status = 1;

                    db.Criar(login);
                }

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(string rm)
        {
            return View(obterLoginPorRm(rm));
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(Login login)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    login.Status = 1;

                    db.Atualizar(login);
                }

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Delete/5

        public ActionResult Delete(string rm)
        {
            return View(obterLoginPorRm(rm));
        }

        //
        // POST: /Login/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Login login)
        {
            try
            {
                // TODO: Add delete logic here

                login = obterLoginPorRm(login.Rm);
                login.Status = 0;
                login.ConfirmaSenha = login.Senha;

                db.Atualizar(login);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        private Login obterLoginPorRm(string rm)
        {
            return db.Obter().Where(p => p.Rm == rm).FirstOrDefault();
        }

        private IQueryable<Login> listarLoginsValidos()
        {
            return db.Obter().Where(p => p.Status == 1);
        }

        private bool verificarLogin(string login, string senha)
        {
            IList<Login> logins = db.Obter().ToList();

            bool retorno = false;

            foreach (var item in logins)
            {
                if (item.StatusAdm)
                {
                    if (login == item.Rm && senha == item.Senha)
                    {
                        Session["usuario"] = item;

                        retorno = true;

                        break;
                    }
                }
            }

            return retorno;
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
