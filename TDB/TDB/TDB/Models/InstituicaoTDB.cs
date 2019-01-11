using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class InstituicaoTDB
    {
        [Key]
        public Int64 idInstituicao { get; set; }

        public String nomeInstituicao { get; set; }

        public String enderecoInstituicao { get; set; }

        public String cidadeInstituicao { get; set; }

        public String estadoInstituicao { get; set; }

        public String cepInstituicao { get; set; }

        public String telefoneInstituicao { get; set; }

        public String statusInstituicao { get; set; }


    }
}