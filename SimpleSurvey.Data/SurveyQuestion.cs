using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public class SurveyQuestion
    {
        [Key]
        public int SurveyQuestionId { get; set; }
        
        [ForeignKey(nameof(Survey))]
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
