using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class AlunoPersonalizadoTDB
    {
        [Key]
        public Int64 idAluno { get; set; }

        public Int64 idInstituicao { get; set; }

        public Int64 idEvento { get; set; }

        public String nomeAluno { get; set; }

        public String serieAluno { get; set; }

        public String turnoAluno { get; set; }

        public String sexoAluno { get; set; }

        public DateTime dataNascimentoAluno { get; set; }

        public Decimal rendaFamiliaAluno { get; set; }

        public String planoSaudeAluno { get; set; }

        public String planoDentalAluno { get; set; }

        public string nomeResponsavel { get; set; }

        public string cpfResponsavel { get; set; }
    }
}