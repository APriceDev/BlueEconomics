using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlueEconomics.Platform.Domain
{
    public class FAQ_QuestionSource
    {
        public FAQ_QuestionSource()
        {
            FAQ_Questions = new HashSet<FAQ_Question>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Generate SQL DDL for foreign key  relationship with FAQ_Question entity
        public ICollection<FAQ_Question> FAQ_Questions { get; set; }
     }
}
