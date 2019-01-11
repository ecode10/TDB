using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TDB.DAO
{
    public class MinhaConexao : DbContext
    {
        public MinhaConexao()
            : base("name=MinhaConexao")
        {
            Database.SetInitializer<MinhaConexao>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }

        public System.Data.Entity.DbSet<TDB.Models.UsuarioTDB> UsuarioTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.InstituicaoTDB> InstituicaoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.EventoTDB> EventoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.AlunoTDB> AlunoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.ConsideracaoTDB> ConsideracaoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.DocumentoTDB> DocumentoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.FotoTDB> FotoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.IHCTDB> IHCTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.PercepcaoTDB> PercepcaoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.TipoFotoTDB> TipoFotoTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.PerguntasSocioEconomicasTDB> PerguntasSocioEconomicasTDB { get; set; }

        public System.Data.Entity.DbSet<TDB.Models.ResponsavelTDB> ResponsavelTDB { get; set; }
    }
}