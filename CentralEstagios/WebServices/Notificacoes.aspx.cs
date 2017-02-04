using CentralEstagios.Repositorio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentralEstagios.WebServices
{
    public partial class Notificacoes : System.Web.UI.Page
    {
        private readonly Repositorio<Models.Notificacoes> dbNotificacoes = new Repositorio<Models.Notificacoes>();
        private readonly Repositorio<Models.PerfilNotificacao> dbPerfilNotificacao = new Repositorio<Models.PerfilNotificacao>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null && Request.QueryString["acao"] != null)
            {
                string rm = Request.QueryString["rm"];
                NotificacaoResposta resposta = new NotificacaoResposta();

                if (Request.QueryString["acao"] == "listar")
                {
                    bool novaVaga = false;

                    foreach (var item in dbPerfilNotificacao.Obter())
                    {
                        if (item.PerfilId == rm && item.Status != 0)
                        {
                            novaVaga = true;

                            break;
                        }
                    }

                    if (novaVaga)
                    {
                        resposta.Titulo = "resposta";
                        resposta.Conteudo = "vaga";
                    }
                    else
                    {
                        resposta.Titulo = "resposta";
                        resposta.Conteudo = "nada";
                    }

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(resposta);
                    Response.Write(strJson);
                }

                if (Request.QueryString["acao"] == "atualizar")
                {
                    List<Models.PerfilNotificacao> listaNotificacoes = dbPerfilNotificacao.Obter().ToList();

                    foreach (var item in listaNotificacoes)
                    {
                        if (item.Status != 0 && item.PerfilId == rm)
                        {
                            item.Status = 0;

                            dbPerfilNotificacao.Atualizar(item);
                        }
                    }
                }
            }
        }
    }

    public class NotificacaoResposta
    {
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
    }
}