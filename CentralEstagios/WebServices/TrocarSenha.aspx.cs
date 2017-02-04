using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentralEstagios.WebServices
{
    public partial class TrocarSenha : System.Web.UI.Page
    {
        public readonly Repositorio<Models.Login> dbLogin = new Repositorio<Models.Login>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["senha"] != null && Request.QueryString["rm"] != null)
            {
                string rm = Request.QueryString["rm"];
                string senha = Request.QueryString["senha"];

                Models.Login login = new Models.Login();

                login = dbLogin.Obter().Where(p => p.Rm == rm).FirstOrDefault();

                login.Senha = senha;
                login.ConfirmaSenha = senha;

                try
                {
                    dbLogin.Atualizar(login);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var valErrs in ex.EntityValidationErrors)
                    {
                        foreach (var valErr in valErrs.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",  
                    valErrs.Entry.Entity.ToString(),  
                    valErr.ErrorMessage); 
                        }
                    }
                }
            }
        }
    }
}