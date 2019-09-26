using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICommon
{
    public class MsgEventArg : EventArgs
    {
        public Dictionary<string, string> _value { get; set; }
        public string _cmdCode { get; set; }
        public string _message { get; set; }
        public byte[] _btdata { get; set; }
        public string[] _reply { get; set; }
        public int _conn { get; set; }
        public bool _cmdAck { get; set; }
    }
}
