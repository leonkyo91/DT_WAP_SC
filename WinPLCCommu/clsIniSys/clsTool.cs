using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mirle.Library
{
    public class clsTool
    {
        private clsTool()
        {
        }

        /// <summary>
        /// 將字串轉成列舉值
        /// </summary>
        /// <typeparam name="TEnum">列舉Type</typeparam>
        /// <param name="EnumAsString">列舉字串</param>
        /// <returns>傳回列舉值</returns>
        public static TEnum funGetEnumValue<TEnum>(string EnumAsString) where TEnum : struct
        {
            TEnum enumType = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
            Enum.TryParse<TEnum>(EnumAsString, out enumType);
            return enumType;
        }

        /// <summary>
        /// 將數值轉成列舉值
        /// </summary>
        /// <typeparam name="TEnum">列舉Type</typeparam>
        /// <param name="EnumAsString">列舉數值</param>
        /// <returns>傳回列舉值</returns>
        public static TEnum funGetEnumValue<TEnum>(int EnumAsInt) where TEnum : struct
        {
            TEnum enumType = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
            Enum.TryParse<TEnum>(EnumAsInt.ToString(), out enumType);
            return enumType;
        }

        /// <summary>
        /// 將String強制轉成Int型態
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static int funConvertToInt(string Value)
        {
            int intTmp;
            int intValue = 0;
            double dobValue = 0;
            double dobTmp;
            string strValue = string.Empty;

            try
            {
                if(int.TryParse(Value, out intTmp))
                    intValue = int.Parse(Value);
                else
                {
                    if(double.TryParse(Value, out dobTmp))
                    {
                        dobValue = double.Parse(Value);
                        dobValue = Math.Floor(dobValue);
                        strValue = dobValue.ToString();
                        if(int.TryParse(strValue, out intTmp))
                            intValue = int.Parse(strValue);
                        else
                            intValue = 0;
                    }
                    else
                        intValue = 0;
                }
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
            }
            return intValue;
        }       
        
        /// <summary>
        /// 取Combox的值(去除描述)
        /// Input : Combox.Text
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string funGetComboxValue(string Value)
        {
            try
            {
                int i = Value.IndexOf(":");
                if(i >= 0)
                    return Value.Substring(0, i).Trim();
                else
                    return Value.Trim();
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                return Value;
            }
        }

        /// <summary>
        /// 判斷字串是否都為數字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool funIsNumeric(string strNumber)
        {
            Regex NumberPattern = new Regex("[^0-9.-]");
            return !NumberPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 將字串轉成Double
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static double Double(string sData)
        {
            int iValue = 0;
            double dValue = 0;
            string sValue = "";
            int n;

            try
            {

                double n1;
                if (double.TryParse(sData, out n1))
                {
                    dValue = double.Parse(sData);     // 取Double                    
                    //dValue = Math.Floor(dValue + 0.5);  // 四捨五入
                    //dValue = Math.Floor(dValue);  // 無條件捨去
                    //sValue = dValue.ToString();         // 轉字串
                    //if (int.TryParse(sValue, out n))
                    //{
                    //    iValue = int.Parse(sValue);     // 取整數
                    //}
                    //else
                    //{
                    //    iValue = 0;
                    //}
                }
                else
                {
                    dValue = 0;
                }


            }
            catch
            {

            }
            return dValue;
        }
        
    }
}
