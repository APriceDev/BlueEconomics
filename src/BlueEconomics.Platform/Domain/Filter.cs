// Project: BlueEconomics.Platform
// Date:18/06/2013
// File: Filters.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco
namespace BlueEconomics.Platform.Domain
{
    public class Filter:EntityBase
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public int Order { get; set; }
        public string TableFieldName { get; set; }
        public string FilterId { get; set; }
    }
}