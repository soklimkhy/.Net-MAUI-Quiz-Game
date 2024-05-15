using Quiz_Game.Model;
using SQLite;
using System;
using System.IO;
using Microsoft.Maui.Controls;
namespace Quiz_Game.View
{

    public partial class LoginPage : ContentPage
    {
        private SQLiteConnection conn;
        public static Student LoggedInUser { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            string libFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            string dbPath = Path.Combine(libFolder, "QuizGameDB.db3");
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Student>();
        }

        private async void onLoginButtonClicked(object sender, EventArgs e)
        {
            var user = conn.Table<Student>().Where(u => u.Username == usernameLogin.Text && u.Password == passwordLogin.Text).FirstOrDefault();

            if (usernameLogin.Text == "ITSTEP" && passwordLogin.Text == "ITSTEP")
            {
                // Navigate to TeacherDashboardPage
                await Navigation.PushAsync(new TeacherDashboardPage());
            }
            else if (user != null)
            {
                // Store the logged-in user's data
                LoggedInUser = user;

                // Navigate to StudentDashboardPage
                await Navigation.PushAsync(new StudentDashboardPage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password", "OK");
            }
            
        }

        private void OnRegisterLabelTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
