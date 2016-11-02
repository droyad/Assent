using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assent;

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
