using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class ER03_reply
    {
        private string _eqpid = string.Empty;
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }
    }
}
