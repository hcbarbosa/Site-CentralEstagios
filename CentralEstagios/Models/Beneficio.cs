using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class Beneficio
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Auxílio Odontológico")]
        public bool AuxilioOdontologico { get; set; }

        [Display(Name = "Plano de Saúde")]
        public bool PlanoSaude { get; set; }

        [Display(Name = "Vale Transporte")]
        public bool ValeTransporte { get; set; }

        [Display(Name = "Vale Alimentação")]
        public bool ValeAlimentacao { get; set; }

        public string Outros { get; set; }
    }
}