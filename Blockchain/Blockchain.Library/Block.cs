using System;

namespace Blockchain.Library
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
}
