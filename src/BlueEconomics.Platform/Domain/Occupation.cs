namespace BlueEconomics.Platform.Domain
{
    public class Occupation : EntityBase
    {
        public int IndustryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string SocCode { get; set; }

        public decimal? MedianPayAnnual { get; set; }

        public decimal? MedianPayHourly { get; set; }

        public string EntryLevelEducation { get; set; }

        public string WorkExperience { get; set; }

        public double NumberOfJobs { get; set; }

        public int? EmploymentOpenings { get; set; }
    }
}
