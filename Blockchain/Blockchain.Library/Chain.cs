using System.Collections.Generic;
using System.Linq;

namespace Blockchain.Library
{
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

        public int GetLastIndex()
        {
            return _blocks.Last().Index;
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

}
