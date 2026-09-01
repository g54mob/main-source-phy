using System;
using System.Collections.Generic;
using System.Text;
using Steamworks;
using UnityEngine;

public class MachineInfo
{
	public enum MachineType
	{
		Built = 0,
		Local = 1,
		Workshop = 2,
		Multiplayer = 3
	}

	public List<BlockInfo> Blocks;

	public MachineType Type { get; set; }

	public Vector3 Position { get; set; }

	public Quaternion Rotation { get; set; }

	public string Name { get; set; }

	public string Author { get; set; }

	public XDataHolder MachineData { get; set; }

	public List<BlockSkinLoader.SkinPack> SkinPacks { get; set; }

	public MachineInfo()
	{
		Position = Vector3.zero;
		Rotation = Quaternion.identity;
		Name = "Uninitialized";
		Author = ((!StatMaster.isMP) ? GetLocalAuthor() : string.Empty);
		Blocks = new List<BlockInfo>();
		MachineData = new XDataHolder();
		Type = (StatMaster.isMP ? MachineType.Multiplayer : MachineType.Built);
	}

	public MachineInfo(Vector3 position, Quaternion rotation, string name, List<BlockInfo> blocks)
	{
		Position = position;
		Rotation = rotation;
		Name = name;
		Author = ((!StatMaster.isMP) ? GetLocalAuthor() : string.Empty);
		Blocks = blocks;
		MachineData = new XDataHolder();
		Type = (StatMaster.isMP ? MachineType.Multiplayer : MachineType.Built);
	}

	public MachineInfo(Vector3 position, Quaternion rotation, string name, List<BlockInfo> blocks, XDataHolder machineData)
	{
		Position = position;
		Rotation = rotation;
		Name = name;
		Author = ((!StatMaster.isMP) ? GetLocalAuthor() : string.Empty);
		Blocks = blocks;
		MachineData = machineData;
		Type = (StatMaster.isMP ? MachineType.Multiplayer : MachineType.Built);
	}

	private string GetLocalAuthor()
	{
		if (SteamManager.Initialized)
		{
			return SteamUser.GetSteamID().m_SteamID.ToString();
		}
		return string.Empty;
	}

	public static int HeaderLength(byte[] mName, bool hasMachineData, byte[] txMachineData)
	{
		return 1 + mName.Length + 12 + 16 + 1 + (hasMachineData ? txMachineData.Length : 0);
	}

	public static int WriteHeader(byte[] nameBytes, bool hasMachineData, byte[] machineData, Vector3 pos, Quaternion rot, XDataHolder data, byte[] buffer, int offset)
	{
		int num = offset;
		buffer[offset] = (byte)nameBytes.Length;
		offset++;
		Buffer.BlockCopy(nameBytes, 0, buffer, offset, nameBytes.Length);
		offset += nameBytes.Length;
		NetworkCompression.PackVector(pos, buffer, offset);
		offset += 12;
		NetworkCompression.PackQuaternion(rot, buffer, offset);
		offset += 16;
		buffer[offset] = (byte)(hasMachineData ? 1u : 0u);
		offset++;
		if (hasMachineData)
		{
			Buffer.BlockCopy(machineData, 0, buffer, offset, machineData.Length);
			offset += machineData.Length;
		}
		return offset - num;
	}

	public byte[] Encode()
	{
		int count = Blocks.Count;
		byte[][] array = new byte[count][];
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			BlockInfo blockInfo = Blocks[i];
			num += (array[i] = blockInfo.Encode()).Length;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(Name);
		byte[] outData;
		bool hasMachineData = MachineData.Encode(out outData);
		int num2 = HeaderLength(bytes, hasMachineData, outData);
		int num3 = NetworkCompression.PackedUIntLength(count, true);
		byte[] array2 = new byte[num2 + num3 + num];
		int num4 = WriteHeader(bytes, hasMachineData, outData, Position, Rotation, MachineData, array2, 0);
		NetworkCompression.PackUInt(count, array2, num4, true, num3);
		num4 += num3;
		NetworkCompression.WriteArray(array, array2, num4);
		return array2;
	}

	public static MachineInfo Decode(byte[] data)
	{
		MachineInfo machineInfo = new MachineInfo();
		machineInfo.Type = MachineType.Multiplayer;
		machineInfo.Author = string.Empty;
		int num = 0;
		int num2 = data[num];
		num++;
		string name = Encoding.UTF8.GetString(data, num, num2);
		machineInfo.Name = name;
		num += num2;
		Vector3 vec = default(Vector3);
		NetworkCompression.UnpackVector(data, num, out vec);
		machineInfo.Position = vec;
		num += 12;
		Quaternion quat = default(Quaternion);
		NetworkCompression.UnpackQuaternion(data, num, out quat);
		machineInfo.Rotation = quat;
		num += 16;
		bool flag = data[num] == 1;
		num++;
		XDataHolder xDataHolder = new XDataHolder();
		if (flag)
		{
			num += xDataHolder.Decode(data, num);
		}
		machineInfo.MachineData = xDataHolder;
		int count;
		num += NetworkCompression.UnpackUInt(data, num, true, out count);
		for (ushort num3 = 0; num3 < count; num3++)
		{
			BlockInfo blockInfo = BlockInfo.Decode(num3, data, num);
			machineInfo.Blocks.Add(blockInfo);
			num += blockInfo.EncodedSize;
		}
		return machineInfo;
	}
}
