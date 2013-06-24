namespace BlueEconomics.Platform.Domain
{
    public class Occupation : EntityBase
    {
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }
        
        public int WorkExperienceId { get; set; }
        public WorkExperience WorkExperience { get; set; }

        public int? EducationLevelId { get; set; }
        public EducationLevel EducationLevel { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string SocCode { get; set; }

        public decimal? MedianPayAnnual { get; set; }

        public decimal? MedianPayHourly { get; set; }

        public double NumberOfJobs { get; set; }

        public int EmploymentOpenings { get; set; }
    }
}
