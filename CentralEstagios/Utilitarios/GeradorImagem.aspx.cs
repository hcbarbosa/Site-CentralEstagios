using CentralEstagios.Models;
using CentralEstagios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Diagnostics;
using System.Drawing.Printing;

namespace CentralEstagios.Views
{
    public partial class GeradorImagem : System.Web.UI.Page
    {
        private readonly Repositorio<Vaga> dbVaga = new Repositorio<Vaga>();
        private readonly Repositorio<Beneficio> dbBeneficio = new Repositorio<Beneficio>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["vaga"] != null)
            {
                int vagaId = int.Parse(Request.QueryString["vaga"]);

                Vaga vaga = new Vaga();
                vaga = dbVaga.Obter().Where(p => p.Id == vagaId).FirstOrDefault();

                vaga.Beneficio = dbBeneficio.Obter().Where(p => p.Id == vaga.BeneficioId).FirstOrDefault();

                string listaBeneficios = string.Empty;

                if (vaga.Beneficio.AuxilioOdontologico)
                {
                    listaBeneficios += "<li>Auxílio Odontológico</li>";
                }
                if (vaga.Beneficio.PlanoSaude)
                {
                    listaBeneficios += "<li>Plano de Saúde</li>";
                }
                if (vaga.Beneficio.ValeAlimentacao)
                {
                    listaBeneficios += "<li>Vale Alimentação</li>";
                }
                if (vaga.Beneficio.ValeTransporte)
                {
                    listaBeneficios += "<li>Vale Transporte</li>";
                }

                descricao.Text = vaga.Descricao;
                empresa.Text = vaga.Empresa;
                horario.Text = vaga.Horario;
                periodo.Text = vaga.Periodo;
                tipoVaga.Text = vaga.TipoVaga;
                remuneracao.Text = vaga.Remuneracao.ToString("c");
                beneficios.Text = listaBeneficios;
                observacoes.Text = vaga.Observacoes;
                telefone.Text = vaga.TelefoneEmpresa;
                email.Text = vaga.EmailEmpresa;
                pessoaContato.Text = vaga.PessoaContato;
            }
        }
    }
}