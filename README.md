# Netherum.Maker

Nethereum.Maker is a .Net api to interact with Maker. For more information about Maker check its [website](https://makerdao.com/) or [white paper] (http://makerdao.github.io/docs/).

The current implementation has two main services, the Registry and Token services.

##Registry

The Maker registry holds all the different tokens which make the Maker ecosystem. This can be MKR the main asset, DAI, ETH and many more which are expected in the future.

Example:

``` csharp
    var web3 = new Web3.Web3();
    var makerRegistry = new MakerTokenRegistryService(web3, MakerTokenRegistryService.MakerTokenRegistryAddresses.MORDEN);
    var mkrTokenService = await makerRegistry.GetEthTokenServiceAsync(MakerTokenRegistryService.MakerTokenSymbols.MKR);
````

In the example above, I have used a preconfigured address for MORDEN (the testnet), this is just a plain string. Expect this to change in the future, (as it is the tesnet). 

Using the maker registry, you can query Ethereum for the different contract addreses of the tokens used by Maker. Or as in the example above create directly an instance of a Token Service.

##Token Service

All the tokens that are part of Maker, provide an standard ERC20 interface, providing a generice way to interact with them. This is an [example of a simple Solidty contract of ERC20](https://github.com/Nethereum/Netherum.Maker/blob/master/Nethereum.ERC20.Sample/StandardToken.sol). Maker is based on [Dappsys](https://github.com/nexusdev/dappsys) which is a more complex implementation.

### Examples

Check the total supply of MKR

``` csharp
var totalSupply = await mkrTokenService.GetTotalSupplyAsync<BigInteger>();
Console.WriteLine("Maker Supply");
Console.WriteLine(totalSupply);

```

Check the balance of an address

``` csharp
   var address = "0xbb7e97e5670d7475437943a1b314e661d7a9fa2a";
   var balance =  await mkrTokenService.GetBalanceOfAsync<BigInteger>(address);
   Console.WriteLine("Balance " + address);
   Console.WriteLine(balance);
```

Transfer to another address

``` csharp
var transactionHash = await mkrTokenService.TransferAsync(address, newAddress, 10, new HexBigInteger(150000));
Console.WriteLine("Transfering 10 MKR to " + newAddress);
Console.WriteLine("Transfer txId:");
Console.WriteLine(transactionHash);
````

Transfers events

``` charp

var filterId = await mkrTokenService.GetTransferEvent().CreateFilterAsync(new BlockParameter(500000));  
var transfers = await mkrTokenService.GetTransferEvent().GetAllChanges<Transfer>(filterId);
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
```









