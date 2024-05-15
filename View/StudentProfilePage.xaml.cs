using Quiz_Game.Model;
namespace Quiz_Game.View;

public partial class StudentProfilePage : ContentPage
{
    private Student _student;

    public StudentProfilePage(Student student)
    {
        InitializeComponent();

        // Store the student object
        _student = student;

        // Use the student object to populate fields
        // For example:
        usernameLabel.Text = _student.Username;
        birthdateLabel.Text = _student.BirthDate.ToString();
    }
    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage(_student));
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}