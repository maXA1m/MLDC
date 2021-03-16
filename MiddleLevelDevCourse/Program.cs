using System;
using System.Collections.Generic;

namespace MiddleLevelDevCourse
{
    internal class Program
    {
        public static void Main()
        {
            try
            {
                new List<ILesson>
                {
                    //new MaxBlockToAllocateLesson.MaxBlockToAllocateLesson(),
                    //new CustomCollectionLesson.CustomCollectionLesson(),
                    //new UnionLesson.UnionLesson(),
                    new TransactionsLesson.TransactionsLesson()
                }.ForEach(l => l.Run());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
