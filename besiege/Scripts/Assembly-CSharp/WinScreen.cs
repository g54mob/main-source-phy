using System;
using System.Collections;
using System.Collections.Generic;
using InternalModding;
using InternalModding.Blocks;
using InternalModding.Mods;
using Localisation;
using Steamworks;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UI/Win Screen/Win Screen")]
public class WinScreen : SingleInstanceFindOnly<WinScreen>, ILocalisationAware
{
	protected const int InvincibleScore = 4;

	[Header("UI")]
	private StartGameButton nextZoneButton;

	public StartGameButton returnGameButton;

	public UIButton closeWinScreen;

	public UIButton returnToMenu;

	public UIButton nextLevel;

	[Header("References")]
	public WorldConqueredController lastLevelController;

	public BarPositionController barPosController;

	[FormerlySerializedAs("barAnimation")]
	public TriumphBarsLerpIn triumphBarLerpIn;

	public StampFanfareController stamp;

	protected WinCondition _winConditionScript;

	[Header("3D Elements")]
	public Transform stampObj;

	public ParticleSystem[] dustParticles;

	public TriumphFlagAnim[] flagsAndTrumpets;

	public MeshRenderer[] flagsToColour;

	[Header("Stamp Animation")]
	public float scaleDuration = 0.2f;

	public float scaleUpAmount = 1.4f;

	protected Vector3 startScale;

	[Header("Togglers")]
	public Renderer BG;

	public GameObject OldHeader;

	public GameObject NewHeader;

	public GameObject newSubHeader;

	public GameObject victorySubHeader;

	public GameObject defeatSubHeader;

	public GameObject succesSubHeader;

	public GameObject closeWinScreenGO;

	public GameObject nextLevelGO;

	public GameObject returnToMenuGO;

	public GameObject[] hideOnNoTeams;

	[Header("Header Text")]
	public DynamicText playerNames;

	public DynamicText victoryHeader;

	public MeshRenderer leftHeaderSymbol;

	public MeshRenderer rightHeaderSymbol;

	[Header("Value Fields")]
	public DynamicText bestPct;

	public DynamicText bestType;

	public TextMesh blocksUsed;

	public TextMesh timeTaken;

	public TextMesh teamBlocksUsed;

	public TextMesh teamSize;

	[Header("Objectives")]
	public Transform objectivesParent;

	public MeshRenderer noDamageDisplay;

	public MeshRenderer legitMachineDisplay;

	public MeshRenderer achievementDisplay;

	public MeshRenderer achievementNADisplay;

	public TextMesh noDamageComplete;

	public TextMesh achievementComplete;

	public TextMesh legitComplete;

	public TextMesh achievementTooltip;

	[Header("Histograms")]
	[SerializeField]
	internal StatHistogram statHistogram;

	internal static float pctDmg = 1f;

	internal static float pctTime = 1f;

	internal static float pctBlocks = 1f;

	protected Dictionary<MPTeam, Color> TeamColours = new Dictionary<MPTeam, Color>
	{
		{
			MPTeam.Blue,
			Color.blue
		},
		{
			MPTeam.Green,
			Color.green
		},
		{
			MPTeam.Orange,
			Color.red + Color.yellow
		},
		{
			MPTeam.Red,
			Color.red
		}
	};

	public float textWaitDuration = 0.3f;

	protected Coroutine fadingText;

	internal static bool noErrorsDetected = true;

	private bool allModsValid = true;

	public bool level_or_prerequisites_changed_by_mod;

	private bool addedToEsc;

	internal static int blockScore = 1000;

	internal static float blockDmg;

	internal float timeElapsed;

	internal bool isValid;

	private int previousPlace = int.MaxValue;

	private float previousPct = float.MaxValue;

	private bool justGotAchievement;

	private Color objectiveColor = Color.black;

	public bool Visible { get; private set; }

	public override string Name
	{
		get
		{
			return "WinScreen";
		}
	}

	private bool ShowHistograms
	{
		get
		{
			return SteamManager.Initialized && statHistogram != null && StatMaster.GetCurrentIsland() != Island.None && OptionsMaster.BesiegeConfig.UseLeaderboards;
		}
	}

	public WinCondition WinCondition
	{
		get
		{
			return _winConditionScript;
		}
		set
		{
			_winConditionScript = value;
			if (closeWinScreen != null)
			{
				closeWinScreen.ResetDelegates();
				closeWinScreen.Down += _winConditionScript.CloseWinScreen;
			}
			if (nextLevel != null)
			{
				nextLevel.ResetDelegates();
				nextLevel.Down += OnNextZone;
			}
			if (returnToMenu != null)
			{
				returnToMenu.ResetDelegates();
				returnToMenu.Down += OnReturn;
			}
		}
	}

	public void SetZoneToLoad(string s)
	{
		if (nextZoneButton == null)
		{
			nextZoneButton = nextLevel.GetComponent<StartGameButton>();
		}
		nextZoneButton.levelToLoad = s;
	}

	private void OnReturn()
	{
		if ((bool)returnGameButton)
		{
			ReferenceMaster.Instance.StartCoroutine(returnGameButton.IELoadLevel());
		}
	}

	private void OnNextZone()
	{
		if (StatMaster.isMP)
		{
			_winConditionScript.OnNext();
			return;
		}
		if (nextZoneButton == null)
		{
			nextZoneButton = nextLevel.GetComponent<StartGameButton>();
		}
		if (nextZoneButton != null)
		{
			nextZoneButton.LoadLevel();
		}
		else
		{
			Debug.LogError("Next zone button doesn't have a StartGameButton!");
		}
	}

