using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Maker.ERC20Token.Events.DTO
{
    public class Approval
    {
        [Parameter("address", "owner", 1, true)]
        public string AddressOwner { get; set; }

        [Parameter("address", "spender", 2, true)]
        public string AddressSpender { get; set; }

        [Parameter("uint", "value", 3)]
        public ulong Value { get; set; }
    }
}