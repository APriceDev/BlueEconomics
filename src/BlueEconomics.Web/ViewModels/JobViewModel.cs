// BlueEconomics.Platform
// BlueEconomics.Web
// 27
// Michel Oliveira

namespace BlueEconomics.Web.ViewModels
{
    public class JobViewModel
    {
        //public List<Industry> Industries { get; set; }

        public int? IdIndustry { get; set; }

        public bool FilterByEducationNone { get; set; }
        
        public bool FilterByEducationLessThaOneYear { get; set; }

        public bool FilterByEducationOneToFiveYear { get; set; }

        public bool FilterByEducationOther { get; set; }

        public bool SortByWage { get; set; }

        public bool SortByScore { get; set; }

    }
}