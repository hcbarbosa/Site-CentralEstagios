using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class Login
    {
        [Key]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "RM")]
        public string Rm { get; set; }

        [StringLength(32, ErrorMessage = "A senha precisar ter no mínimo {2} e no máximo {1} caracteres!", MinimumLength = 6)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas devem ser iguais!")]
        [Display(Name = "Confirme a Senha")]
        public string ConfirmaSenha { get; set; }

        [ScaffoldColumn(false)]
        public int Status { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Administrador?")]
        public bool StatusAdm { get; set; }
    }
}