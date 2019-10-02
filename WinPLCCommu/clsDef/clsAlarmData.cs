using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsAlarmData
    {
        public int BufferIndex
        {
            get;
            set;
        }

        public int AlarmIndex
        {
            get;
            set;
        }

        public string BufferName
        {
            get;
            set;
        }

        public string AlarmDesc
        {
            get;
            set;
        }

        public bool Signal
        {
            get;
            set;
        }

        public bool LastSignal
        {
            get;
            set;
        }

        public clsAlarmData()
        {
            BufferIndex = 0;
            AlarmIndex = 0;
            BufferName = string.Empty;
            AlarmDesc= string.Empty;
            Signal = false;
            LastSignal = false;
        }
    }
}
