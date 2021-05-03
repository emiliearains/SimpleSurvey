using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Data
{
    public class UserSurvey
    {
        public int UserSurveyId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Survey))]
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }

        public DateTime DateCompleted { get; set; }
    }
}
