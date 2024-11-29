namespace ExceptionHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Demo01();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handle elsewhere: {e.GetType()} from {e.Source}");
                }
            }
            

            Console.WriteLine("End of main!");
            Console.ReadKey();
        }

        private static void Demo01()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this") { Source = "t1" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }
                    else
                    {
                         return false;
                    }
                });

                /*foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
                }*/
            }
        }
    }
}
