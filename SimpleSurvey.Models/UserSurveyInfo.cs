using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class UserSurveyInfo
    {
        public int UserSurveyId { get; set; }
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyDescription { get; set; }
    }
}
