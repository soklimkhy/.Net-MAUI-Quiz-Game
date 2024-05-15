using Quiz_Game.Model;
using SQLite;
using System;
using System.IO;

namespace Quiz_Game.Service
{
    public class DatabaseService
    {
        private SQLiteConnection conn;

        public DatabaseService()
        {
            string libFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            string dbPath = Path.Combine(libFolder, "QuizGameDB.db3");
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Student>();
        }

        public bool UpdateStudent(Student student)
        {
            try
            {
                conn.Update(student);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

  
    }
}
