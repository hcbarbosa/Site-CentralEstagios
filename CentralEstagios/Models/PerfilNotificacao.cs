using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models
{
    public class PerfilNotificacao
    {
        [NotMapped]
        [ForeignKey("PerfilId")]
        public Perfil Perfil { get; set; }

        [Key, Column(Order = 0)]
        public string PerfilId { get; set; }

        [NotMapped]
        [ForeignKey("NotificacaoId")]
        public Notificacoes Notificacao { get; set; }

        [Key, Column(Order = 1)]
        public int NotificacaoId { get; set; }

        public int Status { get; set; }
    }
}