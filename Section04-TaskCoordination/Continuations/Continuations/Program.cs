namespace Continuations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*var task1 = Task.Factory.StartNew(() => {
                Console.WriteLine("Boiling Water");
            });

            var task2 = task1.ContinueWith(t => {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            });

            task2.Wait();

            Console.ReadKey();*/

            var task1 = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            /*var task3 = Task.Factory.ContinueWhenAll(new[] { task1, task2 },
                tasks =>
                {
                    Console.WriteLine("Tasks completed");
                    foreach (var t in tasks)
                        Console.WriteLine(" - " + t.Result);
                    Console.WriteLine("All tasks done");
                });*/

            var task3 = Task.Factory.ContinueWhenAny(new[] { task1, task2 },
                t =>
                {
                    Console.WriteLine("Tasks completed");
                    Console.WriteLine(" - " + t.Result);
                    Console.WriteLine("All tasks done");
                });

            task3.Wait();

            Console.ReadKey();
        }
    }
}