	protected override void Awake()
	{
		LevelAttributes.FindInstance();
		base.Awake();
		if (barPosController == null)
		{
			barPosController = UnityEngine.Object.FindObjectOfType<BarPositionController>();
		}
		startScale = stampObj.localScale;
		LevelAchievementTrigger.OnLevelAchievement = (Action<int>)Delegate.Combine(LevelAchievementTrigger.OnLevelAchievement, new Action<int>(SetAchievement));
		blockScore = 0;
		blockDmg = 0f;
		CheckMods();
	}

	private void CheckMods()
	{
		foreach (ModContainer mod in ModManager.Mods)
		{
			if (!mod.IsActive || mod.Assemblies.Count <= 0)
			{
				continue;
			}
			switch (mod.Info.Id.ToString())
			{
			case "3eb31d9c-f404-4ec3-b5ad-7e0c641d2b46":
			case "bb7ba333-e72b-49a8-8c0d-011a3fcadaf3":
			case "7062baee-484e-4cdd-8750-b4baa7b964e5":
			case "76b13186-708a-4719-84a1-aa243aeda824":
			case "625c67dd-2751-4420-b4a8-c57e08453fe3":
			case "14e26127-af46-48e9-8077-22a672f7a322":
			case "8bbf03ed-9050-4466-8974-18a6b62a26f4":
			case "c305c81a-0265-4db3-9552-1e20c07b4203":
			case "3af785fd-25ff-4d18-856e-4c46f4311376":
			case "90f11d60-679e-489b-a5d2-77a828787397":
			case "2960c24f-cc96-4ea7-adb6-8f46d2f505d9":
			case "ded86ea5-2bf2-4cee-864c-bd2155385b73":
			case "3c1fa3de-ec74-44e4-807c-9eced79ddd3f":
			case "61d89dcf-88a2-4a16-8eb2-08aeed441f1d":
			case "565fdc41-d8bc-452c-81f8-09b16934f618":
			case "af6776f1-a755-4653-9eff-97a8ee6e67c1":
			case "2cb408a2-f5ea-4630-9a16-7fcb30135096":
			case "68b77b77-2dea-404e-b0c5-bc20e0c51923":
			case "ae264284-e41e-4997-912e-51d89c374fee":
			case "d69c6b5d-7bf3-4285-b06f-3832f2c5ddfc":
			case "63347db1-0541-4a04-b741-d77d7ffe4931":
			case "9aed1ee7-3515-41b1-8c63-b744d9248244":
			case "0fba22a9-8f51-43f8-9b0d-053ba4453262":
			case "693bc82f-d464-4367-99b2-bbd0d407ecdf":
			case "9a255fc8-7c0b-4108-ba5e-288551dba27f":
			case "5327dce5-05df-4e6b-8a36-c63d1944b231":
			case "68285ed8-a14e-4879-8dbb-76e2ac9a99da":
				{
					switch (mod.Info.Author)
					{
					case "dagriefaa":
					case "dagriefaa + Shadé":
					case "Shadé":
					case "EEX":
					case "EEX-slime":
					case "tamakoro":
					case "spaar":
					case "ITR":
					case "渔夫lotsofone":
					case "MaxTCC":
					case "Lench, adapted by spaar":
					case "Spoonail":
						if (mod.Info.FromWorkshop)
						{
							goto end_IL_0049;
						}
						break;
					}
					Debug.LogWarning(string.Concat(mod.Info.Id, ": ", mod.Info.Name, " is active and not allowed"));
					allModsValid = false;
					break;
				}
				end_IL_0049:
				break;
			}
		}
	}

	protected void OnDestroy()
	{
		LevelAchievementTrigger.OnLevelAchievement = (Action<int>)Delegate.Remove(LevelAchievementTrigger.OnLevelAchievement, new Action<int>(SetAchievement));
	}

	private IEnumerator Start()
	{
		if (LevelAttributes.instance.campaignFinalLevel)
		{
			objectivesParent.SetParent(lastLevelController.planet.transform);
			lastLevelController.AddRender(noDamageDisplay);
			lastLevelController.AddRender(legitMachineDisplay);
			lastLevelController.AddRender(achievementDisplay);
			Vector3 pos = objectivesParent.localPosition;
			pos.x = 0f;
			pos.y = 0.16f;
			pos.z = 0.2135f;
			objectivesParent.localPosition = pos;
		}
		else
		{
			stamp.SetObjectiveParent(objectivesParent);
			Vector3 pos2 = objectivesParent.localPosition;
			pos2.y = ((!LevelAttributes.instance.islandFinalLevel) ? 0.105f : 0.012f);
			objectivesParent.localPosition = pos2;
		}
		returnToMenuGO.SetActive(!StatMaster.isMP);
		fadingText = StartCoroutine(FadeTeamWinnerText(0f, 0f));
		if (!TeamColours.ContainsKey(MPTeam.None))
		{
			TeamColours = new Dictionary<MPTeam, Color>
			{
				{
					MPTeam.Blue,
					ReferenceMaster.Instance.teamColors[4]
				},
				{
					MPTeam.Green,
					ReferenceMaster.Instance.teamColors[2]
				},
				{
					MPTeam.Orange,
					ReferenceMaster.Instance.teamColors[3]
				},
				{
					MPTeam.Red,
					ReferenceMaster.Instance.teamColors[1]
				},
				{
					MPTeam.None,
					flagsToColour[0].material.color
				}
			};
		}
		if (!ObjectiveTrackerUI.LevelHasAchievement(WinCondition.Instance.myLevelIndex))
		{
			noDamageDisplay.transform.localPosition += new Vector3(0.25f, -0.0255f, 0f);
			legitMachineDisplay.transform.localPosition += new Vector3(0.25f, 0.0255f, 0f);
			achievementDisplay.transform.localPosition += new Vector3(1000f, -0.0255f, 0f);
			noDamageDisplay.GetComponent<Tooltip>().tooltipParent.transform.localPosition += new Vector3(0.25f, -0.047f, 0f);
			legitMachineDisplay.GetComponent<Tooltip>().tooltipParent.transform.localPosition += new Vector3(0.25f, 0.047f, 0f);
		}
		else
		{
			ChangeTooltip();
		}
		if (statHistogram != null)
		{
			yield return null;
			yield return null;
			statHistogram.Init();
			for (int i = 0; i < statHistogram.scorehandlers.Length; i++)
			{
				yield return null;
				statHistogram.scorehandlers[i].Init();
			}
		}
	}

