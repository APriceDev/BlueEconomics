using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BlueEconomics.Data.XML.Reader
{

    class Program
    {
        static void Main(string[] args)
        {
            var import = new ImportData();

            Console.WriteLine("Creating Database");

            //import.ClearDatabase();

            Console.WriteLine("Starting data import");
            import.StartImport();

            Console.WriteLine("Creating Filters");
            import.CreateFrilters();

            Console.WriteLine("Loading FAQ Data");
            import.SetupFAQTables();

            Console.WriteLine("All tasks done.Enter to finish");

            Console.ReadKey();
        }
    }
}
