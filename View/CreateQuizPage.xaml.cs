using Quiz_Game.Model;
using Quiz_Game.Service;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


namespace Quiz_Game.View
{
    public partial class CreateQuizPage : ContentPage
    {
        

        
        public CreateQuizPage()
        {
            InitializeComponent();
            
        }
        public async void OnSaveQuizClicked(object sender, EventArgs e)
        {
            // Checking subject select or not   
            // Count the number of subjects selected
            int subjectCount = 0;
            if (MathCheckbox.IsChecked) subjectCount++;
            if (BiologyCheckbox.IsChecked) subjectCount++;
            if (GeographyCheckbox.IsChecked) subjectCount++;
            if (HistoryCheckbox.IsChecked) subjectCount++;

            // Validate the number of subjects selected
            if (subjectCount == 0)
            {
                DisplayAlert("Validation Error", "Please select at least one subject.", "OK");
                return;
            }
            else if (subjectCount > 2)
            {
                DisplayAlert("Validation Error", "Please select no more than two subjects.", "OK");
                return;
            }



            // Validate that all question entries are filled in
            Entry[] questionEntries = new Entry[] { questionEntry1, questionEntry2, questionEntry3, questionEntry4, questionEntry5, questionEntry6, questionEntry7, questionEntry8, questionEntry9, questionEntry10,
                questionEntry11, questionEntry12, questionEntry13, questionEntry14, questionEntry15, questionEntry16, questionEntry17, questionEntry18, questionEntry19, questionEntry20 };

            foreach (Entry questionEntry in questionEntries)
            {
                if (string.IsNullOrWhiteSpace(questionEntry.Text))
                {
                    DisplayAlert("Validation Error", "Please fill in all question fields.", "OK");
                    return;
                }
            }
            // Validate that at least one answer checkbox is checked for each question
            CheckBox[][] answerCheckboxes = new CheckBox[][] {
            new CheckBox[] { answerCheckbox11, answerCheckbox12, answerCheckbox13, answerCheckbox14 },
            new CheckBox[] { answerCheckbox21, answerCheckbox22, answerCheckbox23, answerCheckbox24 },
            new CheckBox[] { answerCheckbox31, answerCheckbox32, answerCheckbox33, answerCheckbox34 },
            new CheckBox[] { answerCheckbox41, answerCheckbox42, answerCheckbox43, answerCheckbox44 },
            new CheckBox[] { answerCheckbox51, answerCheckbox52, answerCheckbox53, answerCheckbox54 },
            new CheckBox[] { answerCheckbox61, answerCheckbox62, answerCheckbox63, answerCheckbox64 },
            new CheckBox[] { answerCheckbox71, answerCheckbox72, answerCheckbox73, answerCheckbox74 },
            new CheckBox[] { answerCheckbox81, answerCheckbox82, answerCheckbox83, answerCheckbox84 },
            new CheckBox[] { answerCheckbox91, answerCheckbox92, answerCheckbox93, answerCheckbox94 },
            new CheckBox[] { answerCheckbox101, answerCheckbox102, answerCheckbox103,answerCheckbox104},
            new CheckBox[] { answerCheckbox111, answerCheckbox112, answerCheckbox113,answerCheckbox114},
            new CheckBox[] { answerCheckbox121, answerCheckbox122, answerCheckbox123,answerCheckbox124},
            new CheckBox[] { answerCheckbox131, answerCheckbox132, answerCheckbox133,answerCheckbox134},
            new CheckBox[] { answerCheckbox141, answerCheckbox142, answerCheckbox143,answerCheckbox144},
            new CheckBox[] { answerCheckbox151, answerCheckbox152, answerCheckbox153,answerCheckbox154},
            new CheckBox[] { answerCheckbox161, answerCheckbox162, answerCheckbox163,answerCheckbox164},
            new CheckBox[] { answerCheckbox171, answerCheckbox172, answerCheckbox173,answerCheckbox174},
            new CheckBox[] { answerCheckbox181, answerCheckbox182, answerCheckbox183,answerCheckbox184},
            new CheckBox[] { answerCheckbox191, answerCheckbox192, answerCheckbox193,answerCheckbox194},
            new CheckBox[] { answerCheckbox201, answerCheckbox202, answerCheckbox203,answerCheckbox204},
            };

            foreach (CheckBox[] answerCheckboxGroup in answerCheckboxes)
            {
                bool atLeastOneChecked = false;
                foreach (CheckBox answerCheckbox in answerCheckboxGroup)
                {
                    if (answerCheckbox.IsChecked)
                    {
                        atLeastOneChecked = true;
                        break;
                    }
                }

                if (!atLeastOneChecked)
                {
                    DisplayAlert("Validation Error", "Please select at least one correct answer for each question.", "OK");
                    return;
                }
            }

            // Validate that all answer entries are filled in
            Entry[][] answerEntries = new Entry[][] {
            new Entry[] { answerEntry11, answerEntry12, answerEntry13, answerEntry14 },
            new Entry[] { answerEntry21, answerEntry22, answerEntry23, answerEntry24 },
            new Entry[] { answerEntry31, answerEntry32, answerEntry33, answerEntry34 },
            new Entry[] { answerEntry41, answerEntry42, answerEntry43, answerEntry44 },
            new Entry[] { answerEntry51, answerEntry52, answerEntry53, answerEntry54 },
            new Entry[] { answerEntry61, answerEntry62, answerEntry63, answerEntry64 },
            new Entry[] { answerEntry71, answerEntry72, answerEntry73, answerEntry74 },
            new Entry[] { answerEntry81, answerEntry82, answerEntry83, answerEntry84 },
            new Entry[] { answerEntry91, answerEntry92, answerEntry93, answerEntry94 },
            new Entry[] { answerEntry101, answerEntry102, answerEntry103, answerEntry104 },
            new Entry[] { answerEntry111, answerEntry112, answerEntry113, answerEntry114 },
            new Entry[] { answerEntry121, answerEntry122, answerEntry123, answerEntry124 },
            new Entry[] { answerEntry131, answerEntry132, answerEntry133, answerEntry134 },
            new Entry[] { answerEntry141, answerEntry142, answerEntry143, answerEntry144 },
            new Entry[] { answerEntry151, answerEntry152, answerEntry153, answerEntry154 },
            new Entry[] { answerEntry161, answerEntry162, answerEntry163, answerEntry164 },
            new Entry[] { answerEntry171, answerEntry172, answerEntry173, answerEntry174 },
            new Entry[] { answerEntry181, answerEntry182, answerEntry183, answerEntry184 },
            new Entry[] { answerEntry191, answerEntry192, answerEntry193, answerEntry194 },
            new Entry[] { answerEntry201, answerEntry202, answerEntry203, answerEntry204 }

            };

            foreach (Entry[] answerEntryGroup in answerEntries)
            {
                foreach (Entry answerEntry in answerEntryGroup)
                {
                    if (string.IsNullOrWhiteSpace(answerEntry.Text))
                    {
                        DisplayAlert("Validation Error", "Please fill in all answer fields.", "OK");
                        return;
                    }
                }
            }
            // Create a new Quiz object
            Quiz newQuiz = new Quiz();

            // Set the Subject of the quiz based on the selected subjects
            if (MathCheckbox.IsChecked)
                newQuiz.Subject = "Math";
            else if (BiologyCheckbox.IsChecked)
                newQuiz.Subject = "Biology";
            else if (GeographyCheckbox.IsChecked)
                newQuiz.Subject = "Geography";
            else if (HistoryCheckbox.IsChecked)
                newQuiz.Subject = "History";




            
            newQuiz.Questions = new List<QuizDetails>();


            for (int i = 1; i <= 20; i++)
            {
                QuizDetails newQuestion = new QuizDetails();

                Entry questionEntry = (Entry)this.FindByName("questionEntry" + i);

                newQuestion.QuestionEntry = questionEntry.Text;
                newQuestion.AnswerEntry = new List<string>();
                newQuestion.AnswerCheckbox = new List<bool>();

                for (int j = 1; j <= 4; j++)
                {
                    Entry answerEntry = (Entry)this.FindByName("answerEntry" + i + j);
                    newQuestion.AnswerEntry.Add(answerEntry.Text);

                    CheckBox answerCheckbox = (CheckBox)this.FindByName("answerCheckbox" + i + j);
                    newQuestion.AnswerCheckbox.Add(answerCheckbox.IsChecked);
                }

                newQuiz.Questions.Add(newQuestion);
            }
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "quiz.json");
            // Read the existing quizzes from the file
            List<Quiz> quizzes;
            if (File.Exists(path))
            {
                string existingJson = File.ReadAllText(path);
                quizzes = JsonConvert.DeserializeObject<List<Quiz>>(existingJson);
            }
            else
            {
                quizzes = new List<Quiz>();
            }

            // Set the ID of the new quiz
            newQuiz.ID = quizzes.Count > 0 ? quizzes.Max(q => q.ID) + 1 : 0;

            // Add the new quiz to the list
            quizzes.Add(newQuiz);

            // Serialize the updated list of quizzes
            string json = JsonConvert.SerializeObject(quizzes);
            
            File.WriteAllText(path, json);


            await DisplayAlert("Success", "Quiz saved successfully!", "OK");
            await Navigation.PushAsync(new TeacherDashboardPage());







        }















        private async void OnBackClicked(object sender, EventArgs e)
        {
                await Navigation.PopAsync();
        }
        
    }
}