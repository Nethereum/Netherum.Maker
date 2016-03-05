using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.ERC20.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var erc20TokenTester = new Erc20TokenTester();
            Console.WriteLine(erc20TokenTester.Test().Result);
        }


    }
}
