using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuestionsEntity
    {
        public int lessonId { get; set; }
        public string prompt { get; set; }
        public string option_a { get; set; }
        public string option_b { get; set; }
        public string option_c { get; set; }
        public string option_d { get; set; }
        public string option_e { get; set; }
        public int score { get; set; }
        public string type { get; set; }
        public int sort_order { get; set; }
        public string user_create { get; set; }
        public string user_update { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }
}
