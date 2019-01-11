using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class PercepcaoTDB
    {
        [Key]
        public Int64 idPercepcao { get; set; }

        public Int64 idAluno { get; set; }

        public String temCarie { get; set; }

        public String vergonhaDosDentes { get; set; }

        public String foiAoDentista { get; set; }

        public String Onde { get; set; }


    }
}