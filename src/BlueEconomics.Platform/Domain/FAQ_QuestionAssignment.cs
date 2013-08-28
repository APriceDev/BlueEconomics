using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace BlueEconomics.Platform.Domain 
{
    public class FAQ_QuestionAssignment
    {
        public FAQ_QuestionAssignment()
        {
            IsAnswered = false;
            dateAssigned = DateTime.Now;
            dateAnswered = DateTime.MaxValue;
        }

        [Key]
        public int Id { get; set; }
 
        [Required]
        public DateTime dateAssigned { get; set; }
        public DateTime dateAnswered { get; set; }

        public bool IsAnswered { get; set; }

        // Foreign key into FAQ_ResponseSource
        public int FAQ_ResponseSourceId { get; set; }
        public FAQ_ResponseSource FAQ_ResponseSource { get; set; }

        // Foreign key into FAQ_Question table
        public int FAQ_QuestionId { get; set; }
        public FAQ_Question FAQ_Question { get; set; }
    }
}
