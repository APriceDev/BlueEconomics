namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                        Quantity = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        TableFieldName = c.String(),
                        FilterId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GrowthScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LevelTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Occupations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryId = c.Int(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        SocCode = c.String(),
                        MedianPayAnnual = c.Decimal(precision: 18, scale: 2),
                        MedianPayHourly = c.Decimal(precision: 18, scale: 2),
                        WorkExperience = c.String(),
                        NumberOfJobs = c.Double(nullable: false),
                        EmploymentOpenings = c.Int(),
                        EducationLevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Occupations");
            DropTable("dbo.LevelTypes");
            DropTable("dbo.Industries");
            DropTable("dbo.GrowthScores");
            DropTable("dbo.Filters");
            DropTable("dbo.EducationLevels");
        }
    }
}
