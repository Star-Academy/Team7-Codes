using System;
using System.Collections.Generic;
using Phase04.Model;

namespace Phase04
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            Processor processor = new Processor();
            var topStudents = processor.CalculateTopStudents(database.GetStudents(), database.GetScores(), 3);
            ListPrinter.printIEnumerable(topStudents);
        }


    }
}
