public class FallbackHost
{
	public string ResolvedAddress { get; private set; }

	public FallbackHost(string hostname, string fallbackIp)
	{
		ResolvedAddress = IPAddressHelper.ResolveOrFallback(hostname, fallbackIp);
	}
}
