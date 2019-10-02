using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsTrace
    {
        /// <summary>
        /// 下達週邊PLC入庫命令:11
        /// </summary>
        public const string cstrStoreInTrace_ReleaseEquPLCCmd = "11";
        /// <summary>
        /// 棧板到達入庫站口:12
        /// </summary>
        public const string cstrStoreInTrace_ItemOnStn = "12";
        /// <summary>
        /// 下達Crane入庫命令:13
        /// </summary>
        public const string cstrStoreInTrace_ReleaseCraneCmd = "13";
        /// <summary>
        /// Crane命令完成:14
        /// </summary>
        public const string cstrStoreInTrace_CraneCmdFinish = "14";
        /// <summary>
        /// 下達Crane出庫命令:21
        /// </summary>
        public const string cstrStoreOutTrace_ReleaseEquPLCCmd = "21";
        /// <summary>
        /// 棧板到達出庫站口:22
        /// </summary>
        public const string cstrStoreOutTrace_ReleaseCraneCmd = "22";
        /// <summary>
        /// Crane出庫命令完成:23
        /// </summary>
        public const string cstrStoreOutTrace_CraneCmdFinish = "23";
        /// <summary>
        /// 下達Crane庫對庫命令:51
        /// </summary>
        public const string cstrLocToLocTrace_ReleaseCraneCmd = "51";
        /// <summary>
        /// Crane庫對庫命令完成:52
        /// </summary>
        public const string cstrLocToLocTrace_CraneCmdFinish = "52";

        private clsTrace()
        {
        }
    }

    public class clsReBCRSts
    {
        public const string cstrOK = "1";
        public const string cstrNG = "2";
        public const string cstrNoCmdSno = "3";
        public const string cstrCmdRepeat = "4";
        public const string cstrCmdErr = "5";

        private clsReBCRSts()
        {
        }
    }

    public class clsBCRSts
    {
        public const string cstrNone = "0";
        public const string cstrReading = "1";
        public const string cstrReadFinish = "2";

        private clsBCRSts()
        {
        }
    }

    public class clsReBCRID
    {
        public const string cstrBCRDataInit = "n/a";
        public const string cstrBCRError = "ERROR";

        private clsReBCRID()
        {
        }
    }
    public class clsWTSts
    {
        public const string cstrNone = "0";
        public const string cstrReading = "1";
        public const string cstrReadFinish = "2";
        public const string cstrReadWeightOver = "E1";
        public const string cstrReadFail = "E2";
    }
    public class clsReWTID
    {
        public const string cstrWTDataInit = "n/a";
        public const string cstrWTError = "ERROR";

        private clsReWTID()
        {
        }
    }

    public class clsCmdSts
    {
        /// <summary>
        /// 命令初始狀態:0
        /// </summary>
        public const string cstrCmdSts_Init = "0";
        /// <summary>
        /// 命令執行中:1
        /// </summary>
        public const string cstrCmdSts_Start = "1";
        /// <summary>
        /// 命令完成待過帳:7  
        /// </summary>
        public const string cstrCmdSts_CompletedWaitPost = "7";
        /// <summary>
        /// 命令取消待過帳:8
        /// </summary>
        public const string cstrCmdSts_CancelWaitPost = "8";
        /// <summary>
        /// 命令完成:9
        /// </summary>
        public const string cstrCmdSts_Completed = "9";
        /// <summary>
        /// 命令取消:D
        /// </summary>
        public const string cstrCmdSts_Cancel = "D";
        /// <summary>
        /// 過帳失敗:E
        /// </summary>
        public const string cstrCmdSts_PostFail = "E";

        private clsCmdSts()
        {
        }
    }

    public class clsInOutMode
    {
        public const string cstrNone = "0";
        public const string cstrInMode = "1";
        public const string cstrOutMode = "2";
        public const string cstrBoth = "3";

        private clsInOutMode()
        {
        }
    }

    public class clsEquCmdMode
    {
        public const string cstrInMode = "1";
        public const string cstrOutMode = "2";
        public const string cstrStnToStn = "4";
        public const string cstrLocToLoc = "5";
        public const string cstrMove = "6";
        public const string cstrScan = "7";
        public const string cstrPickUp = "8";
        public const string cstrDeposite = "9";
        public const string cstrReturnHP = "10";

        private clsEquCmdMode()
        {
        }
    }

    public class clsStnDef
    {
        public string StnNo;
        public int StnIndex;
        public int BufferIndex;
        public string Buffer;
        public int CraneNo;
        public string PortNo;
        public string DoublePortNo;

        public clsStnDef()
        {
            CraneNo = 0;
            BufferIndex = 0;
            StnIndex = 0;
            StnNo = string.Empty;
            Buffer = string.Empty;
            PortNo = string.Empty;
            DoublePortNo = string.Empty;
        }
    }

    public class clsStnNoDef
    {
        public const string cstrCraneOPRight_Left = "000007";
        public const string cstrCraneOPRight_Right = "000008";
        public const string cstrCraneOPRight_Both = "000009";

        public const string cstrCraneOPLeft_Left = "000011";
        public const string cstrCraneOPLeft_Right = "000010";
        public const string cstrCraneOPLeft_Both = "000012";

        public const string cstrCraneHPRight_Left = "000004";
        public const string cstrCraneHPRight_Right = "000005";
        public const string cstrCraneHPRight_Both = "000006";

        public const string cstrCraneHPLeft_Left = "000002";
        public const string cstrCraneHPLeft_Right = "000001";
        public const string cstrCraneHPLeft_Both = "000003";

        public const string cstrCraneHP_2F_IN = "000001";
        public const string cstrCraneHP_2F_OUT = "000002";
        public const string cstrCraneHP_3F_IN = "000003";
        public const string cstrCraneHP_3F_OUT = "000004";
        public const string cstrCraneHP_4F_IN = "000005";
        public const string cstrCraneHP_4F_OUT = "000006";
        public const string cstrCraneHP_5F_IN = "000007";
        public const string cstrCraneHP_5F_OUT = "000008";
        public const string cstrCraneHP_6F_IN = "000009";
        public const string cstrCraneHP_6F_OUT = "000010";
        public const string cstrCraneHP_7F_IN = "000011";
        public const string cstrCraneHP_7F_OUT = "000012";
        public const string cstrCraneHP_8F_IN = "000013";
        public const string cstrCraneHP_8F_OUT = "000014";
        private clsStnNoDef()
        {
        }
    }

    public class clsLocSts
    {
        public const string cstrEmpty = "N";
        public const string cstrEmptyStored = "E";
        public const string cstrStored = "S";
        public const string cstrDisable = "X";
        public const string cstrStorageInReserved = "I";
        public const string cstrStorageOutReserved = "O";
        public const string cstrRevisionReserved = "L";
        public const string cstrRevision = "H";

        public const string cstrLoc_NNNN = "NNNN";
        public const string cstrLoc_ENNE = "ENNE";
        public const string cstrLoc_SNNS = "SNNS";
        public const string cstrLoc_HNNH = "HNNH";   //v1.0.1.9
        public const string cstrLoc_XNNX = "XNNX";   //v1.0.2.0
        private clsLocSts()
        {
        }
    }

    public class clsIOType
    {
        public const string cstrRtoR = "51";
        //11 > ERP單據混料入庫作業
        //12 > ERP單據整批入庫作業
        //13 > ASRS 入庫作業
        //14 > 空棧板入庫作業
        //15 > 板架貨物入庫作業
        //16 > 餘料入庫
        //21 > ERP單據出庫作業
        //22 > ASRS出庫作業
        //23 > 併版出庫作業
        //25 > 空棧板出庫作業
        //51 > 庫對庫作業
        //41 > 站對站作業
        //31 > 依儲位盤點
        //32 > 依料號盤點
        //33 > 盤點調帳
        //61> 模具資料維護
        //62> 模具領用作業
        //63> 模具入庫作業
        //71> 庫對庫預排出庫作業
        //72> 庫對庫調整作業-呆滯料
        //73> 庫對庫調整作業-先進先出庫存調整


        private clsIOType()
        {
        }
    }

    public class clsPC2PLC_Sts
    {
        public const string cstrNone = "0";
        public const string cstrInitial = "1";
        public const string cstrCheckOK = "2";
        public const string cstrCheckNG = "3";
        public const string cstrStnInMode = "1";
        public const string cstrStnOutMode = "2";
        public const string cstrNoCMD = "1";
        public const string cstrBCRReadFail = "2";
        public const string cstrOverWeight = "3";
        public const string cstrLocFail = "4";
    }
    public class clsCmdAbnormal
    {
        /// <summary>
        /// 正常:NA
        /// </summary>
        public const string cstrNA = "NA";
        /// <summary>
        /// 電腦強制完成:CF
        /// </summary>
        public const string cstrCF = "CF";
        /// <summary>
        /// 電腦取消:CC
        /// </summary>
        public const string cstrCC = "CC";
        /// <summary>
        /// 地上盤強制完成:FF
        /// </summary>
        public const string cstrFF = "FF";
        /// <summary>
        /// 地上盤強制取消:EF
        /// </summary>
        public const string cstrEF = "EF";
        /// <summary>
        /// 空出庫:E2
        /// </summary>
        public const string cstrEmptyOut = "E2";
        /// <summary>
        /// 二重格:EC
        /// </summary>
        public const string cstrEC = "EC";
    }
}
