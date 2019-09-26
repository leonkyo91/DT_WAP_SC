using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class HC08_reply
    {
        private string _eqpid = string.Empty;
        private string _report_time = string.Empty;
        private string _subeqid = string.Empty;
        private string _awstate = string.Empty;
        private string _awlevel = string.Empty;
        private string _errcode = string.Empty;
        private string _errdesc = string.Empty;
        public string eqpid { get { return _eqpid; } set { value = _eqpid; } }
        public string report_time { get { return _report_time; } set { value = _report_time; } }
        public string subeqid { get { return _subeqid; } set { value = _subeqid; } }
        public string awstate { get { return _awstate; } set { value = _awstate; } }
        public string awlevel { get { return _awlevel; } set { value = _awlevel; } }
        public string errcode { get { return _errcode; } set { value = _errcode; } }
        public string errdesc { get { return _errdesc; } set { value = _errdesc; } }

    }
}
