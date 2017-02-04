using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentralEstagios.WebServices
{
    public partial class PrimeiroAcesso : System.Web.UI.Page
    {
        Repositorio<Models.Perfil> dbPerfil = new Repositorio<Models.Perfil>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["curso"] != null && Request.QueryString["telefone"] != null && Request.QueryString["cep"] != null &&
                Request.QueryString["uf"] != null && Request.QueryString["cidade"] != null && Request.QueryString["bairro"] != null &&
                Request.QueryString["logradouro"] != null && Request.QueryString["complemento"] != null && Request.QueryString["ano"] != null &&
                Request.QueryString["semestre"] != null && Request.QueryString["nome"] != null && Request.QueryString["email"] != null &&
                Request.QueryString["rm"] != null)
            {
                Models.Perfil perfil = new Models.Perfil();

                perfil.LoginRm = Request.QueryString["rm"];
                perfil.CursoId = int.Parse(Request.QueryString["curso"]);
                perfil.Telefone = Request.QueryString["telefone"];
                perfil.Cep = Request.QueryString["cep"];
                perfil.Uf = Request.QueryString["uf"];
                perfil.Cidade = Request.QueryString["cidade"];
                perfil.Bairro = Request.QueryString["bairro"];
                perfil.Logradouro = Request.QueryString["logradouro"];
                perfil.Complemento = Request.QueryString["complemento"];
                perfil.Ano = int.Parse(Request.QueryString["ano"]);
                perfil.Semestre = int.Parse(Request.QueryString["semestre"]);
                perfil.Nome = Request.QueryString["nome"];
                perfil.Email = Request.QueryString["email"];
                perfil.LoginRm = Request.QueryString["rm"];

                try
                {
                    dbPerfil.Criar(perfil);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(new Resposta { Titulo = "Resposta", Conteudo = "ok" });
                    Response.Write(strJson);
                }
                catch (Exception ex)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(new Resposta { Titulo = "Resposta", Conteudo = "erro" });
                    Response.Write(strJson);
                }
            }
        }
    }
}