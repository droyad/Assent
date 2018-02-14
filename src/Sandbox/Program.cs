using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assent;
using Assent.Reporters;

namespace Sandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            try
            {
                this.Assent("foobayr");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
