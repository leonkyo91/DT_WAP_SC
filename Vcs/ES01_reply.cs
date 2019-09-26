using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcs
{
    public class ES01_reply
    {
        private string _ack = string.Empty;
        private string _desc = string.Empty;
        public string ack { get { return _ack; } set { value = _ack; } }
        public string desc { get { return _desc; } set { value = _desc; } }
    }
}
