namespace WatsonTcp
{
	public class AuthenticationRequestedEventArgs
	{
		public string IpPort { get; }

		internal AuthenticationRequestedEventArgs(string ipPort)
		{
			IpPort = ipPort;
		}
	}
}
