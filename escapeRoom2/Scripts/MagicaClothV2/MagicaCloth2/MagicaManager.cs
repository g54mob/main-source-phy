using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;

namespace MagicaCloth2
{
	public static class MagicaManager
	{
		public delegate void UpdateMethod();

		public enum InitializationLocation
		{
			Start = 0,
			Awake = 1
		}

		private static List<IManager> managers;

		public static UpdateMethod afterEarlyUpdateDelegate;

		public static UpdateMethod afterFixedUpdateDelegate;

		public static UpdateMethod afterUpdateDelegate;

		public static UpdateMethod beforeLateUpdateDelegate;

		public static UpdateMethod afterLateUpdateDelegate;

		public static UpdateMethod afterDelayedDelegate;

		public static UpdateMethod afterRenderingDelegate;

		public static UpdateMethod defaultUpdateDelegate;

		private static volatile bool isPlaying;

		public static Action OnPreSimulation;

		public static Action OnPostSimulation;

		internal static InitializationLocation initializationLocation;

		public static TimeManager Time => managers?[0] as TimeManager;

		public static TeamManager Team => managers?[1] as TeamManager;

		public static ClothManager Cloth => managers?[2] as ClothManager;

		public static RenderManager Render => managers?[3] as RenderManager;

		public static TransformManager Bone => managers?[4] as TransformManager;

		public static VirtualMeshManager VMesh => managers?[5] as VirtualMeshManager;

		public static SimulationManager Simulation => managers?[6] as SimulationManager;

		public static ColliderManager Collider => managers?[7] as ColliderManager;

		public static WindManager Wind => managers?[8] as WindManager;

		public static PreBuildManager PreBuild => managers?[9] as PreBuildManager;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Initialize()
		{
			Dispose();
			managers = new List<IManager>();
			managers.Add(new TimeManager());
			managers.Add(new TeamManager());
			managers.Add(new ClothManager());
			managers.Add(new RenderManager());
			managers.Add(new TransformManager());
			managers.Add(new VirtualMeshManager());
			managers.Add(new SimulationManager());
			managers.Add(new ColliderManager());
			managers.Add(new WindManager());
			managers.Add(new PreBuildManager());
			foreach (IManager manager in managers)
			{
				manager.Initialize();
			}
			InitCustomGameLoop();
			isPlaying = true;
		}

		private static void Dispose()
		{
			if (managers != null)
			{
				foreach (IManager manager in managers)
				{
					manager.Dispose();
				}
				managers = null;
			}
			OnPreSimulation = null;
			OnPostSimulation = null;
		}

		public static bool IsPlaying()
		{
			if (isPlaying)
			{
				return Application.isPlaying;
			}
			return false;
		}

		public static void InitCustomGameLoop()
		{
			PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();
			if (!CheckRegist(ref playerLoop))
			{
				SetCustomGameLoop(ref playerLoop);
				PlayerLoop.SetPlayerLoop(playerLoop);
			}
		}

