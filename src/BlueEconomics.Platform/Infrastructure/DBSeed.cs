// Project: BlueEconomics.Platform
// Date:19/06/2013
// File: DBSeed.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco

using System.Data.Entity.Migrations;
using BlueEconomics.Platform.Domain;

namespace BlueEconomics.Platform.Infrastructure
{
    public static class DBSeed
    {
        public static void Seed(Infrastructure.BlueDbContext context)
        {
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

            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Less than high school", Score = 4, Alias = "Lessthanhighschool" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "High school", Score = 3, Alias = "Highschooldiplomaorequivalent" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Some college, no degree", Score = 2, Alias = "Somecollegenodegree" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Associate's degree", Score = 1, Alias = "Associatesdegree" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Bachelor's degree", Score = 0, Alias = "Bachelorsdegree" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Master's degree", Score = 0, Alias = "Mastersdegree" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Doctoral or professional degree", Score = 0, Alias = "Doctoralorprofessionaldegree" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Post Secondary, Non Degree Award", Score = 0, Alias = "Postsecondarynondegreeaward" });
            context.EducationLevels.AddOrUpdate(new EducationLevel() { Name = "Typical level of education that most workers need to enter this occupation.", Score = 0, Alias = "Typical level of education that most workers need to enter this occupation." });

            context.SaveChanges();


            //LevelType
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Premium", Alias = "" });
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Great", Alias = "" });
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Good", Alias = "" });
            context.LevelTypes.AddOrUpdate(new LevelType() { Name = "Not Recommended", Alias = "" });

            context.SaveChanges();

            context.WorkExperiences.AddOrUpdate(new WorkExperience() { Name = "1 to 5 Years", Alias = "1to5years" });
            context.WorkExperiences.AddOrUpdate(new WorkExperience() { Name = "Less than 1 Year", Alias = "Lessthan1year" });
            context.WorkExperiences.AddOrUpdate(new WorkExperience() { Name = "More than 5 Years", Alias = "Morethan5years" });
            context.WorkExperiences.AddOrUpdate(new WorkExperience() {
                Name = "Work experience that is commonly considered necessary by employers, or is a commonly accepted substitute for more formal types of training or education",
                Alias = "Work experience that is commonly considered necessary by employers, or is a commonly accepted substitute for more formal types of training or education." });

            context.WorkExperiences.AddOrUpdate(new WorkExperience() { Name = "None", Alias = "None" });
            
            context.SaveChanges();
        } 
    }
}