using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class HC07_reply
    {
        private string _eqpid = string.Empty;
        private string _report_time = string.Empty;
        private string _eqstate = string.Empty;
        private string _linkmode = string.Empty;
        private string _operationmode = string.Empty;
        private string _plcbattery = string.Empty;
        private string _battery = string.Empty;
        private string _palletid = string.Empty;
        private string _isempty = string.Empty;
        private string _idt = string.Empty;
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string report_time { get { return _report_time; } set { value = _report_time; } }
        public string eqstate { get { return _eqstate; } set { value = _eqstate; } }
        public string linkmode { get { return _linkmode; } set { value = _linkmode; } }
        public string operationmode { get { return _operationmode; } set { value = _operationmode; } }
        public string plcbattery { get { return _plcbattery; } set { value = _plcbattery; } }
        public string battery { get { return _battery; } set { value = _battery; } }
        public string palletid { get { return _palletid; } set { value = _palletid; } }
        public string isempty { get { return _isempty; } set { value = _isempty; } }
        public string idt { get { return _idt; } set { value = _idt; } }
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }
    }
}
