using System.Collections.Generic;
using Phase04.Model;
using System.Linq;
using System;

namespace Phase04
{
    public class Processor
    {
        public void PrintTopStudents(List<Student> students, List<LessonScore> scores, int numberOfStudents){
            var topStudents =  students.GroupJoin(scores, st => st.StudentNumber, sc => sc.StudentNumber, 
            (st, ssc) => new {
                firstName = st.FirstName, lastName = st.LastName, averageScore = ssc.Sum(c => c.Score) / ssc.Count()
            }).
            OrderByDescending(s => s.averageScore).
            Take(numberOfStudents);

            foreach(var student in topStudents)
            {
                Console.WriteLine("{0}, {1}, {2}", student.firstName, student.lastName, student.averageScore);
            }            
        }


    }
}