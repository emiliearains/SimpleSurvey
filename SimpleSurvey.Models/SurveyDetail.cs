using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class SurveyDetail
    {
        [Required]
        [MinLength(5, ErrorMessage = "Minimum survey title length is at least 5 characters.")]
        [MaxLength(100, ErrorMessage = "Maximum survey title length is 100 characters.")]
        public string SurveyTitle { get; set; }

        [MinLength(5, ErrorMessage = "Minimum survey description length is at least 5 characters.")]
        [MaxLength(1000, ErrorMessage = "Maximum survey description length is 100 characters.")]
        public string SurveyDescription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
