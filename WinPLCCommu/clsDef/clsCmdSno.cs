using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsCmdSno
    {

        //大立光

        /// <summary>
        /// 序號
        /// </summary>
        public string CmdSno
        {
            get;
            set;
        }
        /// <summary>
        /// 模式
        /// </summary>
        public int CmdMode
        {
            get;
            set;
        }
        /// <summary>
        /// 初始通知
        /// </summary>
        public int IniNotice
        {
            get;
            set;
        }
        /// <summary>
        /// 功能通知(1):0: 無 1:檢查無誤放行 2:檢查有問題退出站口
        /// </summary>
        public string FunNotice_1
        {
            get;
            set;
        }
        /// <summary>
        /// 功能通知(2):0:站口無模式 1:站口入庫模式 2:站口出庫模式
        /// </summary>
        public string FunNotice_2
        {
            get;
            set;
        }
        /// <summary>
        /// 功能通知(3):0:無 1:無命令 2:條碼讀不到 3.超重 4:儲位不符
        /// </summary>
        public string FunNotice_3
        {
            get;
            set;
        }
        public string CmdSno_L
        {
            get;
            set;
        }
        public int CmdMode_L
        {
            get;
            set;
        }
        public int StnMode_L
        {
            get;
            set;
        }
        public int FunNotice_L
        {
            get;
            set;
        }
        public int ReadNotice_L
        {
            get;
            set;
        }
        public int PathNotice_L
        {
            get;
            set;
        }
        public string Loc
        {
            get;
            set;
        }
        public string NewLoc
        {
            get;
            set;
        }
        public string Stn_No
        {
            get;
            set;
        }

        public string CmdSno_R
        {
            get;
            set;
        }
        public int CmdMode_R
        {
            get;
            set;
        }
        public int StnMode_R
        {
            get;
            set;
        }
        public int FunNotice_R
        {
            get;
            set;
        }
        public int ReadNotice_R
        {
            get;
            set;
        }
        public int PathNotice_R
        {
            get;
            set;
        }
        public int Priority
        {
            get;
            set;
        }

        public clsCmdSno()
        {
            this.CmdSno_L = string.Empty;
            this.CmdSno_R = string.Empty;
            this.CmdMode_L = 0;
            this.CmdMode_R = 0;
            this.StnMode_L = 0;
            this.StnMode_R = 0;
            this.FunNotice_L = 0;
            this.FunNotice_R = 0;
            this.ReadNotice_L = 0;
            this.ReadNotice_R = 0;
            this.PathNotice_L = 0;
            this.PathNotice_R = 0;
            this.Loc = string.Empty;
            this.NewLoc = string.Empty;
            this.Priority = 5;

            //大立光
            CmdSno = string.Empty;
            CmdMode = 0;
            IniNotice = 0;
            FunNotice_1 = string.Empty;
            FunNotice_2 = string.Empty;
            FunNotice_3 = string.Empty;
        }
    }
}
