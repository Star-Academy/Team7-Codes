namespace Phase04.Model
{
    public class Student
    {
        public int StudentNumber{get; set;}
        public string FirstName{get; set;}
        public string LastName{get; set;}
        public double Score{get; set;}

        public Student(string firstName, string lastName, double score){
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Score = score;
        }

        public override string ToString()
        {
        return string.Format("{0} , {1} , {2}", this.FirstName, this.LastName, this.Score);
        }
    }
}