using System.Collections.Generic;
using System.Linq;

namespace Blockchain.Library
{
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

}
