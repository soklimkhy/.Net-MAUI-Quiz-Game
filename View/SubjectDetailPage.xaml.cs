using Quiz_Game.Model;
using Quiz_Game.Service;

namespace Quiz_Game.View;

public partial class SubjectDetailPage : ContentPage
{
    private Quiz _quiz;
    private QuizService _quizService = new QuizService();

    public SubjectDetailPage(Quiz quiz)
    {
        InitializeComponent();
        _quiz = quiz;
        DisplayQuiz();
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
                    IsEnabled = false
                };
                answerLayout.Children.Add(answerCheckbox);

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

            QuizLayout.Children.Add(questionLayout);
            questionNumber++;
        }

    }
    private async void onEditButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the EditQuizPage when the Edit button is clicked
        // You need to create an EditQuizPage and pass the current quiz to it
        await Navigation.PushAsync(new EditQuizPage(_quiz));
    }

    private async void onDeleteButtonClicked(object sender, EventArgs e)
    {
        _quizService.DeleteQuiz(_quiz.ID);
        await DisplayAlert("Success", "Quiz deleted successfully!", "OK");
        await Navigation.PushAsync(new TeacherDashboardPage());
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}