	public void OnLocalisationChange()
	{
		if (ObjectiveTrackerUI.LevelHasAchievement(WinCondition.Instance.myLevelIndex))
		{
			ChangeTooltip();
		}
	}

	private void ChangeTooltip()
	{
		string text = string.Format(LocalisationManager.GetTranslation(4964), ObjectiveTrackerUI.GetAchievementDescription(WinCondition.Instance.myLevelIndex));
		achievementTooltip.text = text;
		Tooltip component = achievementDisplay.GetComponent<Tooltip>();
		MeshRenderer background = component.Background;
		if ((bool)background)
		{
			int num = text.Split('\n').Length;
			Vector3 localScale = background.transform.localScale;
			localScale.y = 0.85f + 0.75f * (float)num / 5f;
			background.transform.localScale = localScale;
			Vector3 localPosition = background.transform.localPosition;
			localPosition.y = (0f - localScale.y) * 0.5f - 0.36f;
			background.transform.localPosition = localPosition;
		}
	}

	public void DisableNextLevelGO()
	{
		nextLevelGO.SetActive(false);
	}

	public void Disable()
	{
		if (!Visible)
		{
			return;
		}
		if (addedToEsc)
		{
			addedToEsc = false;
			InputManager.RemoveAsNextToClose(Disable);
		}
		Visible = false;
		barPosController.Set();
		objectivesParent.gameObject.SetActive(false);
		if (WinCondition.finalCampaignLevel)
		{
			lastLevelController.Disable();
			SetBottomBarLabel(1);
			return;
		}
		SetBottomBarLabel(0);
		triumphBarLerpIn.HideBars();
		for (int i = 0; i < flagsAndTrumpets.Length; i++)
		{
			flagsAndTrumpets[i].Disable();
		}
		stamp.LevelReset();
		ClearTeamWinnerText();
	}

	public void ToggleBG(bool toggle)
	{
		BG.enabled = toggle;
	}

	public void ShowNextZoneButton()
	{
		triumphBarLerpIn.ShowNextZoneButtonBuildModeMP();
	}

	private void InvokeOnLevelWon()
	{
		if (ReferenceMaster.onLevelWon != null)
		{
			ReferenceMaster.onLevelWon();
		}
	}

	public void Display(MPTeam winner = MPTeam.None)
	{
		if (!addedToEsc)
		{
			addedToEsc = true;
			InputManager.AddAsNextToClose(Disable);
		}
		barPosController.Set();
		if (StatMaster.isMP && (!PlayerData.hasLocalPlayer || (!PlayerData.localPlayer.isSpectator && !PlayerData.localPlayer.machine.isSimulating)))
		{
			Disable();
		}
		else
		{
			if (Visible)
			{
				return;
			}
			Visible = true;
			InvokeOnLevelWon();
			if (WinCondition.finalCampaignLevel)
			{
				lastLevelController.Display();
				SetBottomBarLabel(1);
				SetStats();
				for (int i = 0; i < hideOnNoTeams.Length; i++)
				{
					hideOnNoTeams[i].SetActive(false);
				}
				return;
			}
			if (StatMaster.isMP)
			{
				if (PlayerData.localPlayer.isSpectator)
				{
					SetBottomBarLabel(4);
				}
				else
				{
					if (winner == MPTeam.None)
					{
						SetBottomBarLabel(1);
					}
					else if (PlayerData.localPlayer.team == winner)
					{
						SetBottomBarLabel(2);
					}
					else
					{
						SetBottomBarLabel(3);
					}
					if (!StatMaster.isLocalSim)
					{
						AchievementHelper.Increment(33, 1);
					}
				}
				SetHeader(winner);
				bool flag = StatMaster.isClient || StatMaster.isLocalSim || StatMaster.Mode.levelEdit || NetworkScene.ServerSettings.playList.Count < 2;
				closeWinScreenGO.SetActive(flag);
				nextLevelGO.SetActive(!flag);
			}
			else
			{
				SetBottomBarLabel(1);
				SetHeader();
			}
			for (int j = 0; j < flagsToColour.Length; j++)
			{
				flagsToColour[j].material.color = TeamColours[winner];
			}
			for (int k = 0; k < flagsAndTrumpets.Length; k++)
			{
				flagsAndTrumpets[k].Display();
			}
			triumphBarLerpIn.ShowBars();
		}
	}

	public void Display(MPTeam[] winners)
	{
		barPosController.Set();
		if (!PlayerData.hasLocalPlayer || (!PlayerData.localPlayer.isSpectator && !PlayerData.localPlayer.machine.isSimulating))
		{
			Disable();
		}
		else
		{
			if (Visible)
			{
				return;
			}
			Visible = true;
			if (PlayerData.localPlayer.isSpectator)
			{
				SetBottomBarLabel(4);
			}
			else
			{
				int num = 0;
				while (true)
				{
					if (num < winners.Length)
					{
						if (PlayerData.localPlayer.team == winners[num])
						{
							SetBottomBarLabel(4);
							break;
						}
						num++;
						continue;
					}
					SetBottomBarLabel(3);
					break;
				}
			}
			SetHeader(winners);
			InvokeOnLevelWon();
			bool flag = StatMaster.isClient || StatMaster.Mode.levelEdit || NetworkScene.ServerSettings.playList.Count < 2;
			closeWinScreenGO.SetActive(flag);
			nextLevelGO.SetActive(!flag);
			for (int i = 0; i < flagsToColour.Length; i++)
			{
				flagsToColour[i].material.color = TeamColours[winners[i % winners.Length]];
			}
			for (int j = 0; j < flagsAndTrumpets.Length; j++)
			{
				flagsAndTrumpets[j].Display();
			}
			triumphBarLerpIn.ShowBars();
		}
	}

