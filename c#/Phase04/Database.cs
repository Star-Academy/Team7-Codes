using System.Collections.Generic;
using Phase04.Model;
using System.IO;
using static Newtonsoft.Json.JsonConvert;

namespace Phase04
{
    public class Database
    {
        private const string StudentsFile = "Resources/Students.json";
        private const string ScoresFile = "Resources/Scores.json";
        public List<Student> GetStudents() 
        {
            string studentsJson = File.ReadAllText(StudentsFile);
            var students = DeserializeObject<List<Student>>(studentsJson);
            return students;
        }
        public List<LessonScore> GetScores() 
        {
            string scoresJson = File.ReadAllText(ScoresFile);
            var scores = DeserializeObject<List<LessonScore>>(scoresJson);
            return scores;   
        }
    }
}