		private static void SetCustomGameLoop(ref PlayerLoopSystem playerLoop)
		{
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					afterEarlyUpdateDelegate?.Invoke();
				}
			}, ref playerLoop, "EarlyUpdate", string.Empty, last: true);
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					afterFixedUpdateDelegate?.Invoke();
				}
			}, ref playerLoop, "FixedUpdate", "ScriptRunBehaviourFixedUpdate");
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					afterUpdateDelegate?.Invoke();
					if (Application.isPlaying)
					{
						defaultUpdateDelegate?.Invoke();
					}
				}
			}, ref playerLoop, "Update", "ScriptRunDelayedTasks");
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					beforeLateUpdateDelegate?.Invoke();
				}
			}, ref playerLoop, "PreLateUpdate", "ScriptRunBehaviourLateUpdate", last: false, before: true);
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					afterLateUpdateDelegate?.Invoke();
				}
			}, ref playerLoop, "PreLateUpdate", "ScriptRunBehaviourLateUpdate");
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					afterDelayedDelegate?.Invoke();
				}
			}, ref playerLoop, "PostLateUpdate", "ScriptRunDelayedDynamicFrameRate");
			AddPlayerLoop(new PlayerLoopSystem
			{
				type = typeof(MagicaManager),
				updateDelegate = delegate
				{
					afterRenderingDelegate?.Invoke();
				}
			}, ref playerLoop, "PostLateUpdate", "FinishFrameRendering");
		}

		private static void AddPlayerLoop(PlayerLoopSystem method, ref PlayerLoopSystem playerLoop, string categoryName, string systemName, bool last = false, bool before = false)
		{
			int num = Array.FindIndex(playerLoop.subSystemList, (PlayerLoopSystem s) => s.type.Name == categoryName);
			PlayerLoopSystem playerLoopSystem = playerLoop.subSystemList[num];
			List<PlayerLoopSystem> list = new List<PlayerLoopSystem>(playerLoopSystem.subSystemList);
			if (last)
			{
				list.Add(method);
			}
			else
			{
				int num2 = list.FindIndex((PlayerLoopSystem h) => h.type.Name.Contains(systemName));
				if (before)
				{
					list.Insert(num2, method);
				}
				else
				{
					list.Insert(num2 + 1, method);
				}
			}
			playerLoopSystem.subSystemList = list.ToArray();
			playerLoop.subSystemList[num] = playerLoopSystem;
		}

		private static bool CheckRegist(ref PlayerLoopSystem playerLoop)
		{
			Type t = typeof(MagicaManager);
			PlayerLoopSystem[] subSystemList = playerLoop.subSystemList;
			for (int i = 0; i < subSystemList.Length; i++)
			{
				PlayerLoopSystem playerLoopSystem = subSystemList[i];
				if (playerLoopSystem.subSystemList != null && playerLoopSystem.subSystemList.Any((PlayerLoopSystem x) => x.type == t))
				{
					return true;
				}
			}
			return false;
		}

		public static void SetGlobalTimeScale(float timeScale)
		{
			if (IsPlaying())
			{
				Time.GlobalTimeScale = Mathf.Clamp01(timeScale);
			}
		}

		public static float GetGlobalTimeScale()
		{
			if (IsPlaying())
			{
				return Time.GlobalTimeScale;
			}
			Develop.LogError((object)"MagicaManager is not starting!");
			return 1f;
		}

		public static void SetSimulationFrequency(int freq)
		{
			if (IsPlaying())
			{
				Time.simulationFrequency = Mathf.Clamp(freq, 30, 150);
			}
		}

		public static int GetSimulationFrequency()
		{
			if (IsPlaying())
			{
				return Time.simulationFrequency;
			}
			Develop.LogError((object)"MagicaManager is not starting!");
			return 0;
		}

		public static void SetMaxSimulationCountPerFrame(int count)
		{
			if (IsPlaying())
			{
				Time.maxSimulationCountPerFrame = Mathf.Clamp(count, 1, 5);
			}
		}

		public static int GetMaxSimulationCountPerFrame()
		{
			if (IsPlaying())
			{
				return Time.maxSimulationCountPerFrame;
			}
			Develop.LogError((object)"MagicaManager is not starting!");
			return 0;
		}

		public static void SetUpdateLocation(TimeManager.UpdateLocation updateLocation)
		{
			if (IsPlaying())
			{
				Time.updateLocation = updateLocation;
			}
		}

		public static TimeManager.UpdateLocation GetUpdateLocation()
		{
			if (IsPlaying())
			{
				return Time.updateLocation;
			}
			Develop.LogError((object)"MagicaManager is not starting!");
			return TimeManager.UpdateLocation.AfterLateUpdate;
		}

		public static void UnloadUnusedData()
		{
			if (IsPlaying())
			{
				PreBuild.UnloadUnusedData();
			}
		}

		public static void SetInitializationLocation(InitializationLocation initLocation)
		{
			initializationLocation = initLocation;
		}

		public static void SetSplitProxyMeshVertexCount(int vertexCount)
		{
			if (IsPlaying())
			{
				Simulation.splitProxyMeshVertexCount = vertexCount;
			}
		}

		public static int GetSplitProxyMeshVertexCount()
		{
			if (IsPlaying())
			{
				return Simulation.splitProxyMeshVertexCount;
			}
			Develop.LogError((object)"MagicaManager is not starting!");
			return 0;
		}
	}
}
