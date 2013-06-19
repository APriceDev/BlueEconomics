using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueEconomics.Platform.Infrastructure;

namespace BlueEconomics.Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new  BlueDbContext();

            var filters = context.Filters.ToList();
        }
    }
}
