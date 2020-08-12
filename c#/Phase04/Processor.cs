using System.Collections.Generic;
using Phase04.Model;
using System.Linq;
using System;

namespace Phase04
{
    public class Processor
    {
        private Database database { get; set; }

        public Processor(Database database)
        {
            this.database = database;
        }

        public IEnumerable<Student> CalculateTopStudents(int numberOfStudents)
        {
            var students = database.GetStudents();
            var scores = database.GetScores();
            return students.GroupJoin(scores, st => st.StudentNumber, sc => sc.StudentNumber,
                (st, ssc) => new Student(st.FirstName, st.LastName, ssc.Average(c => c.Score)))
                .OrderByDescending(s => s.Score)
                .Take(numberOfStudents);

        }



    }
}