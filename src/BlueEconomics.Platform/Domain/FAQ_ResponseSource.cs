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
            FAQ_QuestionAssignments = new HashSet<FAQ_QuestionAssignment>();
        }

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Organization { get; set; }

        // Navigational property
        public virtual HashSet<FAQ_QuestionAssignment> FAQ_QuestionAssignments { get; set; }

    }
}
