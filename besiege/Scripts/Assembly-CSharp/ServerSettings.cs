using System;
using System.Collections.Generic;
using InternalModding.Mods;

public class ServerSettings
{
	private static int SIZE = 23;

	public bool levelEditor;

	public bool curtainMode;

	public int maxPlayers;

	public string password;

	public float sendRate;

	public int skipChildCount;

	public float camUpdateRate;

	public float vecThreshold;

	public float rotThreshold;

	public bool useUPNP;

	public bool upnpLogDiscovery;

	public bool upnpLogVerbosely;

	public uint dlcMask;

	public float smoothness;

	public List<string> playList;

	public int playListIndex;

	public ServerSettings()
	{
		Init();
	}

	public ServerSettings(int maximumPlayers)
	{
		Init();
		maxPlayers = maximumPlayers;
	}

	private void Init()
	{
		password = string.Empty;
		levelEditor = false;
		maxPlayers = OptionsMaster.maxPlayers;
		playList = new List<string>();
		playListIndex = -1;
		dlcMask = 0u;
		sendRate = OptionsMaster.defaultSendRate;
		camUpdateRate = OptionsMaster.defaultCamUpdateRate;
		skipChildCount = OptionsMaster.defaultSkipChildCount;
		vecThreshold = OptionsMaster.defaultVecThreshold;
		rotThreshold = OptionsMaster.defaultRotThreshold;
		smoothness = OptionsMaster.defaultSmoothness;
	}

	public byte[] Encode()
	{
		byte[] array = ModStatus.EncodeLocalBlockEntityHideStatus();
		byte[] array2 = new byte[SIZE + array.Length];
		int num = 0;
		array2[num] = (byte)maxPlayers;
		num++;
		array2[num] = (byte)(levelEditor ? 1u : 0u);
		num++;
		array2[num] = (byte)skipChildCount;
		num++;
		NetworkCompression.WriteUInt(dlcMask, false, array2, num);
		num += 4;
		byte[] bytes = BitConverter.GetBytes(sendRate);
		byte[] bytes2 = BitConverter.GetBytes(camUpdateRate);
		byte[] bytes3 = BitConverter.GetBytes(vecThreshold);
		byte[] bytes4 = BitConverter.GetBytes(rotThreshold);
		Buffer.BlockCopy(bytes, 0, array2, num, bytes.Length);
		num += bytes.Length;
		Buffer.BlockCopy(bytes2, 0, array2, num, bytes2.Length);
		num += bytes2.Length;
		Buffer.BlockCopy(bytes3, 0, array2, num, bytes3.Length);
		num += bytes3.Length;
		Buffer.BlockCopy(bytes4, 0, array2, num, bytes4.Length);
		num += bytes4.Length;
		Buffer.BlockCopy(array, 0, array2, num, array.Length);
		num += array.Length;
		return array2;
	}

	public static ServerSettings Decode(byte[] data, ref int offset)
	{
		ServerSettings serverSettings = new ServerSettings(data[offset]);
		offset++;
		byte b = data[offset];
		offset++;
		serverSettings.levelEditor = (b & 1) != 0;
		serverSettings.skipChildCount = data[offset];
		offset++;
		serverSettings.dlcMask = NetworkCompression.ReadUInt(false, data, offset);
		offset += 4;
		serverSettings.sendRate = BitConverter.ToSingle(data, offset);
		offset += 4;
		serverSettings.camUpdateRate = BitConverter.ToSingle(data, offset);
		offset += 4;
		serverSettings.vecThreshold = BitConverter.ToSingle(data, offset);
		offset += 4;
		serverSettings.rotThreshold = BitConverter.ToSingle(data, offset);
		offset += 4;
		ModStatus.ApplyRemoteBlockEntityHideStatus(data, ref offset);
		return serverSettings;
	}
}
