using System.Runtime.InteropServices;

namespace Reader_Writer_Lock
{
    internal class Program
    {
        public class BankAccount
        {
            public int Balance { get; set; }

            public void Deposit(int amount)
            {
                //+=
                //op1: temp <- get_Balance() + amount
                //op2: set_Balance(temp)
                Balance += amount;
            }

            public void Withdraw(int amount)
            {
                Balance -= amount;
            }

            public int GetBalance()
            {
                return Balance;
            }

            public void Transfer(BankAccount where, int amount)
            {
                Balance -= amount;
                where.Balance += amount;
            }
        }

        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();
        static Random random = new Random();

        static void Main(string[] args)
        {
            int x = 0;

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++) 
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padlock.EnterReadLock();

                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(2000 );

                    padlock.ExitReadLock();

                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine($"Write lock acquired");
                int newValue = random.Next();
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");

            }
            Console.WriteLine("End of Main!");
            Console.ReadKey();
        }
    }
}