	protected void SetBottomBarLabel(int val)
	{
		bool showHistograms = ShowHistograms;
		switch (val)
		{
		case 0:
			if ((bool)newSubHeader)
			{
				newSubHeader.SetActive(false);
			}
			succesSubHeader.SetActive(false);
			victorySubHeader.SetActive(false);
			defeatSubHeader.SetActive(false);
			break;
		case 1:
			if ((bool)newSubHeader)
			{
				newSubHeader.SetActive(showHistograms);
			}
			succesSubHeader.SetActive(!showHistograms);
			victorySubHeader.SetActive(false);
			defeatSubHeader.SetActive(false);
			break;
		case 2:
			if ((bool)newSubHeader)
			{
				newSubHeader.SetActive(false);
			}
			succesSubHeader.SetActive(false);
			victorySubHeader.SetActive(true);
			defeatSubHeader.SetActive(false);
			break;
		case 3:
			if ((bool)newSubHeader)
			{
				newSubHeader.SetActive(false);
			}
			succesSubHeader.SetActive(false);
			victorySubHeader.SetActive(false);
			defeatSubHeader.SetActive(true);
			break;
		case 4:
			if ((bool)newSubHeader)
			{
				newSubHeader.SetActive(false);
			}
			succesSubHeader.SetActive(false);
			victorySubHeader.SetActive(false);
			defeatSubHeader.SetActive(false);
			break;
		}
	}

	public void SetHeader(MPTeam winner = MPTeam.None)
	{
		if (winner == MPTeam.None)
		{
			ClearTeamWinnerText();
			SetStats();
			stamp.LevelCompleted();
			for (int i = 0; i < hideOnNoTeams.Length; i++)
			{
				hideOnNoTeams[i].SetActive(false);
			}
			return;
		}
		objectivesParent.gameObject.SetActive(false);
		SetTeamWinnerText(winner);
		SetStats(true);
		stamp.LevelReset();
		for (int j = 0; j < hideOnNoTeams.Length; j++)
		{
			hideOnNoTeams[j].SetActive(true);
		}
	}

	public void SetHeader(MPTeam[] winners)
	{
		if (winners.Length < 2)
		{
			SetHeader(winners[0]);
			return;
		}
		objectivesParent.gameObject.SetActive(false);
		SetTeamWinnerText(winners);
		SetStats(true);
		stamp.LevelReset();
		for (int i = 0; i < hideOnNoTeams.Length; i++)
		{
			hideOnNoTeams[i].SetActive(true);
		}
	}

	protected void SetTeamWinnerText(MPTeam winner)
	{
		string translation = LocalisationManager.GetTranslation(2402);
		switch (winner)
		{
		case MPTeam.Blue:
			translation = LocalisationManager.GetTranslation(1942);
			break;
		case MPTeam.Green:
			translation = LocalisationManager.GetTranslation(1943);
			break;
		case MPTeam.Red:
			translation = LocalisationManager.GetTranslation(1944);
			break;
		case MPTeam.Orange:
			translation = LocalisationManager.GetTranslation(1945);
			break;
		case MPTeam.None:
			Debug.LogError("Team winning can't be None, This function should only be accessed through SetHeader() so it can fall back to the default winning header");
			break;
		}
		ReferenceMaster.SetDynamicText(victoryHeader, string.Format(LocalisationManager.GetTranslation(3556), translation));
		SetIcons();
		string text = string.Empty;
		string text2 = ", ";
		List<PlayerData> list = new List<PlayerData>();
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			if (Playerlist.Players[i].team == winner)
			{
				list.Add(Playerlist.Players[i]);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (j == list.Count - 2)
			{
				text2 = " & ";
			}
			else if (j == list.Count - 1)
			{
				text2 = string.Empty;
			}
			text = text + list[j].name + text2;
		}
		if (text == string.Empty)
		{
			text = LocalisationManager.GetTranslation(3558);
		}
		ReferenceMaster.SetDynamicText(playerNames, text.ToUpper());
		StopAllCoroutines();
		fadingText = StartCoroutine(Stamp());
	}

