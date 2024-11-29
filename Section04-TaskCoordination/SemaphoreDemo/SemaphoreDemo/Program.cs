namespace SemaphoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var semaphore = new SemaphoreSlim(2, 10);

            for (int i = 0; i < 20; i++) {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    semaphore.Wait(); //Release Count--
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }

            while(semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
                Console.ReadKey();
                semaphore.Release(2); //Release Count += 2
            }

        }
    }
}
