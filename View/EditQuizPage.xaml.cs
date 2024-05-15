using Quiz_Game.Model;
using Quiz_Game.Service;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace Quiz_Game.View
{
    public partial class EditQuizPage : ContentPage
    {
        private Quiz _quiz;
        private QuizService _quizService = new QuizService();
        private List<Entry> questionEntries = new List<Entry>();
        private List<List<Entry>> answerEntries = new List<List<Entry>>();

        public EditQuizPage(Quiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;
            DisplayQuizForEditing();
        }

        private void DisplayQuizForEditing()
        {
            // Display the quiz title
            Label quizTitle = new Label
            {
                Text = _quiz.Subject,
                HorizontalOptions = LayoutOptions.Center,
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
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                };

                StackLayout questionTextLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
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

                Entry questionEntry = new Entry
                {
                    Text = quizDetails.QuestionEntry,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromHex("#0000000"),
                    Margin = new Thickness(0, 10, 0, 10)
                };
                questionTextLayout.Children.Add(questionEntry);
                questionEntries.Add(questionEntry); // Store the Entry field

                questionLayout.Children.Add(questionTextLayout);

                List<Entry> answerEntriesForQuestion = new List<Entry>();
                for (int i = 0; i < quizDetails.AnswerEntry.Count; i++)
                {
                    StackLayout answerLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center
                    };

                    CheckBox answerCheckbox = new CheckBox
                    {
                        IsChecked = quizDetails.AnswerCheckbox[i],
                        IsEnabled = true
                    };
                    answerLayout.Children.Add(answerCheckbox);

                    Entry answerEntry = new Entry
                    {
                        Text = quizDetails.AnswerEntry[i],
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#0000000"),
                    };
                    answerLayout.Children.Add(answerEntry);
                    answerEntriesForQuestion.Add(answerEntry); // Store the Entry field

                    questionLayout.Children.Add(answerLayout);
                }

                answerEntries.Add(answerEntriesForQuestion); // Store the list of Entry fields for this question

                QuizLayout.Children.Add(questionLayout);
                questionNumber++;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            // Update the _quiz object with the new values from the Entry fields
            for (int i = 0; i < _quiz.Questions.Count; i++)
            {
                _quiz.Questions[i].QuestionEntry = questionEntries[i].Text;
                for (int j = 0; j < _quiz.Questions[i].AnswerEntry.Count; j++)
                {
                    _quiz.Questions[i].AnswerEntry[j] = answerEntries[i][j].Text;
                }
            }

            // Save the changes to the quiz
            _quizService.UpdateQuiz(_quiz);

            // Show a confirmation message
            await DisplayAlert("Quiz Saved", "The quiz has been successfully saved.", "OK");

            // Navigate back to the previous page
            await Navigation.PopAsync();


        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
