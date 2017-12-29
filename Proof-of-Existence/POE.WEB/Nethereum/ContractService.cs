using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace POE.WEB.Nethereum
{

    public class ContractService
    {
        private readonly string _owner;
        private readonly string _privateKey;
        private readonly string _fileHash;
        private const string Abi = @"[{""constant"":false,""inputs"":[{""name"":""fileHash"",""type"":""string""}],""name"":""get"",""outputs"":[{""name"":""owner"",""type"":""string""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""owner"",""type"":""string""},{""name"":""fileHash"",""type"":""string""}],""name"":""set"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""status"",""type"":""bool""},{""indexed"":false,""name"":""timestamp"",""type"":""uint256""},{""indexed"":false,""name"":""owner"",""type"":""string""},{""indexed"":false,""name"":""fileHash"",""type"":""string""}],""name"":""logFileAddedStatus"",""type"":""event""}]";

        public ContractService( string owner, string privateKey, string fileHash)
        {
            _owner = owner;
            _privateKey = privateKey;
            _fileHash = fileHash;
        }

        public async Task<string> SetFileHash( )
        {
            var privateKey = this._privateKey;
            var senderAddress = _owner;
            var account = new Account(privateKey);
            var web3 = new Web3(account);
            var contract =web3.Eth.GetContract(Abi, "0x243e72b69141f6af525a9a5fd939668ee9f2b354");
            var setHash = contract.GetFunction("set");
            var res = await setHash.EstimateGasAsync(_owner, _fileHash);
            var result = await setHash.SendTransactionAsync(senderAddress,res,null, _owner, _fileHash);
            return result;

        }

        public async Task<string> GetFileHash()
        {
            var privateKey = this._privateKey;
            var account = new Account(privateKey);
            var web3 = new Web3(account);
            var contract = web3.Eth.GetContract(Abi, "0x243e72b69141f6af525a9a5fd939668ee9f2b354");
            var setHash = contract.GetFunction("get");
            var result = await setHash.CallAsync<string>(_fileHash);
            return result;
        }


    }
}