using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICommon
{
    public class Enmu
    {
        public enum CmdCode { EQP_START = 01, EQP_STOP = 02, CYCLE_CMD = 03, DATA_DELETE_CMD = 04, CYCLE_COMPLETE_REPLY = 05, EQP_STATUS_CMD = 06, HEART_BEAT = 10, CYCLE_REPLY = 23, DATA_DELETE_REPLY = 24, EQP_STATUS = 26, REPLY_HEART_BEAT = 30, CYCLE_COMPLETE = 35, PLATFORM_MODE_CHANGE = 40, PLATFORM_MODE_CHANGE_REPLY = 42, DATA_REPLY = 50 }
        //public Enmu Cmd { }
    }
}
