using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Communication
{
  public class CommonTCPServer : IDisposable
  {
    string _ListenerIp;
    int _ListenerPort;
    bool _Ssl;
    string _PfxFilename = null;
    string _PfxPassword = null;
    string _LastClientIpPort = null;
    int _IdleClientTimeoutMs = 0;
    //public event DataReceiveHandler OnDataReceived;
    //public event DataSentHandler OnClientSend;
    //public event DataBackupHandler OnBackup;
    SimpleTcpServer _Server;
    bool _RunForever = true;

    public CommonTCPServer Init(string host, int port, bool ssl = false)
    {
      _ListenerIp = host;
      _ListenerPort = port;
      _Ssl = ssl;
      //if (_Ssl)
      //{
      //    _PfxFilename = InputString("PFX Certificate File:", "simpletcp.pfx", false);
      //    _PfxPassword = InputString("PFX File Password:", "simpletcp", false);
      //    _Server.Connections.
      //    _Server = new SimpleTcpServer(_ListenerIp, _ListenerPort, _Ssl, _PfxFilename, _PfxPassword);
      //}
      //else
      {
        _Server = new SimpleTcpServer(_ListenerIp, _ListenerPort);
      }
      _Server.Events.ClientConnected += ClientConnected;
      _Server.Events.ClientDisconnected += ClientDisconnected;
      _Server.Events.DataReceived += DataReceived;
      _Server.Events.DataSent += DataSent;
      _Server.Keepalive.EnableTcpKeepAlives = true;
      _Server.Settings.IdleClientTimeoutMs = _IdleClientTimeoutMs;
      _Server.Settings.MutuallyAuthenticate = false;
      _Server.Settings.AcceptInvalidCertificates = true;
      _Server.Settings.NoDelay = true;

      return this;
    }
    public void Start()
    {
      _Server.Start();
    }
    public static string GetLocalIPAddress()
    {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in host.AddressList)
      {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
          return ip.ToString();
        }
      }
      throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    //public static string GetLocalIPAddress(string templateIP)
    //{
    //    var template = templateIP.Split('.');
    //    if (template.Length != 4)
    //    {
    //        throw new Exception("Wrong Template IP");
    //    }
    //    var host = Dns.GetHostEntry(Dns.GetHostName());
    //    foreach (var ip in host.AddressList)
    //    {
    //        if (Regex.IsMatch(ip.ToString(), $"{template[0]}.{template[1]}.{template[2]}.[0-254]"))
    //            return ip.ToString();
    //    }
    //    return "0.0.0.0";
    //}
    public void Stop()
    {
      _Server.Stop();
    }
    void ClientConnected(object sender, ConnectionEventArgs e)
    {
      _LastClientIpPort = e.IpPort;
      Console.WriteLine("[" + e.IpPort + "] client connected");
      //DataReceived?.Invoke(_LastClientIpPort, Enums.eTCPConn_State.Init_Success, e.Reason);
    }

    void ClientDisconnected(object sender, ConnectionEventArgs e)
    {
      Console.WriteLine("[" + e.IpPort + "] client disconnected: " + e.Reason.ToString());
      //DataReceived?.Invoke(_LastClientIpPort, Enums.eTCPConn_State.Init_Fail, e.Reason);

    }

    void DataReceived(object sender, DataReceivedEventArgs e)
    {
      //OnDataReceived?.Invoke(sender, e);
    }

    private void DataSent(object sender, DataSentEventArgs e)
    {
      //Console.WriteLine("[" + e.IpPort + "] sent " + e.BytesSent + " bytes");
      //OnClientSend?.Invoke(sender, e);
    }

    public void Send(string ipPort, byte[] data)
    {
      if (!string.IsNullOrEmpty(ipPort))
      {
        if (data != null && data.Length > 0)
        {
          _Server.Send(ipPort, data);
        }
      }
    }
    public List<string> ListClients()
    {
      return _Server.GetClients().ToList();
    }

    public async Task SendAsync(string ipPort, byte[] data)
    {
      if (!string.IsNullOrEmpty(ipPort))
      {
        if (data != null && data.Length > 0)
        {
          await _Server.SendAsync(ipPort, data);
        }
      }
    }

    public void RemoveClient(string ipPort)
    {
      if (!string.IsNullOrEmpty(ipPort))
      {
        _Server.DisconnectClient(ipPort);
      }
    }

    public void Dispose()
    {
      if (this._Server != null)
      {
        _Server.Stop();
        _Server.Dispose();
      }
    }
  }
}
