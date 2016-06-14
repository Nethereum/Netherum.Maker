using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.StandardTokenEIP20;

namespace Nethereum.Maker.ERC20Token
{
    public class MakerTokenRegistryService
    {
        public static class MakerTokenRegistryAddresses
        {
            public const string MORDEN = "0x9e2a411a0eb749fa380497bdcd97dedee5514c8d";

        }
        public static class MakerTokenSymbols
        {
            public const string MKR = "MKR";
            public const string ETH = "ETH";
        }

        private readonly Web3.Web3 web3;

        private string abi = @"[{""constant"":false,""inputs"":[{""name"":""symbol"",""type"":""bytes32""}],""name"":""getToken"",""outputs"":[{""name"":"""",""type"":""address""}],""type"":""function""}]";

        private Contract contract;

        public MakerTokenRegistryService(Web3.Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(abi, address);
        }

        public Function GetGetTokenFunction()
        {
            return contract.GetFunction("getToken");

        }
        public async Task<string> GetTokenAsyncCall(string symbol)
        {
            var function = GetGetTokenFunction();
            return await function.CallAsync<string>(symbol);
        }
        public async Task<string> GetTokenAsync(string addressFrom, string symbol, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetGetTokenFunction();
            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, symbol);

        }

        public async Task<StandardTokenService> GetEthTokenServiceAsync(string symbol)
        {
            var tokenAddress = await GetTokenAsyncCall(symbol);
            return new StandardTokenService(web3, tokenAddress);
        } 
    }
}