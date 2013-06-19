using System.Web.Mvc;

namespace BlueEconomics.Web.Controllers
{
    public class JobController : Controller
    {
        public JobController()
        {
        }

        ////
        //// GET: /Job/

        //public ActionResult Index()
        //{
        //    var model = new JobViewModel();

        //    model.Industries = context.Industries.OrderBy(i => i.Name).ToList();

        //    return View(model);
        //}

        ////JSON Calls;


        //public JsonResult GetOccupationByIndustry(int idIndustry)
        //{
        //    try
        //    {
        //        var occupations =
        //            context.Occupations.Where(o => o.IndustryID == idIndustry).OrderBy(o => o.Name).ToList();

        //        return Json(new {Status = "OK", Occupations = occupations}, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new {Status = "Error"}, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult FilterOccupationBy(string criteria, string education, string sortType, int? idIndustry)
        //{
        //    try
        //    {
        //        var occupations = context.Occupations.Where(a=>true);


        //        if (idIndustry.HasValue && idIndustry.Value > 0)
        //            occupations = occupations.Where(o => o.IndustryID == idIndustry.Value);
                
        //        if (!string.IsNullOrEmpty(criteria))
        //            occupations = occupations.Where(o =>
        //                                            o.Name.Contains(criteria) || o.Description.Contains(criteria) ||
        //                                            o.EntryLevelEducation.Contains(criteria) ||
        //                                            o.WorkExperience.Contains(criteria));

        //        if (!string.IsNullOrEmpty(education))
        //        {
        //            var educationsFilter = new List<string>();

        //            if(education.Contains("None"))
        //                educationsFilter.Add("None");

        //            if(education.Contains("LessThan1Year"))
        //                educationsFilter.Add("Lessthan1year");

        //            if(education.Contains("1to5Years"))
        //                educationsFilter.Add("1to5years");

        //            if(education.Contains("MoreThan5Years"))
        //                educationsFilter.Add("Morethan5years");
                   
        //            occupations = occupations.Where(e => educationsFilter.Contains(e.WorkExperience));
        //        }

        //        if (!string.IsNullOrEmpty(sortType))
        //        {
        //            if (sortType.Equals("NumberOfJobs"))
        //                occupations = occupations.OrderBy(o => o.NumberOfJobs);
        //            else
        //            {
        //                occupations = occupations.OrderBy(o => o.MedianPayAnnual);
        //            }
        //        }
        //        else
        //        {
        //            occupations = occupations.OrderBy(o => o.Name);
        //        }

        //        return Json(new { Status = "OK", Occupations = occupations.ToList() }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { Status = "Error" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
