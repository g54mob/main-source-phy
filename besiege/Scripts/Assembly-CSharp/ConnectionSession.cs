public class ConnectionSession
{
	public SessionPlayerStatus PlayerStatus = SessionPlayerStatus.Unknown;

	public ushort ConnectionID;

	public string Username = string.Empty;

	public double LastClientTS;

	public double Ping;
}