	protected void SetTeamWinnerText(MPTeam[] winners)
	{
		string text = string.Empty;
		string text2 = ((winners.Length != 2) ? ", " : " & ");
		string text3 = string.Empty;
		if (winners.Length < 4)
		{
			for (int i = 0; i < winners.Length; i++)
			{
				if (i == winners.Length - 1)
				{
					text2 = string.Empty;
				}
				switch (winners[i])
				{
				case MPTeam.Blue:
					text = text + LocalisationManager.GetTranslation(1942) + text2;
					break;
				case MPTeam.Green:
					text = text + LocalisationManager.GetTranslation(1943) + text2;
					break;
				case MPTeam.Red:
					text = text + LocalisationManager.GetTranslation(1944) + text2;
					break;
				case MPTeam.Orange:
					text = text + LocalisationManager.GetTranslation(1945) + text2;
					break;
				case MPTeam.None:
					Debug.LogError("Teams drawing can't contain a None team.");
					break;
				}
			}
		}
		else
		{
			text = LocalisationManager.GetTranslation(1637);
		}
		text2 = ", ";
		ReferenceMaster.SetDynamicText(victoryHeader, string.Format(LocalisationManager.GetTranslation(3557), text));
		SetIcons();
		List<PlayerData> list = new List<PlayerData>();
		for (int j = 0; j < winners.Length; j++)
		{
			for (int k = 0; k < Playerlist.Players.Count; k++)
			{
				if (Playerlist.Players[k].team == winners[j])
				{
					list.Add(Playerlist.Players[k]);
				}
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			if (l == list.Count - 2)
			{
				text2 = "& ";
			}
			else if (l == list.Count - 1)
			{
				text2 = string.Empty;
			}
			text3 = text3 + list[l].name + text2;
		}
		if (text3 == string.Empty)
		{
			text3 = LocalisationManager.GetTranslation(3559);
		}
		ReferenceMaster.SetDynamicText(playerNames, text3.ToUpper());
		StopAllCoroutines();
		fadingText = StartCoroutine(Stamp());
	}

	public void ClearTeamWinnerText()
	{
		StopAllCoroutines();
		fadingText = StartCoroutine(FadeTeamWinnerText(0f, 0.1f));
	}

	protected IEnumerator FadeTeamWinnerText(float alpha, float time, float delay = 0f)
	{
		MeshRenderer headerRender = victoryHeader.GetComponent<MeshRenderer>();
		MeshRenderer namesHeaderRender = playerNames.GetComponent<MeshRenderer>();
		CreateDropShadows headerDropShadows = victoryHeader.GetComponent<CreateDropShadows>();
		if (delay > 0f)
		{
			yield return new WaitForSecondsRealtime(delay);
		}
		if (alpha > 0f)
		{
			headerDropShadows.Create();
		}
		else
		{
			headerDropShadows.Clear();
		}
		Color c1 = headerRender.material.color;
		Color c2 = namesHeaderRender.material.color;
		Color c3 = leftHeaderSymbol.material.GetColor("_TintColor");
		Color c4 = rightHeaderSymbol.material.GetColor("_TintColor");
		for (float t = 0f; t < time; t += Time.deltaTime)
		{
			float elapsed = t / time;
			headerRender.material.color = new Color(c1.r, c1.g, c1.b, Mathf.Lerp(c1.a, alpha, elapsed));
			namesHeaderRender.material.color = new Color(c2.r, c2.g, c2.b, Mathf.Lerp(c2.a, alpha * 0.62f, elapsed));
			leftHeaderSymbol.material.SetColor("_TintColor", new Color(c3.r, c3.g, c3.b, Mathf.Lerp(c3.a, alpha, elapsed)));
			rightHeaderSymbol.material.SetColor("_TintColor", new Color(c4.r, c4.g, c4.b, Mathf.Lerp(c4.a, alpha, elapsed)));
			yield return null;
		}
		headerRender.material.color = new Color(c1.r, c1.g, c1.b, alpha);
		namesHeaderRender.material.color = new Color(c2.r, c2.g, c2.b, alpha * 0.62f);
		leftHeaderSymbol.material.SetColor("_TintColor", new Color(c3.r, c3.g, c3.b, alpha));
		rightHeaderSymbol.material.SetColor("_TintColor", new Color(c4.r, c4.g, c4.b, alpha));
	}

	private IEnumerator Stamp()
	{
		yield return new WaitForSeconds(textWaitDuration);
		float textFadeDuration = 0.3f;
		stampObj.localScale = startScale * scaleUpAmount;
		StartCoroutine(FadeTeamWinnerText(1f, textFadeDuration));
		yield return new WaitForSeconds(textFadeDuration * 0.75f);
		GetComponent<AudioSource>().Play();
		yield return StartCoroutine(LerpSize());
	}

	private IEnumerator LerpSize()
	{
		float cTime = 0f;
		float rate = 1f / scaleDuration;
		Vector3 sizeToBe = startScale * scaleUpAmount;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			stampObj.localScale = Vector3.Lerp(sizeToBe, startScale, cTime);
			yield return null;
		}
		for (int i = 0; i < dustParticles.Length; i++)
		{
			dustParticles[i].Stop();
			dustParticles[i].randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
			dustParticles[i].Play();
		}
	}

	protected void SetIcons()
	{
		float num = victoryHeader.transform.position.x + victoryHeader.bounds.max.x;
		float num2 = victoryHeader.transform.position.x + victoryHeader.bounds.min.x;
		float num3 = 0.5f;
		leftHeaderSymbol.transform.position = new Vector3(num2 - num3, leftHeaderSymbol.transform.position.y, leftHeaderSymbol.transform.position.z);
		rightHeaderSymbol.transform.position = new Vector3(num + num3 - 0.055f, rightHeaderSymbol.transform.position.y, rightHeaderSymbol.transform.position.z);
	}

	public void SetStyle(bool isNew)
	{
		if ((bool)NewHeader)
		{
			OldHeader.SetActive(!isNew);
			NewHeader.SetActive(isNew);
		}
	}

