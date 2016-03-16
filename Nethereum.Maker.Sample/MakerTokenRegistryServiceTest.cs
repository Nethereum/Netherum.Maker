using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Maker.ERC20Token;
using Nethereum.Maker.ERC20Token.Events.DTO;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Nethereum.ERC20.Sample
{
    public class MakerTokenRegistryServiceExample
    {
        public async Task<bool> RunExampleAsync()
        {
            var web3 = new Web3.Web3();
            var makerRegistry = new MakerTokenRegistryService(web3,
                MakerTokenRegistryService.MakerTokenRegistryAddresses.MORDEN);

            var mkrTokenService = await makerRegistry.GetEthTokenServiceAsync(MakerTokenRegistryService.MakerTokenSymbols.MKR);

            var totalSupply = await mkrTokenService.GetTotalSupplyAsync<BigInteger>();
            Console.WriteLine("Maker Supply");
            Console.WriteLine(totalSupply);

            var address = "0xbb7e97e5670d7475437943a1b314e661d7a9fa2a";

           

            var balance =  await mkrTokenService.GetBalanceOfAsync<BigInteger>(address);
            Console.WriteLine("Balance " + address);
            Console.WriteLine(balance);


            var ethTokenService = await makerRegistry.GetEthTokenServiceAsync(MakerTokenRegistryService.MakerTokenSymbols.ETH);

            var ethTotalSupply = await ethTokenService.GetTotalSupplyAsync<BigInteger>();
            Console.WriteLine("Eth Supply");
            Console.WriteLine(ethTotalSupply);


            var result = await web3.Personal.UnlockAccount.SendRequestAsync(address, "password", new HexBigInteger(600));

            var newAddress = await web3.Personal.NewAccount.SendRequestAsync("password");
            Console.WriteLine("New address");
            Console.WriteLine(newAddress);


            var transactionHash = await mkrTokenService.TransferAsync(address, newAddress, 10, new HexBigInteger(150000));
            Console.WriteLine("Transfering 10 MKR to " + newAddress);
            Console.WriteLine("Transfer txId:");
            Console.WriteLine(transactionHash);
            //wait to be mined
            var transferReceipt = await GetTransactionReceiptAsync(web3.Eth.Transactions, transactionHash);

            var filterId = await mkrTokenService.GetTransferEvent().CreateFilterAsync(new BlockParameter(500000)); //<object, string>(null, "0xbb7e97e5670d7475437943a1b314e661d7a9fa2a", new BlockParameter(1000));


            var transfers = await mkrTokenService.GetTransferEvent().GetAllChanges<Transfer>(filterId);


            var balanceNewAddress = await mkrTokenService.GetBalanceOfAsync<BigInteger>(newAddress);
            Console.WriteLine("Balance of " + newAddress);
            Console.WriteLine(balanceNewAddress);

            balance = await mkrTokenService.GetBalanceOfAsync<BigInteger>(address);
            Console.WriteLine("Balance " + address);
            Console.WriteLine(balance);

            Console.WriteLine("Total Transfers since block 500000");
            Console.WriteLine(transfers.Count);

            foreach (var transfer in transfers)
            {
                Console.WriteLine("From:");
                Console.WriteLine(transfer.Event.AddressFrom);
                Console.WriteLine("To:");
                Console.WriteLine(transfer.Event.AddressTo);
                Console.WriteLine("Amount:");
                Console.WriteLine(transfer.Event.Value);
                    
            }

            return true;
        }

        private static async Task<TransactionReceipt> GetTransactionReceiptAsync(EthTransactionsService transactionService, string transactionHash)
        {
            TransactionReceipt receipt = null;
            //wait for the contract to be mined to the address
            while (receipt == null)
            {
                await Task.Delay(5000);
                receipt = await transactionService.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }
            return receipt;
        }
    }
}