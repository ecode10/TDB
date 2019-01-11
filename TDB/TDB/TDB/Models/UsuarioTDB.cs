using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDB.Models
{
    public class UsuarioTDB
    {
        [Key]
        public Int64 IdUsuario { get; set; }

        public String nomeUsuario { get; set; }

        public String emailUsuario { get; set; }

        public String senhaUsuario { get; set; }

        public String statusUsuario { get; set; }

    }
}