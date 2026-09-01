using System;
using System.IO;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct SteamGameServerConfiguration
	{
		[FormerlySerializedAs("autoInitialize")]
		public bool autoInitialise;

		public bool autoLogon;

		public uint ip;

		public ushort gamePort;

		public ushort queryPort;

		public ushort spectatorPort;

		public string serverVersion;

		public bool usingGameServerAuthApi;

		public bool enableHeartbeats;

		public bool supportSpectators;

		public string spectatorServerName;

		public bool anonymousServerLogin;

		public string gameServerToken;

		public bool isPasswordProtected;

		public string serverName;

		public string gameDescription;

		public string gameDirectory;

		public bool isDedicated;

		public int maxPlayerCount;

		public int botPlayerCount;

		public string mapName;

		public string gameData;

		public StringKeyValuePair[] rulePairs;

		public static SteamGameServerConfiguration Default => default(SteamGameServerConfiguration);

		public string IpAddress
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public readonly bool DebugValidate()
		{
			return false;
		}

		public static SteamGameServerConfiguration Get()
		{
			return default(SteamGameServerConfiguration);
		}

		public static bool Get(FileInfo configFile, out SteamGameServerConfiguration config)
		{
			config = default(SteamGameServerConfiguration);
			return false;
		}

		public static bool Get(string configFile, out SteamGameServerConfiguration config)
		{
			config = default(SteamGameServerConfiguration);
			return false;
		}

		public static bool Get(byte[] serializedData, out SteamGameServerConfiguration config)
		{
			config = default(SteamGameServerConfiguration);
			return false;
		}

		public override string ToString()
		{
			return null;
		}

		public byte[] ToBytes()
		{
			return null;
		}

		public bool SaveToDisk(string path)
		{
			return false;
		}

		public static bool LoadFromDisk(string path, out SteamGameServerConfiguration config)
		{
			config = default(SteamGameServerConfiguration);
			return false;
		}

		public bool SaveToDiskAsIni(string path)
		{
			return false;
		}

		public static bool LoadFromDiskAsIni(string path, out SteamGameServerConfiguration config)
		{
			config = default(SteamGameServerConfiguration);
			return false;
		}

		public static SteamGameServerConfiguration ParseIniString(string iniData)
		{
			return default(SteamGameServerConfiguration);
		}

		public static string ToIniString(SteamGameServerConfiguration config)
		{
			return null;
		}

		public void Clear()
		{
		}
	}
}
