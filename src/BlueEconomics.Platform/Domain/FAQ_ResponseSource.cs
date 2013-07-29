using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlueEconomics.Platform.Domain
{
    public class FAQ_ResponseSource 
    {
         public FAQ_ResponseSource()
        {
            FAQ_Responses = new HashSet<FAQ_Response>();
        }

        [Key]
        public int Id { get; set; }
        public string Organization { get; set; }

        // Associated with zero or more FAQ_Responses
        public ICollection<FAQ_Response> FAQ_Responses { get; set; }
    }
}
