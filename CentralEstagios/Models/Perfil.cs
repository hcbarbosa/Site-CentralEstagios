using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class Perfil
    {
        [NotMapped]
        [ForeignKey("LoginRm")]
        public Login Login { get; set; }

        [Key]
        public string LoginRm { get; set; }

        [NotMapped]
        [ForeignKey("CursoId")]
        public Curso Curso { get; set; }

        public int CursoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "UF")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Semestre { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Email { get; set; }
    }
}