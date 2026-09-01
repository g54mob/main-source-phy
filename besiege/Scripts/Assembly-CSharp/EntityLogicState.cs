using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityLogicState
{
	public class LogicProgress
	{
		public ushort logicID;

		public ushort eventID;

		public float progress;

		public byte[] eventData;

		public bool hasEventData;

		public LogicProgress(ushort logic, ushort evt, float evtProgress, bool hasEvtData, byte[] evtProgressData)
		{
			logicID = logic;
			eventID = evt;
			progress = evtProgress;
			hasEventData = hasEvtData;
			eventData = evtProgressData;
		}
	}

	public bool stateChanged;

	public bool hasPosition;

	public bool hasRotation;

	public bool hasScale;

	public Vector3 position = default(Vector3);

	public Quaternion rotation = default(Quaternion);

	public Vector3 scale = default(Vector3);

	public List<LogicProgress> runningLogic;

	private static List<byte[]> logicData = new List<byte[]>(10);

	public bool hasRunningLogic;

	public EntityLogicState()
	{
	}

	public EntityLogicState(bool hasPos, bool hasRot, bool hasSc, bool state, Vector3 pos, Quaternion rot, Vector3 sc, List<LogicProgress> logic)
	{
		hasPosition = hasPos;
		hasRotation = hasRot;
		hasScale = hasSc;
		stateChanged = state;
		position = pos;
		rotation = rot;
		scale = sc;
		runningLogic = logic;
	}

	public int Decode(byte[] data, int offset)
	{
		int num = offset;
		int num2 = data[offset];
		offset++;
		hasPosition = (num2 & 1) != 0;
		if (hasPosition)
		{
			NetworkCompression.DecompressPosition(data, offset, out position);
			offset += 6;
		}
		hasRotation = (num2 & 2) != 0;
		if (hasRotation)
		{
			NetworkCompression.DecompressRotation(data, offset, out rotation);
			offset += 7;
		}
		hasScale = (num2 & 4) != 0;
		if (hasScale)
		{
			NetworkCompression.DecompressPosition(data, offset, out scale);
			offset += 6;
		}
		stateChanged = (num2 & 8) != 0;
		hasRunningLogic = (num2 & 0x10) != 0;
		if (hasRunningLogic)
		{
			runningLogic = new List<LogicProgress>();
			int count;
			offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
			for (int i = 0; i < count; i++)
			{
				ushort logic = NetworkCompression.ReadUInt16(data, offset);
				offset += 2;
				ushort evt = NetworkCompression.ReadUInt16(data, offset);
				offset += 2;
				float evtProgress = (float)(int)data[offset] / 255f;
				offset++;
				int num3 = data[offset];
				offset++;
				byte[] array = null;
				bool flag = num3 > 0;
				if (flag)
				{
					array = new byte[num3];
					Buffer.BlockCopy(data, offset, array, 0, num3);
					offset += num3;
				}
				runningLogic.Add(new LogicProgress(logic, evt, evtProgress, flag, array));
			}
		}
		return offset - num;
	}

	public static bool Encode(LevelEntity entity, int prefix, out byte[] data)
	{
		bool flag = entity.behaviour.hasRunningLogic;
		bool flag2 = entity.hasSpawned != entity.behaviour.ActiveOnStart();
		int num = 0;
		int count = entity.behaviour.runningLogic.Count;
		logicData.Clear();
		int num3;
		if (flag)
		{
			for (int i = 0; i < count; i++)
			{
				EntityLogic entityLogic = entity.behaviour.runningLogic[i];
				EntityEvent entityEvent = entityLogic.events[entityLogic.currentIndex];
				if (entityEvent.eventData.IsProgressEvent())
				{
					int num2 = entityEvent.eventData.EventDataSize();
					byte[] array = new byte[6 + num2];
					num3 = 0;
					NetworkCompression.WriteUInt16(entityLogic.ID, array, num3);
					num3 += 2;
					NetworkCompression.WriteUInt16(entityEvent.ID, array, num3);
					num3 += 2;
					float progress = entityEvent.eventData.GetProgress();
					array[num3] = (byte)Mathf.FloorToInt(progress * 255f);
					num3++;
					array[num3] = (byte)num2;
					num3++;
					if (num2 > 0)
					{
						entityEvent.eventData.EncodeEventData(array, num3);
						num3 += num2;
					}
					num += array.Length;
					logicData.Add(array);
				}
			}
		}
		count = logicData.Count;
		flag = count > 0;
		bool changedPos = entity.changedPos;
		bool changedRot = entity.changedRot;
		bool changedScale = entity.changedScale;
		if (!changedPos && !changedRot && !changedScale && !flag && !flag2)
		{
			data = null;
			return false;
		}
		int num4 = (flag ? NetworkCompression.PackedUIntLength(count, true) : 0);
		data = new byte[prefix + 1 + (changedPos ? 6 : 0) + (changedRot ? 7 : 0) + (changedScale ? 6 : 0) + (flag ? (num4 + num) : 0)];
		num3 = prefix;
		data[num3] = (byte)((changedPos ? 1 : 0) | (changedRot ? 2 : 0) | (changedScale ? 4 : 0) | (flag2 ? 8 : 0) | (flag ? 16 : 0));
		num3++;
		Transform transform = entity.transform;
		if (changedPos)
		{
			NetworkCompression.CompressPosition(transform.position, data, num3);
			num3 += 6;
		}
		if (changedRot)
		{
			NetworkCompression.CompressRotation(transform.rotation, data, num3);
			num3 += 7;
		}
		if (changedScale)
		{
			NetworkCompression.CompressPosition(transform.localScale, data, num3);
			num3 += 6;
		}
		if (flag)
		{
			NetworkCompression.PackUInt(count, data, num3, true, num4);
			num3 += num4;
			NetworkCompression.WriteArray(logicData, data, num3);
		}
		return true;
	}
}
