using System;
using Selectors;
using UnityEngine;

public class LevelSettingsScreen : MonoBehaviour
{
	[Serializable]
	public class RuleEntry
	{
		public string ruleName;

		public UIButtonExtended onButton;

		public UIButtonExtended offButton;

		public UIButtonExtended lockButton;
	}

	[Serializable]
	public class EnvEntry
	{
		public UIButtonExtended button;

		public LevelSettings.LevelEnvironment env;
	}

	public ValueHolderDefaulting waterHeight;

	public ValueHolderDefaulting minValue;

	public ValueHolderDefaulting maxValue;

	public EnvEntry[] envButtons;

	public RuleEntry[] ruleButtons;

	public UIButtonExtended voteButton;

	public UIButtonExtended curtainButton;

	public UIButtonExtended copyMachineButton;

	public UIButtonExtended excessPlayerButton;

	public UIButtonExtended hidePlayerLabelButton;

	public BlockLimitList limitList;

	public LevelMachineList machineList;

	public MusicSelectionUI musicSelection;

	public GameObject waterSpecific;

	public GameObject envVariation;

	public UIButtonExtended envIncrease;

	public UIButtonExtended envDecrease;

	public TextMesh envLabel;

	private LevelEditor levelEditor;

	private NetworkAuxAddPiece auxAddPiece;

	private string lastName;

	private int selectedType;

	private int maxTypes = 3;

	private bool isTriggeringChangeEvent;

	public void ApplySettings(LevelSettings settings)
	{
		UpdateEnv(settings.Environment);
		voteButton.ToggleBG(settings.UseVoting);
		curtainButton.ToggleBG(settings.CurtainMode);
		copyMachineButton.ToggleBG(settings.AllowCopyMachine);
		excessPlayerButton.ToggleBG(settings.AllowExcessPlayers);
		hidePlayerLabelButton.ToggleBG(settings.HidePlayerLabels);
		maxValue.SetText(settings.MaxPlayers);
		minValue.SetText(settings.MinPlayers);
		waterHeight.SetText((float)settings.WaterHeight * 0.1f);
		SetType(settings.EnvType);
		limitList.Refresh();
		machineList.Refresh();
		musicSelection.Refresh();
		for (int i = 0; i < ruleButtons.Length; i++)
		{
			RuleEntry ruleEntry = ruleButtons[i];
			LevelSettings.GodPowerSetting value;
			if (settings.GodPowerSettings.TryGetValue(ruleEntry.ruleName, out value))
			{
				UpdateRule(i, value.Enabled, value.Locked);
			}
		}
	}

	protected void Start()
	{
		auxAddPiece = NetworkAuxAddPiece.Instance;
	}

	protected void Awake()
	{
		voteButton.Click += ToggleVoteMode;
		curtainButton.Click += ToggleCurtainMode;
		copyMachineButton.Click += ToggleMachineCopyMode;
		excessPlayerButton.Click += ToggleExcessPlayers;
		hidePlayerLabelButton.Click += ToggleHideLabels;
		maxValue.ValueChanged += MaxPlayersChanged;
		minValue.ValueChanged += MinPlayersChanged;
		waterHeight.ValueChanged += WaterHeightChanged;
		envIncrease.Click += IncreaseType;
		envDecrease.Click += DecreaseType;
		machineList.Init(this);
		limitList.Init(this);
		musicSelection.Init(this);
		for (int i = 0; i < envButtons.Length; i++)
		{
			InitEnv(i);
		}
		for (int i = 0; i < ruleButtons.Length; i++)
		{
			InitRuleButtons(i);
		}
	}

	private void InitEnv(int index)
	{
		EnvEntry envButton = envButtons[index];
		envButton.button.Click += delegate
		{
			UpdateEnv(envButton.env);
			OnUpdateSettings();
		};
	}

