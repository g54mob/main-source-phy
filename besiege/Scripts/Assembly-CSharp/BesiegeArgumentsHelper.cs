using System.IO;
using System.Net;
using UnityEngine;

internal static class BesiegeArgumentsHelper
{
	public static IPEndPoint ParseIPPort(Arguments args)
	{
		if (args == null)
		{
			Debug.LogError("Could not parse IP and port, arguments is null");
			return null;
		}
		return ParseIPPort(args.Single("connect"));
	}

	public static IPEndPoint ParseIPPort(string ipPortString)
	{
		string[] array = ipPortString.Split(':');
		bool flag = true;
		if (array.Length != 2)
		{
			flag = false;
		}
		IPAddress address;
		if (!IPAddress.TryParse(array[0], out address))
		{
			Debug.LogError(string.Concat("Unable to parse the ip address '", address, "'."));
			return null;
		}
		int result = StatMaster.DefaultPort;
		if (flag && !int.TryParse(array[1], out result))
		{
			Debug.LogError("Unable to parse the port '" + array[1] + "'.");
			return null;
		}
		return new IPEndPoint(address, result);
	}

	public static ulong ParseUlong(string numberString)
	{
		ulong result = 0uL;
		if (string.IsNullOrEmpty(numberString))
		{
			return result;
		}
		ulong.TryParse(numberString, out result);
		return result;
	}

	public static float ParseFloat(string numberString)
	{
		float result = float.MaxValue;
		if (string.IsNullOrEmpty(numberString))
		{
			return result;
		}
		double result2;
		if (!float.TryParse(numberString, out result) && double.TryParse(numberString, out result2))
		{
			result = (float)result2;
		}
		return result;
	}

	public static string ParseSceneName(string rawLevelName)
	{
		if (string.IsNullOrEmpty(rawLevelName))
		{
			return null;
		}
		if (rawLevelName.StartsWith("MISTY"))
		{
			return "MISTY MOUNTAIN";
		}
		if (rawLevelName.StartsWith("BARREN"))
		{
			return "BARREN EXPANSE";
		}
		if (rawLevelName.StartsWith("LEGACY"))
		{
			return "LEGACY SANDBOX";
		}
		return rawLevelName;
	}

	public static string ParseMachinePath(string rawPath)
	{
		if (string.IsNullOrEmpty(rawPath))
		{
			return null;
		}
		string result = rawPath.Replace("\\\\", "\\");
		if (!File.Exists(rawPath))
		{
			return null;
		}
		return result;
	}

	public static ulong ParseServerId(string serverIdString)
	{
		return ParseUlong(serverIdString);
	}

	public static ulong ParseLobbyId(string lobbyIdString)
	{
		return ParseUlong(lobbyIdString);
	}
}
