using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obraigatório!")]
        [Display(Name = "Curso")]
        public string Descricao { get; set; }

        [ScaffoldColumn(false)]
        public int Status { get; set; }

        [NotMapped]
        public bool EstaSelecionado { get; set; }
    }
}