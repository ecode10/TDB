using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class IHCTDB
    {
        [Key]
        public Int64 idIHC { get; set; }

        public Int64 idAluno { get; set; }

        public String controleDoenca { get; set; }

        public String controleLesao { get; set; }

        public String tratamentoEspecializado { get; set; }

        public String necessidadeEspecialista { get; set; }

        public String pacienteComDor { get; set; }

        public string descricaoEspecializado { get; set; }

    }
}