using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public class QuestionChoice
    {
        public int QuestionChoiceId { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string QuestionChoiceText { get; set; }
        public int QuestionChoiceValue { get; set; }
    }
}
