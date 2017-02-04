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
    public partial class Perfil : System.Web.UI.Page
    {
        Repositorio<Models.Perfil> dbPerfil = new Repositorio<Models.Perfil>();
        Repositorio<Models.PerfilConhecimento> dbPerfilConhecimento = new Repositorio<Models.PerfilConhecimento>();
        Repositorio<Models.Conhecimento> dbConhecimento = new Repositorio<Models.Conhecimento>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null && Request.QueryString["acao"] != null)
            {
                string rm = Request.QueryString["rm"];

                if (Request.QueryString["acao"] == "obter")
                {
                    Models.Perfil perfil = new Models.Perfil();

                    perfil = dbPerfil.Obter().Where(p => p.LoginRm == rm).FirstOrDefault();

                    List<Models.Conhecimento> listaConhecimento = new List<Models.Conhecimento>();

                    foreach (var item in dbPerfilConhecimento.Obter())
                    {
                        if (item.PerfilRm == rm)
                        {
                            listaConhecimento.Add(dbConhecimento.Obter().Where(p => p.Id == item.ConhecimentoId).FirstOrDefault());
                        }
                    }
                    
                    List<object> resposta = new List<object>();

                    resposta.Add(perfil);
                    resposta.Add(listaConhecimento);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(resposta);
                    Response.Write(strJson);
                }

                if (Request.QueryString["acao"] == "atualizar")
                {
                    if (Request.QueryString["curso"] != null && Request.QueryString["telefone"] != null && Request.QueryString["cep"] != null &&
                        Request.QueryString["uf"] != null && Request.QueryString["cidade"] != null && Request.QueryString["bairro"] != null &&
                        Request.QueryString["logradouro"] != null && Request.QueryString["complemento"] != null && Request.QueryString["ano"] != null &&
                        Request.QueryString["semestre"] != null && Request.QueryString["nome"] != null && Request.QueryString["email"] != null)
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

                        try
                        {
                            dbPerfil.Atualizar(perfil);

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
    }

    public class Resposta
    {
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
    }
}