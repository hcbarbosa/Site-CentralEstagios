using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralEstagios.Models
{
    public class Vaga
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BeneficioId")]
        public Beneficio Beneficio { get; set; }

        public int BeneficioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Nome da Vaga")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Telefone da Empresa")]
        public string TelefoneEmpresa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Horário")]
        public string Horario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Pessoa para Contato")]
        public string PessoaContato { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Período")]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Tipo da Vaga")]
        public string TipoVaga { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Remuneração")]
        public decimal Remuneracao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Email da Empresa")]
        public string EmailEmpresa { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [ScaffoldColumn(false)]
        public int Status { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<DateTime> DataCriacao { get; set; }

        [NotMapped]
        public string DataString { get; set; }
    }
}