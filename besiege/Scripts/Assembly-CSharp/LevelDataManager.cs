using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataManager
{
	private byte[] transformData;

	private Dictionary<ServerMachine, List<byte[]>> inputData;

	private int inputSize;

	private byte[] input;

	private int offset;

	private CustomLevel level;

	private ProjectileManager projectileManager;

	private float MAX_FPS = 60f;

	private float fpsMultiplier;

	private int levelSimSize;

	private int projSimSize;

	private PerformanceAnalyser perfAnalyzer;

	private float maxFps;

	private NetworkScene networkScene;

	private BesiegeNetworkManager networkManager;

	private uint frame;

	public int inputDataSize
	{
		get
		{
			return inputDataDirty ? (1 + inputData.Count * 4 + inputSize) : 0;
		}
	}

	public bool inputDataDirty
	{
		get
		{
			return inputData.Count > 0;
		}
	}

	public LevelDataManager()
	{
		inputData = new Dictionary<ServerMachine, List<byte[]>>(16);
	}

	public void SetLevel(CustomLevel lvl)
	{
		level = lvl;
		projectileManager = ProjectileManager.Instance;
		maxFps = MAX_FPS;
		fpsMultiplier = MAX_FPS / 255f;
		perfAnalyzer = SingleInstance<PerformanceAnalyser>.Instance;
	}

	public void Init()
	{
		networkScene = NetworkScene.Instance;
		networkManager = BesiegeNetworkManager.Instance;
	}

	public int GetSimFrame()
	{
		levelSimSize = level.GetSimFrame();
		projSimSize = projectileManager.GetSimFrame();
		return levelSimSize + projSimSize;
	}

	public int WriteSimFrame(byte[] data, int offset)
	{
		level.WriteSimFrame(data, offset);
		projectileManager.WriteSimFrame(data, offset + levelSimSize);
		return levelSimSize + projSimSize;
	}

	public int ReadSimFrame(byte[] data, int offset, float timeCorrection)
	{
		int num = level.ReadSimFrame(data, offset, timeCorrection);
		int num2 = projectileManager.ReadSimFrame(data, offset + num);
		return num + num2;
	}

	private void SendLevelData(ushort current, byte[] data)
	{
		networkManager.SendLevelData(frame, level.Session, current, data);
	}

	public void PollLevel(uint currentFrame, bool hasPlayers)
	{
		frame = currentFrame;
		level.PollObjects();
		projectileManager.PollObjects();
		if (hasPlayers && PackTransformData(frame, out transformData))
		{
			FragmentedRPC.Send(SendLevelData, transformData, 0, networkManager.LevelMessageHeaderSize);
		}
		else
		{
			ClearTransformData();
		}
	}

	public bool IsFPSFrame(uint frame)
	{
		return frame % 5 == 0;
	}

	public void ClearTransformData()
	{
		projectileManager.ClearSpawnData();
	}

	public bool PackTransformData(uint frame, out byte[] updateData)
	{
		bool flag = StatMaster.levelSimulating && !StatMaster.isLocalSim;
		if (!flag)
		{
			updateData = null;
			return false;
		}
		bool flag2 = IsFPSFrame(frame);
		int spawnDataLength = projectileManager.SpawnDataLength;
		updateData = new byte[(flag2 ? 2 : 0) + 1 + (flag ? (level.BufferLength + spawnDataLength + projectileManager.BufferLength) : 0)];
		int num = 0;
		if (flag2)
		{
			float fPS = perfAnalyzer.FPS;
			updateData[num] = (byte)(Mathf.Clamp(fPS, 0f, MAX_FPS) / maxFps * 255f);
			num++;
			int num2 = Mathf.RoundToInt(perfAnalyzer.CPULoad);
			updateData[num] = (byte)num2;
			ServerHealth.Instance.SetServerCPULoad(num2);
			num++;
		}
		updateData[num] = (byte)(flag ? 1u : 0u);
		num++;
		if (flag)
		{
			level.WriteBufferData(updateData, num);
			num += level.BufferLength;
			projectileManager.WriteSpawnData(updateData, num);
			num += spawnDataLength;
			projectileManager.WriteBufferData(updateData, num);
			num += projectileManager.BufferLength;
		}
		return true;
	}

	public void UnpackData(uint frame, int session, byte[] data)
	{
		offset = 0;
		if (IsFPSFrame(frame))
		{
			float serverFPS = (float)(int)data[offset] * fpsMultiplier;
			ServerHealth.Instance.SetServerFPS(serverFPS);
			offset++;
			int serverCPULoad = data[offset];
			ServerHealth.Instance.SetServerCPULoad(serverCPULoad);
			offset++;
		}
		bool flag = data[offset] == 1;
		offset++;
		if (flag)
		{
			offset += level.ReadBufferData(frame, session, data, offset);
		}
		level.NewFrame(frame);
		int num = projectileManager.ReadSpawnData(frame, data, offset);
		offset += num;
		int num2 = projectileManager.ReadBufferData(frame, data, offset);
		offset += num2;
		projectileManager.NewFrame(frame);
	}

	public void ClearInput(ServerMachine machine)
	{
		if (inputData.ContainsKey(machine))
		{
			inputData.Remove(machine);
		}
	}

	public void AddInput(ServerMachine machine, byte[] input)
	{
		if (input.Length != 0)
		{
			List<byte[]> value;
			if (!inputData.TryGetValue(machine, out value))
			{
				value = new List<byte[]>();
				inputData.Add(machine, value);
			}
			value.Add(input);
			inputSize += 1 + input.Length;
		}
	}

	public void ClearInputData()
	{
		inputSize = 0;
		inputData.Clear();
	}

	public void WriteInputData(byte[] data, int offset)
	{
		Dictionary<ServerMachine, List<byte[]>>.KeyCollection keys = inputData.Keys;
		data[offset++] = (byte)keys.Count;
		foreach (ServerMachine item in keys)
		{
			int num = NetworkCompression.PackedUIntLength(item.PlayerID, true);
			NetworkCompression.PackUInt(item.PlayerID, data, offset, true, num);
			offset += num;
			data[offset++] = (byte)item.Session;
			List<byte[]> list = inputData[item];
			data[offset++] = (byte)list.Count;
			for (int i = 0; i < list.Count; i++)
			{
				byte[] array = list[i];
				data[offset++] = (byte)array.Length;
				Buffer.BlockCopy(array, 0, data, offset, array.Length);
				offset += array.Length;
			}
		}
		ClearInputData();
	}

	public void UnpackInputData(byte[] data, int offset)
	{
		int num = data[offset++];
		int num2 = 0;
		while (num2++ < num)
		{
			int count;
			offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
			int num3 = data[offset++];
			ServerMachine machine;
			bool flag = networkScene.GetMachine((ushort)count, out machine) && machine.isSimulating && machine.Session == num3;
			int num4 = data[offset++];
			for (int i = 0; i < num4; i++)
			{
				byte b = data[offset++];
				if (flag)
				{
					machine.ReadInputData(data, offset);
				}
				offset += b;
			}
		}
	}
}
