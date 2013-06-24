// Project: BlueEconomics.Data.XML.Reader
// Date:18/06/2013
// File: ImportData.cs
// Author: Michel Oliveira
// Team: Michel Oliveira and João Bosco

using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ImportData()
        {
        }

        public void ClearDatabase()
        {
            using (BlueDbContext context = new BlueDbContext())
            {
                Database.SetInitializer(new BlueDbInitializer());

                context.Database.Delete();
                context.Database.CreateIfNotExists();
                context.Database.Initialize(true);
            }
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
            using (BlueDbContext context = new BlueDbContext())
            {
                var industries = context.Industries.ToList();
                var educationsLevels = context.EducationLevels.ToList();
                var workExperiences = context.WorkExperiences.ToList();

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
                        foreach (var occupationToImport in occupationsOfCurrentIndustry)
                        {

                            var quickFacts = occupationToImport.summary.quick_facts;

                            var medianPayAnnual =
                                this.ReplaceItemString(quickFacts.median_pay_annual.value.Value.ToString());
                            var medianPayHourly =
                                this.ReplaceItemString(quickFacts.median_pay_hourly.value.Value.ToString());

                            var educationLevelAlias =
                                this.ReplaceItemString(quickFacts.entry_level_education.value.Value.ToString());

                            if (string.IsNullOrEmpty(educationLevelAlias))
                            {
                                educationLevelAlias = this.ReplaceItemString(quickFacts.entry_level_education.help.Value);
                            }

                            var workExperienceAlias =
                                this.ReplaceItemString(quickFacts.work_experience.value.Value.ToString());

                            if (string.IsNullOrEmpty(workExperienceAlias))
                            {
                                workExperienceAlias = this.ReplaceItemString(quickFacts.work_experience.help.Value);
                            }


                            var educationLevel = educationsLevels.Single(e => e.Alias == educationLevelAlias);

                            var workExperience = workExperiences.Single(e => e.Alias == workExperienceAlias);


                            
                            var occupation = new Occupation()
                                {
                                    IndustryId = industry.Id,
                                    Name = occupationToImport.title.Value,
                                    Description = occupationToImport.description.Value,
                                    Code = occupationToImport.occupation_code.ToString(),
                                    SocCode =
                                        occupationToImport.soc_coverage.First().Value.ToString().Replace("Item", ""),
                                    MedianPayAnnual = ConvertToDecimal(medianPayAnnual),
                                    MedianPayHourly = ConvertToDecimal(medianPayHourly),
                                    EducationLevelId = educationLevel.Id,
                                    WorkExperienceId = workExperience.Id
                                };

                            if (occupation.MedianPayHourly.HasValue)
                                occupation.MedianPayHourly = occupation.MedianPayHourly.Value/100;
                            
                            var numberOfJobs = this.ReplaceItemString(quickFacts.number_of_jobs.value.Value.ToString());

                            int numberOfJobsValue = 0;

                            if (int.TryParse(numberOfJobs, out numberOfJobsValue))
                                occupation.NumberOfJobs = numberOfJobsValue;

                            var employmentOpenings =
                                this.ReplaceItemString(quickFacts.employment_openings.value.Value.ToString());

                            int employmentOpeningsValue = 0;
                            if (int.TryParse(employmentOpenings, out employmentOpeningsValue))
                                occupation.EmploymentOpenings = employmentOpeningsValue;

                            context.Ocuppations.Add(occupation);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public void CreateFrilters()
        {
            CreateEducationFilter();

            CreateWorkExperienceFilter();

            CreateIndustryFilter();
        }

        private void CreateEducationFilter()
        {
            var category = "Education";
            using (BlueDbContext context = new BlueDbContext())
            {

                var educations = context.EducationLevels.OrderBy(e => e.Name).ToList();

                var groupedEducations = (from oc in context.Ocuppations
                                         group oc by oc.EducationLevelId);

                foreach (var groupedEducation in groupedEducations)
                {
                    var edu = educations.Single(e => e.Id == groupedEducation.Key);

                    context.Filters.Add(new Filter()
                        {
                            Category = category,
                            Order = 1,
                            Name = edu.Name,
                            Quantity = groupedEducation.Count(),
                            FilterId = edu.Id
                        });
                }
                context.SaveChanges();
            }
        }

        private void CreateWorkExperienceFilter()
        {
            var category = "Work Experience";
            using (var context = new BlueDbContext())
            {
                var workExperiences = context.WorkExperiences.ToList();

                var groupedWorkExperience = (from oc in context.Ocuppations
                                         group oc by oc.WorkExperienceId);

                foreach (var gExperience in groupedWorkExperience)
                {
                    var edu = workExperiences.Single(e => e.Id == gExperience.Key);

                    context.Filters.Add(new Filter()
                        {
                            Category = category,
                            Order = 2,
                            Name = edu.Name,
                            Quantity = gExperience.Count(),
                            FilterId = edu.Id
                        });
                }
                context.SaveChanges();
            }
        }

        private void CreateIndustryFilter()
        {
            var category = "Industry";
            using (var context = new BlueDbContext())
            {
                var industries = context.Industries.ToList();

                var groupedIndustries = (from oc in context.Ocuppations
                                             group oc by oc.IndustryId);

                foreach (var gIndustry in groupedIndustries)
                {
                    var edu = industries.Single(e => e.Id == gIndustry.Key);

                    context.Filters.Add(new Filter()
                    {
                        Category = category,
                        Order = 3,
                        Name = edu.Name,
                        Quantity = gIndustry.Count(),
                        FilterId = edu.Id
                    });
                }
                context.SaveChanges();
            }
        }

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
    }
}