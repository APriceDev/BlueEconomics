namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnAliasToLevelType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LevelTypes", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LevelTypes", "Alias");
        }
    }
}
