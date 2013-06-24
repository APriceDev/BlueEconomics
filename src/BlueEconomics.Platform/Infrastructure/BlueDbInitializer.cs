using System.Data.Entity;
using System.Data.Entity.Migrations;
using BlueEconomics.Platform.Domain;

namespace BlueEconomics.Platform.Infrastructure
{
    public class BlueDbInitializer : DropCreateDatabaseAlways<BlueEconomics.Platform.Infrastructure.BlueDbContext>
    {
        protected override void Seed(BlueDbContext context)
        {
            DBSeed.Seed(context);
        }
    }
}