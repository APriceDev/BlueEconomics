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

            import.ClearDatabase();

            import.StartImport();
        }
    }
}
