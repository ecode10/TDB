using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class TipoFotoTDB
    {
        [Key]
        public Int64 idTipoFoto { get; set; }

        public String descricaoTipoFoto { get; set; }

    }
}