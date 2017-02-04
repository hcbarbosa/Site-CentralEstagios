namespace CentralEstagios.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CentralEstagios.Contexto.ProjetoContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CentralEstagios.Contexto.ProjetoContexto context)
        {

        }
    }
}
