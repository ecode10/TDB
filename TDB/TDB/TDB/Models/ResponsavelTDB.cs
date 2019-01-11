using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class ResponsavelTDB
    {
        [Key]
        public Int64 idResponsavel { get; set; }

        public String nomeResponsavel { get; set; }

        public String cpfResponsavel { get; set; }

    }
}