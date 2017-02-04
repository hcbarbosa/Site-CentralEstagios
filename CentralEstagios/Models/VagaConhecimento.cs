using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class VagaConhecimento
    {
        [ForeignKey("VagaId")]
        [NotMapped]
        public Vaga Vaga { get; set; }

        [Key, Column(Order = 0)]
        public int VagaId { get; set; }

        [ForeignKey("ConhecimentoId")]
        [NotMapped]
        public Conhecimento Conhecimento { get; set; }

        [Key, Column(Order = 1)]
        public int ConhecimentoId { get; set; }

        [NotMapped]
        public IList<Conhecimento> Conhecimentos { get; set; }
    }
}