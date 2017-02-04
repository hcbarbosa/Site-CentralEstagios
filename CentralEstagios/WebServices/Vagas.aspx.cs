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
    public partial class Vagas : System.Web.UI.Page
    {
        private readonly Repositorio<Vaga> dbVagas = new Repositorio<Vaga>();
        private readonly Repositorio<Models.Perfil> dbPerfil = new Repositorio<Models.Perfil>();
        private readonly Repositorio<VagaCurso> dbVagaCurso = new Repositorio<VagaCurso>();
        private readonly Repositorio<Beneficio> dbBeneficio = new Repositorio<Beneficio>();
        private readonly Repositorio<Candidato> dbCandidato = new Repositorio<Candidato>();
        private readonly Repositorio<Conhecimento> dbConhecimento = new Repositorio<Conhecimento>();
        private readonly Repositorio<VagaConhecimento> dbConhecimentoVaga = new Repositorio<VagaConhecimento>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rm"] != null)
            {
                String rm = Request.QueryString["rm"];

                Models.Perfil perfil = new Models.Perfil();

                perfil.CursoId = dbPerfil.Obter().Where(p => p.LoginRm == rm).FirstOrDefault().CursoId;

                List<Vaga> listaVaga = new List<Vaga>();
                List<Beneficio> listaBeneficio = new List<Beneficio>();
                List<Candidato> listaCandidato = new List<Candidato>();
                List<ConhecimentoHelper> listaConhecimento = new List<ConhecimentoHelper>();
                ConhecimentoHelper conhecimento = new ConhecimentoHelper();
                conhecimento.listaConhecimentos = new List<Conhecimento>();

                foreach (var vaga in dbVagas.Obter().Where(p => p.Status != 0))
                {
                    int vagaId = 0;
   
                    foreach (var vagaCurso in dbVagaCurso.Obter())
                    {
                        if (vagaCurso.CursoId == perfil.CursoId && vagaCurso.VagaId == vaga.Id)
                        {
                            vagaId = vagaCurso.VagaId;
                        }
                    }
                    if (vagaId != 0)
                    {

                        Vaga vagaCompleta = dbVagas.Obter().Where(p => p.Id == vagaId).FirstOrDefault();
                        vagaCompleta.DataString = vagaCompleta.DataCriacao.Value.Date.ToShortDateString();
                        listaVaga.Add(vagaCompleta);
                    }
                }

                foreach (var itemVaga in listaVaga)
                {
                    foreach (var itemBeneficio in dbBeneficio.Obter())
                    {
                        if (itemBeneficio.Id == itemVaga.BeneficioId)
                        {
                            listaBeneficio.Add(itemBeneficio);
                        }
                    }
                }

                foreach (var item in dbCandidato.Obter())
                {
                    if (item.PerfilRm == rm)
                    {
                        listaCandidato.Add(item);
                    }
                }

                //List<Vaga> vagasCandidatadas = new List<Vaga>();

                //foreach (var itemCandidato in dbCandidato.Obter())
                //{
                //    foreach (var itemVaga in dbVagas.Obter())
                //    {
                //        if (itemVaga.Id == itemCandidato.VagaId && itemCandidato.PerfilRm == rm)
                //        {
                //            vagasCandidatadas.Add(itemVaga);
                //        }
                //    }
                //}

                //foreach (var itemVagasCandidatadas in vagasCandidatadas)
                //{
                //    conhecimento = new ConhecimentoHelper();
                //    conhecimento.listaConhecimentos = new List<Conhecimento>();

                //    conhecimento.Id = itemVagasCandidatadas.Id;

                //    foreach (var itemConhecimentoVaga in dbConhecimentoVaga.Obter())
                //    {
                //        foreach (var item in dbConhecimento.Obter())
                //        {
                //            if (itemConhecimentoVaga.ConhecimentoId == item.Id && itemConhecimentoVaga.VagaId == itemVagasCandidatadas.Id)
                //            {
                //                conhecimento.listaConhecimentos.Add(item);
                //            }
                //        }
                //    }

                //    listaConhecimento.Add(conhecimento);
                //}


                foreach (var itemVagas in listaVaga)
                {
                    conhecimento = new ConhecimentoHelper();
                    conhecimento.listaConhecimentos = new List<Conhecimento>();

                    conhecimento.Id = itemVagas.Id;

                    foreach (var itemConhecimentoVaga in dbConhecimentoVaga.Obter())
                    {
                        foreach (var item in dbConhecimento.Obter())
                        {
                            if (itemConhecimentoVaga.ConhecimentoId == item.Id && itemConhecimentoVaga.VagaId == itemVagas.Id)
                            {
                                conhecimento.listaConhecimentos.Add(item);
                            }
                        }
                    }

                    listaConhecimento.Add(conhecimento);
                }




                List<object> listaJson = new List<object>();

                listaJson.Add(listaVaga);
                listaJson.Add(listaBeneficio);
                listaJson.Add(listaCandidato);
                listaJson.Add(listaConhecimento);

                List<Vaga> list = new List<Vaga>();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string strJson = js.Serialize(listaJson);
                Response.Write(strJson);
            }
        }
    }

    public class ConhecimentoHelper
    {
        public int Id { get; set; }
        public List<Conhecimento> listaConhecimentos;
    }
}