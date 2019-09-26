using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICommon
{
    public interface IProtocol
    {
        Dictionary<string,int> _defaultDataItem { get; set; }
        //<_commandType,_defaultDataItem<Item,Bytes>>
        Dictionary<string, Dictionary<string, int>> _commandType { get; set; }
        Dictionary<string, string> _itemValue { get; set; }
        Dictionary<string, int> _itemIdx { get; set; }
        string _startPoint { get; set; }
        string _endPoint { get; set; }
        string _seqNo { get; set; }
        string _commandCode { get; set; }
        string _reSent { get; set; }
        string _triggerTime { get; set; }
        List<string> _waitConfirm { get; set; }
        Dictionary<string, string> _data { get; set; }
        
        byte[] BCC(Dictionary<string, string> dic);
        List<string> _dataItem { get; set; }
    }
    
}
