using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleLevelDevCourse.TransactionsLesson
{
    public class TransactionsLesson : ILesson
    {
        public void Run()
        {
            var collection = new TransactionalDictionary<string, int>();

            CheckCollection(collection);
            CheckTransaction(collection);

            // Code from the lesson is wrong, because it creates different transactions at the same time.
            // Remove can be applayed before add (second transaction before first).
            CheckDifferentThreadsTransaction(collection).Wait();
        }

        private static void CheckCollection(TransactionalDictionary<string, int> collection)
        {
            collection.Add("a", 10504);
            collection.Add("b", 228);

            collection.Remove("a");
            collection.Remove("b");
        }

        private static void CheckTransaction(TransactionalDictionary<string, int> collection)
        {
            var tran = collection.BeginTransaction();

            collection.Add("e", 11);
            collection.Add("f", 2);
            collection.Remove("e");

            collection.Commit(tran);

            tran = collection.BeginTransaction();

            collection.Remove("f");

            collection.Commit(tran);
        }

        private static async Task CheckDifferentThreadsTransaction(TransactionalDictionary<string, int> collection)
        {
            await Task.Run(() =>
            {
                var tran = collection.BeginTransaction();
                Console.WriteLine("Add transaction was created");

                Console.WriteLine("Start adding 'a'");

                collection.Add("a", 10500);
                collection.Commit(tran);

                Console.WriteLine("Add transaction was commited.");
            });

            await Task.Run(() =>
            {
                var tran = collection.BeginTransaction();
                Console.WriteLine("Remove transaction was created");

                Thread.Sleep(1000);

                Console.WriteLine("Start removing 'a'");

                collection.Remove("a");
                collection.Commit(tran);

                Console.WriteLine("Remove transaction was commited.");
            });
        }
    }
}
