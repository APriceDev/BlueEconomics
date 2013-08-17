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
            FAQ_QuestionAssignments = new HashSet<FAQ_QuestionAssignment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }

        // Navigational property
        public virtual HashSet<FAQ_QuestionAssignment> FAQ_QuestionAssignments { get; set; }

        // Other side of many-to-many between FAQ_Question and FAQ_Response entities
        public virtual HashSet<FAQ_Response> FAQ_Responses { get; set; }

        // Foreign key into Occupations table
        public int OccupationId { get; set; }
        public Occupation Occupation { get; set; }

        // Foreign key into FAQ_QuestionSource table
        public int FAQ_QuestionSourceId { get; set; }
        public FAQ_QuestionSource FAQ_QuestionSource { get; set; }

    }

}
