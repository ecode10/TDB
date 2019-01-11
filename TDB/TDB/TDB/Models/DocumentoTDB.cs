using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class DocumentoTDB
    {
        [Key]
        public Int64 idDocumento { get; set; }

        public Int64 idAluno { get; set; }

        public String nomeDocumento { get; set; }

        public String statusDocumento { get; set; }

    }
}