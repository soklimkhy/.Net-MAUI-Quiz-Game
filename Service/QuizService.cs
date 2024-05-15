using Quiz_Game.Model;
using Newtonsoft.Json;

namespace Quiz_Game.Service
{
    public class QuizService
    {
        private string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "quiz.json");

        // ...

        public List<Quiz> GetQuizzes()
        {
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                return JsonConvert.DeserializeObject<List<Quiz>>(json);
            }

            return new List<Quiz>();
        }


        public void SaveQuiz(Quiz quiz)
        {
            List<Quiz> quizzes = GetQuizzes();
            quizzes.Add(quiz);

            string json = JsonConvert.SerializeObject(quizzes);
            File.WriteAllText(_path, json);
        }

        public void EditQuiz(Quiz updatedQuiz)
        {
            List<Quiz> quizzes = GetQuizzes();
            int index = quizzes.FindIndex(q => q.ID == updatedQuiz.ID); // Assuming each quiz has a unique ID
            if (index != -1)
            {
                quizzes[index] = updatedQuiz;
            }

            string json = JsonConvert.SerializeObject(quizzes);
            File.WriteAllText(_path, json);
        }

        public void UpdateQuiz(Quiz updatedQuiz)
        {
            EditQuiz(updatedQuiz); // UpdateQuiz and EditQuiz are essentially the same in this context
        }

        public void DeleteQuiz(int quizID)
        {
            List<Quiz> quizzes = GetQuizzes();
            quizzes.RemoveAll(q => q.ID == quizID); // Assuming each quiz has a unique ID

            string json = JsonConvert.SerializeObject(quizzes);
            File.WriteAllText(_path, json);
        }
    }


}
