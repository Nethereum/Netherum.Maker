using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;

namespace Nethereum.ERC20.Sample
{
    public class RegisteredTokenTest
    {
        public async Task<string> Test()
        {
            var mordenAddress = "0x213183be469a38e99facc2c468bb7e3c01377bce";
            var abi = @"[{""constant"":false,""inputs"":[{""name"":""symbol"",""type"":""bytes32""}],""name"":""getToken"",""outputs"":[{""name"":"""",""type"":""address""}],""type"":""function""}]";
            var web3 = new Web3.Web3();
            var contract = web3.Eth.GetContract(abi, mordenAddress);
            var function = contract.GetFunction("getToken");

            var addressFrom = "0xbb7e97e5670d7475437943a1b314e661d7a9fa2a";

            var pass = "password";
            var unlocked = await web3.Personal.UnlockAccount.SendRequestAsync(addressFrom, pass, new HexBigInteger(600));

            var result = await function.CallAsync<string>("MKR");
            return result;

        }
    }
}