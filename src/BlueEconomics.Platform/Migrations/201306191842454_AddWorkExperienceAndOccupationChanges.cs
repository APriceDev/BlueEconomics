namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkExperienceAndOccupationChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Occupations", "WorkExperienceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Occupations", "EmploymentOpenings", c => c.Int(nullable: false));
            DropColumn("dbo.Occupations", "WorkExperience");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Occupations", "WorkExperience", c => c.String());
            AlterColumn("dbo.Occupations", "EmploymentOpenings", c => c.Int());
            DropColumn("dbo.Occupations", "WorkExperienceId");
            DropTable("dbo.WorkExperiences");
        }
    }
}
