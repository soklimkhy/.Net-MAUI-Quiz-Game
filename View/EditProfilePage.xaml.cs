using Quiz_Game.Model;
using Quiz_Game.Service;

namespace Quiz_Game.View;


public partial class EditProfilePage : ContentPage
{
    private Student _student;

    public EditProfilePage(Student student)
    {
        InitializeComponent();

        // Store the student object
        _student = student;

        // Use the student object to populate fields
        usernameEntry.Text = _student.Username;
        birthdatePicker.Date = _student.BirthDate;
    }

    private DatabaseService dbService = new DatabaseService();

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Update the student object
        _student.Username = usernameEntry.Text;
        _student.BirthDate = birthdatePicker.Date;


        // Update the student in the database
        bool updateSuccessful = dbService.UpdateStudent(_student);
        if (updateSuccessful)
        {
            // Navigate back to the previous page
            await DisplayAlert("Good", "Update Successfully", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            // Show an error message
            await DisplayAlert("Error", "Failed to update profile. Please try again.", "OK");
        }
    }


    

}
