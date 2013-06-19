using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BlueEconomics.Platform.Domain;
using BlueEconomics.Platform.Infrastructure;

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

        public List<Occupation> Get(int  industryId)
        {
            return context.Ocuppations.Where(o => o.IndustryId == industryId).ToList();
        }
    }
}
