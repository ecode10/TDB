using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class EventoTDB
    {

        [Key]
        public Int64 idEvento { get; set; }

        public Int64 idInstituicao { get; set; }

        public String nomeEvento { get; set; }

        public DateTime dataInicioEvento { get; set; }

        public DateTime dataFimEvento { get; set; }

        public String statusEvento { get; set; }

        public String localEvento { get; set; }

        public String observacaoEvento { get; set; }


    }
}