using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetKeyInputController : KeyInputController
{
	public class NetKeyInfo : KeyInfo
	{
		public byte sendId;

		public NetKeyInfo(ushort code)
			: base(code)
		{
			sendId = 0;
		}

		public bool IsNewestUpdate(byte newId)
		{
			return !hasUpdate || newId > sendId || Mathf.Abs(newId - sendId) > 100;
		}

		public void UpdateSendID()
		{
			if (sendId < byte.MaxValue)
			{
				sendId++;
			}
			else
			{
				sendId = 0;
			}
		}

		public override string ToString()
		{
			return string.Format("[{0}] IsDown: {1}, IsPressed: {2}, IsReleased: {3}", keyCode, base.IsDown, base.IsPressed, base.IsReleased);
		}
	}

	public class EmulationEntry
	{
		public int ID;

		public bool Emulate;

		public int BuildIndex;

		public int[] ActivateIndex;

		public int EmulateIndex;
	}

	public const int MAX_SEND_ID = 255;

	public const int SEND_MARGIN = 100;

	public const int BUFFER_ENTRY_LEN = 4;

	private List<byte[]> inputBuffer;

	private int inputSize;

	private ServerMachine machine;

	private List<EmulationEntry> emulationData;

	private byte emulateId;

	public bool isDirty
	{
		get
		{
			return isActive && inputBuffer.Count > 0;
		}
	}

	public int InputSize
	{
		get
		{
			return NetworkCompression.PackedUIntLength(inputBuffer.Count, true) + inputSize;
		}
	}

	public void Init(ServerMachine machine)
	{
		this.machine = machine;
		inputBuffer = new List<byte[]>();
		inputSize = 0;
		emulationData = new List<EmulationEntry>();
	}

	public override void ResetKeys()
	{
		for (int i = 0; i < keyCodes.Count; i++)
		{
			NetKeyInfo netKeyInfo = keys[keyCodes[i]] as NetKeyInfo;
			netKeyInfo.sendId = 0;
			netKeyInfo.keyState = 0;
			netKeyInfo.lastDown = false;
			netKeyInfo.hasUpdate = false;
		}
	}

	public override void Add(KeyCode key)
	{
		ushort num = (ushort)key;
		if (!keys.ContainsKey(num))
		{
			keys.Add(num, new NetKeyInfo(num));
			keyCodes.Add(num);
		}
	}

	public override void Clear()
	{
		base.Clear();
		emulateId = 0;
		ClearInputBuffer();
	}

	public virtual int ReadInput(byte[] data, int offset)
	{
		int num = offset;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
		for (int i = 0; i < count; i++)
		{
			switch (data[offset++])
			{
			case 0:
				if (!machine.isLocalMachine)
				{
					ushort key = NetworkCompression.ReadUInt16(data, offset);
					KeyInfo value;
					if (keys.TryGetValue(key, out value))
					{
						byte b = data[offset + 2];
						NetKeyInfo netKeyInfo = value as NetKeyInfo;
						if (netKeyInfo.IsNewestUpdate(b))
						{
							bool flag = data[offset + 3] == 1;
							if (value.hasUpdate || flag)
							{
								netKeyInfo.sendId = b;
								value.hasUpdate = true;
								StartCoroutine(SetKey(value, flag));
							}
						}
					}
				}
				offset += 4;
				break;
			case 1:
			{
				int num2 = data[offset++];
				bool emulate = data[offset++] == 1;
				int count2;
				offset += NetworkCompression.UnpackUInt(data, offset, true, out count2);
				int num3 = data[offset++];
				int[] array = new int[num3];
				for (int j = 0; j < num3; j++)
				{
					int num4 = data[offset++];
					array[j] = ((num4 != 0) ? (num4 >> 1) : (-1));
				}
				int emulateIndex = data[offset++];
				if (machine.SimPhysics)
				{
					break;
				}
				EmulationEntry emulationEntry = new EmulationEntry();
				emulationEntry.ID = num2;
				emulationEntry.Emulate = emulate;
				emulationEntry.BuildIndex = count2;
				emulationEntry.ActivateIndex = array;
				emulationEntry.EmulateIndex = emulateIndex;
				EmulationEntry emulationEntry2 = emulationEntry;
				if (num2 == emulateId)
				{
					while (emulationEntry2 != null)
					{
						emulationData.Remove(emulationEntry2);
						ExecuteEmulation(emulationEntry2);
						UpdateEmulateID();
						emulationEntry2 = emulationData.Find((EmulationEntry x) => x.ID == emulateId);
					}
				}
				else
				{
					emulationData.Add(emulationEntry2);
				}
				break;
			}
			}
		}
		return offset - num;
	}

	public static int SkipInput(byte[] data, int offset)
	{
		int num = offset;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
		for (int i = 0; i < count; i++)
		{
			switch (data[offset++])
			{
			case 0:
				offset += 4;
				break;
			case 1:
			{
				offset++;
				offset++;
				int count2;
				offset += NetworkCompression.UnpackUInt(data, offset, true, out count2);
				offset++;
				offset++;
				break;
			}
			}
		}
		return offset - num;
	}

	public void ClearInputBuffer()
	{
		inputBuffer.Clear();
		inputSize = 0;
	}

	public virtual void WriteInput(byte[] data, int offset)
	{
		int num = NetworkCompression.PackedUIntLength(inputBuffer.Count, true);
		NetworkCompression.PackUInt(inputBuffer.Count, data, offset, true, num);
		offset += num;
		for (int i = 0; i < inputBuffer.Count; i++)
		{
			byte[] array = inputBuffer[i];
			Buffer.BlockCopy(array, 0, data, offset, array.Length);
			offset += array.Length;
		}
		ClearInputBuffer();
	}

	private IEnumerator SetKey(KeyInfo info, bool toggle)
	{
		int targetState = (toggle ? 4 : 0);
		info.keyState = (byte)(targetState | (toggle ? 1 : 2));
		yield return new WaitForEndOfFrame();
		info.keyState = (byte)targetState;
	}

	public override void UpdateKeys()
	{
		if (!isActive || StatMaster.inMenu || StatMaster.stopHotkeys)
		{
			return;
		}
		for (int i = 0; i < keyCodes.Count; i++)
		{
			NetKeyInfo netKeyInfo = keys[keyCodes[i]] as NetKeyInfo;
			netKeyInfo.keyState = GetState(netKeyInfo.keyCode);
			bool isDown = netKeyInfo.IsDown;
			if (netKeyInfo.lastDown != isDown)
			{
				byte[] array = new byte[5];
				int num = 0;
				array[num++] = 0;
				NetworkCompression.WriteUInt16(netKeyInfo.dictKey, array, num);
				num += 2;
				array[num++] = netKeyInfo.sendId;
				netKeyInfo.UpdateSendID();
				array[num++] = (byte)(isDown ? 1u : 0u);
				inputBuffer.Add(array);
				inputSize += array.Length;
			}
			netKeyInfo.lastDown = isDown;
		}
	}

	private void ExecuteEmulation(EmulationEntry entry)
	{
		BlockBehaviour block;
		if (!machine.GetBlockFromIndex(entry.BuildIndex, out block) || !block.hasSimBlock)
		{
			return;
		}
		BlockBehaviour simBlock = block.SimBlock;
		MKey[] array = new MKey[entry.ActivateIndex.Length];
		MKey mKey = null;
		if (block.KeyList.Count == simBlock.KeyList.Count)
		{
			for (int i = 0; i < entry.ActivateIndex.Length; i++)
			{
				if (entry.ActivateIndex[i] != -1)
				{
					array[i] = simBlock.KeyList[entry.ActivateIndex[i]];
				}
			}
			mKey = simBlock.KeyList[entry.EmulateIndex];
		}
		else if (simBlock.KeyList.Count > 0)
		{
			for (int j = 0; j < entry.ActivateIndex.Length; j++)
			{
				if (entry.ActivateIndex[j] != -1)
				{
					string aKey = block.KeyList[entry.ActivateIndex[j]].Key;
					array[j] = simBlock.KeyList.Find((MKey x) => x.Key.Equals(aKey));
				}
			}
			if (block.KeyList.Count < entry.EmulateIndex)
			{
				string eKey = block.KeyList[entry.EmulateIndex].Key;
				mKey = simBlock.KeyList.Find((MKey x) => x.Key.Equals(eKey));
			}
			else
			{
				mKey = simBlock.KeyList[entry.EmulateIndex];
			}
		}
		if (mKey == null)
		{
			return;
		}
		simBlock.OnRemoteEmulate(mKey, entry.Emulate);
		for (int num = 0; num < mKey.KeysCount; num++)
		{
			if (mKey.useMessage)
			{
				string text = mKey.message[num];
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				foreach (KeyEntry item in usedMessages[text])
				{
					bool flag = false;
					MKey key = item.Key;
					for (int num2 = 0; num2 < array.Length; num2++)
					{
						if (key == array[num2])
						{
							flag = true;
						}
					}
					if (!flag)
					{
						key.UpdateEmulation(entry.Emulate);
					}
				}
				continue;
			}
			KeyCode key2 = mKey.GetKey(num);
			if (key2 == KeyCode.None)
			{
				continue;
			}
			if (!usedKeys.ContainsKey(key2))
			{
				string text2 = string.Empty;
				foreach (KeyCode key4 in usedKeys.Keys)
				{
					text2 += key4;
				}
				Debug.LogError(string.Concat("[NetKeyInputController] Missing used key ", key2, ", usedKeys contain ", usedKeys.Count, " keys.\n the available keys are: ", text2));
				continue;
			}
			foreach (KeyEntry item2 in usedKeys[key2])
			{
				bool flag2 = false;
				MKey key3 = item2.Key;
				for (int num3 = 0; num3 < array.Length; num3++)
				{
					if (key3 == array[num3])
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					key3.UpdateEmulation(entry.Emulate);
				}
			}
		}
	}

	public void UpdateEmulateID()
	{
		if (emulateId < byte.MaxValue)
		{
			emulateId++;
		}
		else
		{
			emulateId = 0;
		}
	}

	public override int Emulate(BlockBehaviour block, MKey[] activationKeys, MKey emulateKey, bool emulate)
	{
		if (!machine.SimPhysics)
		{
			return 0;
		}
		int result = base.Emulate(block, activationKeys, emulateKey, emulate);
		int buildIndex = block.BuildIndex;
		int num = NetworkCompression.PackedUIntLength(buildIndex, true);
		byte[] array = new byte[3 + num + 1 + 1 + 1 + activationKeys.Length - 1];
		int num2 = 0;
		array[num2++] = 1;
		array[num2++] = emulateId;
		UpdateEmulateID();
		array[num2++] = (byte)(emulate ? 1u : 0u);
		NetworkCompression.PackUInt(buildIndex, array, num2, true, num);
		num2 += num;
		if (activationKeys != null)
		{
			array[num2++] = (byte)activationKeys.Length;
			for (int i = 0; i < activationKeys.Length; i++)
			{
				if (activationKeys[i] != null)
				{
					int num3 = block.KeyList.IndexOf(activationKeys[i]);
					array[num2++] = (byte)((num3 << 1) | 1);
				}
				else
				{
					array[num2++] = 0;
				}
			}
		}
		else
		{
			array[num2++] = 0;
		}
		int num4 = block.KeyList.IndexOf(emulateKey);
		array[num2++] = (byte)num4;
		inputBuffer.Add(array);
		inputSize += array.Length;
		return result;
	}
}
