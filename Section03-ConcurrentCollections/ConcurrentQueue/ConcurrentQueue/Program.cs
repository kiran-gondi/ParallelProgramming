using System.Collections.Concurrent;

namespace ConcurrentQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queue = new ConcurrentQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            //3 2 1 <- Front
            int result;
            if (queue.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            if(queue.TryPeek(out result)){
                Console.WriteLine($"Font element {result}");
            }

            Console.WriteLine("End of Main!");
            Console.ReadKey();
        }
    }
}
