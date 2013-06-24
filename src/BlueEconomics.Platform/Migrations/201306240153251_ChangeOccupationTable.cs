namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOccupationTable : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Occupations", "WorkExperienceId", "dbo.WorkExperiences", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Occupations", "EducationLevelId", "dbo.EducationLevels", "Id");
            CreateIndex("dbo.Occupations", "WorkExperienceId");
            CreateIndex("dbo.Occupations", "EducationLevelId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Occupations", new[] { "EducationLevelId" });
            DropIndex("dbo.Occupations", new[] { "WorkExperienceId" });
            DropForeignKey("dbo.Occupations", "EducationLevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.Occupations", "WorkExperienceId", "dbo.WorkExperiences");
        }
    }
}
