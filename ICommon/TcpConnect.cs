using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICommon
{
    public class TcpConnect:ITransfer
    {
        private TcpListener _serverListen = null;
        private TcpClient _client = null;
        private NetworkStream _stream = null;
        private IPEndPoint _remoteIpEndPoint;
        private IPAddress _iPaddress = null;
        private int _port =0;
        private Boolean isStop = false;
        private int iConnectCount = 0;
        private int _maxbytes = 0;
        //public event EventHandler<MsgEventArg> SendMsg;
        public event EventHandler<MsgEventArg> ReceiveMsg;
        public event EventHandler<MsgEventArg> RefreshMessageInfo;
        public event EventHandler<MsgEventArg> UpdateConnect;
        Thread _Listen;
        MsgHandler _msgHandel;
        private enum E { waiting = 1, connected = 2, display = 3, disconnected = 4 };
        public IPAddress IPaddress { get { return _iPaddress; } set { _iPaddress = value; } }
        public int Port { get { return _port; } set { _port = value; } }
        public int Maxbytes { get { return _maxbytes; } set { _maxbytes = value; } }
        public TcpListener ServerListen
        {
            get { return _serverListen; }
            set { _serverListen = value; }
        }
        public TcpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
        public NetworkStream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }
        public IPEndPoint RemoteIpEndPoint
        {
            get { return _remoteIpEndPoint; }
            set { _remoteIpEndPoint = value; }
        }

        public void OpenServer(bool IsServer)
        {
            try
            {
                if (IsServer)
                {
                    _Listen = new Thread(Listen);
                    _Listen.Start();
                }
                else
                {
                    _Listen = new Thread(Listen);
                    _Listen.Start();
                    //RemoteIpEndPoint = new IPEndPoint(IPaddress, Port);
                    //Client = new TcpClient();
                    //Client.Connect(RemoteIpEndPoint);
                    //Stream = Client.GetStream();
                    //MsgEventArg ms = new MsgEventArg();
                    //ms._conn = Convert.ToInt32(E.connected);
                    //Updateconnect(ms);
                    //while (Client.Connected)
                    //{
                    //    try
                    //    {
                    //        while (true)
                    //        {
                    //            if (Stream.CanRead)
                    //            {
                    //                break;
                    //            }
                    //        }
                    //        _Listen = new Thread(ReceiveReplyData);
                    //        _Listen.Start(Stream);
                    //        //ReceiveReplyData(Stream, Client.ReceiveBufferSize);

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MsgEventArg msg = new MsgEventArg();
                    //        msg._message = ex.Message + "-" + ex.StackTrace;
                    //        RefreshMsg(msg);
                    //        break;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MsgEventArg msg = new MsgEventArg();
                msg._message = ex.Message + "-" + ex.StackTrace;
                RefreshMsg(msg);
            }
            
            //if (IsServer)
            //{
            //    ServerListen = new TcpListener(IPaddress, Port);
            //    ServerListen.Start();
            //    Client = new TcpClient();
            //}
            //else
            //{
            //    RemoteIpEndPoint = new IPEndPoint(IPaddress, Port);
            //    Client = new TcpClient();
            //    Client.Connect(RemoteIpEndPoint);
            //    Stream = Client.GetStream();
            //}
        }
        public void CloseServer(bool IsServer)
        {
            if (Client != null)
            {
                Client.Close();
                Client.Dispose();
                if (IsServer)
                    ServerListen.Stop();

                MsgEventArg ms = new MsgEventArg();
                ms._conn= Convert.ToInt32(E.disconnected);
                Updateconnect(ms);
            }
        }
        public void OnSendMsg(MsgEventArg e)
        {

        }
        private void ReceiveReplyData(NetworkStream myNetworkStream, int ReceiveBufferSize)
        {
            int InBytesCount;
            Byte[] myReceiveBytes = new byte[ReceiveBufferSize];
            DateTime startTime = DateTime.Now;

            while (true)
            {
                try
                {
                    InBytesCount = myNetworkStream.Read(myReceiveBytes, 0, myReceiveBytes.Length);
                    Thread.Sleep(50);

                    if (InBytesCount != 0)
                    {
                        byte[] bytesRead = new byte[InBytesCount];
                        Array.Copy(myReceiveBytes, bytesRead, InBytesCount);
                        try
                        {
                            Thread Receve = new Thread(OnReceiveMsg);
                            MsgEventArg msg = new MsgEventArg();
                            msg._btdata = bytesRead;
                            Receve.Start(msg);
                            //OnReceiveMsg(msg);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        CloseServer(false);
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
        
        protected virtual void OnReceiveMsg(object obj)//MsgEventArg e)
        {
            EventHandler<MsgEventArg> handler = ReceiveMsg;
            if (handler != null)
            {
                MsgEventArg e = obj as MsgEventArg;
                handler(this, e);
            }
        }
        
        protected virtual void RefreshMsg(MsgEventArg e)
        {
            EventHandler<MsgEventArg> handler = RefreshMessageInfo;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void Updateconnect(MsgEventArg e)
        {
            EventHandler<MsgEventArg> handler = UpdateConnect;
            if (handler != null)
            {
                handler(this,e);
            }
        }
        private void Listen()
        {
            try
            {
                //_msgHandel = new MsgHandler();
                RemoteIpEndPoint = new IPEndPoint(IPaddress, Port);
                Client = new TcpClient();
                Client.Connect(RemoteIpEndPoint);
                Stream = Client.GetStream();
                MsgEventArg ms = new MsgEventArg();
                ms._conn = Convert.ToInt32(E.connected);
                Updateconnect(ms);
                while (Client.Connected)
                {
                    try
                    {
                        while (true)
                        {
                            if (Stream.CanRead)
                            {
                                break;
                            }
                        }
                        //_Listen = new Thread(ReceiveReplyData);
                        //_Listen.Start(Stream);
                        ReceiveReplyData(Stream, Maxbytes);
                    }
                    catch (Exception ex)
                    {
                        MsgEventArg msg = new MsgEventArg();
                        msg._message = ex.Message + "-" + ex.StackTrace;
                        RefreshMsg(msg);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEventArg msg = new MsgEventArg();
                msg._message = ex.Message + "-" + ex.StackTrace;
                msg._conn = Convert.ToInt32(E.disconnected);
                Updateconnect(msg);
                RefreshMsg(msg);
            }
        }

    }
}
