using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlueEconomics.Platform.Domain
{
    public class FAQ_Response 
    {

        public FAQ_Response()
        {
            FAQ_Questions = new HashSet<FAQ_Question>();
        }

        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        // Associated with zero or more FAQ_Questions
        public virtual HashSet<FAQ_Question> FAQ_Questions { get; set; }
  
    }
}
