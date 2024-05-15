
namespace Quiz_Game.View;

public partial class StudentDashboardPage : ContentPage
{
	public StudentDashboardPage()
	{
		InitializeComponent();
	}
    
    
    private void onClickLeaderboard(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LeaderboardPage());
    }
    

    private async void OnStartQuizClicked(object sender, EventArgs e)
    {
        // Navigate to StudentProfilePage with the logged-in user's data
        await Navigation.PushAsync(new StartQuizPage());
    }
    private async void OnProfileButtonClicked(object sender, EventArgs e)
    {
        // Navigate to StudentProfilePage with the logged-in user's data
        await Navigation.PushAsync(new StudentProfilePage(LoginPage.LoggedInUser));
    }
    
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    } 
}
