using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueEconomics.Data.XML.Reader
{
    public class Industry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }



        public static List<Industry> GetAll()
        {
            var industries = new List<Industry>();
            industries.Add(new Industry(){ Code = 01, Name = "Others" });

            industries.Add(new Industry(){ Code = 11, Name = "Management Occupations"});
            industries.Add(new Industry(){ Code = 13, Name = "Business and Financial Operations Occupations"});
            industries.Add(new Industry(){ Code = 15, Name = "Computer and Mathematical Occupations"});

            industries.Add(new Industry(){ Code = 17, Name = "Architecture and Engineering Occupations"});
            
            industries.Add(new Industry(){ Code = 19, Name = "Life, Physical, and Social Science Occupations"});
            industries.Add(new Industry(){ Code = 21, Name = "Community and Social Service Occupations"});

            industries.Add(new Industry(){ Code = 23, Name = "Legal Occupations"});
            industries.Add(new Industry(){ Code = 25, Name = "Education, Training, and Library Occupations"});

            industries.Add(new Industry(){ Code = 27, Name = "Arts, Design, Entertainment, Sports, and Media Occupations"});

            industries.Add(new Industry(){ Code = 29, Name = "Healthcare Practitioners and Technical Occupations"});

            industries.Add(new Industry(){ Code = 31, Name = "Healthcare Support Occupations"});

            industries.Add(new Industry(){ Code = 33, Name = "Protective Service Occupations"});

            industries.Add(new Industry(){ Code = 35, Name = "Food Preparation and Serving Related Occupations"});

            industries.Add(new Industry(){ Code = 37, Name = "Building and Grounds Cleaning and Maintenance Occupations"});
            
            industries.Add(new Industry(){ Code = 39, Name = "Personal Care and Service Occupations"});
            industries.Add(new Industry(){ Code = 41, Name = "Sales and Related Occupations"});
            industries.Add(new Industry(){ Code = 43, Name = "Office and Administrative Support Occupations"});
            industries.Add(new Industry(){ Code = 45, Name = "Farming, Fishing, and Forestry Occupations"});
            industries.Add(new Industry(){ Code = 47, Name = "Construction and Extraction Occupations"});
            industries.Add(new Industry(){ Code = 49, Name = "Installation, Maintenance, and Repair Occupations"});
          
            industries.Add(new Industry(){ Code = 51, Name = "Production Occupations"});

            industries.Add(new Industry(){ Code = 53, Name = "Transportation and Material Moving Occupations"});
            industries.Add(new Industry(){ Code = 55, Name = "Military Specific Occupations"});

            return industries;
        }
    }
}
