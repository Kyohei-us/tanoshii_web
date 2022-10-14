using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace tanoshii_web
{
    public class BlockchainController : Controller
    {
        public IActionResult Index()
        {
            var bc = new Blockchain(1, "test bc 1");
            var block1 = new Block(11);
            block1.AddTransaction(111, "Jack sends 100 BTC to Alice.");
            block1.AddTransaction(112, "Mike sends 120 ETH to Alice.");
            var block2 = new Block(12);
            block2.AddTransaction(121, "Jack sends 50 BTC to Mike.");
            block2.AddTransaction(122, "Alice sends 50 BTC to Mike.");
            bc.AddBlock(block1);
            bc.AddBlock(block2);

            ViewBag.bc = bc;
            return View();
        }

        public IActionResult CreateForm()
        {
            ViewBag.numOfInputs = 10;
            int maxNumTransactionsPerBlock = 10;
            var tempListItems = new List<SelectListItem>();
            for (int i = 0; i < maxNumTransactionsPerBlock; i++)
            {
                tempListItems.Add(
                    new SelectListItem()
                    {
                        Value = i.ToString(),
                        Text = i.ToString()
                    }
                );
            }
            ViewBag.SelectListItems = tempListItems.ToArray();
            return View();
        }

        [HttpPost]
        public IActionResult ShowResult(Models.ShowResultModel sr)
        {
            var numTransactions = new List<int>();

            foreach (var tx in sr.numTransactions)
            {
                if (tx == 0)
                    continue;
                numTransactions.Add(tx);
            }

            ViewBag.numTransactions = numTransactions;

            var bc = new Blockchain(1, "test bc 1");
            int bid = 11;
            foreach (int i in numTransactions)
            {
                var block1 = new Block(bid);
                for (int j = 0; j < i; j++)
                {
                    block1.AddTransaction(bid*10 + j, $"Jack sends {(int)((i+1)*(j+1) % 10 + 1)} BTC to Alice.");
                }
                bc.AddBlock(block1);
                bid++;
            }
            ViewBag.bc = bc;
            return View();
        }
    }

    public class Blockchain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Block> Blocks { get; set; }
        public List<Transaction> TransactionPool { get; set; }

        public Blockchain(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            this.Blocks = new List<Block>();
            this.TransactionPool = new List<Transaction>();
        }

        public bool AddBlock(Block Block)
        {
            this.Blocks.Add(Block);
            return true;
        }

        public bool EnqueueTransaction(Transaction Tx)
        {
            this.TransactionPool.Add(Tx);
            return true;
        }
    }

    public class Block
    {
        public int Id { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(int Id)
        {
            this.Id = Id;
            this.Transactions = new List<Transaction>();
        }

        public bool AddTransaction(int Id, string Content)
        {
            Transaction tx = new Transaction(Id, Content);
            this.Transactions.Add(tx);
            return true;
        }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public Transaction(int Id, string Content)
        {
            this.Id = Id;
            this.Content = Content;
        }
    }
}