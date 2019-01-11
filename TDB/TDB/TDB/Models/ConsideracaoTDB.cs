using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class ConsideracaoTDB
    {
        [Key]
        public Int64 idConsideracao { get; set; }

        public Int64 idAluno { get; set; }

        public String descricaoConsideracao { get; set; }

        public DateTime dataConsideracao { get; set; }

    }
}