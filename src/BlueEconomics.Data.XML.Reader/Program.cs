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
        private static string connectionString = string.Format("Data Source = {0}",
                                                               @"E:\Projects\blueeconomics\src\BlueEconomics.Web\App_Data\BlueDB.sdf");
      
        static void Main(string[] args)
        {
            ClearDatabase();

            InsertIndustries();

            InsertOcuppations();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
        
        private static void InsertIndustries()
        {
            var industries = Industry.GetAll();
          
            using (var con = new SqlCeConnection(connectionString))
            {
                var command=new SqlCeCommand();
                command.Connection = con;
                command.CommandText = "INSERT INTO INDUSTRY(Name,Code) VALUES(@Name,@Code)";
               
                con.Open();
                foreach (var industry in industries)
                { 
                    command.Parameters.Clear();

                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = industry.Name;
                    command.Parameters.Add("@Code", SqlDbType.Int).Value = industry.Code;
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Industries inserteds");
        }

        private static List<Industry> GetallIndustries()
        {
            var industries=new  List<Industry>();

            using (var con = new SqlCeConnection(connectionString))
            {
                var command = new SqlCeCommand();
                command.Connection = con;
                command.CommandText = "SELECT * FROM INDUSTRY";

                con.Open();
                var dbReader = command.ExecuteReader();

                while (dbReader.Read())
                {
                    industries.Add(new Industry(){ Id = Convert.ToInt32(dbReader["Id"]), Code = Convert.ToInt32(dbReader["Code"])});                    
                }
            }

            return industries;
        }


        private static void InsertOcuppations()
        {
            var xmlSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\xml-compilation.xml");

            //Get all Industries

            var industries = GetallIndustries();

            if (!industries.Any())
                return;

            //Load xml in memory
            var serializer = new XmlSerializer(typeof (ooh));
            var occupationHandbook = (ooh) serializer.Deserialize(new XmlTextReader(xmlSourcePath));

            foreach (var industry in industries)
            {
                var socCodePattern = "Item" + industry.Code.ToString().PadLeft(2, '0'); //only because of 1

                var occupationsOfCurrentIndustry = occupationHandbook.occupation.Where(oc =>
                                                                                       oc.soc_coverage.First()
                                                                                         .Value.ToString()
                                                                                         .StartsWith(socCodePattern,
                                                                                                     true,
                                                                                                     CultureInfo
                                                                                                         .InvariantCulture))
                                                                     .ToList();

                if (occupationsOfCurrentIndustry.Any())
                {
                    using (var con = new SqlCeConnection(connectionString))
                    {
                        var command = new SqlCeCommand();
                        command.Connection = con;
                        command.CommandText = "INSERT INTO OCCUPATION(IndustryID, Name, Description, Code, SocCode, Published, MedianPayAnnual, MedianPayHourly, EntryLevelEducation, WorkExperience, NumberOfJobs, "+
                                               "EmploymentOutlookValue, EmploymentOutlookDescription, EmploymentOpenings) VALUES (" +
                                              "@IndustryID, @Name, @Description, @Code, @SocCode, @Published, @MedianPayAnnual, @MedianPayHourly, @EntryLevelEducation, " +
                                              "@WorkExperience, @NumberOfJobs, @EmploymentOutlookValue, @EmploymentOutlookDescription, @EmploymentOpenings)";
                         

                        con.Open();

                        foreach (var occupation in occupationsOfCurrentIndustry)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@IndustryID", industry.Id);
                            command.Parameters.AddWithValue("@Name", occupation.title.Value);
                            command.Parameters.AddWithValue("@Description", occupation.description.Value);
                            command.Parameters.AddWithValue("@Code", occupation.occupation_code.ToString());
                            command.Parameters.AddWithValue("@SocCode",
                                                            occupation.soc_coverage.First()
                                                                      .Value.ToString()
                                                                      .Replace("Item", ""));

                            var published = occupation.publish_date.Value.ToString().Replace("Item", "");
                            DateTime datePublished = DateTime.Now;

                            if(DateTime.TryParse(published,out datePublished))
                                command.Parameters.AddWithValue("@Published", datePublished);
                            else
                            {
                                command.Parameters.AddWithValue("@Published", DBNull.Value);
                            }

                            var quickFacts = occupation.summary.quick_facts;

                            var medianPayAnnual = quickFacts.median_pay_annual.value.Value.ToString()
                                                            .Replace("Item", "");

                            decimal medianPayAnnualValue = 0;
                            if (Decimal.TryParse(medianPayAnnual, out medianPayAnnualValue))
                                command.Parameters.Add("@MedianPayAnnual",SqlDbType.Money).Value=medianPayAnnualValue/100;
                            else
                            {
                                command.Parameters.AddWithValue("@MedianPayAnnual", DBNull.Value);
                            }


                            var medianPayHourly = quickFacts.median_pay_hourly.value.Value.ToString()
                                                            .Replace("Item", "");
                            
                            decimal medianPayHourlyValue = 0;

                            if (Decimal.TryParse(medianPayHourly, out medianPayHourlyValue))
                                command.Parameters.Add("@MedianPayHourly", SqlDbType.Money).Value =
                                    medianPayHourlyValue / 100;//workaround
                            else
                            {
                                command.Parameters.AddWithValue("@MedianPayHourly", DBNull.Value);
                            }


                            string educationLevel = quickFacts.entry_level_education.value.Value.ToString()
                                                           .Replace("Item", "");
                           
                            if (string.IsNullOrEmpty(educationLevel))
                            {
                                educationLevel = quickFacts.entry_level_education.help.Value.Replace("Item", "");
                            }

                            command.Parameters.AddWithValue("@EntryLevelEducation", educationLevel);


                            var workExperience = quickFacts.work_experience.value.Value.ToString().Replace("Item", "");

                            if(string.IsNullOrEmpty(workExperience))
                                workExperience =quickFacts.work_experience.help.Value.Replace("Item", "");

                            command.Parameters.AddWithValue("@WorkExperience", workExperience);


                            var numberOfJobs = quickFacts.number_of_jobs.value.Value.ToString().Replace("Item", "");

                            int numberOfJobsValue = 0;

                            if (int.TryParse(numberOfJobs, out numberOfJobsValue))
                                command.Parameters.AddWithValue("@NumberOfJobs",numberOfJobsValue);
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


        private static void ClearDatabase()
        {
            using (var con = new SqlCeConnection(connectionString))
            {
                var command = new SqlCeCommand();
                command.Connection = con;
                command.CommandText = "DELETE FROM INDUSTRY";

                con.Open();
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM OCCUPATION";
                command.ExecuteNonQuery();
            }
        }
    }
}
