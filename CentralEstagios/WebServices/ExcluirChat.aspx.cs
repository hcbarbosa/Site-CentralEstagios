using CentralEstagios.Models;
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
    public partial class ExcluirChat : System.Web.UI.Page
    {
        private readonly Repositorio<Observacao> dbObservacao = new Repositorio<Observacao>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null && Request.QueryString["vaga"] != null)
            {
                string rm = Request.QueryString["rm"];
                int vaga = Int32.Parse(Request.QueryString["vaga"]);

                List<Observacao> mensagensExcluidas = dbObservacao.Obter().Where(p => p.AlunoId == rm && p.VagaId == vaga).ToList();

                Resposta resposta = new Resposta();

                try
                {
                    foreach (var item in mensagensExcluidas)
                    {
                        dbObservacao.Excluir(item);
                    }

                    resposta.Titulo = "Resposta";
                    resposta.Conteudo = "ok";
                }
                catch (Exception ex)
                {
                    resposta.Titulo = "Resposta";
                    resposta.Conteudo = "erro";
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                string strJson = js.Serialize(resposta);
                Response.Write(strJson);
            }
        }
    }
}