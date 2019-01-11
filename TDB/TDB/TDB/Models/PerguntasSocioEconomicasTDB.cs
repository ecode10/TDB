using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{

    public class PerguntasSocioEconomicasTDB
    {
        [Key]
        public Int64 idPerguntas { get; set; }

        public Int64 idAluno { get; set; }

        public Double carroProprio { get; set; }

        public int casaComodo { get; set; }

        public String quemTrabalha { get; set; }

    }
}