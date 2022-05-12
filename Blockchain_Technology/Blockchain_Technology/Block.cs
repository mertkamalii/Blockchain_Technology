// PROGRAMLAMA DİLLERİNİN PRENSİPLERİ
// B201200037 MERT KAMALI

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace Blockchain_Technology
{
    public class Block
    {
        public IList<Transaction> Transactions { get; set; }
        public int ID { get; set; }
        public int Nonce { get; set; } = 0;
        public DateTime TimeStamp { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }        
       
        //public string Data { get; set; }
        
        public Block(DateTime timeStamp, string previousHash, IList<Transaction> transactions)
        {
            ID = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            //Data = data;
            Transactions = transactions;
            //Hash = CalculateHash();
        }
        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inbytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outbytes = sha256.ComputeHash(inbytes);
            return Convert.ToBase64String(outbytes);
        }
        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
