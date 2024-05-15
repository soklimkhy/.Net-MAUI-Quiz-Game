using System.Collections.Generic;
using Newtonsoft.Json;
using Quiz_Game.Model; 

namespace Quiz_Game.View;

public partial class TeacherDashboardPage : ContentPage
{
	public TeacherDashboardPage()
	{
		InitializeComponent();

    }
    private async void OnCreateQuizClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateQuizPage());
    }


    public Quiz LoadQuiz()
    {
        // Get the path to the AppData directory
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Combine the AppData path with the filename
        string filePath = Path.Combine(appDataPath, "quiz.json");

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            // If the file doesn't exist, return a new Quiz object
            return new Quiz();
        }

        // Read the JSON string from the file
        string json = File.ReadAllText(filePath);

        // Deserialize the JSON string back into a Quiz object
        Quiz quiz = JsonConvert.DeserializeObject<Quiz>(json);

        return quiz;
    }

    //OnSubjectClicked
    private async void OnSubjectClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new SubjectPage());
    }
    private void onClickLeaderboard(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LeaderboardPage());
    }



    private async void onTeacherProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeacherProfilePage());

    } 
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    } 


}