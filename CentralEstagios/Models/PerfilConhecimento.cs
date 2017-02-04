using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class PerfilConhecimento
    {
        [ForeignKey("PerfilRm")]
        public Perfil Perfil { get; set; }

        [Key, Column(Order = 0)]
        public string PerfilRm { get; set; }

        [ForeignKey("ConhecimentoId")]
        public Conhecimento Conhecimento { get; set; }

        [Key, Column(Order = 1)]
        public int ConhecimentoId { get; set; }
    }
}