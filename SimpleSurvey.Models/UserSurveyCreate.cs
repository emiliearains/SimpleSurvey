using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class UserSurveyCreate
    {
        public int SurveyId { get; set; }
        public Guid UserId { get; set; }
    }
}
