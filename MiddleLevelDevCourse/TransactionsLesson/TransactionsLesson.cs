namespace MiddleLevelDevCourse.TransactionsLesson
{
    public class TransactionsLesson : ILesson
    {
        // Code from the lesson is wrong, because it creates different transactions at the same time.
        // Maybe remove should be applayed before add.

        public void Run()
        {
            var collection = new TransactionalDictionary<string, int>();

            CheckCollection(collection);
            CheckSimpleTransaction(collection);
            CheckTransaction(collection);
        }

        private static void CheckCollection(TransactionalDictionary<string, int> collection)
        {
            collection.Add("a", 10504);
            collection.Add("b", 228);

            System.Console.WriteLine(collection.Get("a"));
            System.Console.WriteLine(collection.Get("b"));

            collection.Remove("a");
            collection.Remove("b");

            System.Console.WriteLine(collection.Count);
        }

        private static void CheckSimpleTransaction(TransactionalDictionary<string, int> collection)
        {
            var tran = collection.BeginTransaction();

            collection.Add("c", 666);
            collection.Add("d", 555);

            collection.Commit(tran);

            System.Console.WriteLine(collection.Get("c"));
            System.Console.WriteLine(collection.Get("d"));
            System.Console.WriteLine(collection.Count);
        }

        private static void CheckTransaction(TransactionalDictionary<string, int> collection)
        {
            var tran = collection.BeginTransaction();

            collection.Add("e", 11);
            collection.Add("f", 2);
            collection.Remove("e");

            collection.Commit(tran);

            System.Console.WriteLine(collection.Get("f"));
            System.Console.WriteLine(collection.Count);
        }
    }
}
