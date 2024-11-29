using System.Collections.Concurrent;

namespace ConcurrentBag
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Stack LIFO
            //Queue FIFO
            //no ordering

            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++) {
                var i1 = 1;
                tasks.Add(Task.Factory.StartNew(() => {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result)) 
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            if (bag.TryTake(out last)) {
                Console.WriteLine($"I got {last}");
            }

            Console.WriteLine("End Main!");
            Console.ReadKey();
        }
    }
}
