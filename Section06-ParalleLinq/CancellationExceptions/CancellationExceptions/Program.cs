namespace CancellationExceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();    

            var items = ParallelEnumerable.Range(0, 50);

            var results = items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);

                //if(result > 1) throw new InvalidOperationException();

                Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
                return result;
            });

            try
            {
                foreach (var item in results)
                {
                    if(item > 1) cts.Cancel();

                    Console.WriteLine($"result = {item}");

                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }catch(OperationCanceledException e)
            {
                Console.WriteLine("Cancelled");
            }
        }
    }
}
