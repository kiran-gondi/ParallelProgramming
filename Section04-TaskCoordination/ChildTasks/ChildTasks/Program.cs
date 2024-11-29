namespace ChildTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var parentTask = new Task(() =>
            {
                //detached
                var childTask = new Task(() => {
                    Console.WriteLine($"Child task starting");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Child task finishing");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = childTask.ContinueWith(t =>
                {
                    Console.WriteLine($"Horray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = childTask.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops, task {t.Id}' state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                childTask.Start();
            });

            parentTask.Start();

            try
            {
                parentTask.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e=> true);
                throw;
            }

            Console.WriteLine("End of MAin!");
            Console.ReadKey();
        }
    }
}
