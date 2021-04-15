using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
