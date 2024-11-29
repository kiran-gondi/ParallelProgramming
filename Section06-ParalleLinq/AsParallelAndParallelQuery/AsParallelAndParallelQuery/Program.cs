namespace AsParallelAndParallelQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int count = 50;

            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = (x * x * x);
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x-1] = newValue;
            });

            Console.WriteLine();

            //foreach (var item in results) { 
            //    Console.WriteLine($"{item}\t");
            //}

            var cubes = items.AsParallel().AsOrdered().Select(x=> x*x*x);
            foreach (var cub in cubes) { 
                Console.Write($"{cub}\t");
            }
            Console.WriteLine();    

            Console.ReadKey();

        }
    }
}
