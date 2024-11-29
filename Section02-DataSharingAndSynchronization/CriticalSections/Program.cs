namespace CriticalSections
{
    internal class Program
    {
        public class BankAccount
        {
            //protected int Balance { get; set; }
            public object padlock = new object();
            public int Balance { get; set; }

            public void AddAmount(int amount)
            {
                //+=
                //op1: temp <- get_Balance() + amount
                //op2: set_Balance(temp)
                lock (padlock)
                {
                    Balance += amount;
                }
            }

            public void DeductAmount(int amount) {
                lock (padlock)
                {
                    Balance -= amount;
                }
            }

            public int GetBalance()
            {
                return Balance;
            }
        }

        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10; i++) 
            {
                tasks.Add(Task.Factory.StartNew(() => 
                {
                    for (int j = 0; j < 1000; j++) 
                    { 
                        bankAccount.AddAmount(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.DeductAmount(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            //Console.WriteLine($"Final balance is {bankAccount.GetBalance()}");
            Console.WriteLine($"Final balance is {bankAccount.Balance}");

            Console.WriteLine("End of main!");
            Console.ReadKey();
        }
    }
}
