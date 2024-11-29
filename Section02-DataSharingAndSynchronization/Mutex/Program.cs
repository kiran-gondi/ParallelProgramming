using System.Threading;

namespace Mutex1
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
            BankAccount bankAccount1 = new BankAccount();
            BankAccount bankAccount2 = new BankAccount();
            List<Task> tasks = new List<Task>();

            Mutex mutex1 = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveMutexLock = mutex1.WaitOne();
                        try
                        {
                            bankAccount1.Deposit(1);
                        }
                        finally
                        {
                            if(haveMutexLock) mutex1.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveMutexLock2 = mutex2.WaitOne();
                        try
                        {
                            bankAccount2.Withdraw(1);
                        }
                        finally
                        {
                            if(haveMutexLock2) mutex2.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0;j < 1000; j++)
                    {
                        bool haveLock = Mutex.WaitAll(new[] { mutex1, mutex2 });
                        try
                        {
                            bankAccount1.Transfer(bankAccount2, 1);
                        }
                        finally
                        {
                            if(haveLock) 
                            { 
                                mutex1.ReleaseMutex(); 
                                mutex2.ReleaseMutex(); 
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance1 is {bankAccount1.Balance}");
            Console.WriteLine($"Final balance2 is {bankAccount2.Balance}");

            Console.WriteLine("End of Main!");
            Console.ReadKey();
        }
    }
}
