using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsPC2PLCBuffer
    {
        #region 列舉
        /// <summary>
        /// 命令模式列舉
        /// </summary>
        public enum enuCmdMode
        {
            None = 0,
            Left = 1,
            Right = 2,
            Both = 3,
        }

        /// <summary>
        /// 站口模式列舉
        /// </summary>
        public enum enuStnMode
        {
            None = 0,
            InMode = 1,
            OutMode = 2,
            Pibk = 3,
        }

        /// <summary>
        /// 讀取通知列舉
        /// </summary>
        public enum enuReadNotice
        {
            None = 0,
            BCRReadOK = 1,
            BCRReadNG = 2,
            NoCmd = 3,
            CmdRepeat = 4,
            CmdError = 5,
        }
        #endregion 列舉

        #region 屬性
        

        /// <summary>
        /// 序號
        /// </summary>
        public string CmdSno
        {
            get;
            set;
        }

        /// <summary>
        /// 站口模式; 1:入庫 2:出庫 3:撿料
        /// </summary>
        public int StnMode
        {
            get;
            set;
        }
        /// <summary>
        /// 初始通知 0:無 1:通知初始
        /// </summary>
        public int IniNotice
        {
            get;
            set;
        }

        /// <summary>
        /// 功能通知(1):0: 無 2:檢查無誤放行 3:檢查有問題退出站口
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

        /// <summary>
        /// 路徑通知; 1：B85 , 2：B86 , 3: B87 , 4: B88 , 5: B89 , 6: B90 , 7: B91 , 8: B92 , 9: B93 , 10: B94 , 11：B82 , 12：B84
        /// </summary>
        public int PathNotice
        {
            get;
            set;
        }
        #endregion 屬性

        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsPC2PLCBuffer 類別的新執行個體
        /// </summary>
        public clsPC2PLCBuffer()
        {
            CmdSno = "0";
            StnMode = 0;
            IniNotice = 0;
            FunNotice_1 = "0";
            FunNotice_2 = "0";
            FunNotice_3 = "0";
            PathNotice = 0;

        }

    }

    /// <summary>
    /// PC->PLC 功能通知(1)訊號
    /// </summary>
    public class clsFunNotice1
    {
        /// <summary>
        /// 0:無
        /// </summary>
        public const string None = "0";
        /// <summary>
        /// 1:檢查命令無誤放行
        /// </summary>
        public const string CMD_OK = "2"; //By Leon 
        /// <summary>
        /// 2:檢查命令有誤退出站口
        /// </summary>
        public const string CMD_NG = "3"; //By Leon 
    }
    /// <summary>
    /// PC->PLC 功能通知(2)訊號
    /// </summary>
    public class clsFunNotice2
    {
        /// <summary>
        /// 0:站口無模式
        /// </summary>
        public const string None = "0";
        /// <summary>
        /// 1:站口入庫模式
        /// </summary>
        public const string StnInMode = "1";
        /// <summary>
        /// 2:站口出庫模式
        /// </summary>
        public const string StnOutMode = "2";
    }
    /// <summary>
    /// PC->PLC 功能通知(3)訊號
    /// </summary>
    public class clsFunNotice3
    {
        /// <summary>
        /// 0:無
        /// </summary>
        public const string None = "0";
        /// <summary>
        /// 1:沒有命令
        /// </summary>
        public const string No_CMD = "1";
        /// <summary>
        /// 2:條碼讀不到
        /// </summary>
        public const string BCR_ReadFail = "2";
        /// <summary>
        /// 3:超重
        /// </summary>
        public const string OverWeight = "3";
        /// <summary>
        /// 4:儲位不符
        /// </summary>
        public const string Loc_Mismatch = "4";
    }
}
