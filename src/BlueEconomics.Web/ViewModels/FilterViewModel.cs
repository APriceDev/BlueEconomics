// Project: BlueEconomics.Web
// Date:19/06/2013
// File: FilterViewModel.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco

using System.Collections.Generic;

namespace BlueEconomics.Web.ViewModels
{
    public class FilterViewModel
    {
        public string Category { get; set; }
        public List<FilterItem> Itens { get; set; }

        public FilterViewModel()
        {
            Itens=new List<FilterItem>();
        }
    }

    public class FilterItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FilterItem(int id, string name, int quantity)
        {
            this.Id = id;
            this.Name = string.Format("{0} ({1})", name, quantity);
        }
    }
}