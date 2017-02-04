using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using CentralEstagios.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.WebServices
{
    public partial class teste : System.Web.UI.Page
    {

        private readonly Repositorio<Models.Login> dbl = new Repositorio<Models.Login>();
        private readonly Repositorio<Models.Perfil> dbp = new Repositorio<Models.Perfil>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null && Request.QueryString["senha"] != null)
            {
                String rm = Request.QueryString["rm"];
                String senha = Request.QueryString["senha"];
                //acesso ao banco

                /* acesso = status 0 excluido
                 * rm = nao existe rm igual
                 * senha = senha incorreta
                 * ok = ok
                 */

                Models.Login lg = new Models.Login();
                lg = dbl.Obter().Where(l => l.Rm == rm).FirstOrDefault();
                if (lg != null)
                {
                    if (lg.Status != 0)
                    {
                        if (lg.Senha == senha)
                        {
                            //pega dados do perfil e passa pro app

                            Models.Perfil perfil = new Models.Perfil();
                            perfil = dbp.Obter().Where(p => p.LoginRm == rm).FirstOrDefault();

                            if (perfil == null)
                            {
                                List<Teste> list = new List<Teste>();
                                list.Add(new Teste() { resposta = "requerido", rm = rm });
                                JavaScriptSerializer js = new JavaScriptSerializer();
                                string strJson = js.Serialize(list);
                                Response.Write(strJson);
                            }
                            else
                            {
                                List<Teste> list = new List<Teste>();
                                list.Add(new Teste() { resposta = "ok", nome = perfil.Nome, email = perfil.Email, rm = rm, img = "" });
                                JavaScriptSerializer js = new JavaScriptSerializer();
                                string strJson = js.Serialize(list);
                                Response.Write(strJson);
                            }
                        }
                        else
                        {
                            List<Teste> list = new List<Teste>();
                            list.Add(new Teste() { resposta = "senha" });
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string strJson = js.Serialize(list);
                            Response.Write(strJson);
                        }
                    }
                    else
                    {
                        List<Teste> list = new List<Teste>();
                        list.Add(new Teste() { resposta = "acesso" });
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string strJson = js.Serialize(list);
                        Response.Write(strJson);
                    }
                }
                else
                {
                    List<Teste> list = new List<Teste>();
                    list.Add(new Teste() { resposta = "rm" });
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(list);
                    Response.Write(strJson);
                }



            }
        }
    }

    public class Teste
    {
        public string resposta { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string img { get; set; }
        public string rm { get; set; }
    }
}