namespace MiddleLevelDevCourse.SecondLesson
{
    public class SecondLesson : ILesson
    {
        public void Run()
        {
            System.Console.WriteLine("Hash = index");
            TestCollection(new CustomCollection<SimpleObject>(HashFunctions.NoHash, 100_000));

            System.Console.WriteLine("Hash = old java hash");
            TestCollection(new CustomCollection<SimpleObject>(HashFunctions.OldHash, 100_000));

            System.Console.WriteLine("Hash = microsoft strong hash");
            TestCollection(new CustomCollection<SimpleObject>(HashFunctions.StrongHash, 100_000));
        }

        private static void TestCollection(CustomCollection<SimpleObject> collection)
        {
            for(int i = 0; i < collection.InitialCapacity; i++)
            {
                var key = i * 10;
                collection.Add(key, new SimpleObject { Number = key });
            }

            for(int i = 0; i < 90_000; i++)
            {
                var key = i * 10;
                System.Console.WriteLine($"{key}: {collection[key]}"); 
            }

            System.Console.WriteLine($"Last item: {collection[(collection.InitialCapacity - 1) * 10]}");
        }
    }
}
