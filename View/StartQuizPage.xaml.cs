using Quiz_Game.Model;
using Quiz_Game.Service;
namespace Quiz_Game.View;

public partial class StartQuizPage : ContentPage
{
    
    private QuizService _quizService = new QuizService();
    public StartQuizPage()
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
        await Navigation.PushAsync(new StartQuizDetailPage(quiz));
    }
}