using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class UserSurveyAssign
    {
        public Guid UserId { get; set; }

        public int SurveyId { get; set; }
    }
}
