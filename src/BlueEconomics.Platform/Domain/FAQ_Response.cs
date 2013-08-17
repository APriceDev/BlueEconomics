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
        [Required]
        public string Text { get; set; }

        // Generate SQL DDL for foreign key  relationship with FAQ_ResponseSource entity
        public int FAQ_ResponseSourceId { get; set; }
        public FAQ_ResponseSource FAQ_ResponseSource { get; set; }

        // Other side of many-to-many between FAQ_Question and FAQ_Response entities  
        public virtual HashSet<FAQ_Question> FAQ_Questions { get; set; }

    }
}
