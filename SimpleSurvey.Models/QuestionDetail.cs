using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class QuestionDetail
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        [Display(Name = "Question Choices")]
        public List<QuestionChoiceDetails> QuestionChoiceText { get; set; }
        public int SelectedChoice { get; set; }
    }
}
