using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class HC02_reply
    {
        private string _eqpid = string.Empty;
        private string _cmdid = string.Empty;
        private string _from = string.Empty;
        private string _to = string.Empty;
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string cmdid { get { return _cmdid; } set { value = _cmdid; } }
        public string from { get { return _from; } set { value = _from; } }
        public string to { get { return _to; } set { value = _to; } }
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }
    }
}
