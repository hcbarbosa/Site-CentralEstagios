using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class VagaCurso
    {
        [ForeignKey("VagaId")]
        [NotMapped]
        public Vaga Vaga { get; set; }

        [Key, Column(Order = 0)]
        public int VagaId { get; set; }

        [ForeignKey("CursoId")]
        [NotMapped]
        public Curso Curso { get; set; }

        [Key, Column(Order = 1)]
        public int CursoId { get; set; }

        [NotMapped]
        public IList<Curso> Cursos { get; set; }
    }
}