using System.Collections.Generic;
using Phase04.Model;
using System.Linq;
using System;

namespace Phase04
{
    public class Processor
    {
        private Database database;

        public Processor(Database database)
        {
            this.database = database;
        }

        public IEnumerable<Student> CalculateTopStudents(int numberOfStudents)
        {
            var students = database.GetStudents();
            var scores = database.GetScores();
            return students.GroupJoin(scores, students => students.StudentNumber, scores => scores.StudentNumber,
                (students, studentsJoinScores) => new Student(students.FirstName, students.LastName, studentsJoinScores.Average(c => c.Score)))
                .OrderByDescending(s => s.Score)
                .Take(numberOfStudents);

        }
    }
}