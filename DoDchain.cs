using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace DoDChain
{
    public class Block
    {
        int _index;
        DateTime _timeStamp;
        string _data;
        string _previousHash;
        string _hash;

        public int Index { get => _index; }
        public string Hash { get => _hash; }
        public string PreviousHash { get => _previousHash; }
        public DateTime TimeStamp { get => _timeStamp; }
        public string Data { get => _data; }

        public Block(
          int index,
          string data,
          string previousHash)
        {
            _index = index;
            _timeStamp = DateTime.Now;
            _data = data;
            _previousHash = previousHash;
            _hash = GetHash();
        }

        public string GetHash()
        {
            return Hasher.Hash($"{_index}{_timeStamp}{_data}{_previousHash}");
        }
    }

    public class Chain
    {
        List<Block> _blocks;

        public List<Block> Blocks { get => _blocks; }

        public Chain()
        {
            _blocks = new List<Block>();
            Block genesis = new Block(0, "Genesis Block", "0");
            _blocks.Add(genesis);
        }

        public Block GetLastBlock()
        {
            return _blocks.Last();
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
        }

        public Result CheckValidity()
        {
            Result result = new Result();

            for (int i = 0; i < _blocks.Count; i++)
            {
                var block = _blocks[i];

                if (_blocks[i].GetHash() != block.Hash)
                {
                    result.AddError($"Block at index {block.Index} has invalid hash.");
                }

                if (i > 0)
                {
                    var previousBlock = _blocks[i - 1];

                    if (block.Index != previousBlock.Index + 1)
                    {
                        result.AddError($"Wrong index at {block.Index}.");
                    }

                    if (block.PreviousHash != previousBlock.Hash)
                    {
                        result.AddError($"Hashes don't match with previous at {block.Index}.");
                    }
                }
            }

            return result;
        }
    }

    public class Hasher
    {
        public static string Hash(string input)
        {
            HashAlgorithm hasher = SHA256.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashedString = BitConverter.ToString(hash).Replace("-", String.Empty);
            return hashedString;
        }
    }

    public class Result
    {
        List<string> _errors;

        public Result()
        {
            _errors = new List<string>();
        }

        public List<string> Errors { get => _errors; }

        public void AddError(string error)
        {
            _errors.Add(error);
        }

        public bool Success
        {
            get
            {
                return _errors.Count() == 0;
            }
        }
    }

    public class ChainServer
    {
        Chain chain = new Chain();

        public void Add(string data)
        {
            var previousBlock = chain.GetLastBlock();
            chain.AddBlock(new Block(previousBlock.Index + 1, data, previousBlock.Hash));
        }

        public Result CheckChainValidity()
        {
            return chain.CheckValidity();
        }

        public List<Block> GetBlocks()
        {
            return chain.Blocks;
        }
    }
}
