using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }
        [Required]
        public string SurveyTitle { get; set; }
        public string SurveyDescription { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