	private void InitRuleButtons(int index)
	{
		RuleEntry ruleEntry = ruleButtons[index];
		ruleEntry.onButton.Click += delegate
		{
			ToggleRuleOn(index);
			OnUpdateSettings();
		};
		ruleEntry.offButton.Click += delegate
		{
			ToggleRuleOff(index);
			OnUpdateSettings();
		};
		if ((bool)ruleEntry.lockButton)
		{
			ruleEntry.lockButton.Click += delegate
			{
				ToggleRuleLock(index);
				OnUpdateSettings();
			};
		}
	}

	private void SetType(int i, bool update = false)
	{
		selectedType = i;
		levelEditor.Settings.EnvType = selectedType;
		envLabel.text = string.Empty + (selectedType + 1);
		if (update)
		{
			OnUpdateSettings();
		}
	}

	private void IncreaseType()
	{
		selectedType++;
		if (selectedType >= maxTypes)
		{
			selectedType = 0;
		}
		levelEditor.Settings.EnvType = selectedType;
		envLabel.text = string.Empty + (selectedType + 1);
		OnUpdateSettings();
	}

	private void DecreaseType()
	{
		selectedType--;
		if (selectedType < 0)
		{
			selectedType = maxTypes - 1;
		}
		levelEditor.Settings.EnvType = selectedType;
		envLabel.text = string.Empty + (selectedType + 1);
		OnUpdateSettings();
	}

	private void OnDisable()
	{
		StatMaster.SetInMenu(false);
	}

	protected void OnEnable()
	{
		levelEditor = LevelEditor.Instance;
		StatMaster.SetInMenu(true);
		ApplySettings(levelEditor.Settings);
	}

	private void ToggleRuleOn(int index)
	{
		RuleEntry ruleEntry = ruleButtons[index];
		ruleEntry.onButton.ToggleBG(true);
		ruleEntry.offButton.ToggleBG(false);
		LevelSettings.GodPowerSetting value;
		if (levelEditor.Settings.GodPowerSettings.TryGetValue(ruleEntry.ruleName, out value))
		{
			value.Enabled = true;
		}
	}

	private void ToggleRuleOff(int index)
	{
		RuleEntry ruleEntry = ruleButtons[index];
		ruleEntry.onButton.ToggleBG(false);
		ruleEntry.offButton.ToggleBG(true);
		LevelSettings.GodPowerSetting value;
		if (levelEditor.Settings.GodPowerSettings.TryGetValue(ruleEntry.ruleName, out value))
		{
			value.Enabled = false;
		}
	}

	private void ToggleRuleLock(int index)
	{
		RuleEntry ruleEntry = ruleButtons[index];
		if (!ruleEntry.lockButton.IsBGActive)
		{
			ruleEntry.lockButton.ToggleBG(true);
		}
		else
		{
			ruleEntry.lockButton.ToggleBG(false);
		}
		LevelSettings.GodPowerSetting value;
		if (levelEditor.Settings.GodPowerSettings.TryGetValue(ruleEntry.ruleName, out value))
		{
			value.Locked = ruleEntry.lockButton.IsBGActive;
		}
	}

	private void ToggleVoteMode()
	{
		LevelSettings settings = levelEditor.Settings;
		settings.UseVoting = !settings.UseVoting;
		voteButton.ToggleBG(settings.UseVoting);
		OnUpdateSettings();
	}

	private void ToggleMachineCopyMode()
	{
		LevelSettings settings = levelEditor.Settings;
		settings.AllowCopyMachine = !settings.AllowCopyMachine;
		copyMachineButton.ToggleBG(settings.AllowCopyMachine);
		OnUpdateSettings();
	}

	private void ToggleCurtainMode()
	{
		LevelSettings settings = levelEditor.Settings;
		settings.CurtainMode = !settings.CurtainMode;
		curtainButton.ToggleBG(settings.CurtainMode);
		OnUpdateSettings();
	}

