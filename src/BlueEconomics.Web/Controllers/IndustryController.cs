using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueEconomics.Platform.Domain;
using BlueEconomics.Platform.Infrastructure;

namespace BlueEconomics.Web.Controllers
{
    public class IndustryController : ApiController
    {
        private readonly BlueDbContext context;

        public IndustryController()
        {
            context=new BlueDbContext();
        }

        // GET api/values
        public List<Industry> Get()
        {
            return context.Industries.OrderBy(i => i.Name).ToList();
        }

        // GET api/values/5
        public Industry Get(int id)
        {
            return context.Industries.FirstOrDefault(i => i.Id == id);
        }
    }
}