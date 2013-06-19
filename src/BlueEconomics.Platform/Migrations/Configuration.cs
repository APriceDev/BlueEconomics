using BlueEconomics.Platform.Domain;

namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlueEconomics.Platform.Infrastructure.BlueDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BlueEconomics.Platform.Infrastructure.BlueDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Industries.AddOrUpdate(new Industry() { Code = 11, Name = "Management Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 13, Name = "Business and Financial Operations Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 15, Name = "Computer and Mathematical Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 17, Name = "Architecture and Engineering Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 19, Name = "Life, Physical, and Social Science Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 21, Name = "Community and Social Service Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 23, Name = "Legal Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 25, Name = "Education, Training, and Library Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 27, Name = "Arts, Design, Entertainment, Sports, and Media Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 29, Name = "Healthcare Practitioners and Technical Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 31, Name = "Healthcare Support Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 33, Name = "Protective Service Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 35, Name = "Food Preparation and Serving Related Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 37, Name = "Building and Grounds Cleaning and Maintenance Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 39, Name = "Personal Care and Service Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 41, Name = "Sales and Related Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 43, Name = "Office and Administrative Support Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 45, Name = "Farming, Fishing, and Forestry Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 47, Name = "Construction and Extraction Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 49, Name = "Installation, Maintenance, and Repair Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 51, Name = "Production Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 53, Name = "Transportation and Material Moving Occupations" });
            context.Industries.AddOrUpdate(new Industry() { Code = 55, Name = "Military Specific Occupations" });

            context.SaveChanges();

            //Education Score

            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Less than high school", Score = 4 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "High school", Score = 3 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Some college, no degree", Score = 2 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Associate's degree", Score = 1 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Bachelor's degree", Score = 0 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Master's degree", Score = 0 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Doctoral or professional degree", Score = 0 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Post Secondary Non Degree Award", Score = 0 });
            context.EducationScores.AddOrUpdate(new EducationScore() { Name = "Typical level of education that most workers need to enter this occupation.", Score = 0 });

            context.SaveChanges();


            //LevelType
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Premium", Alias = "" });
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Great", Alias = "" });
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Good", Alias = "" });
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Not Recommended", Alias = "" });
            
            context.SaveChanges();

        }
    }
}
