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
    public partial class Curriculo : System.Web.UI.Page
    {
        private readonly Repositorio<PerfilConhecimento> dbConhecimentoPerfil = new Repositorio<PerfilConhecimento>();
        private readonly Repositorio<Conhecimento> dbConhecimento = new Repositorio<Conhecimento>();
        private readonly Repositorio<ConhecimentoCurso> dbConhecimentoCurso = new Repositorio<ConhecimentoCurso>();
        private readonly Repositorio<Models.Perfil> dbPerfil = new Repositorio<Models.Perfil>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null && Request.QueryString["acao"] != null)
            {
                String rm = Request.QueryString["rm"];
                String acao = Request.QueryString["acao"];

                if (acao.Equals("listar"))
                {
                    ConhecimentoList conhecimento = new ConhecimentoList();
                    List<int> conhecimentoCurso;
                    List<int> conhecimentoPerfil;
                    conhecimento.conhecimentosPerfil = new List<Conhecimento>();
                    conhecimento.conhecimentosCurso = new List<Conhecimento>();

                    Models.Perfil perfil = dbPerfil.Obter().Where(p => p.LoginRm == rm).FirstOrDefault();
                    int curso = perfil.CursoId;

                    conhecimentoCurso = dbConhecimentoCurso.Obter().Where(p => p.CursoId == curso ).Select(x => x.ConhecimentoId).ToList();
                    conhecimentoPerfil = dbConhecimentoPerfil.Obter().OrderBy(x => x.Conhecimento.Descricao).Where(p => p.PerfilRm == rm).Select(x => x.ConhecimentoId).ToList();

                    foreach (int c in conhecimentoCurso)
                    {
                        conhecimento.conhecimentosCurso.Add(dbConhecimento.Obter().Where(p => p.Id == c).FirstOrDefault());
                    }

                    foreach (int c in conhecimentoPerfil)
                    {
                        conhecimento.conhecimentosPerfil.Add(dbConhecimento.Obter().Where(p => p.Id == c).FirstOrDefault());
                    }
                    

                    List<object> listaJson = new List<object>();

                    listaJson.Add(conhecimento);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strJson = js.Serialize(listaJson);
                    Response.Write(strJson);
                }else if(acao.Equals("editar")){

                    if (Request.QueryString["conhecimentos"]!=null)
                    {
                        try
                        {
                            
                            foreach (PerfilConhecimento c in dbConhecimentoPerfil.Obter().Where(p => p.PerfilRm == rm).ToList())
                            {
                                dbConhecimentoPerfil.Excluir(c);
                            }

                            String[] vet = Request.QueryString["conhecimentos"].Split(',');
                            List<Conhecimento> lista = new List<Conhecimento>();
                            int i;

                            foreach (String s in vet)
                            {
                                i = Int32.Parse(s);
                                lista.Add(dbConhecimento.Obter().Where(p => p.Id == i).FirstOrDefault());
                            }

                            foreach (Conhecimento c in lista)
                            {
                                PerfilConhecimento pc = new PerfilConhecimento();
                                pc.ConhecimentoId = c.Id;
                                pc.PerfilRm = rm;
                                dbConhecimentoPerfil.Criar(pc);
                            }
                                                  
                            
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string strJson = js.Serialize(new Resposta { resposta = "ok" });
                            Response.Write(strJson);
                        }
                        catch (Exception ex)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string strJson = js.Serialize(new Resposta { resposta = "erro" });
                            Response.Write(strJson);
                        }
                    }


                }
            }
        }

        public class ConhecimentoList
        {
            public List<Conhecimento> conhecimentosCurso;
            public List<Conhecimento> conhecimentosPerfil;
        }

        public class Resposta
        {
            public string resposta { get; set; }
        }
    }

    
}