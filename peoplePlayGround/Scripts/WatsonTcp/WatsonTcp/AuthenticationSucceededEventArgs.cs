namespace WatsonTcp
{
	public class AuthenticationSucceededEventArgs
	{
		public string IpPort { get; }

		internal AuthenticationSucceededEventArgs(string ipPort)
		{
			IpPort = ipPort;
		}
	}
}
