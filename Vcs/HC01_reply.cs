using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class HC01_reply
    {
        private string _eqpid = string.Empty;
        private string _report_time = string.Empty;
        private string _synctime = string.Empty;
        private string _systimetime = string.Empty;
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string report_time { get { return _report_time; } set { value = _report_time; } }
        public string synctime { get { return _synctime; } set { value = _synctime; } }
        public string systimetime { get { return _systimetime; } set { value = _systimetime; } }
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }

    }
}
