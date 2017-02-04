using CentralEstagios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CentralEstagios.Contexto
{
    public class ProjetoContexto : DbContext
    {
        /// <summary>
        /// construtor statico
        /// </summary>
        static ProjetoContexto()
        {
            Database.SetInitializer<ProjetoContexto>(null);
        }

        /// <summary>
        /// construtor responsavel para resolver a string de conexao
        /// </summary>
        public ProjetoContexto()
            : base("DefaultConnection")
        {
            this.Configuration.ValidateOnSaveEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Database.CreateIfNotExists();

        }

        /// <summary>
        /// método para configurar entidade ao criar
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Configurar minha forma de montar o ID da tabela
            //modelBuilder.Properties()
            //    .Where(p => p.Name == p.ReflectedType.Name + "Id")
            //    .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(255));
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public DbSet<Conhecimento> Conhecimentos { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Observacao> Observacoes { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<ConhecimentoCurso> ConhecimentosCursos { get; set; }
        public DbSet<PerfilConhecimento> PerfisConhecimentos { get; set; }
        public DbSet<VagaConhecimento> VagasConhecimentos { get; set; }
        public DbSet<VagaCurso> VagasCursos { get; set; }
        public DbSet<Notificacoes> Notificacoes { get; set; }
        public DbSet<PerfilNotificacao> PerfilNotificacoes { get; set; }
    }
}