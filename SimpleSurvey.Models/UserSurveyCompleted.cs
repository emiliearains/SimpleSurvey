using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class UserSurveyCompleted
    {
        public int UserSurveyId { get; set; }
        public string UserName { get; set; }
        public DateTime DateCompleted { get; set; }

    }
}
