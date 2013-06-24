// BlueEconomics.Platform
// BlueEconomics.Web
// 27
// Michel Oliveira

using System.Collections.Generic;

namespace BlueEconomics.Web.ViewModels
{
    public class JobViewModel
    {

        public JobViewModel()
        {
            Filters=new List<FilterViewModel>();

        }

        public List<FilterViewModel> Filters { get; set; }


    }
}