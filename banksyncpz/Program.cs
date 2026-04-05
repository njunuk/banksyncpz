using System.Diagnostics.Metrics;
using System.Reflection;
using System.Security.Cryptography;

public class BankAccount
{
    public static int balance = 0;
    public static object block = new object();
}
public class Program
{
    //private static Mutex mutex = new Mutex(false, "mmutex");
    Random rng = new Random();
    private static Semaphore semaphore = new Semaphore(3, 3);
    static void Main(string[] args)
    {
        Random rng = new Random();
        Thread[] threads = new Thread[10];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 100_000; j++)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting");
                    semaphore.WaitOne();
                    Thread.Sleep(1000);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} enters, num: "+rng.Next(0, 10000));
                    Thread.Sleep(3000);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} exits");
                    semaphore.Release();
                }
            });
            threads[i].Start();
        }
        for (int i = 0; i < threads.Length; ++i)
        {
            threads[i].Join();
        }
        //zav1
        //BankAccount.balance += 10000;
        //for (int i = 0; i < threads.Length; i++)
        //{
        //    threads[i] = new Thread(() =>
        //    {
        //        for (int j = 0; j < 10; j++)
        //        {
        //            int ch = rng.Next(0, 2);
        //            if (ch == 0)
        //            {
        //                lock (BankAccount.block)
        //                {
        //                    int crng = rng.Next(1, 10000);
        //                    BankAccount.balance += crng;
        //                    Console.WriteLine($"Added: {crng}, current balance: {BankAccount.balance}");
        //                }
        //            }
        //            else
        //            {
        //                lock (BankAccount.block)
        //                {
        //                    int crng = rng.Next(0, BankAccount.balance/2);
        //                    BankAccount.balance -= crng;
        //                    Console.WriteLine($"Removed: {crng}, current balance: {BankAccount.balance}");
        //                }
        //            }
        //        }
        //    });
        //    threads[i].Start();
        //}
        //for (int i = 0; i < threads.Length; ++i)
        //{
        //    threads[i].Join();
        //}
        //Console.WriteLine(BankAccount.balance);
    }
}