// Project: BlueEconomics.Data.XML.Reader
// Date:18/06/2013
// File: ImportData.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BlueEconomics.Platform.Domain;
using BlueEconomics.Platform.Infrastructure;

namespace BlueEconomics.Data.XML.Reader
{
    public class ImportData
    {
        private BlueDbContext context = null;
        public ImportData()
        {
            context = new BlueDbContext();
        }

        public void ClearDatabase()
        {
            context.Database.Delete();
            context.Database.CreateIfNotExists();
        }

        private string ReplaceItemString(string value)
        {
            if (value == null)
                return string.Empty;

            return value.ToString(CultureInfo.InvariantCulture).Replace("Item", "");
        }

        private decimal? ConvertToDecimal(string value)
        {
            decimal result = 0;

            if (Decimal.TryParse(value, out result))
                return result;

            return null;
        }

        public void StartImport()
        {
             var xmlSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\xml-compilation.xml");

                //Get all Industries

                var industries = context.Industries.ToList();
                var educationsLevel = context.EducationScores.ToList();
                
                //Load xml in memory
                var serializer = new XmlSerializer(typeof(ooh));
                var occupationHandbook = (ooh)serializer.Deserialize(new XmlTextReader(xmlSourcePath));

                foreach (var industry in industries)
                {
                    var socCodePattern = "Item" + industry.Code.ToString().PadLeft(2, '0'); //only because of 1

                    var occupationsOfCurrentIndustry = occupationHandbook.occupation.Where(oc =>
                                                                                           oc.soc_coverage.First()
                                                                                             .Value.ToString()
                                                                                             .StartsWith(socCodePattern,
                                                                                                         true,CultureInfo.InvariantCulture)).ToList();

                    if (occupationsOfCurrentIndustry.Any())
                    {
                            foreach (var occupation in occupationsOfCurrentIndustry)
                            {

                                var quickFacts = occupation.summary.quick_facts;

                                var medianPayAnnual = this.ReplaceItemString(quickFacts.median_pay_annual.value.Value.ToString());
                                var medianPayHourly = this.ReplaceItemString(quickFacts.median_pay_hourly.value.Value.ToString());
                                
                                var educationLevel = this.ReplaceItemString(quickFacts.entry_level_education.value.Value.ToString());

                                if (string.IsNullOrEmpty(educationLevel))
                                {
                                    educationLevel = this.ReplaceItemString(quickFacts.entry_level_education.help.Value);
                                }

                                context.Ocuppations.Add(new Occupation()
                                    {
                                        IndustryId = industry.Id,
                                        Name = occupation.title.Value,
                                        Description = occupation.description.Value,
                                        Code = occupation.occupation_code.ToString(),
                                        SocCode =   occupation.soc_coverage.First().Value.ToString().Replace("Item", ""),
                                        MedianPayAnnual = ConvertToDecimal(medianPayAnnual),
                                        MedianPayHourly = ConvertToDecimal(medianPayHourly),
                                    });

                                context.SaveChanges();
                                
                                command.Parameters.AddWithValue("@EntryLevelEducation", educationLevel);


                                var workExperience = quickFacts.work_experience.value.Value.ToString().Replace("Item", "");

                                if (string.IsNullOrEmpty(workExperience))
                                    workExperience = quickFacts.work_experience.help.Value.Replace("Item", "");

                                command.Parameters.AddWithValue("@WorkExperience", workExperience);


                                var numberOfJobs = quickFacts.number_of_jobs.value.Value.ToString().Replace("Item", "");

                                int numberOfJobsValue = 0;

                                if (int.TryParse(numberOfJobs, out numberOfJobsValue))
                                    command.Parameters.AddWithValue("@NumberOfJobs", numberOfJobsValue);
                                else
                                {
                                    command.Parameters.AddWithValue("@NumberOfJobs", DBNull.Value);
                                }

                                var employmentOutlook = quickFacts.employment_outlook.value.Value.ToString()
                                                                  .Replace("Item", "");

                                int employmentOutlookValue = 0;

                                if (int.TryParse(employmentOutlook, out employmentOutlookValue))
                                    command.Parameters.AddWithValue("@EmploymentOutlookValue", employmentOutlookValue);
                                else
                                {
                                    command.Parameters.AddWithValue("@EmploymentOutlookValue", DBNull.Value);
                                }


                                command.Parameters.AddWithValue("@EmploymentOutlookDescription",
                                                                quickFacts.employment_outlook.description.Value);


                                var employmentOpenings =
                                    quickFacts.employment_openings.value.Value.ToString().Replace("Item", "");

                                int employmentOpeningsValue = 0;
                                if (int.TryParse(employmentOpenings, out employmentOpeningsValue))
                                    command.Parameters.Add("@EmploymentOpenings", SqlDbType.Int).Value =
                                        employmentOpeningsValue;
                                else
                                {
                                    command.Parameters.Add("@EmploymentOpenings", SqlDbType.Int).Value = DBNull.Value;
                                }

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }    

        }
        //#region [Filters ]

        //private static void InsertOccupationFilters()
        //{
        //    //InsertActivityilters();

        //    //InsertEducationFillters();

        //    //  InsertGrowthScoreFillters();

        //    InsertLevelTypeFillters();
        //}


        //private static void InsertEducationFillters()
        //{
        //    //Industries
        //    using (var con = new SqlCeConnection(connectionString))
        //    {
        //        var command = new SqlCeCommand();
        //        command.Connection = con;
        //        command.CommandText = "SELECT Id,name FROM EducationScore";
        //        con.Open();

        //        var reader = command.ExecuteReader();

        //        List<Industry> industryId = new List<Industry>();
        //        while (reader.Read())
        //        {
        //            industryId.Add(new Industry() { Id = int.Parse(reader[0].ToString()), Name = reader[1].ToString() });
        //        }

        //        command.CommandText = "SELECT count(1) FROM OCCUPATION WHERE EducationScoreId=@EducationScoreId";
        //        foreach (var industry in industryId)
        //        {
        //            command.Parameters.Clear();
        //            command.Parameters.AddWithValue("@EducationScoreId", industry.Id);

        //            var quantity = int.Parse(command.ExecuteScalar().ToString());

        //            if (quantity > 0)
        //                AddOccupationFilter(industry.Name, "Education Score", quantity, 2, "EducationScoreId", industry.Id);
        //        }

        //    }
        //}

        //private static void InsertGrowthScoreFillters()
        //{
        //    //Industries
        //    using (var con = new SqlCeConnection(connectionString))
        //    {
        //        var command = new SqlCeCommand();
        //        command.Connection = con;
        //        command.CommandText = "SELECT Id,name FROM GrowthScore ";
        //        con.Open();

        //        var reader = command.ExecuteReader();

        //        List<Industry> industryId = new List<Industry>();
        //        while (reader.Read())
        //        {
        //            industryId.Add(new Industry() { Id = int.Parse(reader[0].ToString()), Name = reader[1].ToString() });
        //        }

        //        command.CommandText = "SELECT count(1) FROM OCCUPATION WHERE GrowthScoreId=@GrowthScoreId";
        //        foreach (var industry in industryId)
        //        {
        //            command.Parameters.Clear();
        //            command.Parameters.AddWithValue("@GrowthScoreId", industry.Id);

        //            var quantity = int.Parse(command.ExecuteScalar().ToString());

        //            if (quantity > 0)
        //                AddOccupationFilter(industry.Name, "Growth Score", quantity, 3, "GrowthScoreId", industry.Id);
        //        }
        //    }
        //}

        //private static void InsertIndustryilters()
        //{
        //    //Industries
        //    using (var con = new SqlCeConnection(connectionString))
        //    {
        //        var command = new SqlCeCommand();
        //        command.Connection = con;
        //        command.CommandText = "SELECT Id,name FROM INDUSTRY";
        //        con.Open();

        //        var reader = command.ExecuteReader();

        //        List<Industry> industryId = new List<Industry>();
        //        while (reader.Read())
        //        {
        //            industryId.Add(new Industry() { Id = int.Parse(reader[0].ToString()), Name = reader[1].ToString() });
        //        }

        //        command.CommandText = "SELECT count(1) FROM OCCUPATION WHERE IndustryId=@IndustryId";
        //        foreach (var industry in industryId)
        //        {
        //            command.Parameters.Clear();
        //            command.Parameters.AddWithValue("@IndustryId", industry.Id);

        //            var quantity = int.Parse(command.ExecuteScalar().ToString());

        //            if (quantity > 0)
        //                AddOccupationFilter(industry.Name, "Industry", quantity, 1, "IndustryId", industry.Id);
        //        }

        //    }
        //}

        //#endregion


        //private static void AddOccupationFilter(string Name, string Category, int Quantity, int Order, string TableFieldName, int fieldId)
        //{
        //    using (var con = new SqlCeConnection(connectionString))
        //    {
        //        var command = new SqlCeCommand();
        //        command.Connection = con;
        //        command.CommandText = "INSERT INTO GroupedFilters([Name],[Category],[Quantity],[Order],[TableFieldName],[FilterId]) VALUES (@Name, @Category, @Quantity, @Order, @TableFieldName, @FilterId)";

        //        con.Open();
        //        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        //        command.Parameters.Add("@Category", SqlDbType.NVarChar).Value = Category;
        //        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;
        //        command.Parameters.Add("@Order", SqlDbType.Int).Value = Order;
        //        command.Parameters.Add("@TableFieldName", SqlDbType.NVarChar).Value = TableFieldName;
        //        command.Parameters.Add("@FilterId", SqlDbType.Int).Value = fieldId;
        //        command.ExecuteNonQuery();
        //    }
        //}

        


        //private static decimal calcIncomeScore(decimal income)
        //{
        //    const int MaxIncome = 130000 / 4;

        //    return income / MaxIncome;
        //}

        //private static decimal calcAvalabilityScore(decimal workers)
        //{
        //    const int maxJob = 11000 / 4;


        //    return workers / maxJob;
        //}

        //private static int getGrowthLevelID(string growthName)
        //{

        //    switch (growthName)
        //    {
        //        case "Very Favorable":
        //            return 1;
        //        case "Favorable":
        //            return 2;
        //        case "Unfavorable":
        //            return 3;
        //        case "Very Unfavorable":
        //            return 4;
        //    }

        //    return 5;
        //}







        //private static int getEducationLevelID(string educationLevel)
        //{
        //    switch (educationLevel)
        //    {
        //        case "Lessthanhighschool":
        //            return 1;
        //        case "Highschooldiplomaorequivalent":
        //            return 2;
        //        case "Somecollegenodegree":
        //            return 3;
        //        case "Associatesdegree":
        //            return 4;
        //        case "Bachelorsdegree":
        //            return 5;
        //        case "Mastersdegree":
        //            return 6;
        //        case "Doctoralorprofessionaldegree":
        //            return 7;
        //        case "Postsecondarynondegreeaward":
        //            return 8;
        //        case "Typical level of education that most workers need to enter this occupation.":
        //            return 9;

        //    }

        //    return 9;
        //}

        ////returning levelTypeID
        //private static int getLevelTypeID(decimal total)
        //{
        //    if (total > 3)
        //        return 1;
        //    else
        //    {
        //        if (total > 1.5M)
        //            return 2;
        //        else
        //        {
        //            if (total > 0.4M)
        //                return 3;
        //            else
        //                return 4;
        //        }
        //    }
        //}
    }
}