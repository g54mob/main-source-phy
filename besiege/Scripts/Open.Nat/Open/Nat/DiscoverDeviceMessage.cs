using System.Globalization;

namespace Open.Nat
{
	internal static class DiscoverDeviceMessage
	{
		public static string Encode(string serviceType)
		{
			return string.Format(CultureInfo.InvariantCulture, "M-SEARCH * HTTP/1.1\r\nHOST: 239.255.255.250:1900\r\nMAN: \"ssdp:discover\"\r\nMX: 3\r\nST: urn:schemas-upnp-org:service:{0}\r\n\r\n", serviceType);
		}
	}
}
