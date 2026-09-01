using System;
using System.Net;
using System.Net.Sockets;

public static class IPAddressExtensions
{
	public static uint ToUint(this IPAddress address)
	{
		if (address.AddressFamily == AddressFamily.InterNetwork)
		{
			byte[] addressBytes = address.GetAddressBytes();
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(addressBytes);
			}
			return BitConverter.ToUInt32(addressBytes, 0);
		}
		throw new ArgumentOutOfRangeException("address", "Address must be IPv4");
	}
}
