using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class ER03
    {
        private string _eqpid = string.Empty;
        private string _report_time = string.Empty;
        private string _progress = string.Empty;
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string report_time { get { return _report_time; } set { value = _report_time; } }
        public string progress { get { return _progress; } set { value = _progress; } }
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }
    }
}
