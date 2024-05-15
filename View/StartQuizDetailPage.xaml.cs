using Quiz_Game.Model;
using Quiz_Game.Service;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using SQLite;

namespace Quiz_Game.View
{
    public partial class StartQuizDetailPage : ContentPage
    {
        private Quiz _quiz;
        private QuizService _quizService = new QuizService();
        private List<List<CheckBox>> studentAnswerCheckboxes = new List<List<CheckBox>>();
        private SQLiteConnection conn;


        public StartQuizDetailPage(Quiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;
            DisplayQuiz();
            string libFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            string dbPath = Path.Combine(libFolder, "QuizGameDB.db3");
            conn = new SQLiteConnection(dbPath);

        }

        private void DisplayQuiz()
        {
            // Display the quiz title
            Label quizTitle = new Label
            {
                Text = _quiz.Subject,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            QuizLayout.Children.Add(quizTitle);

            int questionNumber = 1;
            // Display each question and its answers
            foreach (QuizDetails quizDetails in _quiz.Questions)
            {
                StackLayout questionLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                };

                StackLayout questionTextLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                };

                Label questionNumberLabel = new Label
                {
                    Text = "Question " + questionNumber,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromHex("#0000000"),
                    WidthRequest = 100
                };
                questionTextLayout.Children.Add(questionNumberLabel);

                Label questionLabel = new Label
                {
                    Text = quizDetails.QuestionEntry,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromHex("#0000000"),
                    Margin = new Thickness(0, 10, 0, 10)
                };
                questionTextLayout.Children.Add(questionLabel);

                questionLayout.Children.Add(questionTextLayout);

                List<CheckBox> studentAnswerCheckboxesForQuestion = new List<CheckBox>();
                for (int i = 0; i < quizDetails.AnswerEntry.Count; i++)
                {
                    StackLayout answerLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center
                    };

                    CheckBox studentAnswerCheckbox = new CheckBox
                    {
                        IsChecked = false,
                        IsEnabled = true
                    };
                    answerLayout.Children.Add(studentAnswerCheckbox);
                    studentAnswerCheckboxesForQuestion.Add(studentAnswerCheckbox); // Store the CheckBox

                    Label answerLabel = new Label
                    {
                        Text = quizDetails.AnswerEntry[i],
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#0000000"),
                    };
                    answerLayout.Children.Add(answerLabel);

                    questionLayout.Children.Add(answerLayout);
                }

                studentAnswerCheckboxes.Add(studentAnswerCheckboxesForQuestion); // Store the list of CheckBoxes for this question

                QuizLayout.Children.Add(questionLayout);
                questionNumber++;
            }
        }

        private async void onSumbitButtonClicked(object sender, EventArgs e)
        {
            int studentQuizScore = 0;

            // Compare the student's answers with the correct answers
            for (int i = 0; i < _quiz.Questions.Count; i++)
            {
                for (int j = 0; j < _quiz.Questions[i].AnswerCheckbox.Count; j++)
                {
                    if (studentAnswerCheckboxes[i][j].IsChecked && _quiz.Questions[i].AnswerCheckbox[j])
                    {
                        studentQuizScore++;
                    }
                }
            }

            // Update the student's score in the database
            var student = LoginPage.LoggedInUser;
            switch (_quiz.Subject)
            {
                case "Maths":
                    student.ScoreMaths = studentQuizScore;
                    break;
                case "Geography":
                    student.ScoreGeography = studentQuizScore;
                    break;
                case "History":
                    student.ScoreHistory = studentQuizScore;
                    break;
                case "RandomQuiz":
                    student.ScoreRandomQuiz = studentQuizScore;
                    break;
                case "Biology":
                    student.ScoreBiology = studentQuizScore;
                    break;
            }
            conn.Update(student);

            // Display the student's score
            await DisplayAlert("Quiz Score", "You scored " + studentQuizScore + " out of 20", "OK");
        }



        private async void OnBackClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