	private void ToggleExcessPlayers()
	{
		LevelSettings settings = levelEditor.Settings;
		settings.AllowExcessPlayers = !settings.AllowExcessPlayers;
		excessPlayerButton.ToggleBG(settings.AllowExcessPlayers);
		if (settings.MaxPlayers == NetworkAuxAddPiece.BuildZoneCount)
		{
			settings.MaxPlayers = OptionsMaster.maxPlayers;
		}
		maxValue.SetValue(settings.MaxPlayers);
		MaxPlayersChanged(settings.MaxPlayers);
		OnUpdateSettings();
	}

	private void MaxPlayersChanged(float value)
	{
		LevelSettings settings = levelEditor.Settings;
		int num = ((value != (float)maxValue.defaultValue) ? Mathf.RoundToInt(value) : 8);
		int buildZoneCount = NetworkAuxAddPiece.BuildZoneCount;
		if (!settings.AllowExcessPlayers && num > buildZoneCount)
		{
			num = buildZoneCount;
		}
		settings.MaxPlayers = num;
		if (settings.MinPlayers > settings.MaxPlayers)
		{
			isTriggeringChangeEvent = true;
			minValue.SetText(settings.MaxPlayers);
			MinPlayersChanged(settings.MaxPlayers);
			isTriggeringChangeEvent = false;
		}
		if (!isTriggeringChangeEvent)
		{
			OnUpdateSettings();
		}
	}

	private void MinPlayersChanged(float value)
	{
		LevelSettings settings = levelEditor.Settings;
		int minPlayers = ((value == (float)minValue.defaultValue) ? 1 : Mathf.RoundToInt(value));
		settings.MinPlayers = minPlayers;
		if (settings.MinPlayers > settings.MaxPlayers)
		{
			isTriggeringChangeEvent = true;
			maxValue.SetText(settings.MinPlayers);
			MaxPlayersChanged(settings.MinPlayers);
			isTriggeringChangeEvent = false;
		}
		if (!isTriggeringChangeEvent)
		{
			OnUpdateSettings();
		}
	}

	private void WaterHeightChanged(float value)
	{
		float f = value * 10f;
		int num = ((value != (float)waterHeight.defaultValue) ? Mathf.RoundToInt(f) : 0);
		LevelSettings settings = levelEditor.Settings;
		settings.WaterHeight = num;
		OnUpdateSettings();
	}

	private void ToggleHideLabels()
	{
		LevelSettings settings = levelEditor.Settings;
		settings.HidePlayerLabels = !settings.HidePlayerLabels;
		hidePlayerLabelButton.ToggleBG(settings.HidePlayerLabels);
		OnUpdateSettings();
	}

	private void UpdateRule(int index, bool isOn, bool locked)
	{
		RuleEntry ruleEntry = ruleButtons[index];
		ruleEntry.onButton.ToggleBG(isOn);
		ruleEntry.offButton.ToggleBG(!isOn);
		if ((bool)ruleEntry.lockButton)
		{
			ruleEntry.lockButton.ToggleBG(locked);
		}
	}

	private void UpdateEnv(LevelSettings.LevelEnvironment env)
	{
		for (int i = 0; i < envButtons.Length; i++)
		{
			EnvEntry envEntry = envButtons[i];
			if (!(envEntry.button == null))
			{
				envEntry.button.ToggleBG(envEntry.env == env);
			}
		}
		levelEditor.Settings.Environment = env;
		waterSpecific.SetActive(env == LevelSettings.LevelEnvironment.Water);
		envVariation.SetActive(env == LevelSettings.LevelEnvironment.Water);
		if (levelEditor.Settings.EnvType != 0 && env != LevelSettings.LevelEnvironment.Water)
		{
			SetType(0, true);
		}
	}

	public void OnUpdateSettings()
	{
		LevelSettings settings = levelEditor.Settings;
		byte[] settingsBytes = levelEditor.EncodeSettings(settings);
		auxAddPiece.SendLevelSettings(settingsBytes);
	}
}
