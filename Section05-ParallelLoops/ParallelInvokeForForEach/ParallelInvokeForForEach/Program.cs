namespace ParallelInvokeForForEach
{
    internal class Program
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }
        static void Main(string[] args)
        {
            /*var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

            Parallel.Invoke(a, b, c);*/

            /*Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i*i}\t");
            });*/

            /*string[] words = { "oh", "what", "a", "match" };
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has length {word.Length} (task {Task.CurrentId})");
            });*/


            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = new CancellationToken();
            po.MaxDegreeOfParallelism = 2;

            Parallel.For(1, 11, po, i =>
            {
                Console.WriteLine($"{i * i}\t");
            });

            Console.ReadKey();
        }
    }
}
