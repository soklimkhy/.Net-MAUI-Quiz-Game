using Quiz_Game.Model;
using SQLite;
using System;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Quiz_Game.View
{
    public partial class LeaderboardPage : ContentPage
    {
        private SQLiteConnection conn;

        public LeaderboardPage()
        {
            InitializeComponent();
            string libFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            string dbPath = Path.Combine(libFolder, "QuizGameDB.db3");
            conn = new SQLiteConnection(dbPath);
            DisplayLeaderboard();
        }

        private void DisplayLeaderboard()
        {
            var subjects = new[] { "Maths", "Geography", "History",  "Biology" };

            foreach (var subject in subjects)
            {
                var leaderboardLabel = new Label
                {
                    Text = $"{subject} Leaderboard",
                    FontSize = 20,
                    TextColor = Color.FromHex("#0000000")
                };
                LeaderboardStack.Children.Add(leaderboardLabel);

                var students = conn.Table<Student>().ToList();
                var topStudents = students.OrderByDescending(s => GetScore(s, subject)).Take(20).ToList();

                for (int i = 0; i < topStudents.Count; i++)
                {
                    var student = topStudents[i];
                    var score = GetScore(student, subject);
                    var studentLabel = new Label
                    {
                        Text = $"{i + 1}. {student.Username}, Score: {score}",
                        TextColor = Color.FromHex("#0000000")
                    };
                    LeaderboardStack.Children.Add(studentLabel);
                }
            }
        }

        private int GetScore(Student student, string subject)
        {
            switch (subject)
            {
                case "Maths":
                    return student.ScoreMaths;
                case "Geography":
                    return student.ScoreGeography;
                case "History":
                    return student.ScoreHistory;
                case "RandomQuiz":
                    return student.ScoreRandomQuiz;
                case "Biology":
                    return student.ScoreBiology;
                default:
                    return 0;
            }
        }
    }
}
