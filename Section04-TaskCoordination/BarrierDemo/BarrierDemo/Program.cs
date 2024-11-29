namespace BarrierDemo
{
    internal class Program
    {
        /*static Barrier barrier = new Barrier(3, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });*/
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait(); //Signal and wait
            Console.WriteLine("Pouring water into cup");
            barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea.");
            barrier.SignalAndWait();
            Console.WriteLine("Adding Sugar");
        }

        public static void Cleanup()
        {
            Console.WriteLine("Cleanup the nicest cup of tea (fast)");
            barrier.SignalAndWait();
            Console.WriteLine("Cleanup tea.");
            barrier.SignalAndWait();
            Console.WriteLine("Cleaned up everything!");
        }

        static void Main(string[] args)
        {

            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);
            //var cleanUp = Task.Factory.StartNew(Cleanup);

            /*var tea = Task.Factory.ContinueWhenAll(new[] { water, cup, cleanUp}, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea");
            } );*/
            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea");
            });

            //Console.WriteLine("End of Main!");
            Console.ReadKey();
        }
    }
}
