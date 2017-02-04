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
    public partial class Mensagem : System.Web.UI.Page
    {

        private readonly Repositorio<Vaga> dbVagas = new Repositorio<Vaga>();
        private readonly Repositorio<Observacao> dbMensagens = new Repositorio<Observacao>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null && Request.QueryString["acao"] != null)
            {
                String rm = Request.QueryString["rm"];
                String acao = Request.QueryString["acao"];

                if (acao.Equals("listarRooms"))
                {

                    AuxListaVaga auxiliar = new AuxListaVaga();
                    auxiliar.listaVaga = new List<Vaga>();
                    auxiliar.listaQtd = new List<Qtd>();


                    foreach (int m in dbMensagens.Obter().Where(p => p.DonoMsg == rm).Select(x => x.VagaId).ToList())
                    {
                        Vaga v = dbVagas.Obter().Where(p => p.Id == m).FirstOrDefault();
                        if (!auxiliar.listaVaga.Contains(v))
                        {
                            v.DataCriacao = null;
                            auxiliar.listaVaga.Add(v);
                        }
                    }


                    foreach (Vaga v in auxiliar.listaVaga)
                    {
                        List<Observacao> listMensagens = dbMensagens.Obter().Where(p => p.VagaId == v.Id).ToList();
                        if (listMensagens != null)
                        {
                            Qtd qtdd = new Qtd();
                            foreach (Observacao msg in listMensagens)
                            {
                                //nao lido = 1
                                //lido = 0
                                if (msg.AlunoId == rm && msg.DonoMsg != rm && msg.Status == 1)
                                {                                    
                                    qtdd.idVaga = msg.VagaId;
                                    qtdd.qtd = qtdd.qtd + 1;
                                    
                                }
                            }
                            if (qtdd.qtd != 0)
                            {
                                auxiliar.listaQtd.Add(qtdd);
                            }
                        }
                    }


                    List<object> listaJson = new List<object>();
                    listaJson.Add(auxiliar);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(listaJson);
                    Response.Write(strJson);

                }
                else if (acao.Equals("listarChat") && Request.QueryString["vaga"] != null)
                {
                    int vaga = Int32.Parse(Request.QueryString["vaga"]);
                   
                        List<Observacao> listMensagens = dbMensagens.Obter().OrderBy(x => x.Id).Where(p => p.VagaId == vaga && p.AlunoId == rm).ToList();
                        if (listMensagens != null)
                        {
                           
                            foreach (Observacao msg in listMensagens)
                            {
                                //nao lido = 1
                                //lido = 0
                                if (msg.AlunoId == rm && msg.DonoMsg != rm && msg.Status == 1)
                                {
                                    Observacao m = dbMensagens.Obter().Where(p => p.Id == msg.Id).FirstOrDefault();
                                    m.Status = 0;
                                    dbMensagens.Atualizar(m);
                                }
                            }
                        }

                    //Devolver a lista de mensagens dentro do chat de tal vaga
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string strJson = js.Serialize(listMensagens);
                        Response.Write(strJson);
                   
                }
                else if (acao.Equals("enviarMsg"))
                {
                      
                     int VagaId = Int32.Parse(Request.QueryString["vaga"]);;
                     String AlunoId = Request.QueryString["rm"]; ;
                     String AdmId = null ;
                     String DonoMsg = Request.QueryString["rm"]; ;
                     String Descricao = Request.QueryString["msg"]; ;
                     int Status = 1;

                     Observacao msg = new Observacao();
                     msg.VagaId = VagaId;
                     msg.AlunoId = AlunoId;
                     msg.AdmId = AdmId;
                     msg.DonoMsg = DonoMsg;
                     msg.Descricao = Descricao;
                     msg.Status = Status;

                     dbMensagens.Criar(msg);

                     JavaScriptSerializer js = new JavaScriptSerializer();
                     string strJson = js.Serialize(new Resposta { resposta = "ok" , idMsg = msg.Id});
                     Response.Write(strJson);

                }

            }
        }
              

        public class Resposta
        {
            public string resposta { get; set; }
            public int idMsg { get; set; }
        }

        public class AuxListaVaga
        {
            public List<Vaga> listaVaga;
            public List<Qtd> listaQtd;
        }

        public class Qtd
        {
            public int idVaga;
            public int qtd;
        }
    }
}