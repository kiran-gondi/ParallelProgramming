namespace ManualResetEventSlimAutoResetEvent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var evt = new ManualResetEventSlim(false);  
            var autoResetEvent = new AutoResetEvent(false);  

            Task.Factory.StartNew(() => { 
                Console.WriteLine("Boiling Water");
                //evt.Set();
                autoResetEvent.Set();
            });

            var makeTea = Task.Factory.StartNew(() => {
                Console.WriteLine("Waiting for water...");
                //evt.Wait();
                autoResetEvent.WaitOne();
                Console.WriteLine("Here is your tea");
                var ok = autoResetEvent.WaitOne(1000);
                if (ok) {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }

            });

            makeTea.Wait();
            Console.ReadKey();
        }
    }
}
