using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class Observacao
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        [ForeignKey("VagaId")]
        public Vaga Vaga { get; set; }

        public int VagaId { get; set; }

        [NotMapped]
        [ForeignKey("AlunoId")]
        public Perfil PerfilAluno { get; set; }

        public string AlunoId { get; set; }

        public string AdmId { get; set; }

        [NotMapped]
        [ForeignKey("DonoMsg")]
        public Perfil PerfilDono { get; set; }

        public string DonoMsg { get; set; }

        [Required(ErrorMessage = "Campo descrição é obrigatório!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [ScaffoldColumn(false)]
        public int Status { get; set; }
    }
}