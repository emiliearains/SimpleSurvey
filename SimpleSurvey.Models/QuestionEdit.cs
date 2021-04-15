using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class QuestionEdit
    {
        [Required]
        [MinLength(5, ErrorMessage = "Minimum question length is at least 5 characters.")]
        [MaxLength(200, ErrorMessage = "Maximum question length is 200 characters.")]
        public string QuestionText { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum question type is 200 characters.")]
        public string QuestionType { get; set; }
    }
}
