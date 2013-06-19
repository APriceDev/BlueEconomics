using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Web;
using BlueEconomics.Web.Models;

namespace BlueEconomics.Web.Context
{
    public class BlueDbContext:DbContext
    {
        public BlueDbContext():base(string.Format("Data Source={0}",Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"App_Data\\BlueDB.sdf")))
        {
            
        }

        public IDbSet<Industry> Industries { get; set; }

        public IDbSet<Occupation> Occupations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //TODO:

            //modelBuilder.Entity<Industry>().HasKey(i => i.Id).HasRequired(i => i.Name);

            //modelBuilder.Entity<Occupation>().HasKey(o => o.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}