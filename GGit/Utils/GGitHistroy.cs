using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGit.Utils
{
    public class GGitHistroy
    {
        public string hash { get; set; }
        public string message { get; set; }
        public GGitHistroy(string _hash, string _message)
        {
            hash = _hash;
            message = _message;
        }
    }
}
