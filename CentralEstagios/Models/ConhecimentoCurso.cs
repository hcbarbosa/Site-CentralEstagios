using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class ConhecimentoCurso
    {
        [NotMapped]
        [ForeignKey("ConhecimentoId")]
        public Conhecimento Conhecimento { get; set; }

        [Key, Column(Order = 0)]
        public int ConhecimentoId { get; set; }

        [NotMapped]
        [ForeignKey("CursoId")]
        public Curso Curso { get; set; }

        [NotMapped]
        public IList<Curso> Cursos { get; set; }

        [Key, Column(Order = 1)]
        public int CursoId { get; set; }
    }
}