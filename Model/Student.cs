using SQLite;

namespace Quiz_Game.Model
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string? Username { get; set; }

        public string? Password { get; set; }

        public DateTime BirthDate { get; set; }
       


        // Score of Subjects
        public int ScoreMaths { get; set; }
        public int ScoreGeography { get; set; }
        public int ScoreHistory { get; set; }
        public int ScoreRandomQuiz { get; set; }
        public int ScoreBiology { get; set; }
    }
}
