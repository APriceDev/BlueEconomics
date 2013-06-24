namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyIndustryInOccupationTable : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Occupations", "IndustryId", "dbo.Industries", "Id", cascadeDelete: true);
            CreateIndex("dbo.Occupations", "IndustryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Occupations", new[] { "IndustryId" });
            DropForeignKey("dbo.Occupations", "IndustryId", "dbo.Industries");
        }
    }
}
