using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class HC04_reply
    {
        private string _eqpid = string.Empty;
        private string _result = string.Empty;
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string result { get { return _result; } set { value = _result; } }
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }
    }
}
