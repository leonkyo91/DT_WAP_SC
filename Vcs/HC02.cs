using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class HC02
    {
        private string _eqpid = string.Empty;
        private string _cmdid = string.Empty;
        private string _from = string.Empty;
        private string _to = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string cmdid { get { return _cmdid; } set { value = _cmdid; } }
        public string from { get { return _from; } set { value = _from; } }
        public string to { get { return _to; } set { value = _to; } }

    }
}
