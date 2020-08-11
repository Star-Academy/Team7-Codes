using System.Collections.Generic;
using System;
namespace Phase04

{
    public class ListPrinter
    {
        public static void printIEnumerable(IEnumerable<dynamic> enumerable)
        {
            foreach(dynamic t in enumerable)
            {
                Console.WriteLine(t);
            }
        }
    }
}