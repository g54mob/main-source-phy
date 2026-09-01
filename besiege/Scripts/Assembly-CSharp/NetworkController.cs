using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
	public NetworkEntity[] objList;

	public uint objCount;

	protected byte[] fullBuffer;

	protected int fullBufferSize;

	protected int fullBufferCount;

	protected byte[] essentialBuffer;

	protected int essentialBufferSize;

	protected int essentialBufferCount;

	protected bool useEssentialBuffer;

	protected Dictionary<uint, NetworkEntity> networkObjects = new Dictionary<uint, NetworkEntity>();

	private bool simRunning;

	private bool isTracking;

	private uint currentFrame;

	private byte[] simBuffer;

	private int simBufferSize;

	private uint simBufferCount;

	private int currentCapacity = -1;

	private Dictionary<uint, byte[]> simFrameData;

	public uint ObjectCount
	{
		get
		{
			return (uint)networkObjects.Count;
		}
	}

	public bool isDirty
	{
		get
		{
			return PollObjects(true, objList, objCount);
		}
	}

	public int FullBufferLengthRelative
	{
		get
		{
			return NetworkCompression.PackedUIntLength(fullBufferCount, true) + fullBufferSize;
		}
	}

	public int EssentialBufferLengthRelative
	{
		get
		{
			return NetworkCompression.PackedUIntLength(essentialBufferCount, true) + essentialBufferSize;
		}
	}

	public int FullBufferLength
	{
		get
		{
			return 1 + ((!SendShort) ? 4 : 2) + fullBufferSize;
		}
	}

	public int EssentialBufferLength
	{
		get
		{
			return 1 + ((!SendShort) ? 4 : 2) + essentialBufferSize;
		}
	}

	public bool SendShort
	{
		get
		{
			return UseShort((!isTracking) ? ((uint)networkObjects.Count) : objCount);
		}
	}

	protected bool UseShort(uint count)
	{
		return count <= 65535;
	}

	protected void Awake()
	{
		simFrameData = new Dictionary<uint, byte[]>();
		Clear();
	}

	public void ResetFrame()
	{
		currentFrame = 0u;
	}

	public void UpdateEntities(float delta)
	{
		for (int i = 0; i < objCount; i++)
		{
			objList[i].UpdateEntity(delta);
		}
	}

	public void Clear()
	{
		networkObjects.Clear();
		simRunning = false;
		objCount = 0u;
		simFrameData.Clear();
		if (isTracking)
		{
			fullBufferSize = 0;
			fullBufferCount = 0;
			essentialBufferSize = 0;
			essentialBufferCount = 0;
		}
	}

	public void ToggleEssentialBuffer(bool toggle)
	{
		useEssentialBuffer = toggle;
	}

	public void SetCapacity(int capacity)
	{
		if (capacity > currentCapacity)
		{
			int num = (2 + SendEntity.GetMaxDataSize(true)) * capacity;
			fullBuffer = new byte[num];
			essentialBuffer = new byte[num];
			int num2 = (2 + NetworkEntity.GetMaxDataSize()) * capacity;
			simBuffer = new byte[num2];
			objList = new NetworkEntity[capacity];
			currentCapacity = capacity;
		}
	}

	public void FillStaticArray()
	{
		objCount = 0u;
		Dictionary<uint, NetworkEntity>.Enumerator enumerator = networkObjects.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NetworkEntity value = enumerator.Current.Value;
			value.staticIndex = objCount;
			objList[objCount++] = value;
		}
	}

	public void InitSim(bool track)
	{
		isTracking = track;
		simRunning = true;
	}

	public int GetSimFrame()
	{
		int num = 0;
		bool flag = UseShort(objCount);
		int num2 = ((!flag) ? 4 : 2);
		simBufferCount = 0u;
		for (int i = 0; i < objCount; i++)
		{
			NetworkEntity networkEntity = objList[i];
			if (networkEntity.IsChanged)
			{
				NetworkCompression.WriteUInt(networkEntity.id, flag, simBuffer, num);
				num += num2;
				int num3 = networkEntity.EncodeState(simBuffer, num);
				num += num3;
				simBufferCount++;
			}
		}
		simBufferSize = num;
		return 3 + simBufferSize;
	}

	public void WriteSimFrame(byte[] data, int offset)
	{
		bool flag = UseShort(objCount);
		int num = ((!flag) ? 4 : 2);
		data[offset] = (byte)(flag ? 1u : 0u);
		offset++;
		NetworkCompression.WriteUInt(simBufferCount, flag, data, offset);
		offset += num;
		Buffer.BlockCopy(simBuffer, 0, data, offset, simBufferSize);
	}

	public int ReadSimFrame(byte[] data, int offset)
	{
		simFrameData.Clear();
		int num = offset;
		bool flag = data[offset] == 1;
		int num2 = ((!flag) ? 4 : 2);
		offset++;
		uint num3 = NetworkCompression.ReadUInt(flag, data, offset);
		offset += num2;
		for (int i = 0; i < num3; i++)
		{
			uint key = NetworkCompression.ReadUInt(flag, data, offset);
			offset += num2;
			int dataSize = NetworkEntity.GetDataSize(data[offset]);
			byte[] array = new byte[dataSize];
			Buffer.BlockCopy(data, offset, array, 0, dataSize);
			if (simFrameData.ContainsKey(key))
			{
				simFrameData.Remove(key);
			}
			simFrameData.Add(key, array);
			offset += dataSize;
		}
		return offset - num;
	}

	public void ApplySimFrame()
	{
		ApplySimFrame(simFrameData);
		simFrameData.Clear();
	}

	public void ApplySimFrame(Dictionary<uint, byte[]> simData)
	{
		List<uint> list = new List<uint>(simData.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			uint num = list[i];
			NetworkEntity value;
			if (networkObjects.TryGetValue(num, out value))
			{
				value.DecodeState(simData[num], 0);
				continue;
			}
			Debug.LogError("Couldn't find entity with ID " + num + "!");
			break;
		}
	}

	public void Add(NetworkEntity entity)
	{
		if (simRunning && objCount < objList.Length)
		{
			entity.staticIndex = objCount;
			objList[objCount++] = entity;
		}
		entity.addedToController = true;
		if (networkObjects.ContainsKey(entity.id))
		{
			networkObjects[entity.id] = entity;
		}
		else
		{
			networkObjects.Add(entity.id, entity);
		}
	}

	public void Replace(NetworkEntity entity, uint newId)
	{
		NetworkEntity value;
		if (networkObjects.TryGetValue(newId, out value))
		{
			value.addedToController = false;
			networkObjects[newId] = entity;
			entity.addedToController = true;
		}
		else
		{
			Add(entity);
		}
	}

	public void Replace(NetworkEntity entity, NetworkEntity newEntity)
	{
		if (simRunning)
		{
			objList[entity.staticIndex] = newEntity;
			newEntity.staticIndex = entity.staticIndex;
		}
		networkObjects[entity.id] = newEntity;
	}

	public void TryRemoveRange(NetworkEntity entity, uint count)
	{
		if (simRunning && objList[entity.staticIndex] == entity)
		{
			uint num = entity.staticIndex + count;
			if (num < objCount)
			{
				for (uint num2 = num; num2 < objCount; num2++)
				{
					NetworkEntity networkEntity = objList[num2];
					uint num3 = (networkEntity.staticIndex = num2 - count);
					objList[num3] = networkEntity;
				}
			}
			objCount -= count;
		}
		for (uint num2 = 0u; num2 < count; num2++)
		{
			uint key = entity.id + num2;
			NetworkEntity value;
			if (networkObjects.TryGetValue(key, out value))
			{
				value.addedToController = false;
				networkObjects.Remove(key);
			}
		}
	}

	public void Remove(NetworkEntity entity)
	{
		if (simRunning)
		{
			bool flag = false;
			for (uint num = 0u; num < objCount; num++)
			{
				if (objList[num].id == entity.id)
				{
					flag = true;
				}
				if (flag && num < objCount - 1)
				{
					NetworkEntity networkEntity = objList[num + 1];
					networkEntity.staticIndex = num;
					objList[num] = networkEntity;
				}
			}
			if (flag)
			{
				objCount--;
			}
		}
		networkObjects.Remove(entity.id);
		entity.addedToController = false;
	}

	public int ReadBufferRelative(uint frame, byte[] data, int offset, NetworkEntity[] objectList)
	{
		int num = offset;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
		int num2 = 0;
		int num3 = 0;
		int count2 = 0;
		int num4 = objectList.Length;
		while (num2++ < count)
		{
			offset += NetworkCompression.UnpackUInt(data, offset, true, out count2);
			byte entityState = data[offset];
			bool flag = SendEntity.HasPosition(entityState);
			bool flag2 = SendEntity.HasRotation(entityState);
			int num5 = SendEntity.EventCount(entityState);
			int num6 = 1 + (flag ? 6 : 0) + (flag2 ? 7 : 0) + num5;
			num3 += count2;
			if (num3 < num4)
			{
				NetworkEntity networkEntity = objectList[num3];
				networkEntity.SetData(frame, data, offset, flag, flag2, num5);
				offset += num6;
				continue;
			}
			Debug.LogWarning("Entity index out of bounds: " + num3 + " (" + num4 + ")");
			break;
		}
		return offset - num;
	}

	public bool PollObjectsRelative(bool fullUpdate, NetworkEntity[] objectList)
	{
		fullBufferSize = 0;
		fullBufferCount = 0;
		essentialBufferSize = 0;
		essentialBufferCount = 0;
		ushort num = 0;
		if (!fullUpdate)
		{
			for (uint num2 = 0u; num2 < objectList.Length; num2++)
			{
				NetworkEntity networkEntity = objectList[num2];
				int num3 = NetworkCompression.PackedUIntLength(num, true);
				int num4 = networkEntity.PollObject(false, essentialBuffer, essentialBufferSize + num3);
				if (num4 > 1 && networkEntity.isEssential)
				{
					NetworkCompression.PackUInt(num, essentialBuffer, essentialBufferSize, true, num3);
					num = 0;
					int num5 = num3 + num4;
					essentialBufferSize += num5;
					essentialBufferCount++;
				}
				num++;
			}
			return essentialBufferCount > 0;
		}
		if (useEssentialBuffer)
		{
			int num6 = 0;
			for (uint num2 = 0u; num2 < objectList.Length; num2++)
			{
				NetworkEntity networkEntity = objectList[num2];
				int num3 = NetworkCompression.PackedUIntLength(num, true);
				int num7 = fullBufferSize + num3;
				int num4 = networkEntity.PollObject(fullUpdate, fullBuffer, num7);
				if (num4 > 1)
				{
					NetworkCompression.PackUInt(num, fullBuffer, fullBufferSize, true, num3);
					num = 0;
					int num5 = num3 + num4;
					if (networkEntity.isEssential)
					{
						int num8 = NetworkCompression.PackedUIntLength(num6, true);
						NetworkCompression.PackUInt(num6, essentialBuffer, essentialBufferSize, true, num8);
						num6 = 0;
						Buffer.BlockCopy(fullBuffer, num7, essentialBuffer, essentialBufferSize + num8, num4);
						essentialBufferSize += num8 + num4;
						essentialBufferCount++;
					}
					fullBufferSize += num5;
					fullBufferCount++;
				}
				num++;
				num6++;
			}
		}
		else
		{
			for (uint num2 = 0u; num2 < objectList.Length; num2++)
			{
				NetworkEntity networkEntity = objectList[num2];
				int num3 = NetworkCompression.PackedUIntLength(num, true);
				int num4 = networkEntity.PollObject(fullUpdate, fullBuffer, fullBufferSize + num3);
				if (num4 > 1)
				{
					NetworkCompression.PackUInt(num, fullBuffer, fullBufferSize, true, num3);
					num = 0;
					int num5 = num3 + num4;
					fullBufferSize += num5;
					fullBufferCount++;
				}
				num++;
			}
		}
		return fullBufferCount > 0;
	}

	public int WriteBufferRelative(bool fullUpdate, byte[] buffer, int offset)
	{
		int count = (fullUpdate ? fullBufferCount : essentialBufferCount);
		int num = (fullUpdate ? fullBufferSize : essentialBufferSize);
		int num2 = NetworkCompression.PackedUIntLength(count, true);
		NetworkCompression.PackUInt(count, buffer, offset, true, num2);
		offset += num2;
		Buffer.BlockCopy((!fullUpdate) ? essentialBuffer : fullBuffer, 0, buffer, offset, num);
		return num + num2;
	}

	public bool PollObjects(List<LevelEntity> objectList)
	{
		return PollObjects(true, objectList.ToArray(), (uint)objectList.Count);
	}

	public bool PollObjects(bool fullUpdate, NetworkEntity[] objectList, uint objectCount)
	{
		bool flag = UseShort(objCount);
		int num = ((!flag) ? 4 : 2);
		fullBufferSize = 0;
		fullBufferCount = 0;
		essentialBufferSize = 0;
		essentialBufferCount = 0;
		if (fullUpdate)
		{
			if (!useEssentialBuffer)
			{
				for (int i = 0; i < objectCount; i++)
				{
					NetworkEntity networkEntity = objectList[i];
					int num2 = networkEntity.PollObject(fullUpdate, fullBuffer, fullBufferSize + 2);
					if (num2 > 1)
					{
						NetworkCompression.WriteUInt(networkEntity.id, flag, fullBuffer, fullBufferSize);
						int num3 = num + num2;
						fullBufferSize += num3;
						fullBufferCount++;
					}
				}
				return fullBufferCount > 0;
			}
			for (int i = 0; i < objectCount; i++)
			{
				NetworkEntity networkEntity = objectList[i];
				int num2 = networkEntity.PollObject(fullUpdate, fullBuffer, fullBufferSize + 2);
				if (num2 > 1)
				{
					NetworkCompression.WriteUInt(networkEntity.id, flag, fullBuffer, fullBufferSize);
					int num3 = num + num2;
					if (networkEntity.isEssential)
					{
						Buffer.BlockCopy(fullBuffer, fullBufferSize, essentialBuffer, essentialBufferSize, num3);
						essentialBufferSize += num3;
						essentialBufferCount++;
					}
					fullBufferSize += num3;
					fullBufferCount++;
				}
			}
			return fullBufferCount > 0;
		}
		for (int i = 0; i < objectCount; i++)
		{
			NetworkEntity networkEntity = objectList[i];
			int num2 = networkEntity.PollObject(fullUpdate, essentialBuffer, essentialBufferSize + 2);
			if (num2 > 1 && networkEntity.isEssential)
			{
				NetworkCompression.WriteUInt(networkEntity.id, flag, essentialBuffer, essentialBufferSize);
				int num3 = num + num2;
				essentialBufferSize += num3;
				essentialBufferCount++;
			}
		}
		return essentialBufferCount > 0;
	}

	public void WriteBufferData(bool fullUpdate, byte[] buffer, int offset)
	{
		bool flag = UseShort(objCount);
		int num;
		if (flag)
		{
			buffer[offset] = 1;
			num = 2;
		}
		else
		{
			buffer[offset] = 0;
			num = 4;
		}
		offset++;
		if (fullUpdate)
		{
			NetworkCompression.WriteUInt((uint)fullBufferCount, flag, buffer, offset);
			offset += num;
			Buffer.BlockCopy(fullBuffer, 0, buffer, offset, fullBufferSize);
		}
		else
		{
			NetworkCompression.WriteUInt((uint)essentialBufferCount, flag, buffer, offset);
			offset += num;
			Buffer.BlockCopy(essentialBuffer, 0, buffer, offset, essentialBufferSize);
		}
	}

	public static int BufferDataLength(byte[] data, int offset)
	{
		int num = offset;
		bool flag = (data[offset] & 1) != 0;
		int num2 = ((!flag) ? 4 : 2);
		offset++;
		uint num3 = NetworkCompression.ReadUInt(flag, data, offset);
		offset += num2;
		uint num4 = 0u;
		while (num4++ < num3)
		{
			offset += num2;
			byte entityState = data[offset];
			int dataSize = SendEntity.GetDataSize(entityState, true);
			offset += dataSize;
		}
		return offset - num;
	}

	public int ReadBufferData(uint frame, byte[] data, int offset)
	{
		int num = offset;
		bool flag = (data[offset] & 1) != 0;
		int num2 = ((!flag) ? 4 : 2);
		offset++;
		uint num3 = NetworkCompression.ReadUInt(flag, data, offset);
		offset += num2;
		uint num4 = 0u;
		while (num4++ < num3)
		{
			uint key = NetworkCompression.ReadUInt(flag, data, offset);
			offset += num2;
			byte entityState = data[offset];
			bool flag2 = SendEntity.HasPosition(entityState);
			bool flag3 = SendEntity.HasRotation(entityState);
			int num5 = SendEntity.EventCount(entityState);
			int num6 = 1 + (flag2 ? 6 : 0) + (flag3 ? 7 : 0) + num5;
			NetworkEntity value;
			if (networkObjects.TryGetValue(key, out value))
			{
				value.SetData(frame, data, offset, flag2, flag3, num5);
			}
			offset += num6;
		}
		return offset - num;
	}

	public void NewFrame(uint frame)
	{
		NewFrame(frame, objList, objCount);
	}

	public void NewFrame(uint frame, NetworkEntity[] objectList, uint objectCount)
	{
		if (frame >= currentFrame)
		{
			for (int i = 0; i < objectCount; i++)
			{
				objectList[i].NewFrame(frame);
			}
			currentFrame = frame;
		}
	}
}
