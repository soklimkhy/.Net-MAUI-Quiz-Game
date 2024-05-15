using System.Collections.Generic;
using Newtonsoft.Json;

namespace Quiz_Game.Model
{
    

    public class Quiz
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public List<QuizDetails> Questions { get; set; }
    }
    public class QuizDetails   
    {
        public string QuestionEntry { get; set; }
        public List<string> AnswerEntry { get; set; }
        public List<bool> AnswerCheckbox { get; set; }

    }


}
