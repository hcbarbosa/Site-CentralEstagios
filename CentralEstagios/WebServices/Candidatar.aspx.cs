using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentralEstagios.WebServices
{
    public partial class Cadidatar : System.Web.UI.Page
    {
        private readonly Repositorio<Candidato> dbCandidato = new Repositorio<Candidato>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["rm"] != null && Request.QueryString["vaga"] != null)
            {
                Candidato candidato = new Candidato();

                string rm = Request.QueryString["rm"];
                int vaga = int.Parse(Request.QueryString["vaga"]);

                candidato.PerfilRm = rm;
                candidato.VagaId = vaga;

                dbCandidato.Criar(candidato);
            }
        }
    }
}