namespace MutexDemo2
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

        static void Main(string[] args)
        {
            const string appName = "My App";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.WriteLine("End of Main!");
            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