	internal void SetStats(bool showTeamStats = false)
	{
		if (!showTeamStats && ShowHistograms)
		{
			SetStyle(true);
			UploadScores();
			return;
		}
		GetMachineScores(Machine.Active());
		SetStyle(false);
		blocksUsed.text = string.Empty + StatMaster.BlockCount;
		float num = GetTimeTaken();
		int num2 = Mathf.FloorToInt(num / 60f);
		int num3 = Mathf.FloorToInt(num - (float)(num2 * 60));
		int num4 = Mathf.RoundToInt(num * 100f % 100f);
		timeTaken.text = string.Empty + string.Format("{0:00}:{1:00}:{2:00}", num2, num3, num4);
		if (!showTeamStats)
		{
			return;
		}
		List<PlayerData> list = new List<PlayerData>();
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			if (Playerlist.Players[i].team == PlayerData.localPlayer.team)
			{
				list.Add(Playerlist.Players[i]);
			}
		}
		teamSize.text = string.Empty + list.Count;
		int num5 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			PlayerData playerData = list[j];
			if (!playerData.isSpectator)
			{
				num5 += playerData.machine.BlockCount;
			}
		}
		teamBlocksUsed.text = string.Empty + num5;
	}

	internal void UploadScores()
	{
		GetMachineScores(Machine.Active());
		for (int i = 0; i < statHistogram.scorehandlers.Length; i++)
		{
			statHistogram.scorehandlers[i].ResetLine();
			if (isValid)
			{
				switch (statHistogram.scorehandlers[i].dataType)
				{
				case LeaderboardDataType.Time:
					statHistogram.scorehandlers[i].Upload((int)(timeElapsed * 1000f), OnTimeScoreUploadComplete);
					break;
				case LeaderboardDataType.BlockScore:
					statHistogram.scorehandlers[i].Upload(blockScore, OnBlockScoreUploadComplete);
					break;
				case LeaderboardDataType.DamageTaken:
					statHistogram.scorehandlers[i].Upload((int)(blockDmg * 1000f), OnDamageScoreUploadComplete);
					break;
				}
			}
			else
			{
				switch (statHistogram.scorehandlers[i].dataType)
				{
				case LeaderboardDataType.Time:
					statHistogram.scorehandlers[i].ShowTopBoard();
					break;
				case LeaderboardDataType.BlockScore:
					statHistogram.scorehandlers[i].ShowTopBoard();
					break;
				case LeaderboardDataType.DamageTaken:
					statHistogram.scorehandlers[i].ShowTopBoard();
					break;
				}
				if (statHistogram.scorehandlers[i].dataType == LeaderboardDataType.Time)
				{
					statHistogram.scorehandlers[i].SetPct(float.NaN);
					SetNewStat(string.Empty, 1f, statHistogram.scorehandlers[i].leaderboardData.uploadDataStored);
				}
				else
				{
					statHistogram.scorehandlers[i].SetPct(float.NaN);
				}
			}
			switch (statHistogram.scorehandlers[i].dataType)
			{
			case LeaderboardDataType.Time:
				statHistogram.scorehandlers[i].SetDynText(timeElapsed);
				break;
			case LeaderboardDataType.BlockScore:
				statHistogram.scorehandlers[i].SetDynText(blockScore);
				break;
			case LeaderboardDataType.DamageTaken:
				statHistogram.scorehandlers[i].SetDynText(blockDmg);
				break;
			}
		}
	}

	private void OnBlockScoreUploadComplete(LeaderboardScoreUploaded_t uploadResponse, float scorePct, ScoreHandler scoreScript)
	{
		SetPct(ref pctBlocks, uploadResponse, scoreScript.leaderboardData.GetLeaderBoardEntryCount(), scorePct);
		scoreScript.SetPct(pctBlocks);
		SetNewStat(LocalisationManager.GetTranslation(4893), pctBlocks, scoreScript.leaderboardData.uploadDataStored);
		statHistogram.ScoreHandlerDoneUploading(0);
	}

	private void OnDamageScoreUploadComplete(LeaderboardScoreUploaded_t uploadResponse, float scorePct, ScoreHandler scoreScript)
	{
		SetPct(ref pctDmg, uploadResponse, scoreScript.leaderboardData.GetLeaderBoardEntryCount(), scorePct);
		scoreScript.SetPct(pctDmg);
		SetNewStat(LocalisationManager.GetTranslation(4892), pctDmg, scoreScript.leaderboardData.uploadDataStored);
		statHistogram.ScoreHandlerDoneUploading(1);
	}

	private void OnTimeScoreUploadComplete(LeaderboardScoreUploaded_t uploadResponse, float scorePct, ScoreHandler scoreScript)
	{
		SetPct(ref pctTime, uploadResponse, scoreScript.leaderboardData.GetLeaderBoardEntryCount(), scorePct);
		scoreScript.SetPct(pctTime);
		SetNewStat(LocalisationManager.GetTranslation(889), pctTime, scoreScript.leaderboardData.uploadDataStored);
		statHistogram.ScoreHandlerDoneUploading(2);
	}

	private void SetPct(ref float pct, LeaderboardScoreUploaded_t uploadResponse, int count, float fallback)
	{
		pct = fallback;
	}

	private void SetNewStat(string s, float p, LeaderboardScoreUploaded_t v)
	{
		if (!isValid)
		{
			ReferenceMaster.SetDynamicText(bestPct, LocalisationManager.GetTranslation(2903));
			ReferenceMaster.SetDynamicText(bestType, string.Format(LocalisationManager.GetTranslation(4901), WinCondition.myLevelIndex + 1));
			return;
		}
		if (p < previousPct && previousPlace > 100)
		{
			previousPct = p;
			float num = Mathf.Clamp01(p) * 100f;
			ReferenceMaster.SetDynamicText(bestPct, string.Format(LocalisationManager.GetTranslation(4897), Mathf.Max(1, Mathf.CeilToInt(num - num % 0.01f))));
			ReferenceMaster.SetDynamicText(bestType, s);
		}
		if (v.m_nGlobalRankNew == 0)
		{
			Debug.LogWarning("Rank is 0, but valid values are 1 and above?");
		}
		if (v.m_bScoreChanged == 0)
		{
			return;
		}
		int num2 = Mathf.Max(1, v.m_nGlobalRankNew);
		if (num2 < previousPlace)
		{
			previousPlace = num2;
			if (num2 < 101)
			{
				ReferenceMaster.SetDynamicText(bestPct, string.Format(LocalisationManager.GetTranslation(4898), num2));
				ReferenceMaster.SetDynamicText(bestType, s);
			}
		}
	}

	internal static float GetTimeTaken()
	{
		double num = (double)Machine.Active().LocalTime * 0.01;
		if (StatMaster.isMP)
		{
			return Math.Max(WinCondition.timeTaken, (float)num);
		}
		double val = (double)(Time.fixedTime - WinCondition.simStarted) + 0.039;
		double num2 = Math.Max(val, num);
		if (num2 <= 0.040001)
		{
			return 5999.99f;
		}
		return WinCondition.timeTaken = (float)num2;
	}

	internal void GetMachineScores(Machine m)
	{
		pctDmg = 1f;
		pctTime = 1f;
		pctBlocks = 1f;
		previousPlace = int.MaxValue;
		previousPct = float.MaxValue;
		timeElapsed = GetTimeTaken();
		isValid = false;
		bool flag = m.MachineType == MachineInfo.MachineType.Built || m.MachineType == MachineInfo.MachineType.Local;
		if (!string.IsNullOrEmpty(m.Author) && m.MachineType != MachineInfo.MachineType.Built && SteamManager.Initialized)
		{
			string text = SteamUser.GetSteamID().m_SteamID.ToString();
			flag = !(m.Author != text);
		}
		bool isVanilla = true;
		bool isIntact = true;
		isValid = IsValid(m, out isVanilla, out isIntact);
		if ((bool)objectivesParent)
		{
			objectivesParent.gameObject.SetActive(true);
		}
		bool cleared = false;
		bool flag2 = ObjectiveTrackerUI.LevelHasAchievement(WinCondition.Instance.myLevelIndex, out cleared);
		int objectiveState = LevelObjectiveFileManager.GetObjectiveState(WinCondition.Instance.myLevelIndex);
		bool flag3 = (objectiveState & 1) != 0;
		bool flag4 = (objectiveState & 2) != 0;
		if (isValid)
		{
			ToggleObjective(legitMachineDisplay, legitComplete, flag, flag3, !StatMaster.isMP);
			ToggleObjective(achievementDisplay, achievementComplete, justGotAchievement, cleared, !StatMaster.isMP && flag2);
			ToggleObjective(noDamageDisplay, noDamageComplete, isIntact, flag4, !StatMaster.isMP);
			LevelObjectiveFileManager.SetObjectiveState(WinCondition.Instance.myLevelIndex, flag || flag3, isIntact || flag4);
		}
		else
		{
			ToggleObjective(legitMachineDisplay, legitComplete, false, flag3, !StatMaster.isMP);
			ToggleObjective(achievementDisplay, achievementComplete, false, cleared, !StatMaster.isMP && flag2);
			ToggleObjective(noDamageDisplay, noDamageComplete, false, flag4, !StatMaster.isMP);
		}
		isValid = isValid && isVanilla;
		justGotAchievement = false;
	}

	internal static bool IsValid(Machine m, out bool isVanilla, out bool isIntact)
	{
		blockScore = 0;
		blockDmg = 0f;
		isVanilla = true;
		isIntact = true;
		foreach (BlockBehaviour simulationBlock in m.SimulationBlocks)
		{
			blockScore += GetBlockScore(simulationBlock);
			blockDmg += GetBlockDamage(simulationBlock, ref isIntact);
			if (isVanilla)
			{
				isVanilla = CheckVanilla(simulationBlock);
				if (!isVanilla)
				{
					Debug.LogWarning(simulationBlock.name + " is not vanilla");
				}
			}
		}
		return blockScore > 1 && IsValid(m);
	}

	internal static bool IsValid(Machine m)
	{
		bool flag = (!StatMaster.GodTools.HasBeenUsed || StatMaster.Bounding.Enabled) && m.isSimulating && m.BlocksCost > 0 && m.SimulationBlocks.Count > 1 && m.InitialSimCount == m.BuildingBlocks.Count && m.SimulationBlocks.Count <= m.BuildingBlocks.Count && m.SimulationBlocks[0].Prefab.Type == BlockType.StartingBlock && noErrorsDetected;
		if (flag)
		{
			flag = SingleInstanceFindOnly<WinScreen>.Instance.allModsValid && !SingleInstanceFindOnly<WinScreen>.Instance.level_or_prerequisites_changed_by_mod;
		}
		if (!flag)
		{
			Debug.Log("WSCHECK: " + ValidHash(m));
		}
		return flag;
	}

	internal static string ValidHash(Machine m)
	{
		bool flag = !StatMaster.GodTools.HasBeenUsed || StatMaster.Bounding.Enabled;
		return ((!flag) ? "C" : "0") + "\u200a" + ((!m.isSimulating) ? "S" : "0") + "\u200a" + ((m.BlocksCost <= 0) ? m.BlocksCost.ToString("X") : "0") + "\u200a" + ((m.SimulationBlocks.Count <= 1) ? m.SimulationBlocks.Count.ToString("X") : "0") + "\u200a" + ((m.InitialSimCount != m.BuildingBlocks.Count) ? m.InitialSimCount.ToString("X") : "0") + "\u200a" + ((m.SimulationBlocks.Count > m.BuildingBlocks.Count) ? m.BuildingBlocks.Count.ToString("X") : "0") + "\u200a" + ((m.SimulationBlocks[0].Prefab.Type != BlockType.StartingBlock) ? m.SimulationBlocks[0].BlockID.ToString("X") : "0") + "\u200a" + ((!noErrorsDetected) ? "E" : "0") + "\u200a" + ((!SingleInstanceFindOnly<WinScreen>.Instance.allModsValid) ? "M" : "0") + "\u200a" + ((!SingleInstanceFindOnly<WinScreen>.Instance.level_or_prerequisites_changed_by_mod) ? "0" : "X");
	}

	private void ToggleObjective(MeshRenderer icon, TextMesh text, bool toggle, bool previousToggle, bool available)
	{
		if (icon == null)
		{
			return;
		}
		if (objectiveColor.r == 0f)
		{
			objectiveColor = icon.sharedMaterial.GetColor("_TintColor");
			objectiveColor.a = 0.5f;
		}
		Color color = objectiveColor;
		text.text = LocalisationManager.GetTranslation((!available) ? 4914 : ((!toggle && !previousToggle) ? 4912 : 4913));
		if (!available)
		{
			icon.enabled = false;
		}
		else
		{
			icon.enabled = true;
			if (!toggle)
			{
				if (previousToggle)
				{
					color.a = 0.25f;
				}
				else
				{
					color = new Color(0.2f, 0.2f, 0.2f, color.a * 0.4f) + color * 0.1f;
				}
			}
		}
		icon.material.SetColor("_TintColor", color);
	}

	internal void SetAchievement(int i)
	{
		bool cleared = true;
		bool available = ObjectiveTrackerUI.LevelHasAchievement(WinCondition.Instance.myLevelIndex, out cleared);
		ToggleObjective(achievementDisplay, achievementComplete, true, false, available);
		justGotAchievement = true;
	}

	internal void GetMachineScore(Machine m)
	{
		blockScore = 0;
		foreach (BlockBehaviour simulationBlock in m.SimulationBlocks)
		{
			blockScore += GetBlockScore(simulationBlock);
		}
		if (blockScore < 1)
		{
			blockScore = 99999;
		}
	}

	internal static int GetBlockScore(BlockBehaviour block)
	{
		if (block == null)
		{
			Debug.LogError("[ERROR035]: Missing block");
			return 2;
		}
		switch (block.Prefab.Type)
		{
		case BlockType.StartingBlock:
			return block.BuildIndex + 1;
		case BlockType.Bomb:
			return 16;
		case BlockType.Flamethrower:
			return 12;
		case BlockType.Torch:
			return 12;
		case BlockType.FlameBall:
			return 10;
		case BlockType.Rocket:
			return 7;
		case BlockType.Pin:
		case BlockType.SqrBalloon:
		case BlockType.Harpoon:
			return 8;
		case BlockType.Cannon:
		case BlockType.ShrapnelCannon:
			return 7;
		case BlockType.FlyingBlock:
		case BlockType.SpinningBlock:
		case BlockType.Balloon:
			return 6;
		case BlockType.Drill:
		case BlockType.FlyWheel:
			return 5;
		case BlockType.BuildSurface:
			return 4;
		case BlockType.WingPanel:
		case BlockType.CogMediumPowered:
		case BlockType.MetalJaw:
		case BlockType.NauticalScrew:
			return 3;
		case BlockType.SteeringBlock:
		case BlockType.SteeringHinge:
			return 2;
		case BlockType.DoubleWoodenBlock:
		case BlockType.WoodenPole:
		case BlockType.Log:
			return (block as ShorteningBlock).Length;
		case BlockType.Wing:
		case BlockType.LargeWheel:
		case BlockType.LargeWheelUnpowered:
			return 2;
		case BlockType.MetalBall:
		case BlockType.Brace:
		case BlockType.Spring:
		case BlockType.CircularSaw:
		case BlockType.ArmorPlateSmall:
		case BlockType.Grabber:
		case BlockType.ArmorPlateRound:
		case BlockType.ArmorPlateLarge:
		case BlockType.Plow:
		case BlockType.HalfPipe:
		case BlockType.BouncyPad:
			return 2;
		case BlockType.Grenade:
		case BlockType.Sensor:
		case BlockType.Timer:
		case BlockType.Altimeter:
		case BlockType.LogicGate:
		case BlockType.Anglometer:
		case BlockType.Speedometer:
			return 2;
		case BlockType.MetalBlade:
		case BlockType.Hinge:
		case BlockType.Swivel:
		case BlockType.Spike:
		case BlockType.Boulder:
		case BlockType.BallJoint:
		case BlockType.RopeWinch:
		case BlockType.Vacuum:
		case BlockType.RopeMeasure:
		case BlockType.Axle:
		case BlockType.SkateWheel:
			return 1;
		case BlockType.CameraBlock:
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
			return 0;
		default:
			return (block.Prefab.hasHealthBar || block.CanBurn) ? 1 : 4;
		}
	}

	internal static float GetBlockDamage(BlockBehaviour block, ref bool intact)
	{
		if (block == null)
		{
			intact = false;
			return 4f;
		}
		float num = 0f;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (block.Prefab.hasHealthBar)
		{
			float num2 = block.BlockHealth.maxHealth - block.BlockHealth.health;
			if (num2 > 0f)
			{
				num += num2;
				switch (block.Prefab.Type)
				{
				case BlockType.Bomb:
					num = 0.001f;
					break;
				default:
					if (block.BlockHealth.health <= 0f)
					{
						flag = true;
					}
					break;
				case BlockType.Grenade:
				case BlockType.Rocket:
					break;
				}
			}
		}
		BlockType type;
		if (block.CanBurn && block.fireTag.hasController && !block.fireTag.igniteOnStart)
		{
			type = block.Prefab.Type;
			if (type != BlockType.Rocket)
			{
				float fireProgress = block.fireTag.fireControllerCode.fireProgress;
				num += fireProgress * 4f;
				if (fireProgress > 0f && block.fireTag.burning)
				{
					flag3 = true;
				}
			}
		}
		type = block.Prefab.Type;
		if (type != BlockType.Decoupler && type != BlockType.Rocket)
		{
			foreach (Joint item in block.iJointTo)
			{
				if (item == null || item.connectedBody == null)
				{
					num += 2f;
					flag2 = true;
				}
			}
		}
		intact = intact && !flag && !flag2 && !flag3;
		return num;
	}

	internal static bool CheckVanilla(BlockBehaviour block)
	{
		Vector3 localScale = block.transform.localScale;
		if (localScale.x > 1.0005f || localScale.y > 1.0005f || localScale.z > 1.0005f)
		{
			return false;
		}
		if (block is ModBlockBehaviourHandler)
		{
			return false;
		}
		if (block is BuildSurface && !(block as BuildSurface).IsCollidable)
		{
			return false;
		}
		foreach (MapperType mapperType in block.MapperTypes)
		{
			if (mapperType is MSlider)
			{
				MSlider mSlider = mapperType as MSlider;
				if (!mSlider.IsWithinRange)
				{
					return false;
				}
			}
		}
		return true;
	}
}
