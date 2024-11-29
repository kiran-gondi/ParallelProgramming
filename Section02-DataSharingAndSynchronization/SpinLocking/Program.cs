namespace SpinLocking
{
    internal class Program
    {
        public class BankAccount
        {
            private int balance;
            public int Balance
            {
                get { return balance; }
                set { balance = value; }
            }

            public void Deposit(int amount)
            {
                balance += amount;
            }

            public void Withdraw(int amount)
            {
                balance -= amount;
            }
        }
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount();
            var tasks = new List<Task>();

            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;

                        try
                        {
                            sl.Enter(ref lockTaken);
                            bankAccount.Deposit(100);
                        }
                        finally
                        {
                            if(lockTaken) sl.Exit();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;

                        try
                        {
                            sl.Enter(ref lockTaken);
                            bankAccount.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }
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
