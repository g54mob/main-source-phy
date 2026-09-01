public class ServerHost
{
	public string ExternalIP;

	public string InternalIP;

	public ulong GUID;

	public int Port;

	public ServerHost(string externalIp, string internalIp, ulong hostGUID, int port)
	{
		ExternalIP = externalIp;
		InternalIP = internalIp;
		GUID = hostGUID;
		Port = port;
	}
}
