// Project: BlueEconomics.Platform
// Date:18/06/2013
// File: BlueDataContext.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco

using System.Data.Entity;
using BlueEconomics.Platform.Domain;

namespace BlueEconomics.Platform.Infrastructure
{
    public class BlueDbContext: DbContext 
    {
        public BlueDbContext()
            : base("Name=BlueDBConString")
        {
            
        }

        public DbSet<EducationLevel> EducationLevels { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }

        public DbSet<Filter> Filters { get; set; }

        public DbSet<GrowthScore> GrowthScores { get; set; }

        public DbSet<Industry> Industries { get; set; }

        public DbSet<LevelType> LevelTypes { get; set; }

        public DbSet<Occupation> Ocuppations { get; set; }

        public DbSet<FAQ_Question> FAQ_Questions { get; set; }

        public DbSet<FAQ_ResponseSource> FAQ_ResponseSources { get; set; }

        public DbSet<FAQ_Response> FAQ_Responses { get; set; }

        public DbSet<FAQ_QuestionSource> FAQ_QuestionSources { get; set; }

        public DbSet<FAQ_QuestionAssignment> FAQ_QuestionAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}