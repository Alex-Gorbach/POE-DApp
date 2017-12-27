using System.Collections.Generic;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Geth;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Xunit;

namespace POE.WEB.Nethereum
{
   
    public class ContractService
    {
        private string owner;
        private string password;
        private string fileHash;
        private string abi = @"[{""constant"":true,""inputs"":[],""name"":""getInstructor"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_fName"",""type"":""string""}],""name"":""setInstructor"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""}]";
        //private string abi = @"[{""constant"":false,""inputs"":[{""name"":""fileHash"",""type"":""string""}],""name"":""get"",""outputs"":[{""name"":""timestamp"",""type"":""uint256""},{""name"":""owner"",""type"":""string""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""owner"",""type"":""string""},{""name"":""fileHash"",""type"":""string""}],""name"":""set"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""status"",""type"":""bool""},{""indexed"":false,""name"":""timestamp"",""type"":""uint256""},{""indexed"":false,""name"":""owner"",""type"":""string""},{""indexed"":false,""name"":""fileHash"",""type"":""string""}],""name"":""logFileAddedStatus"",""type"":""event""}]";

        public ContractService( string address,string password,string fileHash)
        {
            this.owner = address;
            this.password = password;
            this.fileHash = fileHash;
        }

        public async Task SetFileHash( )
        {
            var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            var account = new Account(privateKey);
            var web3 = new Web3(account);
            ulong totalSupply = 10000;
            var contract =web3.Eth.GetContract(abi, "0x8e100a7b2819c1edc8c8fd3cc53068f501a8cc08");
            var setHash = contract.GetFunction("setInstructor");
            var result = await setHash.SendTransactionAsync(senderAddress, new HexBigInteger(4700000), new HexBigInteger(UnitConversion.Convert.ToWei(20)), @"[{""asa""}]");
     
        }

        public async Task<object> GetFileHash()
        {
            var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            var account = new Account(privateKey);
            var web3 = new Web3(account);
            ulong totalSupply = 10000;
            var contract = web3.Eth.GetContract(abi, "0x8e100a7b2819c1edc8c8fd3cc53068f501a8cc08");
            var setHash = contract.GetFunction("getInstructor");

            var result = await setHash.CallAsync<object>();
            return result;
        }


    }
}