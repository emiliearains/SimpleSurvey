using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public enum QuestionType
    {
        OpenEnded,          // text box
        MultipleChoice,     // check box or radio buttons or drop down
        OrdinalScale,       // naturally occuring order and unknown difference
        IntervalScale,      // there is order and difference is meaningful, no true zero
        RatioScale          // differences between measurements, true zero exists
    }
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "The maximum length is 500 characters. Please reduce the question text.")]
        public string QuestionText { get; set; }

        public int QuestionType { get; set; }

        [Required]
        public bool IsActive { get; set; }


    }
}
