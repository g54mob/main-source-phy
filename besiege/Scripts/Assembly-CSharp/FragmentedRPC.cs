using System;
using System.Collections.Generic;
using UnityEngine;

public class FragmentedRPC
{
	public class FragmentInfo
	{
		public ushort ID;

		public Dictionary<ushort, byte[]> fragments;

		public ushort fragmentCount;

		public bool hasCount;

		public int totalLength;

		public FragmentInfo(ushort id)
		{
			ID = id;
			fragments = new Dictionary<ushort, byte[]>(100);
		}

		public void Reset()
		{
			fragmentCount = 0;
			hasCount = false;
			totalLength = 0;
			if (fragments.Count > 0)
			{
				fragments.Clear();
			}
		}
	}

	private const int RPCHeaderSize = 28;

	public float createTime;

	private Dictionary<ushort, FragmentInfo> fragments;

	private static Queue<FragmentInfo> infoPool = new Queue<FragmentInfo>();

	public FragmentedRPC()
	{
		fragments = new Dictionary<ushort, FragmentInfo>();
	}

	public int GetCurrentCount(ushort playerId)
	{
		FragmentInfo value;
		if (fragments.TryGetValue(playerId, out value) && value.hasCount)
		{
			return value.fragments.Count;
		}
		return 0;
	}

	public int GetCompletionPercentage(ushort playerId)
	{
		FragmentInfo value;
		if (fragments.TryGetValue(playerId, out value) && value.hasCount)
		{
			float num = (int)value.fragmentCount;
			return (int)((float)value.fragments.Count / num * 100f);
		}
		return 0;
	}

	public void Clear()
	{
		fragments.Clear();
	}

	public void Clear(ushort id)
	{
		FragmentInfo value;
		if (fragments.TryGetValue(id, out value) && fragments.Remove(id))
		{
			infoPool.Enqueue(value);
		}
	}

	public bool Add(ushort id, ushort index, byte[] data, out byte[] outData)
	{
		FragmentInfo value;
		if (!fragments.TryGetValue(id, out value))
		{
			value = ((infoPool.Count != 0) ? infoPool.Dequeue() : new FragmentInfo(id));
			value.Reset();
			fragments.Add(id, value);
		}
		if (value.fragments.ContainsKey(index))
		{
			Debug.LogWarning("Data conflict (" + id + " > " + index + ") " + Environment.StackTrace);
			if (fragments.Remove(id))
			{
				infoPool.Enqueue(value);
			}
			outData = null;
			return false;
		}
		if (index == 0)
		{
			value.fragmentCount = NetworkCompression.ReadUInt16(data, 0);
			value.hasCount = true;
		}
		value.totalLength += data.Length;
		value.fragments.Add(index, data);
		if (!value.hasCount || value.fragmentCount != value.fragments.Count)
		{
			outData = null;
			return false;
		}
		outData = new byte[value.totalLength - 2];
		ushort num = 0;
		int num2 = 0;
		byte[] value2;
		while (value.fragments.TryGetValue(num, out value2))
		{
			int num3 = value2.Length - ((num == 0) ? 2 : 0);
			if (num3 < 0)
			{
				continue;
			}
			if (num2 + num3 > outData.Length)
			{
				if (fragments.Remove(id))
				{
					infoPool.Enqueue(value);
				}
				Debug.LogError("Fragmented data is corrupt: " + id + " - " + index + "! " + Environment.StackTrace);
				outData = null;
				return false;
			}
			Buffer.BlockCopy(value2, (num++ == 0) ? 2 : 0, outData, num2, num3);
			num2 += num3;
		}
		if (fragments.Remove(id))
		{
			infoPool.Enqueue(value);
		}
		return true;
	}

	public static void Send(Action<ushort, byte[]> sendFunc, byte[] data, int overhead, int headerSize)
	{
		int num = OptionsMaster.BesiegeConfig.MaximumTransmissionUnit - 28 - overhead - headerSize;
		int num2 = 0;
		int num3 = data.Length;
		float num4 = num3 + 2;
		ushort num5 = (ushort)Mathf.CeilToInt(num4 / (float)num);
		for (ushort num6 = 0; num6 < num5; num6++)
		{
			int num7 = ((num6 == 0) ? 2 : 0);
			int num8 = num3 + num7 - num2;
			int num9 = ((num8 >= num) ? num : num8);
			int num10 = num9 - num7;
			byte[] array = new byte[headerSize + num9];
			if (num6 == 0)
			{
				NetworkCompression.WriteUInt16(num5, array, headerSize);
			}
			Buffer.BlockCopy(data, num2, array, num7 + headerSize, num10);
			sendFunc(num6, array);
			num2 += num10;
		}
	}
}
