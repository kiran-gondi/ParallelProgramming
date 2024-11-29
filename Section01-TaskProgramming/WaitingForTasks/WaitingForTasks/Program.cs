namespace WaitingForTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();    
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("I take 3 seconds");

                for (int i = 0; i < 3; i++) { 
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm done");
            }, token);
            t.Start();

            //t.Wait(token);
            Task t2= Task.Factory.StartNew(()=> Thread.Sleep(5000), token);

            //Task.WaitAll(t, t2 );
            
            //Console.ReadKey();
            //cts.Cancel();

            Task.WaitAll([t, t2], 4000, token);

            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");
            
            //Task.WaitAny([t, t2], 4000, token);


            Console.WriteLine("Main program done!");
            Console.ReadKey();
        }
    }
}
