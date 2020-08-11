using System.Collections.Generic;
using Phase04.Model;
using System.Linq;
using System;

namespace Phase04
{
    public class Processor
    {
        public IEnumerable<Student> CalculateTopStudents(List<Student> students, List<LessonScore> scores, int numberOfStudents){
            return  students.GroupJoin(scores, st => st.StudentNumber, sc => sc.StudentNumber, 
                (st, ssc) => new Student(st.FirstName, st.LastName, ssc.Average(c => c.Score)))
                .OrderByDescending(s => s.Score)
                .Take(numberOfStudents);

        }



    }
}