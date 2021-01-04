using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizService.Models {
    public class Quiz {
        public int Id { get; set; }
        public List<Question> Questions { get; set; }
    }
}
