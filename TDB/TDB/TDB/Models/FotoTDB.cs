using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class FotoTDB
    {
        [Key]
        public Int64 idFoto { get; set; }

        public Int64 idTipoFoto { get; set; }

        public Int64 idAluno { get; set; }

        public String nomeFoto { get; set; }

        public String statusFoto { get; set; }

    }
}