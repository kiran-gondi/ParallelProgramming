namespace WaitForTimeToPass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                //Thread.Sleep(100);
                //SpinWait.SpinUntil(()=> true);
                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Bomb disarmed" : "Boom!!!");

            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main Program done!");
            Console.ReadKey();
        }
    }
}
