using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public class UserAnswer
    {
        public int UserAnswerId { get; set; }

        [ForeignKey(nameof(UserSurvey))]
        public int UserSurveyId { get; set; }
        public virtual UserSurvey UserSurvey { get; set; }

        [ForeignKey(nameof(QuestionChoice))]
        public int QuestionChoiceId { get; set; }
        public virtual QuestionChoice QuestionChoice { get; set; }
    }
}
