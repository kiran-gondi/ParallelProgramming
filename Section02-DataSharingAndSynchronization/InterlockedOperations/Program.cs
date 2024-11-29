using static InterlockedOperations.Program;

namespace InterlockedOperations
{
    internal class Program
    {
        public class BankAccount
        {
            private int balance;
            public int Balance {
                get { return balance; }
                set { balance = value; }
            }

            public void Deposit(int amount)
            {
                Interlocked.Add(ref balance, amount);
            }

            public void Withdraw(int amount) { 
                Interlocked.Add(ref balance, -amount);
            }
        }
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount();
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++) {
                tasks.Add(Task.Factory.StartNew (() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {bankAccount.Balance}");

            Console.WriteLine("End of main!");
            Console.ReadKey();
        }
    }
}
