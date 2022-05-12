// PROGRAMLAMA DİLLERİNİN PRENSİPLERİ
// B201200037 MERT KAMALI

using System;
using System.Collections.Generic;


namespace Blockchain_Technology
{
    public class Blockchain
    {
        IList<Transaction> PendingTransactions = new List<Transaction>();
        public IList<Block> Chain { get; set; }
        public int difficulty { get; set; } = 2;
        public int Reward { get; set; } = 2;

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }
        public void InitializeChain()
        {
            Chain = new List<Block>();
        }
        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, null, PendingTransactions);
            block.Mine(difficulty);
            PendingTransactions = new List<Transaction>();

            return block;
        }
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }
        public void AddBlock(Block block)
        {
            Block latestblock = GetLatestBlock();
            block.ID = latestblock.ID + 1;
            block.PreviousHash = latestblock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.difficulty);
            Chain.Add(block);
        }
        public void CreateTransaction( Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }
        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
            AddBlock(block);
            PendingTransactions = new List<Transaction>();
            CreateTransaction(new Transaction(null, minerAddress, Reward));
        }
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                if (currentBlock.Hash!=currentBlock.CalculateHash())
                {
                    return false;
                }
                if (currentBlock.PreviousHash!=previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
