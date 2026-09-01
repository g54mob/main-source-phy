using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

public class IPAddressHelper
{
	public static string TryResolveHostname(string hostname)
	{
		string result = null;
		try
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
			if (hostEntry.AddressList.Length > 0)
			{
				result = hostEntry.AddressList[0].ToString();
			}
		}
		catch
		{
		}
		return result;
	}

	public static string ResolveOrFallback(string hostname, string fallback)
	{
		string text = TryResolveHostname(hostname);
		if (text == null)
		{
			return fallback;
		}
		return text;
	}

	public static bool IsLocalhost(string hostNameOrAddress)
	{
		if (string.IsNullOrEmpty(hostNameOrAddress))
		{
			return false;
		}
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostNameOrAddress);
			IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
			return hostAddresses.Any((IPAddress hostIP) => IPAddress.IsLoopback(hostIP) || localIPs.Contains(hostIP));
		}
		catch
		{
			return false;
		}
	}

	public static bool IsInternal(string testIp)
	{
		if (testIp == "::1")
		{
			return true;
		}
		IPAddress address;
		if (!IPAddress.TryParse(testIp, out address))
		{
			return false;
		}
		byte[] addressBytes = address.GetAddressBytes();
		switch (addressBytes[0])
		{
		case 10:
		case 127:
			return true;
		case 172:
			return addressBytes[1] >= 16 && addressBytes[1] < 32;
		case 192:
			return addressBytes[1] == 168;
		default:
			return false;
		}
	}

	public static bool MatchesInternalIPs(string[] otherInteralIps)
	{
		IEnumerable<string> internalIPs = GetInternalIPs();
		return internalIPs.All((string s) => otherInteralIps.Contains(s));
	}

	public static List<string> GetInternalIPs()
	{
		List<string> list = new List<string>();
		list.AddRange(QueryAllNetworkInterfaces());
		list.AddRange(QueryHostEntry());
		list.Add(Network.player.ipAddress);
		return list.Distinct().ToList();
	}

	private static List<string> QueryAllNetworkInterfaces()
	{
		IEnumerable<NetworkInterface> source = from x in NetworkInterface.GetAllNetworkInterfaces()
			where x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || x.NetworkInterfaceType == NetworkInterfaceType.Ethernet
			select x;
		return source.SelectMany((NetworkInterface x) => from y in x.GetIPProperties().UnicastAddresses
			where y.Address.AddressFamily == AddressFamily.InterNetwork
			select y.Address.ToString()).ToList();
	}

	private static List<string> QueryHostEntry()
	{
		IPHostEntry entry;
		if (!GetHostEntry(out entry))
		{
			return new List<string>();
		}
		return (from x in entry.AddressList
			where x.AddressFamily == AddressFamily.InterNetwork
			select x.ToString()).ToList();
	}

	private static bool GetHostEntry(out IPHostEntry entry)
	{
		try
		{
			entry = Dns.GetHostEntry(Dns.GetHostName());
			return true;
		}
		catch (ArgumentNullException)
		{
		}
		catch (ArgumentOutOfRangeException)
		{
		}
		catch (SocketException)
		{
		}
		catch (ArgumentException)
		{
		}
		entry = null;
		return false;
	}
}
