namespace Quiz_Game.View;

public partial class TeacherProfilePage : ContentPage
{
	public TeacherProfilePage()
	{
		InitializeComponent();
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}