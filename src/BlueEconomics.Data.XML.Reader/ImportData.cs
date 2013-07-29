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
using System.Text;
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

        // D Mitchell
        public void SetupFAQTables()
        {
            string[] FAQ_Questions_Common =
                new string[]
                {
                    "How do I become a ",
                    "What education do I need and how do I get it (what schools/training program do you recommend)?",
                    "What skills do I need and how do I get  them?",
                    "Where can I get help on Immigration issues?",
                    "Where is most of the hiring?",
                    "Do I have to join a union? How do I join a Union?",
                    "What can I do about Financial Aid?",
                    "Whats the best way to apply?",
                    "Whats the best way to network?",
                    "Where can I go for more assistance?"
                };

            string[] Occupation_Names_Healthcare =
                new string[]
                {
                    "Licensed Practical and Licensed Vocational Nurses",
                    "Dental Hygienists","Radiologic Technologists and Technicians",
                    "Respiratory Therapists","Nursing Aides, Orderlies, and Attendants",
                    "Medical Records and Health Information Technicians",
                    "Cardiovascular Technologists and Technicians",
                    "Physical Therapist Assistants",
                    "Medical Equipment Repairers",
                    "Health Technologists and Technicians, All Other"
                };

            string[] FAQ_Responses_Healthcare =
                new string[]
                {            
                    "Depending on the choices, most require an employee to complete a degree program and obtain practicum through clinicals and internships. I would suggest speaking with a current professional, especially since the global economy changed the nature of many of these careers. For an example, biotechnology will be incorporated in healthcare careers. http://www.bls.gov/ooh/ is a great website that provides basic information about the growth of industries, how to enter the field, degree requirement, and potential work environment. Also, almost every field has national and international associations where they can network with professionals in their chosen field so they can educate an interested about how to enter the field of their choice.",
                    "For anyone that is interested in any of these professions, they will have to research community colleges, trade schools, and 4 year schools in their area. Depending on the program, some may take only 18 – 24 months, while other will require 4 years for completion. The best advice is to speak with advisors and representatives at colleges to see if they offer these programs and find out about the admission process. Some programs, such as Respiratory therapy and RN, may require a student to pass an exam and apply for the program after they complete prerequisites.",
                    "Depends on the occupation. The most important skills are communication/writing skills, math/science in the health care field, critical thinking, customer service, teamwork, the ability to work independently, and computer skills. An interested candidate must attend post-secondary institutions to develop most of these skills.",
                    "Contact school counselors about how immigration status will impact eligibility to attend post-secondary institutions and trade schools. Also, contact occupation boards, such as nursing and respiratory therapy to determine how immigration status impacts eligibility to receive licenses/certifications.",
                    "Most hiring places are in larger/public institutions. Private institutions require fewer workers, therefore, interested candidates will have to pursue more creative networking strategies, such as attending social events, connecting with them on social networks, and attending conferences to meet with professionals with private practices and market their skills. The job market is about whom you know, so connecting with professionals on a social level will increase an applicant’s chance of obtaining employment. ",
                    "Depends on the employer and the location of the job. Northeast and Midwest has a stronger union presence than the south. Also, public institutions will more likely require employees to join unions.",
                    "They must file FASFA, online, to determine if they are eligible for funding, which depends on income status. Interested candidates must speak with a school F/A counselor of their choice. Every schools conduct financial aid workshops that educate potential students on how to properly file financial aid and apply for scholarships.",
                    "Research schools of interest and apply according to the admission process. Contact a school advisor to determine chances of entering the institutions. Community colleges typically have open admission policy, which means that they can apply up to the deadline of course registration and will automatically get accepted to the school. Four year institutions and trade schools have more restrictive application and deadline policies. Make sure interested applicants find out if institutions offer a program of their choice and program entrance policies before applying for the school.",
                    "As stated earlier, obtaining employment is about “who you know”. Research and become members of national organizations, which provides benefits such as discounts for workshops/conferences, social events, newsletters, and certifications. Most organizations allow members to participate in committees so they can become actively involve with these organizations and network with other members. Interested candidates should reach out to professionals and program departments at their school to ask about internship and job shadowing opportunities. They should, also, volunteer in community programs that relates to the field of their choice. Interested candidates should contact their inner circles, such as close friends, relatives, or mentors that are already in the field to determine hiring possibilities. Networking should start at an early stage of the career transition process.",
                    "First step is candidates should speak with academic advisors about the application/financial aid process, course requirements, and potential internship opportunities. Once candidates start their education process, then they should start the networking with hopes of landing a position after graduation."
                };

            using (BlueDbContext context = new BlueDbContext())
            {
      
                // Delete existing data
                StringBuilder sb = new StringBuilder();

                sb.Append("DELETE FROM FAQ_Question; DELETE FROM FAQ_Response;");
                sb.Append("DELETE FROM FAQ_QuestionSource; DELETE FROM FAQ_ResponseSource;");

                context.Database.ExecuteSqlCommand(sb.ToString());

                // FAQ Sourcer
                var fqs =
                    new FAQ_QuestionSource()
                    {
                        Name = "Joey Blow"
                    };

                context.FAQ_QuestionSources.Add(fqs);

                // FAQ Responder
                var frs =
                    new FAQ_ResponseSource()
                    {
                        Organization = "Workforce Advisor"
                    };

                context.FAQ_ResponseSources.Add(frs);

                context.SaveChanges();  // Both added to database

                //  Assoicate each occupation in Occupations entity 
                //  with standard FAQ questions and responses
                foreach (var oc in context.Ocuppations)
                {

                    foreach (string question in FAQ_Questions_Common)
                    {
                        string bufferOCCname = string.Empty;
                        string bufferResponseContent = "Unknown";

                        if (question.Contains(FAQ_Questions_Common[0]))
                        {
                            bufferOCCname = oc.Name;
                        }

                        // Instantiate a new FAQ_Question
                        var fq =
                            new FAQ_Question()
                            {
                                Text = (question + " " + bufferOCCname).Trim(),
                                OccupationId = oc.Id
                            };


                        int responseIndex = Array.FindIndex(Occupation_Names_Healthcare, element => element.Contains(oc.Name));

                        if (responseIndex != -1)
                        {
                            bufferResponseContent = FAQ_Responses_Healthcare[responseIndex];
                        }

                        // Instantiate a new FAQ_Response
                        var fr =
                             new FAQ_Response()
                            {
                                Text = bufferResponseContent
                            };

                        //  Incorporate new question and response according to the entity model
                        fq.FAQ_Responses.Add(fr);    // Add the response to the question
                        fr.FAQ_Questions.Add(fq);      // Add the question to the response

                        fqs.FAQ_Questions.Add(fq);   // Add the question to the question source
                        frs.FAQ_Responses.Add(fr);   // Add the response to the response source

                    }

                }

                context.SaveChanges();  // Update the database using the entity model

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