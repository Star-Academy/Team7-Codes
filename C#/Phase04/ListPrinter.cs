using System.Collections.Generic;
using System.Collections;
using System;
using Phase04.Model;

namespace Phase04

{
    public class ListPrinter
    {
        public static void PrintIEnumerable(IEnumerable enumerable)
        {
            foreach (var t in enumerable)
            {
                Console.WriteLine(t);
            }
        }
    }
}