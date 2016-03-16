using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.ERC20.Sample;

namespace Nethereum.Maker.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var makerTokenRegistryServiceTest = new MakerTokenRegistryServiceExample();

            var result = makerTokenRegistryServiceTest.RunExampleAsync().Result;
            Console.ReadLine();
        }
    }
}
