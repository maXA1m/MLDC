using System;
using System.Collections.Generic;

namespace MiddleLevelDevCourse.Extensions
{
    public static class CollectionExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection, string header = null)
        {
            if (!string.IsNullOrEmpty(header))
            {
                Console.WriteLine(header);
            }

            foreach (var i in collection)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\n");
        }
    }
}
