using System;
using System.Collections.Generic;


namespace Phase04
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            Processor processor = new Processor();
            processor.PrintTopStudents(database.GetStudents(), database.GetScores(), 8);
        }


    }
}
