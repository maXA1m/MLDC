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
                    //new FirstLesson.FirstLesson(),
                    //new SecondLesson.SecondLesson(),
                    new ThirdLesson.ThirdLesson()
                }.ForEach(l => l.Run());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
