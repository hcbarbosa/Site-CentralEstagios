using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralEstagios.Models.Administracao
{
    public class VagaCompleta
    {
        // Classe de controle para adicionar vaga com todos seus n para n

        public Vaga Vaga { get; set; }
        public VagaConhecimento VagaConhecimento { get; set; }
        public VagaCurso VagaCurso { get; set; }
    }
}