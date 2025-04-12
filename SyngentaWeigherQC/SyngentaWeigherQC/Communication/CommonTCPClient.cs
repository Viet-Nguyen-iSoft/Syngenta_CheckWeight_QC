using SuperSimpleTcp;
using System;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Communication
{
  public class CommonTCPClient
  {
    string _ServerIp;
    int _ServerPort;
    bool _Ssl;
    public string Name;
    SimpleTcpClient _Client;
    public bool Connected => _Client != null && _Client.IsConnected;
    public event EventHandler<ConnectionEventArgs> OnConnectionEventRaise;
    public event EventHandler<DataReceivedEventArgs> OnDataReceive;
    public CommonTCPClient(string host, int port, string name, bool _ssl = false)
    {
      Init(host, port, name);
    }
    public void Connect()
    {
      try
      {
        _Client.Connect();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public void Disconnect()
    {
      try
      {
        _Client.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public void Init(string host, int port, string Name, bool _ssl = false)
    {
      _ServerIp = host;
      _ServerPort = port;
      _Ssl = _ssl;
      this.Name = Name;
      _Client = new SimpleTcpClient(_ServerIp, _ServerPort);

      _Client.Events.Connected += ConnectedHandler;
      _Client.Events.Disconnected += Disconnected;
      _Client.Events.DataReceived += DataReceived;
      _Client.Events.DataSent += DataSent;
      _Client.Keepalive.EnableTcpKeepAlives = true;
      _Client.Settings.MutuallyAuthenticate = false;
      _Client.Settings.AcceptInvalidCertificates = true;
      _Client.Settings.ConnectTimeoutMs = 300;
      _Client.Settings.NoDelay = true;
    }

    private void ConnectedHandler(object sender, ConnectionEventArgs e)
    {
      Console.WriteLine("*** Server " + e.IpPort + " connected");
      OnConnectionEventRaise?.Invoke(sender, e);
    }

    public void Disconnected(object sender, ConnectionEventArgs e)
    {
      Console.WriteLine("*** Server " + e.IpPort + " disconnected");
      OnConnectionEventRaise?.Invoke(sender, e);
    }

    public void DataReceived(object sender, DataReceivedEventArgs e)
    {
      OnDataReceive?.Invoke(sender, e);
    }

    private static void DataSent(object sender, DataSentEventArgs e)
    {
      //Console.WriteLine("[" + e.IpPort + "] sent " + e.BytesSent + " bytes");
    }
    public void Send(byte[] data)
    {
      if (_Client == null || !_Client.IsConnected) throw new Exception("Socket Server Can Not Be Connect");
      _Client.Send(data);
    }

    public async Task SendAsync(byte[] data)
    {
      if (_Client == null || !_Client.IsConnected) throw new Exception("Socket Server Can Not Be Connect");
      await _Client.SendAsync(data);
    }

    public void Logger(string msg)
    {
      Console.WriteLine(msg);
    }
    //public async Task<ProtocolEntity> NetworkInfo()
    //{
    //  var host = _Client.ServerIpPort;
    //  var ip = host.Split(':')[0];
    //  var port = host.Split(':')[1];
    //  var Name = this.Name;
    //  var status = _Client.IsConnected;
    //  if (status == true)
    //  {
    //    return new ProtocolEntity()
    //    {
    //      ProtocolName = Name,
    //      ProtocolAddress = ip,
    //      ProtocolPort = port,
    //      ProtocolStatus = "Connected",

    //    };
    //  }
    //  else
    //  {
    //    return new ProtocolEntity()
    //    {
    //      ProtocolName = Name,
    //      ProtocolAddress = ip,
    //      ProtocolPort = port,
    //      ProtocolStatus = "Disconnected",

    //    };
    //  }

    //}

  }
}
