using Blockchain.Library;
using System;
using System.Linq;

namespace Blockchain.Tester
{
    class Program
    {
        static Chain blockChain = new Chain();

        static void Main(string[] args)
        {
            Console.WriteLine(blockChain.Blocks.First().Data);

            Console.WriteLine(blockChain.CheckValidity().Success ? "Chain is Valid" : "Chain is INVALID!");

            blockChain.AddBlock(new Block(blockChain.GetLastIndex() + 1, "Some data", blockChain.GetLastBlock().Hash));

            Console.WriteLine(blockChain.CheckValidity().Success ? "Chain is Valid" : "Chain is INVALID!");

            Console.WriteLine(blockChain.Blocks.Skip(1).First().Data);

            Console.ReadLine();
        }
    }
}
