// Project: BlueEconomics.Web
// Date:23/06/2013
// File: OccupationDTO.cs
// Author: Michel Oliveira
// Team: Michel Oliveira
namespace BlueEconomics.Web.DTOs
{
    public class OccupationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WorkExperience { get; set; }
        public decimal? MedianPayHourly { get; set; }
        public decimal? MedianPayAnnual { get; set; }
        public decimal? Income { get; set; }
    }
}