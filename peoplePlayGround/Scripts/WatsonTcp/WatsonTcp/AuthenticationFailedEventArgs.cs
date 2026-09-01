namespace WatsonTcp
{
	public class AuthenticationFailedEventArgs
	{
		public string IpPort { get; }

		internal AuthenticationFailedEventArgs(string ipPort)
		{
			IpPort = ipPort;
		}
	}
}
