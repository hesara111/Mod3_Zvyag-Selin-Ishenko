using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Mod3Korzina
{
    class Program
    {
        static Random rand = new Random();
        static List<Thread> threads = new List<Thread>();
        static List<int> list = new List<int>();
        static int ValueOfBasket = 0;
        static int sum = 0;
        static int uberBuf = 40;
        static int uberCheaterBuf = 40;
        static Thread thread1;
        static Thread thread2;
        static Thread thread3;
        static void Main(string[] args)
        {
            
            Console.Write("Введите вес корзины (40-140): ");
            while (ValueOfBasket < 40 || ValueOfBasket > 140)
            {
                ValueOfBasket = Convert.ToInt32(Console.ReadLine());
                if (ValueOfBasket < 40 || ValueOfBasket > 140)
                {
                    Console.Write("Wrong value of basket, please try again: ");
                }
                Console.WriteLine("");
            }
            thread1 = new Thread(new ThreadStart(SimplePlayer));
            thread1.Name = "Simple";
            thread2 = new Thread(new ThreadStart(UberPlayer));
            thread2.Name = "Uber";
            thread3 = new Thread(new ThreadStart(Cheater));
            thread3.Name = "Cheater";
            threads.Add(thread1);
            threads.Add(thread2);
            threads.Add(thread3);
            foreach(var i in threads)
            {
                i.Start();
            }
            //thread1.Start();
            //thread2.Start();
            //thread3.Start();
        }

        static void SimplePlayer()
        {
         
            while (true)
            {
                Thread.Sleep(1000);
                int buf = rand.Next(40, 140);
                sum++;
                if (buf == ValueOfBasket)
                {
                    
                    Console.WriteLine($"{Thread.CurrentThread.Name} УГАДАЛ!!! первым, число:{buf}");
                    // Console.ReadLine();
                    foreach (var i in threads) i.Abort();
                    return;
                    //Закончить программу 
                }
                else
                {
                    
                    Console.WriteLine($"{Thread.CurrentThread.Name} не угадал, его вариант:{buf}");
                    list.Add(buf);
                }
            }  
        }
        static void UberPlayer()
        {
            while (true)
            {
                Thread.Sleep(1000);
                sum++;
                if (uberBuf == ValueOfBasket)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} УГАДАЛ!!! первым, число:{uberBuf}");
                    Console.ReadLine();
                    foreach (var i in threads) i.Abort();
                    return;
                    //Закончить программу 
                }
                else
                {
                   
                    Console.WriteLine($"{Thread.CurrentThread.Name} не угадал, его вариант:{uberBuf}");
                    list.Add(uberBuf);
                    uberBuf++;
                }
            }
        }
        

        static void  Cheater()
        {
            while (true)
            {
                Thread.Sleep(1000);
                sum++;
            //    Random rand = new Random();
                int buf = rand.Next(40, 140);
                while(list.Contains(buf)==true)
                {
                    buf = rand.Next(40, 140);
                }
                {
                    if (buf == ValueOfBasket)
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} УГАДАЛ!!! первым, число:{buf}");
                        //  Console.ReadLine();
                        foreach (var i in threads) i.Abort();
                        return;
                        //Закончить программу
                    }
                    else
                    {
                        //Thread.Sleep(1000);
                        Console.WriteLine($"{Thread.CurrentThread.Name} не угадал, его вариант:{buf}");
                        list.Add(buf);
                    }
                }
            }
        }
        private static void End()
        {
            thread1.Abort();
            thread1.Join();
            thread2.Abort();
            thread2.Join();
            thread3.Abort();
            thread3.Join();
        }
    }
}
