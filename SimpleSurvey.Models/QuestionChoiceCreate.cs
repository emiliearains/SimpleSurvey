using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class QuestionChoiceCreate
    {
        
        //[ForeignKey(nameof(Question))]
        //[Required]
        //public int QuestionId { get; set; }
        //public virtual Question Question { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The maximum length is 200 characters. Reduce question choice text.")]
        public string QuestionChoiceText { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Select a value between 1 and 5.")]
        public int QuestionChoiceValue { get; set; }
    }
}
