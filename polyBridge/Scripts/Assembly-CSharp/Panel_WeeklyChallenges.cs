using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_WeeklyChallenges : MonoBehaviour
{
	public RectTransform m_Root;

	public Panel_Leaderboard m_Leaderboard;

	[Header("Header")]
	public TextMeshProUGUI m_Title;

	public TextMeshProUGUI m_CreatedByName;

	public Button m_RefreshButton;

	public Button m_CancelButton;

	[Header("Progress")]
	public GameObject m_SeasonProgressParent;

	public TextMeshProUGUI m_SeasonProgressText;

	public GameObject m_LevelProgressParent;

	public Image m_LevelProgressCompletedIcon;

	public Image m_LevelProgressUnderBudgetIcon;

	public Image m_LevelProgressUnderBudgetNoBreaksIcon;

	[Header("Body")]
	public TextMeshProUGUI m_NoLevelsInWeeklyChallenge;

	[Header("Bottom")]
	public GameObject m_BottomBar;

	public GameObject m_ChallengeEndedText;

	public Image m_CoutdownPie;

	public TextMeshProUGUI m_Countdown;

	public TextMeshProUGUI m_LocalTimeExpiration;

	public Button m_PlayButton;

	public GameObject m_PlayButtonTextAndIcon;

	public GameObject m_PlayButtonWaiting;

	[Header("Workshop Items")]
	public TextMeshProUGUI m_CurrentWeek;

	public TMP_Dropdown m_SeasonsDropdown;

	public TMP_Dropdown m_LevelDropdown;

	public Button m_NextButton;

	public Button m_PrevButton;

	public Button m_GalleryButton;

	public RawImage m_RawImage;

	[Header("Budget & Materials")]
	public GameObject m_BudgetAndMaterialsParent;

	public TextMeshProUGUI m_Budget;

	public GameObject[] m_MaterialIconObjects;

	[Header("Description")]
	public RectTransform m_DescriptionRectTransform;

	public TextMeshProUGUI m_Description;

	public ScrollRect m_DescriptionScrollRect;

	[Header("Cheats")]
	public GameObject m_UnlimitedBudgetLocked;

	public GameObject m_UnlimitedMaterialsLocked;

	public TwoStateButton m_UnlimitedBudgetButton;

	public TwoStateButton m_UnlimitedMaterialsButton;

	private int m_SelectedWeek;

	private int DownloadingCount;

	private WorkshopItem m_Item;

	private readonly float NUM_SECONDS_IN_WEEK = 604800f;

	private int m_WeekBeforeManualRefresh;

	private List<WeeklyChallengeStub> m_LevelDropDownStubs = new List<WeeklyChallengeStub>();

	private Action m_ForcePlayCallback;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_RefreshButton.onClick.AddListener(OnRefresh);
		m_NextButton.onClick.AddListener(OnNext);
		m_PrevButton.onClick.AddListener(OnPrev);
		m_PlayButton.onClick.AddListener(OnPlay);
		m_GalleryButton.onClick.AddListener(OnGallery);
		m_Countdown.gameObject.SetActive(value: false);
		m_NextButton.gameObject.SetActive(value: false);
		m_PrevButton.gameObject.SetActive(value: false);
		m_RefreshButton.gameObject.SetActive(value: false);
		m_NoLevelsInWeeklyChallenge.gameObject.SetActive(value: false);
		m_UnlimitedBudgetButton.m_Button.onClick.AddListener(OnUnlimitedBudgetToggle);
		m_UnlimitedMaterialsButton.m_Button.onClick.AddListener(OnUnlimitedMaterialToggle);
		m_LevelDropdown.alphaFadeSpeed = 0f;
		m_SeasonsDropdown.alphaFadeSpeed = 0f;
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		BridgeCheat.m_ForceUnlimitedBudget = false;
		BridgeCheat.m_ForceUnlimitedMaterial = false;
		m_UnlimitedBudgetButton.TurnOn(on: false);
		m_UnlimitedMaterialsButton.TurnOn(on: false);
		m_RawImage.gameObject.SetActive(value: false);
		ShowGamepadLegend();
	}

	public void UpdateForCurrentDevice()
	{
		m_Root.anchoredPosition = new Vector2(0f, (Game.IsRunningOnSteamDeck() || GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? 10 : 0);
	}

	private void PopulateSeasonsDropdown(int selectWeek)
	{
		int seasonForWeek = WeeklyChallenges.GetSeasonForWeek(WeeklyChallenges.GetCurrentWeek());
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		for (int i = 1; i <= seasonForWeek; i++)
		{
			string text = string.Format(Localize.Get("UI_SEASON_NUMBER"), i);
			list.Add(new TMP_Dropdown.OptionData(text));
		}
		m_SeasonsDropdown.options = list;
		int seasonForWeek2 = WeeklyChallenges.GetSeasonForWeek(selectWeek);
		m_SeasonsDropdown.SetValueWithoutNotify(seasonForWeek2 - 1);
	}

	private void PopulateLevelDropdown(int selectWeek)
	{
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		m_LevelDropDownStubs.Clear();
		int num = m_SeasonsDropdown.value + 1;
		int num2 = 1;
		foreach (WeeklyChallengeStub stub in WeeklyChallenges.m_Stubs)
		{
			if (WeeklyChallenges.GetSeasonForWeek(stub.m_Week) == num)
			{
				string text = string.Format(Localize.Get("UI_WEEK"), num2++);
				list.Add(new TMP_Dropdown.OptionData(text));
				m_LevelDropDownStubs.Add(stub);
			}
		}
		m_LevelDropdown.options = list;
		for (int i = 0; i < m_LevelDropDownStubs.Count; i++)
		{
			if (m_LevelDropDownStubs[i].m_Week == selectWeek)
			{
				m_LevelDropdown.value = i;
				break;
			}
		}
	}

	private void PopulateWeeklyThumbnail(int week)
	{
		WorkshopItem weeklyChallenge = WeeklyChallenges.GetWeeklyChallenge(week);
		if (weeklyChallenge != null)
		{
			m_Item = weeklyChallenge;
			PopulateItem(weeklyChallenge);
			UpdateHeader();
			UpdateBudgetMaterialsAndDescription();
			MarkWeeklyChallengesAsViewed(week);
		}
		else if (!GameManager.IsSteamOffline())
		{
			List<string> weeklyChallengeIdsForSeason = WeeklyChallenges.GetWeeklyChallengeIdsForSeason(WeeklyChallenges.GetSeasonForWeek(week));
			if (weeklyChallengeIdsForSeason.Count > 0)
			{
				DownloadingCount++;
				WeeklyChallenges.BatchDownloadWorkshopItems(weeklyChallengeIdsForSeason, week, OnDownloadFeaturedComplete);
			}
		}
	}

	private void MarkWeeklyChallengesAsViewed(int week)
	{
		WorkshopItem weeklyChallenge = WeeklyChallenges.GetWeeklyChallenge(week);
		bool flag = false;
		if (weeklyChallenge != null && !Profiles.m_ActiveProfile.m_OpenedWeeklyChallengeItemIds.Contains(weeklyChallenge.GetId()))
		{
			Profiles.m_ActiveProfile.m_OpenedWeeklyChallengeItemIds.Add(weeklyChallenge.GetId());
			flag = true;
		}
		if (flag)
		{
			Profiles.SaveActiveProfile();
		}
	}

	private void OnDownloadFeaturedComplete(bool success, int week)
	{
		DownloadingCount--;
		if (success)
		{
			m_Item = WeeklyChallenges.GetWeeklyChallenge(week);
			if (m_Item != null)
			{
				PopulateItem(m_Item);
				UpdateHeader();
				UpdateBudgetMaterialsAndDescription();
				MarkWeeklyChallengesAsViewed(week);
			}
		}
	}

	private void PopulateItem(WorkshopItem item)
	{
		item?.DownloadPreviewFromSteam(OnDownloadPreviewComplete);
	}

	private void OnDownloadPreviewComplete(WorkshopItem item)
	{
		m_RawImage.texture = item.m_PreviewTexture;
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			base.transform.localScale = Vector3.zero;
		}
		else
		{
			base.transform.localScale = Vector3.one;
		}
		MaybeSwitchSeason();
		MaybeSwitchLevel();
		UpdateHeader();
		UpdateProgressVisibility();
		UpdateCheatToggles();
		if (string.IsNullOrEmpty(m_CreatedByName.text))
		{
			UpdateHeader();
		}
		ProcessInput();
		if (!m_Leaderboard.DownloadInProgress())
		{
			MaybeRefreshLeaderboard();
		}
		m_Countdown.gameObject.SetActive(value: true);
		if (m_SelectedWeek != WeeklyChallenges.GetCurrentWeek())
		{
			m_BottomBar.SetActive(value: false);
			m_ChallengeEndedText.SetActive(value: true);
		}
		else
		{
			m_BottomBar.SetActive(value: true);
			m_ChallengeEndedText.SetActive(value: false);
			UpdateCountdownText();
			UpdateCountdownPie();
		}
		UpdateBody();
		m_NextButton.interactable = m_SelectedWeek < WeeklyChallenges.GetCurrentWeek();
		m_PrevButton.interactable = m_SelectedWeek > 1;
		WorkshopItem weeklyChallenge = WeeklyChallenges.GetWeeklyChallenge(WeeklyChallenges.GetCurrentWeek());
		if (weeklyChallenge != null && weeklyChallenge.m_PreviewTexture != null)
		{
			m_RawImage.gameObject.SetActive(value: true);
		}
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void Open(string itemId)
	{
		base.gameObject.SetActive(value: true);
		m_Item = null;
		m_ForcePlayCallback = null;
		WeeklyChallengeStub weeklyChallengeStubByItemId = WeeklyChallenges.GetWeeklyChallengeStubByItemId(itemId);
		if (weeklyChallengeStubByItemId != null)
		{
			m_SelectedWeek = weeklyChallengeStubByItemId.m_Week;
		}
		else
		{
			m_SelectedWeek = WeeklyChallenges.GetCurrentWeek();
		}
		PopulateSeasonsDropdown(m_SelectedWeek);
		PopulateLevelDropdown(m_SelectedWeek);
		PopulateWeeklyThumbnail(m_SelectedWeek);
		SetLocalTimeExpiration();
		UpdateProgress();
		UpdateCheatToggles();
		m_Item = WeeklyChallenges.GetWeeklyChallenge(m_SelectedWeek);
		if (m_Item != null)
		{
			UpdateHeader();
			UpdateBudgetMaterialsAndDescription();
		}
		m_Countdown.gameObject.SetActive(value: false);
		m_DescriptionScrollRect.verticalNormalizedPosition = 1f;
		ShowDownloading(on: false);
		m_NextButton.gameObject.SetActive(WeeklyChallenges.m_Stubs.Count > 0);
		m_PrevButton.gameObject.SetActive(WeeklyChallenges.m_Stubs.Count > 0);
		m_Leaderboard.Init();
		m_Leaderboard.OnRefresh(itemId);
		OnRefresh();
	}

	public void ForcePlay(Action callback)
	{
		m_ForcePlayCallback = callback;
		OnPlay();
	}

	private void UpdateHeader()
	{
		if (m_Item != null)
		{
			m_Title.text = m_Item.GetTitle();
			string originalCreatorDisplayName = WeeklyChallenges.GetOriginalCreatorDisplayName(m_Item.GetId());
			if (string.IsNullOrEmpty(originalCreatorDisplayName))
			{
				m_CreatedByName.text = string.Empty;
			}
			else
			{
				m_CreatedByName.text = Localize.Get("UI_WORKSHOP_BY", originalCreatorDisplayName);
			}
		}
	}

	private void UpdateBudgetMaterialsAndDescription()
	{
		m_BudgetAndMaterialsParent.SetActive(value: true);
		string description = m_Item.GetDescription();
		PopulateBudget(WeeklyChallenges.GetBudgetFromEncodedDescription(description));
		PopulateDescription(WeeklyChallenges.GetDescriptionFromEncodedDescription(description));
		string metaDataFromEncodedDescription = WeeklyChallenges.GetMetaDataFromEncodedDescription(description);
		PopulateMaterials(WeeklyChallenges.GetMaterialCountsFromEncodedDescription(description), WorkshopMetaData.IsLegacy(metaDataFromEncodedDescription));
	}

	private void PopulateBudget(int budget)
	{
		m_Budget.text = Utils.FormatCash(budget);
	}

	private void PopulateMaterials(List<int> materialCounts, bool legacyMetaData)
	{
		for (int i = 0; i < m_MaterialIconObjects.Length; i++)
		{
			m_MaterialIconObjects[i].SetActive(materialCounts != null && i < materialCounts.Count && materialCounts[i] > 0);
			WorkshopMetaData.SetMaterialCountForIcon(m_MaterialIconObjects[i], (!legacyMetaData) ? materialCounts[i] : 0);
		}
	}

	private void PopulateDescription(string description)
	{
		GameUI.SetAndEnableText(m_Description, (!string.IsNullOrEmpty(description)) ? description.Replace("\\n", "\n") : string.Empty);
	}

	private void Close()
	{
		if (GameUI.m_Instance.m_Status.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_Status.Close();
		}
		base.gameObject.SetActive(value: false);
		if (GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			GameUI.m_Instance.m_MainMenuNew.Open();
		}
	}

	public void CloseAfterPlay()
	{
		base.gameObject.SetActive(value: false);
	}

	public bool IsLeaderboardShowing()
	{
		if (m_Leaderboard.DownloadInProgress() || m_Leaderboard.m_FailedToLoadText.gameObject.activeInHierarchy)
		{
			return false;
		}
		return true;
	}

	public int GetSelectedSeason()
	{
		return m_SeasonsDropdown.value + 1;
	}

	private void UpdateCountdownText()
	{
		m_Countdown.text = GetCoutdownText();
	}

	private void UpdateCountdownPie()
	{
		TimeSpan timeSpan = GetNextMonday() - DateTime.UtcNow;
		m_CoutdownPie.fillAmount = Mathf.Clamp01((NUM_SECONDS_IN_WEEK - (float)timeSpan.TotalSeconds) / NUM_SECONDS_IN_WEEK);
	}

	private void UpdateBody()
	{
		int weekWithinSeason = WeeklyChallenges.GetWeekWithinSeason(m_SelectedWeek);
		m_CurrentWeek.text = string.Format(Localize.Get("UI_WEEK"), weekWithinSeason);
	}

	private void MaybeSwitchSeason()
	{
		int seasonForWeek = WeeklyChallenges.GetSeasonForWeek(m_SelectedWeek);
		if (m_SeasonsDropdown.value + 1 != seasonForWeek)
		{
			m_SelectedWeek = 10 * m_SeasonsDropdown.value + 1;
			PopulateLevelDropdown(m_SelectedWeek);
			PopulateWeeklyThumbnail(m_SelectedWeek);
			UpdateProgress();
		}
	}

	private void MaybeSwitchLevel()
	{
		if (m_LevelDropdown.value >= 0 && m_LevelDropdown.value < m_LevelDropDownStubs.Count)
		{
			int week = m_LevelDropDownStubs[m_LevelDropdown.value].m_Week;
			if (week != m_SelectedWeek)
			{
				m_SelectedWeek = week;
				PopulateWeeklyThumbnail(m_SelectedWeek);
				UpdateProgress();
			}
		}
	}

	private void UpdateProgressVisibility()
	{
		bool flag = m_SeasonsDropdown.transform.Find("Dropdown List");
		bool flag2 = m_LevelDropdown.transform.Find("Dropdown List");
		WeeklyChallengeStub weeklyChallengeStub = WeeklyChallenges.GetWeeklyChallengeStub(m_SelectedWeek);
		m_SeasonProgressParent.gameObject.SetActive(weeklyChallengeStub != null && !flag);
		m_LevelProgressParent.gameObject.SetActive(weeklyChallengeStub != null && !flag2);
	}

	private void UpdateProgress()
	{
		WeeklyChallengeStub weeklyChallengeStub = WeeklyChallenges.GetWeeklyChallengeStub(m_SelectedWeek);
		if (weeklyChallengeStub != null)
		{
			int seasonForWeek = WeeklyChallenges.GetSeasonForWeek(m_SelectedWeek);
			int numberPassedWeeksInSeason = WeeklyChallenges.GetNumberPassedWeeksInSeason(seasonForWeek);
			int numberWeeksInSeason = WeeklyChallenges.GetNumberWeeksInSeason(seasonForWeek);
			m_SeasonProgressText.text = $"{numberPassedWeeksInSeason} / {numberWeeksInSeason}";
			m_LevelProgressCompletedIcon.gameObject.SetActive(WeeklyChallengesProgress.HasCompletedLevel(weeklyChallengeStub.m_ItemID));
			m_LevelProgressUnderBudgetIcon.gameObject.SetActive(WeeklyChallengesProgress.HasCompletedLevelUnderBudget(weeklyChallengeStub.m_ItemID, WeeklyChallenges.GetBudget(weeklyChallengeStub.m_ItemID)));
			m_LevelProgressUnderBudgetNoBreaksIcon.gameObject.SetActive(WeeklyChallengesProgress.HasCompletedLevelUnderBudgetNoBreaks(weeklyChallengeStub.m_ItemID, WeeklyChallenges.GetBudget(weeklyChallengeStub.m_ItemID)));
		}
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
		{
			CycleToNextLeaderboardType();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			CycleToPrevLeaderboardType();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
		{
			if (m_NextButton.interactable)
			{
				ExecuteEvents.Execute(m_NextButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT))
		{
			if (m_PrevButton.interactable)
			{
				ExecuteEvents.Execute(m_PrevButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
		{
			CycleToNextFilter();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN))
		{
			CycleToPrevFilter();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			ExecuteEvents.Execute(m_PlayButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
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

	private string GetCoutdownText()
	{
		TimeSpan timeSpan = GetNextMonday() - DateTime.UtcNow;
		if (timeSpan.Days > 0)
		{
			return $"<#FFFFFF>{timeSpan.Days:D1}<#F2A908>d <#FFFFFF>{timeSpan.Hours:D1}<#F2A908>h <#FFFFFF>{timeSpan.Minutes:D1}<#F2A908>m";
		}
		if (timeSpan.Hours > 0)
		{
			return $"<#FFFFFF>{timeSpan.Hours:D1}<#F2A908>h <#FFFFFF>{timeSpan.Minutes:D1}<#F2A908>m";
		}
		if (timeSpan.Minutes > 0)
		{
			return $"<#FFFFFF>{timeSpan.Minutes:D1}<#F2A908>m <#FFFFFF>{Mathf.Max(0, timeSpan.Seconds):D1}<#F2A908>s";
		}
		return $"<#FFFFFF>{Mathf.Max(0, timeSpan.Seconds):D1}<#F2A908>s";
	}

	private DateTime GetNextMonday()
	{
		DateTime utcNow = DateTime.UtcNow;
		DateTime dateTime = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);
		int num = 0;
		if (utcNow.Year == 2023 && utcNow.Month == 7 && utcNow.Day < 10)
		{
			num = 7;
		}
		return dateTime.DayOfWeek switch
		{
			DayOfWeek.Monday => dateTime.AddDays(7 + num), 
			DayOfWeek.Tuesday => dateTime.AddDays(6 + num), 
			DayOfWeek.Wednesday => dateTime.AddDays(5 + num), 
			DayOfWeek.Thursday => dateTime.AddDays(4 + num), 
			DayOfWeek.Friday => dateTime.AddDays(3 + num), 
			DayOfWeek.Saturday => dateTime.AddDays(2 + num), 
			DayOfWeek.Sunday => dateTime.AddDays(1 + num), 
			_ => dateTime, 
		};
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnRefresh()
	{
		m_WeekBeforeManualRefresh = WeeklyChallenges.GetCurrentWeek();
		WeeklyChallenges.DownloadStubsAsync(DownloadStubsCompleteCallback);
	}

	private void DownloadStubsCompleteCallback(bool success)
	{
		if (success)
		{
			if (WeeklyChallenges.GetCurrentWeek() != m_WeekBeforeManualRefresh)
			{
				base.gameObject.SetActive(value: false);
				Open(string.Empty);
			}
		}
		else
		{
			GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_FAILED_WEEKLY_CHALLENGE_REFRESH"));
		}
	}

	private void OnNext()
	{
		if (m_LevelDropdown.value < m_LevelDropdown.options.Count - 1)
		{
			m_LevelDropdown.value++;
			m_SelectedWeek = m_LevelDropDownStubs[m_LevelDropdown.value].m_Week;
			PopulateWeeklyThumbnail(m_SelectedWeek);
			UpdateProgress();
			InterfaceAudio.Play("ui_menu_select");
			return;
		}
		int num = m_SeasonsDropdown.value + 1;
		if (num == WeeklyChallenges.GetSeasonForWeek(WeeklyChallenges.GetCurrentWeek()))
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		num++;
		m_SelectedWeek = 10 * (num - 1) + 1;
		m_SeasonsDropdown.SetValueWithoutNotify(num - 1);
		PopulateLevelDropdown(m_SelectedWeek);
		PopulateWeeklyThumbnail(m_SelectedWeek);
		UpdateProgress();
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnPrev()
	{
		if (m_LevelDropdown.value > 0)
		{
			m_LevelDropdown.value--;
			m_SelectedWeek = m_LevelDropDownStubs[m_LevelDropdown.value].m_Week;
			PopulateWeeklyThumbnail(m_SelectedWeek);
			UpdateProgress();
			InterfaceAudio.Play("ui_menu_select");
			return;
		}
		int num = m_SeasonsDropdown.value + 1;
		if (num == 1)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		num--;
		m_SelectedWeek = 10 * (num - 1) + 10;
		m_SeasonsDropdown.SetValueWithoutNotify(num - 1);
		PopulateLevelDropdown(m_SelectedWeek);
		PopulateWeeklyThumbnail(m_SelectedWeek);
		UpdateProgress();
		InterfaceAudio.Play("ui_menu_select");
	}

	private bool AllWeeklyThumbnailsHavePreviewLoaded()
	{
		WorkshopItem weeklyChallenge = WeeklyChallenges.GetWeeklyChallenge(WeeklyChallenges.GetCurrentWeek());
		if (weeklyChallenge != null && weeklyChallenge.m_LoadingPreviewTexture)
		{
			return false;
		}
		return true;
	}

	private void OnGallery()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
		if (!GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_Gallery.OpenWorkshopItem(m_Item.GetTitle(), m_Item.GetId());
			GameUI.m_Instance.m_Gallery.m_ReturnToWeekliesItemID = m_Item.GetId();
		}
	}

	private void OnPlay()
	{
		if (m_SelectedWeek == 11 && VehicleStubs.GetStubByAddressable("AlienCatcher") == null)
		{
			PopUpMessage.DisplayWarningOkOnly("This level requries game version 1.2.6 or higher.");
			return;
		}
		if (IsDownloading())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		if (Utils.FileExists(m_Item.GetLevelLayoutPathAndFilename()))
		{
			Play(success: true);
			return;
		}
		ShowDownloading(on: true);
		m_Item.DownloadFromSteam(Play);
	}

	private void Play(bool success)
	{
		ShowDownloading(on: false);
		if (!success)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_DOWNLOAD_FAIL"));
			return;
		}
		List<string> inactiveModsInLayout = Mods.GetInactiveModsInLayout(m_Item.GetLevelLayoutPathAndFilename());
		if (inactiveModsInLayout.Count > 0)
		{
			GameUI.m_Instance.m_ModsRequiredPopup.Open(inactiveModsInLayout, null, DoPlayAfterModCheck);
		}
		else
		{
			DoPlayAfterModCheck(null);
		}
	}

	private void DoPlayAfterModCheck(FileSlot slot)
	{
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = false;
		BridgeCheat.m_ForceUnlimitedMaterial = false;
		GameStatePreloadingAssets.PreloadLevel(m_Item.GetLevelLayoutPathAndFilename(), slot, PreloadOpenLevelCallback);
	}

	private void PreloadOpenLevelCallback(string layoutPath, FileSlot slot)
	{
		GameUI.m_Instance.m_WeeklyChallenges.CloseAfterPlay();
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = m_UnlimitedBudgetButton.IsOn();
		BridgeCheat.m_ForceUnlimitedMaterial = m_UnlimitedMaterialsButton.IsOn();
		Workshop.PlayLevel(m_Item, layoutPath, GameSubMode.NONE);
		m_ForcePlayCallback?.Invoke();
	}

	private void ShowDownloading(bool on)
	{
		m_PlayButtonTextAndIcon.SetActive(!on);
		m_PlayButtonWaiting.SetActive(on);
	}

	private bool IsDownloading()
	{
		return m_PlayButtonWaiting.activeInHierarchy;
	}

	private void MaybeRefreshLeaderboard()
	{
		if (m_Item != null)
		{
			string id = m_Item.GetId();
			if (id != m_Leaderboard.CurrentLevelId() || m_Leaderboard.FiltersChanged())
			{
				m_Leaderboard.OnRefresh(id);
			}
		}
	}

	private void SetLocalTimeExpiration()
	{
		DateTime dateTime = GetNextMonday().ToLocalTime();
		m_LocalTimeExpiration.text = Utils.FormatShortDate(dateTime) + " " + dateTime.ToShortTimeString();
	}

	private void UpdateCheatToggles()
	{
		WeeklyChallengeStub weeklyChallengeStub = WeeklyChallenges.GetWeeklyChallengeStub(m_SelectedWeek);
		if (weeklyChallengeStub != null)
		{
			bool flag = WeeklyChallengesProgress.HasCompletedLevelUnderBudget(weeklyChallengeStub.m_ItemID, WeeklyChallenges.GetBudget(weeklyChallengeStub.m_ItemID));
			bool flag2 = WeeklyChallengesProgress.HasCompletedLevelUnderBudgetNoBreaks(weeklyChallengeStub.m_ItemID, WeeklyChallenges.GetBudget(weeklyChallengeStub.m_ItemID));
			m_UnlimitedBudgetLocked.SetActive(!flag);
			m_UnlimitedMaterialsLocked.SetActive(!flag2);
			m_UnlimitedBudgetButton.gameObject.SetActive(!m_UnlimitedBudgetLocked.activeSelf);
			m_UnlimitedMaterialsButton.gameObject.SetActive(!m_UnlimitedMaterialsLocked.activeSelf);
		}
	}

	private void OnUnlimitedBudgetToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedBudgetButton.Toggle();
		m_UnlimitedBudgetButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedBudgetButton.IsOn() ? "UI_UNLIMITED_BUDGET_ON" : "UI_UNLIMITED_BUDGET_OFF");
	}

	private void OnUnlimitedMaterialToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedMaterialsButton.Toggle();
		m_UnlimitedMaterialsButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedMaterialsButton.IsOn() ? "UI_UNLIMITED_MATERIAL_ON" : "UI_UNLIMITED_MATERIAL_OFF");
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_VERTICAL, Localize.Get("UI_CHANGE_FILTER"), GamepadButtonType.DPAD_HORIZONTAL, Localize.Get("UI_CHANGE_LEVEL"));
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_PLAY_LEVEL"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
