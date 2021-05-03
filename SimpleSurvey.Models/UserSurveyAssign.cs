using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class UserSurveyAssign
    {
        public List<string> UserIds { get; set; }

        public int SurveyId { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
    }
}
