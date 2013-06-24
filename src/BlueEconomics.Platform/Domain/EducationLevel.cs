// Project: BlueEconomics.Platform
// Date:18/06/2013
// File: EducationScore.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco
namespace BlueEconomics.Platform.Domain
{
    public class EducationLevel : EntityBase
    {
        public string Name { get; set; }
        public decimal Score { get; set; }
        public string Alias { get; set; }
    }
}