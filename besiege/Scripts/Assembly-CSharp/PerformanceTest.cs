using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class PerformanceTest : MonoBehaviour
{
	private float startTestTime;

	private float testDuration = 5f;

	private List<PerformanceCounter> counters = new List<PerformanceCounter>();

	private PerformanceCounterSamples samples = new PerformanceCounterSamples();

	private int numTestMachines;

	private void Awake()
	{
		counters.Add(new FPSPerformanceCounter());
		counters.Add(new CPUPerformanceCounter());
		counters.Add(new BlocksPerformanceCounter());
	}

	public void LoadAndTestMachine(string machinePath, float duration, int numTestMachines)
	{
		this.numTestMachines = numTestMachines;
		if (duration != -1f)
		{
			testDuration = duration;
		}
		samples.TestDuration = testDuration;
		StartCoroutine(TestMachine(machinePath));
	}

	private void ClearCounters()
	{
		for (int i = 0; i < counters.Count; i++)
		{
			counters[i].Clear();
		}
	}

	private IEnumerator UpdateCounters()
	{
		startTestTime = Time.realtimeSinceStartup;
		ClearCounters();
		while (Time.realtimeSinceStartup < startTestTime + testDuration)
		{
			for (int i = 0; i < counters.Count; i++)
			{
				counters[i].Update();
			}
			yield return null;
		}
	}

	private void RecordSamples(List<PerformanceCounterSample> sampleList)
	{
		for (int i = 0; i < counters.Count; i++)
		{
			PerformanceCounterSample performanceCounterSample = new PerformanceCounterSample();
			performanceCounterSample.Name = counters[i].GetType().Name;
			performanceCounterSample.Average = counters[i].Average;
			performanceCounterSample.Highest = counters[i].Highest;
			performanceCounterSample.Lowest = counters[i].Lowest;
			performanceCounterSample.Value = counters[i].Value;
			PerformanceCounterSample item = performanceCounterSample;
			sampleList.Add(item);
		}
	}

	private IEnumerator TestMachine(string machinePath)
	{
		yield return new WaitUntil(() => Machine.Active() != null && Machine.Active().ReadyForSim);
		Machine activeMachine = Machine.Active();
		activeMachine.boundingBoxController.DisableBounds(activeMachine);
		yield return StartCoroutine(UpdateCounters());
		RecordSamples(samples.IdleSamples);
		LoadMachinePerformanceTest("Test machine", machinePath, numTestMachines);
		yield return new WaitUntil(() => Machine.Active().ReadyForSim);
		yield return StartCoroutine(UpdateCounters());
		RecordSamples(samples.PostLoadSamples);
		ServerMachine serverMachine = (ServerMachine)Machine.Active();
		serverMachine.EnableInputRecorder();
		SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulate();
		yield return StartCoroutine(UpdateCounters());
		RecordSamples(samples.SimulationSamples);
		SaveSamples(machinePath);
		Application.Quit();
	}

	private void SaveSamples(string machinePath)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(PerformanceCounterSamples));
		string path = Application.persistentDataPath + "/" + Path.GetFileNameWithoutExtension(machinePath) + "_" + DateTime.Now.ToString("yyyyMMddHHmmFFF") + ".xml";
		FileStream fileStream = new FileStream(path, FileMode.Create);
		xmlSerializer.Serialize(fileStream, samples);
		fileStream.Close();
	}

	private void DuplicateBlocks(MachineInfo machineInfo, int rows, int columns, float spacer)
	{
		int num = rows / 2;
		int num2 = columns / 2;
		foreach (BlockInfo item in new List<BlockInfo>(machineInfo.Blocks))
		{
			Vector3 position = item.Position;
			for (int i = -num; i <= num; i++)
			{
				for (int j = -num2; j <= num2; j++)
				{
					if (i != 0 || j != 0)
					{
						machineInfo.Blocks.Add(new BlockInfo(Guid.NewGuid(), item.ID, new Vector3(position.x + (float)j * spacer, position.y, position.z + (float)i * spacer), item.Rotation, item.Scale, item.Skin, item.Flipped, item.BlockData));
					}
				}
			}
		}
	}

	public void LoadMachinePerformanceTest(string name, string fullPath, int gridSize)
	{
		MachineInfo machineInfo = null;
		if (XmlSaver.IsBsgFormat(fullPath))
		{
			machineInfo = MachineFormatConverter.ConvertBsgToMachineInfo(name, fullPath);
		}
		else if (XmlSaver.IsXmlFormat(fullPath))
		{
			machineInfo = XmlLoader.LoadFromFullPath(fullPath, string.Empty);
		}
		int num = Mathf.CeilToInt(Mathf.Sqrt(gridSize));
		DuplicateBlocks(machineInfo, num, num, 10f);
		if (machineInfo != null)
		{
			LoadMachine(machineInfo, fullPath);
		}
		else
		{
			Debug.LogWarning("Couldn't load a machine.");
		}
	}

	private void LoadMachine(MachineInfo machineInfo, string machinePath)
	{
		Machine machine = Machine.Active();
		if (machine != null)
		{
			if (machine.CanModify)
			{
				if (StatMaster.isMP)
				{
					NetworkAuxAddPiece.Instance.LoadLocalMachine(machineInfo);
				}
				else
				{
					machine.LoadMachineInfo(machineInfo, machinePath);
				}
			}
		}
		else if (!StatMaster.isMP)
		{
			Machine machine2 = SingleInstance<MachineObjectTracker>.Instance.CreateNewMachine();
			machine2.LoadMachineInfo(machineInfo, machinePath);
		}
	}
}
