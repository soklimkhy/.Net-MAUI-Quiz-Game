using Quiz_Game.Model;
using SQLite;
using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace Quiz_Game.View;

public partial class RegisterPage : ContentPage
{
    private SQLiteConnection conn;
    public RegisterPage()
	{
		InitializeComponent();
        string libFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        string dbPath = Path.Combine(libFolder, "QuizGameDB.db3");
        conn = new SQLiteConnection(dbPath);
        conn.CreateTable<Student>(); 
    }
    private async void onRegisterButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(usernameRegister.Text) || string.IsNullOrEmpty(passwordRegister.Text))
        {
            await DisplayAlert("Error", "Username or password cannot be empty", "OK");
            return;
        }

        var existingUser = conn.Table<Student>().Where(u => u.Username == usernameRegister.Text).FirstOrDefault();

        if (existingUser != null)
        {
            await DisplayAlert("Error", "Username already exists", "OK");
            return;
        }

        var newUser = new Student
        {
            Username = usernameRegister.Text,
            Password = passwordRegister.Text,
            BirthDate = datePickerRegister.Date
        };

        conn.Insert(newUser);
        await DisplayAlert("Success", "Registration successful", "OK");
        await Navigation.PushAsync(new LoginPage());
        
    }

    private async void OnLoginLabelTapped(object sender, EventArgs e)
    {
        // Navigate back to login page
        await Navigation.PopAsync();
    }
}