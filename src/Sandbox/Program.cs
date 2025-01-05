using System;
using System.Threading;
using Assent;

namespace Sandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var prog = new Program();
            
            var t1 = new Thread(() => prog.Run(1));
            var t2 = new Thread(() => prog.Run(2));
            
            t1.Start();
            Thread.Sleep(2000);
            t2.Start();

            t1.Join();
            t2.Join();
            
            Console.WriteLine("All Done");
        }

        private void Run(int n)
        {
            try
            {
                this.Assent($"Thread {n}", testName: n.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread {n} Exception: {ex.Message}");
            }
            
            Console.WriteLine($"Thread {n} done");
        }
    }
}
