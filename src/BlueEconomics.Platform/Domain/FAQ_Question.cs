using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlueEconomics.Platform.Domain
{
    public class FAQ_Question
    {

        public FAQ_Question()
        {
            FAQ_Responses = new HashSet<FAQ_Response>();
        }

        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        // Associated with zero or more FAQ_Responses
        public virtual HashSet<FAQ_Response> FAQ_Responses { get; set; }

        // Foreign key into Occupations table
        public int OccupationId { get; set; }
        public Occupation Occupation { get; set; }
        
    }
}
