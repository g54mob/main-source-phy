using System;
using System.Collections.Generic;
using Modding;
using UnityEngine;

[AddComponentMenu("Levels/Win Condition")]
public class WinCondition : MonoBehaviour
{
	public static Action<int> PreSetup;

	public static WinCondition Instance;

	public static bool hasWonBefore;

	public static bool hasWon;

	public static int currentObjsCompleted;

	internal static float timeTaken;

	internal static float simStarted;

	public int myLevelIndex;

	public float percentageToWin = 1f;

	[HideInInspector]
	public int objectiveObjectCount;

	[HideInInspector]
	public int fullObjectiveObjectCount;

	[HideInInspector]
	public float completion;

	private readonly float maxCompletion = 100f;

	private NetworkAuxAddPiece auxAddPiece;

	private List<MPTeam> currentWinningTeams = new List<MPTeam>();

	private DestructionBar destructionBar;

	private bool gravWasDisabled;

	private SaveCompletedLevels levelsCompleteCode;

	private GameObject[] objectiveObjs;

	private bool visState = true;

	private WinScreen winScreen;

	private bool canDisplay = true;

	private bool hasShownWinScreen;

	public bool ObjectiveMet
	{
		get
		{
			return currentObjsCompleted >= objectiveObjectCount;
		}
	}

	public bool HasWinEvent
	{
		get
		{
			return currentWinningTeams.Count > 0;
		}
	}

	public bool sandBoxLevel
	{
		get
		{
			return LevelAttributes.instance.sandBoxLevel;
		}
	}

	public bool finalKingdomLevel
	{
		get
		{
			return LevelAttributes.instance.islandFinalLevel;
		}
	}

	public bool finalCampaignLevel
	{
		get
		{
			return LevelAttributes.instance.campaignFinalLevel;
		}
	}

	public static float GetTimeTaken()
	{
		if (StatMaster.isMP)
		{
			return timeTaken;
		}
		return Time.fixedTime - simStarted;
	}

	private void Awake()
	{
		LevelAttributes.FindInstance();
		Instance = this;
		hasWon = false;
		hasWonBefore = false;
		canDisplay = true;
		DestructionBar.percentToWin = percentageToWin;
		objectiveObjs = GameObject.FindGameObjectsWithTag("ObjectiveObj");
		objectiveObjectCount = 0;
		for (int i = 0; i < objectiveObjs.Length; i++)
		{
			if (CountAsObjective(objectiveObjs[i]))
			{
				objectiveObjectCount++;
			}
		}
		ReferenceMaster.onLevelLoad = (Action)Delegate.Combine(ReferenceMaster.onLevelLoad, new Action(OnLevelLoad));
	}

	private bool CountAsObjective(GameObject obj)
	{
		if (obj.GetComponentInParent<DeactivateOnNotTencent>() != null)
		{
			return false;
		}
		return true;
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelLoad = (Action)Delegate.Remove(ReferenceMaster.onLevelLoad, new Action(OnLevelLoad));
	}

	private void OnLevelLoad()
	{
		Reset();
		winScreen.DisableNextLevelGO();
	}

	private void Start()
	{
		if (PreSetup != null)
		{
			PreSetup(fullObjectiveObjectCount);
		}
		currentObjsCompleted = 0;
		winScreen = SingleInstanceFindOnly<WinScreen>.Instance;
		if (winScreen != null)
		{
			winScreen.WinCondition = this;
		}
		destructionBar = SingleInstanceFindOnly<DestructionBar>.Instance;
		if (StatMaster.isMP)
		{
			auxAddPiece = NetworkAuxAddPiece.Instance;
			percentageToWin = 1f;
			completion = 0f;
		}
		else
		{
			if (objectiveObjectCount < 2)
			{
				percentageToWin = 1f;
				objectiveObjectCount = 1;
				fullObjectiveObjectCount = 1;
			}
			else
			{
				fullObjectiveObjectCount = objectiveObjectCount;
				objectiveObjectCount = Mathf.RoundToInt((float)fullObjectiveObjectCount * percentageToWin);
			}
			GameObject gameObject = GameObject.Find("LEVEL LORD");
			if (gameObject != null)
			{
				levelsCompleteCode = gameObject.GetComponent<SaveCompletedLevels>();
			}
		}
		SetVis(false);
	}

