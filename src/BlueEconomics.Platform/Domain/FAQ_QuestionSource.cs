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

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }

    }
}
