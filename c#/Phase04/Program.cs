using System;
using System.Collections.Generic;
using Phase04.Model;

namespace Phase04
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new Database();
            var processor = new Processor(database);
            var topStudents = processor.CalculateTopStudents(3);
            ListPrinter.PrintIEnumerable(topStudents);
        }
    }
}
