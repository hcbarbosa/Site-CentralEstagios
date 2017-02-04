using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class Candidato
    {
        [ForeignKey("PerfilRm")]
        public Perfil Perfil { get; set; }

        [Key, Column(Order = 0)]
        public string PerfilRm { get; set; }

        [ForeignKey("VagaId")]
        public Vaga Vaga { get; set; }

        [Key, Column(Order = 1)]
        public int VagaId { get; set; }
    }
}