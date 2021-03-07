using System;

namespace MiddleLevelDevCourse
{
    internal class Program
    {
        public static void Main()
        {
            try
            {
                FirstLesson.Run();
                SecondLesson.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