	public void OnNext()
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		if (serverSettings.playList.Count != 0)
		{
			serverSettings.playListIndex++;
			if (serverSettings.playListIndex == serverSettings.playList.Count)
			{
				serverSettings.playListIndex = 0;
			}
			LevelEditor.Instance.LoadPlaylistLevel(serverSettings.playListIndex);
		}
	}

	public void CloseWinScreen()
	{
		if (winScreen != null)
		{
			winScreen.Disable();
		}
	}

	public void SetWinningTeams(List<MPTeam> winningTeams)
	{
		currentWinningTeams = winningTeams;
		hasWon = true;
		hasWonBefore = true;
		SingleInstance<Events>.Instance.LevelWon(winningTeams);
	}

	public void Reset()
	{
		ResetProgress();
		CloseWinScreen();
		hasWonBefore = false;
		canDisplay = true;
	}

	public void ResetProgress()
	{
		CloseWinScreen();
		hasWon = false;
		completion = 0f;
		currentWinningTeams = new List<MPTeam>();
		destructionBar.SetProgress(completion);
		destructionBar.ResetProgress();
		hasShownWinScreen = false;
	}

	public List<MPTeam> GetTeamWins()
	{
		return currentWinningTeams;
	}

	public float GetTeamProgress(MPTeam team)
	{
		return (team != MPTeam.None) ? destructionBar.teamCompletion[(int)(team - 1)] : completion;
	}

	public float[] GetTeamProgress()
	{
		int teamCount = GetTeamCount();
		float[] array = new float[teamCount];
		for (int i = 0; i < teamCount; i++)
		{
			array[i] = destructionBar.teamCompletion[destructionBar.teamBars[i].index];
		}
		return array;
	}

	public int GetTeamCount()
	{
		return destructionBar.teamBars.Length;
	}

	private bool SetLevelCompletion(float percentage)
	{
		completion = Mathf.Clamp(completion + percentage, 0f, maxCompletion);
		destructionBar.SetProgress(completion);
		return completion >= maxCompletion;
	}

	public void AddProgress(MPTeam team, float percentage)
	{
		if (hasWon || completion >= maxCompletion)
		{
			return;
		}
		SingleInstance<Events>.Instance.ProgressAdded(team, percentage);
		if (team == MPTeam.None)
		{
			if (SetLevelCompletion(percentage))
			{
				List<MPTeam> list = new List<MPTeam>();
				list.Add(team);
				List<MPTeam> winningTeams = list;
				SetWinningTeams(winningTeams);
			}
		}
		else if (destructionBar.AddProgress(team, percentage))
		{
			List<MPTeam> list = new List<MPTeam>();
			list.Add(team);
			List<MPTeam> winningTeams2 = list;
			SetWinningTeams(winningTeams2);
		}
	}

	public void OnWinEvent(List<MPTeam> winningTeams)
	{
		if (PlayerData.hasLocalPlayer && PlayerData.localPlayer.PlayMode != BesiegePlayMode.LocalSimulation)
		{
			SetWinningTeams(winningTeams);
		}
		ShowWinningTeams(winningTeams);
	}

	private void ShowWinningTeams(List<MPTeam> winningTeams)
	{
		bool hasLocalPlayer = PlayerData.hasLocalPlayer;
		if (!hasLocalPlayer || (!PlayerData.localPlayer.isSpectator && !PlayerData.localPlayer.machine.isSimulating))
		{
			if (hasLocalPlayer && PlayerData.localPlayer.PlayMode == BesiegePlayMode.BuildMode)
			{
				winScreen.ShowNextZoneButton();
			}
			return;
		}
		if (winningTeams.Count == 1)
		{
			ShowWinScreen(winningTeams[0]);
		}
		else
		{
			ShowWinScreen(winningTeams.ToArray());
		}
		hasShownWinScreen = true;
	}

	private void Update()
	{
		if (StatMaster.isMP)
		{
			SetVis(auxAddPiece.receivedGameState);
			if (StatMaster.levelSimulating && hasWon && !hasShownWinScreen)
			{
				ShowWinningTeams(currentWinningTeams);
			}
		}
		else if (!StatMaster.levelSimulating)
		{
			hasWon = false;
			canDisplay = true;
			currentObjsCompleted = 0;
		}
		else if (StatMaster.Bounding.Enabled && !StatMaster.GodTools.GodToolsUsed() && !gravWasDisabled && !sandBoxLevel)
		{
			SetVis(true);
			if (StatMaster.GodTools.GravityDisabled)
			{
				gravWasDisabled = true;
			}
			SetKillProgress();
			if (ObjectiveMet)
			{
				if (!hasWon)
				{
					if (ReferenceMaster.onBeforeLevelWon != null)
					{
						ReferenceMaster.onBeforeLevelWon();
					}
					if (myLevelIndex < LEVELLORD.levelsComplete.Length)
					{
						LEVELLORD.levelsComplete[myLevelIndex] = 1;
						if (levelsCompleteCode != null)
						{
							levelsCompleteCode.SaveGame();
						}
					}
					hasWon = true;
					hasWonBefore = true;
				}
				if (canDisplay)
				{
					ShowWinScreen();
					canDisplay = false;
				}
			}
			else
			{
				hasWon = false;
				canDisplay = true;
			}
		}
		else
		{
			hasWon = false;
			canDisplay = true;
		}
		if (!StatMaster.levelSimulating)
		{
			StatMaster.GodTools.GodToolsUsed();
			gravWasDisabled = false;
		}
	}

	private void ShowWinScreen(MPTeam t = MPTeam.None)
	{
		winScreen.Display(t);
	}

	private void ShowWinScreen(MPTeam[] t)
	{
		winScreen.Display(t);
	}

	private void SetKillProgress()
	{
		SetKillProgress((float)currentObjsCompleted * 1f);
	}

	private void SetKillProgress(float currentCompleted)
	{
		destructionBar.SetProgress(currentCompleted / ((float)objectiveObjectCount * 1f) * maxCompletion);
		destructionBar.fullPercent = currentCompleted / ((float)fullObjectiveObjectCount * 1f) * maxCompletion;
	}

	private void SetVis(bool toggle)
	{
		if (visState != toggle)
		{
			WinScreen instance = SingleInstanceFindOnly<WinScreen>.Instance;
			if (instance != null)
			{
				instance.ToggleBG(toggle);
			}
			visState = toggle;
		}
	}
}
