using System.Collections.Generic;
using Phase04.Model;
using System.Linq;

namespace Phase04
{
    public class Processor
    {
        public static List<> CalculateAverageScore(List<Student> students, List<LessonScore> scores){
            return students.GroupJoin(scores, st => st.StudentNumber, sc => sc.StudentNumber, 
            (st, ssc) => new {
                firstName = st.FirstName, lastName = st.LastName, averageScore = ssc.sum(c => c.Score) / ssc.count()
            }).
            OrderByDescending(s => s.averageScore).
            Take(3);
        }


    }
}