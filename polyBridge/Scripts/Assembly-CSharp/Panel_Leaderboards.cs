using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_Leaderboards : MonoBehaviour
{
	public Panel_Leaderboard m_Leaderboard;

	[Header("Header")]
	public Button m_CancelButton;

	[Header("Footer")]
	public Button m_PrevButton;

	public Button m_NextButton;

	public GameObject m_LeaderboardDotPrefab;

	public Transform m_LevelDotsParent;

	public Color m_LevelDotColor;

	public Color m_LevelDotSelectedColor;

	[Header("Filters")]
	public TMP_Dropdown m_WorldNameDropdown;

	public TMP_Dropdown m_LevelDropdown;

	private Dictionary<string, string> m_LeaderboadLevelIdMap = new Dictionary<string, string>();

	private List<LeaderboardLevelDot> m_LevelDots = new List<LeaderboardLevelDot>();

	private string m_WorldFilter;

	private void Start()
	{
		m_CancelButton.onClick.AddListener(Close);
		m_NextButton.onClick.AddListener(OnNext);
		m_PrevButton.onClick.AddListener(OnPrev);
		m_WorldNameDropdown.onValueChanged.AddListener(delegate
		{
			OnWorldNameChanged();
		});
		m_WorldNameDropdown.alphaFadeSpeed = 0f;
		m_LevelDropdown.alphaFadeSpeed = 0f;
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		ProcessInput();
		if (!m_Leaderboard.DownloadInProgress())
		{
			MaybeRefreshLeaderboard();
		}
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void Open(string levelID)
	{
		CampaignWorld campaignWorld = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
		if (campaignWorld == null)
		{
			campaignWorld = CampaignWorlds.m_Instance.m_Worlds[0];
			levelID = "001";
		}
		base.gameObject.SetActive(value: true);
		m_Leaderboard.Init();
		CampaignWorldDropdown.Populate(m_WorldNameDropdown, includeAll: false);
		m_WorldFilter = campaignWorld.m_Id;
		CampaignWorldDropdown.Select(m_WorldNameDropdown, campaignWorld.m_Id);
		PopulateLevelDropdown(PopulateLevelDropdownComplete);
		int levelIndexExTutorials = campaignWorld.GetLevelIndexExTutorials(levelID);
		if (levelIndexExTutorials >= 0)
		{
			SetLevelFilterByIndex(levelIndexExTutorials);
		}
		m_Leaderboard.m_CurrentFilterState.Reset();
	}

	private void PopulateLevelDropdownComplete()
	{
		m_Leaderboard.DestroyAllSlots();
		if (m_LeaderboadLevelIdMap.Count != 0)
		{
			string text = GetSelectedLevelId();
			if (!SelectLevelWithId(text))
			{
				SetLevelFilterByIndex(0);
				text = m_LeaderboadLevelIdMap[m_LevelDropdown.options[0].text];
			}
			if (!string.IsNullOrEmpty(text))
			{
				SelectLevelWithId(text);
				SelectDot(text);
			}
		}
	}

	public void CloseImmediate()
	{
		base.gameObject.SetActive(value: false);
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
		InterfaceAudio.Play("ui_menu_cancel");
	}

	private void OnNext()
	{
		int num = m_LevelDropdown.value + 1;
		if (num >= m_LevelDropdown.options.Count)
		{
			num = 0;
		}
		SetLevelFilterByIndex(num);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnPrev()
	{
		int num = m_LevelDropdown.value - 1;
		if (num < 0)
		{
			num = m_LevelDropdown.options.Count - 1;
		}
		SetLevelFilterByIndex(num);
		InterfaceAudio.Play("ui_menu_select");
	}

	private bool SelectLevelWithId(string levelId)
	{
		foreach (KeyValuePair<string, string> item in m_LeaderboadLevelIdMap)
		{
			if (item.Value.Contains(levelId))
			{
				int num = FindLevelNameInDropdownThatContains(item.Key);
				if (num != -1)
				{
					SetLevelFilterByIndex(num);
				}
				return true;
			}
		}
		return false;
	}

	private string GetSelectedLevelId()
	{
		if (m_LeaderboadLevelIdMap.ContainsKey(m_LevelDropdown.captionText.text))
		{
			return m_LeaderboadLevelIdMap[m_LevelDropdown.captionText.text];
		}
		return string.Empty;
	}

	private void SetLevelFilterByIndex(int index)
	{
		m_LevelDropdown.value = index;
		m_LevelDropdown.captionText.text = m_LevelDropdown.options[index].text;
		string selectedLevelId = GetSelectedLevelId();
		if (!string.IsNullOrEmpty(selectedLevelId))
		{
			SelectDot(selectedLevelId);
		}
	}

	private void PopulateLevelDropdown(Action callback)
	{
		m_LeaderboadLevelIdMap.Clear();
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if ((m_WorldFilter != CampaignWorlds.WORLD_ID_ALL && campaignWorld.m_Id != m_WorldFilter) || (campaignWorld.IsSecretWorld() && !GameManager.IsSecretWorldUnlocked()))
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!campaignLevel.IsTutorial())
				{
					string fullNameFormatted = campaignLevel.GetFullNameFormatted();
					m_LeaderboadLevelIdMap.Add(fullNameFormatted, campaignLevel.m_Id);
				}
			}
		}
		PopulateLevelDropdownFromMap(m_LeaderboadLevelIdMap);
		callback?.Invoke();
	}

	private void PopulateLevelDropdownFromMap(Dictionary<string, string> dict)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, string> item in dict)
		{
			list.Add(item.Key);
		}
		list.Sort(SortByName);
		m_LevelDropdown.ClearOptions();
		m_LevelDropdown.AddOptions(list);
	}

	private int SortByName(string a, string b)
	{
		return a.CompareTo(b);
	}

	private int FindLevelNameInDropdownThatContains(string text)
	{
		foreach (TMP_Dropdown.OptionData option in m_LevelDropdown.options)
		{
			if (option.text.Contains(text))
			{
				return m_LevelDropdown.options.IndexOf(option);
			}
		}
		return -1;
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			{
				OnPrev();
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			{
				OnNext();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT))
			{
				ExecuteEvents.Execute(m_PrevButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
			{
				ExecuteEvents.Execute(m_NextButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
			{
				CycleToNextLeaderboardType();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
			{
				CycleToPrevLeaderboardType();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
			{
				CycleToNextFilter();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN))
			{
				CycleToPrevFilter();
			}
		}
	}

	private void CycleToNextLeaderboardType()
	{
		if (m_Leaderboard.m_ShowAllButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_ShowUnbreakingButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_ShowUnbreakingButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_ShowLowestStressButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_ShowLowestStressButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_ShowAllButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToPrevLeaderboardType()
	{
		if (m_Leaderboard.m_ShowAllButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_ShowLowestStressButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_ShowLowestStressButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_ShowUnbreakingButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_ShowUnbreakingButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_ShowAllButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToNextFilter()
	{
		if (m_Leaderboard.m_TopScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_AroundYouScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_AroundYouScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_FriendsScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_FriendsScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_TopScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToPrevFilter()
	{
		if (m_Leaderboard.m_TopScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_FriendsScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_AroundYouScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_TopScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_Leaderboard.m_FriendsScoresButton.IsOn())
		{
			ExecuteEvents.Execute(m_Leaderboard.m_AroundYouScoresButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void MaybeRefreshLeaderboard()
	{
		string text = m_LevelDropdown.captionText.text;
		string text2 = (m_LeaderboadLevelIdMap.ContainsKey(text) ? m_LeaderboadLevelIdMap[text] : string.Empty);
		if (text2 != m_Leaderboard.CurrentLevelId() || m_Leaderboard.FiltersChanged())
		{
			m_Leaderboard.OnRefresh(text2);
			SelectDot(text2);
		}
	}

	private void SelectDot(string levelID)
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
		if (worldWithLevelId == null)
		{
			return;
		}
		int num = 0;
		CampaignLevel[] levels = worldWithLevelId.m_Levels;
		for (int i = 0; i < levels.Length; i++)
		{
			if (!levels[i].IsTutorial())
			{
				num++;
			}
		}
		int num2 = num - m_LevelDots.Count;
		for (int j = 0; j < num2; j++)
		{
			LeaderboardLevelDot component = UnityEngine.Object.Instantiate(m_LeaderboardDotPrefab, m_LevelDotsParent).GetComponent<LeaderboardLevelDot>();
			m_LevelDots.Add(component);
		}
		foreach (LeaderboardLevelDot levelDot in m_LevelDots)
		{
			levelDot.gameObject.SetActive(value: false);
		}
		int num3 = 0;
		levels = worldWithLevelId.m_Levels;
		foreach (CampaignLevel campaignLevel in levels)
		{
			if (!campaignLevel.IsTutorial())
			{
				m_LevelDots[num3].gameObject.SetActive(value: true);
				m_LevelDots[num3].SetCallback(OnLevelDotClicked, campaignLevel.m_Id);
				m_LevelDots[num3].m_Image.color = ((m_LevelDots[num3].m_LevelID == levelID) ? m_LevelDotSelectedColor : m_LevelDotColor);
				num3++;
			}
		}
	}

	private void OnLevelDotClicked(string levelID)
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectLevelWithId(levelID);
		SelectDot(levelID);
	}

	private LeaderboardLevelDot GetLevelDot(string levelID)
	{
		foreach (LeaderboardLevelDot levelDot in m_LevelDots)
		{
			if (levelDot.m_LevelID == levelID)
			{
				return levelDot;
			}
		}
		return null;
	}

	private void OnWorldNameChanged()
	{
		m_WorldFilter = CampaignWorldDropdown.GetValue(m_WorldNameDropdown.captionText.text).m_Id;
		PopulateLevelDropdown(PopulateLevelDropdownComplete);
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_VERTICAL, Localize.Get("UI_CHANGE_FILTER"), GamepadButtonType.DPAD_HORIZONTAL, Localize.Get("UI_CHANGE_PAGE"));
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
