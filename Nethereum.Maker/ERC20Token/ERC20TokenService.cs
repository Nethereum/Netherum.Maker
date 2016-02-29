using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using Org.BouncyCastle.Asn1;

namespace Nethereum.Maker
{
    public class EthTokenService
    {
        private readonly Web3.Web3 web3;

        private string abi =
            @"[{""constant"":false,""inputs"":[{""name"":""spender"",""type"":""address""},{""name"":""value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""ok"",""type"":""bool""}],""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":""supply"",""type"":""uint256""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":""ok"",""type"":""bool""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":""who"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""value"",""type"":""uint256""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""to"",""type"":""address""},{""name"":""value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":""ok"",""type"":""bool""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":""owner"",""type"":""address""},{""name"":""spender"",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":""_allowance"",""type"":""uint256""}],""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""to"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""owner"",""type"":""address""},{""indexed"":true,""name"":""spender"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""}]";

        private Contract contract;

        public EthTokenService(Web3.Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(abi, address);
        }

        public async Task<ulong> GetTotalSupplyAsync()
        {
            var function = contract.GetFunction("totalSuply");
            return await function.CallAsync<ulong>();
        }

        public async Task<ulong> GetBalanceOfAsync(string address)
        {
            var function = contract.GetFunction("balanceOf");
            return await function.CallAsync<ulong>(address);
        }

        public async Task<ulong> GetAllowanceAsync(string addressOwner, string addressSpender)
        {
            var function = contract.GetFunction("allowance");
            return await function.CallAsync<ulong>(addressOwner, addressSpender);
        }

        public async Task TransferAsync(string addressFrom, string addressTo, ulong value, HexBigInteger gas = null)
        {
            var function = contract.GetFunction("transfer");
            await function.SendTransactionAsync(addressFrom, gas, addressTo, value);
        }

        public async Task<bool> TransferAsyncCall(string addressFrom, string addressTo, ulong value)
        {
            var function = contract.GetFunction("transfer");
            return await function.CallAsync<bool>(addressFrom, addressTo, value);
        }

        public async Task TransferFromAsync(string addressFrom, string addressTransferedFrom, string addressTransferedTo,
            ulong value, HexBigInteger gas = null)
        {
            var function = contract.GetFunction("transferFrom");
            await function.SendTransactionAsync(addressFrom, gas, addressTransferedFrom, addressTransferedTo, value);
        }

        public async Task<bool> TransferFromAsyncCall(string addressFrom, string addressTransferedFrom,
            string addressTransferedTo, ulong value)
        {
            var function = contract.GetFunction("transferFrom");
            return await function.CallAsync<bool>(addressFrom, addressTransferedFrom, addressTransferedTo, value);
        }

        public async Task ApproveAsync(string addressFrom, string addressSpender, ulong value, HexBigInteger gas = null)
        {
            var function = contract.GetFunction("approve");
            await function.SendTransactionAsync(addressFrom, gas, addressSpender, value);
        }

        public async Task<bool> ApproveAsyncCall(string addressFrom, string addressSpender, ulong value)
        {
            var function = contract.GetFunction("approve");
            return await function.CallAsync<bool>(addressFrom, addressSpender, value);
        }

        public Event GetApprovalEvent()
        {
            return contract.GetEvent("Approval");
        }

        public Event GetTransferEvent()
        {
            return contract.GetEvent("Transfer");
        }

        public HexBigInteger CreateFilter()
        {
            var contEvent = GetApprovalEvent();
           return null;
        }

    }
}
