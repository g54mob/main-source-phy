using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SleepyNodes;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
	public enum GamePhase
	{
		MainMenu = 0,
		BrowsingMap = 1,
		MissionActive = 2
	}

	[Serializable]
	public class MissionState
	{
		public bool Complete;

		public bool Failed;

		public Dictionary<string, int> Medals;

		public float StartTime;

		public float CompleteTime;

		public MedalTrackedValues TrackingValues;
	}

	[Header("Main Menu")]
	[Tooltip("Scene to use as the Main Menu (loaded additively alongside the master environment).\nIf 'Auto Load Main Menu On Start' is true, this is loaded at startup.\nWhen an operation starts, the menu will be unloaded if 'Auto Manage Main Menu' is true.\nMust be included in Build Settings.")]
	public MissionSceneReference mainMenuScene;

	[Tooltip("If true, automatically loads the Main Menu scene additively on Start.")]
	public bool autoLoadMainMenuOnStart;

	[Tooltip("If true, the manager automatically unloads the Main Menu when an operation starts and reloads it when an operation ends (e.g., after the last mission is advanced past).")]
	public bool autoManageMainMenu;

	[Header("End Of Mission Overlay")]
	public GameObject SceneObject_EndOfMission;

	public DraggableItemGridArea TurretGrid;

	[Header("Runtime")]
	[ReadOnly]
	public MissionState CurrentMissionState;

	private string loadedMainMenuScene;

	public static MissionManager Instance { get; private set; }

	public GamePhase CurrentPhase { get; private set; }

	public string CurrentMissionSceneName { get; private set; }

	public OperationGraph CurrentOperation { get; private set; }

	public MissionGraph CurrentMission { get; private set; }

	public static string BasePath => null;

	public event Action<GamePhase, GamePhase> PhaseChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<MissionGraph, MissionGraph> MissionChanging
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<MissionGraph, MissionGraph> MissionChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<string> MainMenuLoading
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<string> MainMenuLoaded
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<string> MainMenuUnloading
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<string> MainMenuUnloaded
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void LoadMainMenu()
	{
	}

	private void UnloadMainMenuIfLoaded()
	{
	}

	public void StartOperation(OperationGraph operation, MissionGraph mission)
	{
	}

	public void EndOperationAndReturnToMenu()
	{
	}

	public void EnterBrowsingMap()
	{
	}

	private void LoadMission(MissionGraph mission, bool forceReload = false)
	{
	}

	[Button(null)]
	public void FinishMission()
	{
	}

	public void ReturnToMap()
	{
	}

	private void UnloadCurrentMissionSceneIfAny()
	{
	}

	public void MarkMissionComplete()
	{
	}

	public void MarkMissionFailed()
	{
	}

	public void ModifyTrackingValue(MedalTrackedValue trackingId, float value)
	{
	}

	public void ModifyCustomTrackingValue(string trackingId, float value)
	{
	}

	public void SetTrackingValue(MedalTrackedValue trackingId, float value)
	{
	}

	public void SetCustomTrackingValue(string trackingId, float value)
	{
	}

	public void ReloadCurrentMission()
	{
	}

	public OperationState SaveOperationState()
	{
		return null;
	}

	public void LoadOperationState(OperationState state)
	{
	}

	private void SetupMissionPunchcards()
	{
	}

	private static void EnsureMutatorRuntime()
	{
	}

	private void SetPhase(GamePhase next)
	{
	}
}
