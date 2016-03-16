using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Maker.ERC20Token.Events.DTO
{
    public class Transfer
    {
        [Parameter("address", "from", 1, true)]
        public string AddressFrom { get; set; }

        [Parameter("address", "to", 2, true)]
        public string AddressTo { get; set; }

        [Parameter("uint", "value", 3)]
        public BigInteger Value { get; set; }
    }
}
