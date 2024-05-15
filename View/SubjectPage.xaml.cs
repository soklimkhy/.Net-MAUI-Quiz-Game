using Quiz_Game.Model;
using Quiz_Game.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Quiz_Game.View
{
    public partial class SubjectPage : ContentPage
    {
        private QuizService _quizService = new QuizService();

        public SubjectPage()
        {
            InitializeComponent();
            CheckQuiz();
        }

        private void CheckQuiz()
        {
            List<Quiz> quizzes = _quizService.GetQuizzes();

            foreach (Quiz quiz in quizzes)
            {
                // If a quiz has been created, display a button with the subject title
                Button subjectButton = new Button
                {
                    Text = quiz.Subject,
                    TextColor = Color.FromHex("#ffffff"),
                    BackgroundColor = Color.FromHex("#158FFF"),
                    WidthRequest = 226,
                    HeightRequest = 41,
                    Margin = new Thickness(0, 20, 0, 20),
                    CornerRadius = 0
                };

                subjectButton.Clicked += (sender, e) => SubjectButtonClicked(sender, e, quiz);


                SubjectPageCheck.Children.Add(subjectButton);
            }
        }

        private async void SubjectButtonClicked(object sender, EventArgs e, Quiz quiz)
        {
            // Navigate to the SubjectDetailPage when the subject button is clicked
            await Navigation.PushAsync(new SubjectDetailPage(quiz));
        }
    }
}
