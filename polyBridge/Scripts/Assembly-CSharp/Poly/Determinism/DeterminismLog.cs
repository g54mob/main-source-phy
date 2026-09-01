using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Poly.Base;
using Poly.File;
using Poly.Physics;
using Poly.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Poly.Determinism
{
	public class DeterminismLog : PolyBehaviour, IWorldListener
	{
		public enum Mode
		{
			Idle = 0,
			Record = 1,
			VerifyIdentity = 2
		}

		private static bool searchForInstance = true;

		private static DeterminismLog _instance;

		[ShowIf("isInSimulation", false, true, "")]
		public Mode mode = Mode.VerifyIdentity;

		public uint minNumberOfFramesToSave = 10u;

		[ShowIf("isInSimulation", false, true, "")]
		public bool DELETE_LOG;

		[Header("Hydraulic Logic Debugging")]
		public float activateHydraulicControllerPeriod = float.PositiveInfinity;

		public float fistActivationTime = 0.5f;

		[Header("Partial scene logging")]
		public bool logPartialScene;

		public List<int> nodeIndicesToExclude;

		private float timeElapsedSinceLastActivation;

		[NonSerialized]
		public List<EventData> events = new List<EventData>();

		[NonSerialized]
		private SimulationHistory history = new SimulationHistory();

		[NonSerialized]
		private SimulationHistory loadedHistory;

		[NonSerialized]
		private int lastFrameCompared = -1;

		private bool brokeExecutionOnce;

		[SerializeField]
		[HideInInspector]
		private bool isInSimulation;

		private bool restoreVerifyModeAfterRecordingOnce;

		public static DeterminismLog instance
		{
			get
			{
				if (searchForInstance)
				{
					searchForInstance = false;
					_instance = UnityEngine.Object.FindObjectOfType<DeterminismLog>();
				}
				return _instance;
			}
		}

		public int lastFullFrameIndex => history.frames.Count - 2;

		private static string scenePathString => SceneManager.GetActiveScene().path.Replace('/', '_').Replace('\\', '_');

		public static void LogEvent(LoggingBehaviour obj, EventType eventType)
		{
			if ((bool)instance)
			{
				instance._LogEvent(obj, eventType);
			}
		}

		private void _LogEvent(LoggingBehaviour obj, EventType eventType)
		{
			history.currentFrame.events.Add(new EventData(obj, eventType, history.currentFrame.events.Count));
		}

		private void Awake()
		{
			_instance = this;
			if ((bool)SingletonBehaviour<World>.instance)
			{
				SingletonBehaviour<World>.instance.worldListeners.Add(this);
			}
		}

		private void OnDestroy()
		{
			if (SingletonBehaviour<World>.instanceExists)
			{
				SingletonBehaviour<World>.instance.worldListeners.Remove(this);
			}
			MaybeSaveAndClear();
		}

		private void Init()
		{
			if (mode == Mode.VerifyIdentity)
			{
				try
				{
					loadedHistory = LoadHistory();
				}
				catch
				{
					mode = Mode.Record;
					restoreVerifyModeAfterRecordingOnce = true;
				}
			}
		}

		private void MaybeSaveAndClear()
		{
			if (mode == Mode.Record && history.frames.Count > minNumberOfFramesToSave)
			{
				SaveHistory(history);
				if (restoreVerifyModeAfterRecordingOnce)
				{
					mode = Mode.VerifyIdentity;
					restoreVerifyModeAfterRecordingOnce = false;
				}
			}
			events.Clear();
			history.Clear();
			loadedHistory = null;
			brokeExecutionOnce = false;
			lastFrameCompared = -1;
			Singleton<PersistentIdRegistry<OrderedBehaviour>, int>.instance.Clear();
			OrderedBehaviour[] array = UnityEngine.Object.FindObjectsOfType<OrderedBehaviour>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].VerifyOrGetNewId();
			}
		}

		private void OnValidate()
		{
			if (DELETE_LOG)
			{
				DELETE_LOG = false;
				DeleteLog();
			}
		}

		private void DeleteLog()
		{
			try
			{
				MaybeSaveAndClear();
				System.IO.File.Delete(scenePathString + "_history.dat");
				Debug.Log("Log file deleted or doesn't exist.");
				Init();
			}
			catch
			{
				Debug.Log("Error: Couldn't delete the log file.");
			}
		}

		public void BeforeStep()
		{
		}

		public void AfterWorldCleared()
		{
			MaybeSaveAndClear();
			Init();
			if (isInSimulation)
			{
				isInSimulation = false;
			}
		}

		public void AfterWorldFrameUpdate()
		{
			if (isInSimulation)
			{
				CompareAvailableFrames();
			}
		}

		public void AfterWorldFixedUpdate()
		{
			if (!isInSimulation)
			{
				isInSimulation = true;
			}
			Manual_FixedUpdate();
			timeElapsedSinceLastActivation += Time.fixedDeltaTime;
		}

		public void Manual_FixedUpdate()
		{
			if (history.frames.Count == 1)
			{
				switch (mode)
				{
				case Mode.Record:
					Debug.Log("Recording for Determinism");
					break;
				case Mode.VerifyIdentity:
					Debug.Log("Verifying Determinism");
					break;
				}
			}
			foreach (NodeHandle nodeHandle in SingletonBehaviour<World>.instance.nodeHandles)
			{
				if (!logPartialScene || !nodeIndicesToExclude.Contains(nodeHandle.worldIdx))
				{
					NodeData item = new NodeData
					{
						objectId = (nodeHandle.unityNodeComponent ? nodeHandle.unityNodeComponent.persistentId : (-1)),
						invMass = nodeHandle.solverNode.invMass,
						pos = nodeHandle.solverNode.pos,
						oldPos = nodeHandle.solverNode.pos - nodeHandle.solverNode.vel
					};
					history.currentFrame.nodes.Add(item);
				}
			}
			foreach (Poly.Physics.Rigidbody body in SingletonBehaviour<World>.instance.bodies)
			{
				MotionData item2 = new MotionData
				{
					objectId = body.persistentId,
					pos = body.motion.com,
					oldPos = body.motion.com - body.motion.linVel
				};
				history.currentFrame.motions.Add(item2);
			}
			history.CreateNewFrame();
		}

		public void CompareAvailableFrames()
		{
			if (loadedHistory == null)
			{
				return;
			}
			int val = history.frames.Count - 2;
			val = System.Math.Min(loadedHistory.frames.Count - 1, val);
			if (lastFrameCompared >= val)
			{
				return;
			}
			bool num = CompareFrames(history, loadedHistory, lastFrameCompared + 1, val);
			lastFrameCompared = val;
			if (!num)
			{
				if (!brokeExecutionOnce)
				{
					Debug.Log("Simulation different from previous runs.");
					brokeExecutionOnce = true;
					Debug.Break();
				}
			}
			else
			{
				Debug.Log("Frame #" + val + " OK");
			}
			if (lastFrameCompared == loadedHistory.frames.Count - 1)
			{
				Debug.Log("Compared all available frames OK.");
				loadedHistory = null;
			}
		}

		public static bool CompareFrames(SimulationHistory a, SimulationHistory b, int startFrame, int endFrame)
		{
			bool flag = true;
			for (int i = startFrame; i <= endFrame; i++)
			{
				a.frames[i].PrepForComparison();
				b.frames[i].PrepForComparison();
				bool flag2 = a.frames[i].IsIdenticalTo(b.frames[i]);
				if (!flag2)
				{
					List<string> list = new List<string>();
					list.Add("Frame #" + i + " (up to #" + endFrame + ") has differences:");
					a.frames[i].LogDifferences(b.frames[i], list);
					list.Add("-----------------");
					Debug.Log(string.Join("\r\n", list.ToArray()));
				}
				flag = flag && flag2;
			}
			return flag;
		}

		private static SimulationHistory LoadHistory()
		{
			return Serialize.ReadFromBinaryFile<SimulationHistory>(scenePathString + "_history.dat");
		}

		private static void SaveHistory(SimulationHistory history)
		{
			FrameData item = history.frames.Last();
			history.frames.RemoveAt(history.frames.Count - 1);
			Serialize.WriteToBinaryFile(scenePathString + "_history.dat", history);
			history.frames.Add(item);
		}

		public static void LogToFileRaw(string fileName, string message)
		{
			using StreamWriter streamWriter = new StreamWriter(fileName, append: true);
			streamWriter.Write(message);
		}

		public static void OverwriteAndLogToFileRaw(string fileName, string message)
		{
			using StreamWriter streamWriter = new StreamWriter(fileName, append: false);
			streamWriter.Write(message);
		}

		public static string ReadEntireTextFile(string fileName)
		{
			try
			{
				using StreamReader streamReader = new StreamReader(fileName, detectEncodingFromByteOrderMarks: false);
				return streamReader.ReadToEnd();
			}
			catch
			{
			}
			return null;
		}
	}
}
