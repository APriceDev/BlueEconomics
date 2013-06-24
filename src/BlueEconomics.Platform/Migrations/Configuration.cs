using System.Data.Entity;
using BlueEconomics.Platform.Infrastructure;

namespace BlueEconomics.Platform.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BlueEconomics.Platform.Infrastructure.BlueDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            Database.SetInitializer(new BlueDbInitializer());
        }

        protected override void Seed(BlueEconomics.Platform.Infrastructure.BlueDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            DBSeed.Seed(context);
        }
    }
}
