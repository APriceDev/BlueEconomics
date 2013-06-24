using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BlueEconomics.Platform.Domain;
using BlueEconomics.Platform.Infrastructure;
using BlueEconomics.Web.DTOs;

namespace BlueEconomics.Web.Controllers
{
    public class OccupationController : ApiController
    {
        private readonly BlueDbContext context;

        public OccupationController()
        {
            context = new BlueDbContext();
        }
        //
        // GET: /Occupation/
        public Occupation Get(int  id)
        {
            var occupation = context.Ocuppations.Include("EducationLevel").Include("Industry").Include("WorkExperience").SingleOrDefault(o => o.Id== id);

            return occupation;
        }

        public List<OccupationDTO> Get(string criteria)
        {
            var extractedCriteria = ExtractCriteria(criteria);
            
            var occupations = GetByCriterias(extractedCriteria);

            return occupations;
        }

        private List<OccupationDTO> GetByCriterias(Dictionary<string, object> criterias)
        {
            var occupations = from o in context.Ocuppations.Include("WorkExperience")
                              join e in context.EducationLevels on o.EducationLevelId equals e.Id
                              where true
                              select o;

            foreach (var criteria in criterias)
            {
                //if is string filter
                if (criteria.Key == "Filter")
                {
                    var filter = criteria.Value.ToString();

                    occupations = from o in occupations
                                where o.Name.Contains(filter) || o.SocCode.Contains(filter) || o.Description.Contains(filter)
                                || o.Code.Contains(filter)
                                select o;
                }
                else
                {
                    var values = criteria.Value as List<int>;

                    switch (criteria.Key)
                    {
                        case "Education":
                            occupations = from o in occupations
                                          where values.Contains(o.EducationLevelId.Value)
                                          select o;
                            break;
                        case "Industry":
                            occupations = from o in occupations
                                          where values.Contains(o.IndustryId)
                                          select o;
                            break;
                        case "Work Experience":
                            occupations = from o in occupations
                                          where values.Contains(o.WorkExperienceId)
                                          select o;

                            break;
                    }
                }
            }

            var convertedResult = from o in occupations
                                  select new OccupationDTO()
                                      {
                                          Id=o.Id,
                                          Name = o.Name,
                                          MedianPayAnnual = o.MedianPayAnnual,
                                          MedianPayHourly = o.MedianPayHourly,
                                          WorkExperience = o.WorkExperience.Name
                                      };

            return convertedResult.ToList();
        }


        private Dictionary<string, object> ExtractCriteria(string criteria)
        {
            var criteriaResult = new Dictionary<string, object>();

            var criterias = criteria.Split(',');

            if (!criterias.Any())
                return criteriaResult;

            foreach (var crit in criterias)
            {
                var filter = crit.Split('=');

                var field = filter[0];
                var value = filter[1];

                if (!field.Equals("Filter"))
                {
                    if (!criteriaResult.ContainsKey(field))
                    {
                        criteriaResult.Add(field, new List<int>() { int.Parse(value)});
                    }
                    else
                    {
                        var fieldsOfCriteria = (List<int>) criteriaResult[field];
                        fieldsOfCriteria.Add(int.Parse(value));
                        criteriaResult[field] = fieldsOfCriteria;
                    }
                }
                else
                {
                    criteriaResult.Add(field, value);
                }
            }
            return criteriaResult;
        }
    }
}
