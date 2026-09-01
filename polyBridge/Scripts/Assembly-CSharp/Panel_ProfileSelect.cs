using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ProfileSelect : MonoBehaviour
{
	[Header("Edit")]
	public Panel_ProfileEdit m_ProfileEdit;

	[Header("Header")]
	public Button m_Cancel;

	[Header("Slots")]
	public GameObject m_SlotsParent;

	public ProfileSlot[] m_Slots;

	[Header("Bottom Buttons")]
	public Button m_Edit;

	public Button m_Delete;

	[Header("Sprites")]
	public Sprite m_DefaultVehicleSprite;

	private Profile m_NewProfile;

	private readonly int NUM_COLUMNS = 4;

	private bool m_LoadInProgress;

	private void Start()
	{
		m_Cancel.onClick.AddListener(Close);
		m_Edit.onClick.AddListener(OnEdit);
		m_Delete.onClick.AddListener(OnDelete);
		ProfileSlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].SetCallback(OnClicked);
		}
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		PopulateSlots();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_PROFILE_EDIT"), GamepadButtonType.WEST, Localize.Get("UI_PROFILE_DELETE"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void Update()
	{
		ProcessInput();
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_MainMenuNew.Open();
	}

	public void ForceRefresh()
	{
		PopulateSlots();
	}

	public void HideElementsWhenEditActive(bool hide)
	{
		m_SlotsParent.SetActive(!hide);
		m_Edit.gameObject.SetActive(!hide);
		m_Delete.gameObject.SetActive(!hide);
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) || m_ProfileEdit.gameObject.activeInHierarchy)
		{
			return;
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !GameUI.PointerOver(typeof(Panel_ProfileSelect)))
		{
			Close();
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			Close();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			OnEdit();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			OnDelete();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
		{
			SelectNextProfile();
			ForceGamepadCursorToSelecctedSlot();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT))
		{
			SelectPrevProfile();
			ForceGamepadCursorToSelecctedSlot();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
		{
			SelectUpProfile();
			ForceGamepadCursorToSelecctedSlot();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN))
		{
			SelectDownProfile();
			ForceGamepadCursorToSelecctedSlot();
		}
	}

	private void OnAddProfile()
	{
		PopupInputField.Display(Localize.Get("UI_PROFILE_ENTER_NAME"), string.Empty, Profiles.NAME_CHARACTER_LIMIT, isFilename: true, isDirectory: true, AddProfileConfirm);
	}

	private void AddProfileConfirm(string profileName)
	{
		if (profileName != null)
		{
			profileName = profileName.Trim();
		}
		if (string.IsNullOrEmpty(profileName))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_EMPTY_PROFILE_NAME", profileName));
			return;
		}
		if (Utils.HasInvalidPathChars(profileName))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_PROFILENAME", profileName));
			return;
		}
		if (Profiles.Exists(profileName))
		{
			PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("UI_PROFILE_ALREADY_EXISTS"), profileName));
			return;
		}
		int createNewSlotIndex = GetCreateNewSlotIndex();
		m_NewProfile = CreateNewProfile(profileName, createNewSlotIndex, copySettingsFromActiveProfile: true);
		if (m_NewProfile == null)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_PROFILENAME", profileName));
			return;
		}
		Profiles.SetActiveProfile(m_NewProfile);
		Mods.LoadModsFromProfile(null);
		PopulateSlots();
	}

	private void OnDeclineSwitch()
	{
		PopulateSlots();
	}

	private void OnEdit()
	{
		int numCompletedLevels = Campaign.m_CampaignProgress.GetNumCompletedLevels();
		m_ProfileEdit.Open(Profiles.m_ActiveProfile, numCompletedLevels);
		InterfaceAudio.Play("ui_window_open");
		HideElementsWhenEditActive(hide: true);
	}

	private void OnDelete()
	{
		PopUpMessage.DisplayWarning(string.Format(Localize.Get("UI_PROFILE_DELETE_CONFIRM"), Profiles.GetActiveProfileName()), useYesNoLables: false, OnDeleteConfirm);
	}

	private int GetNumProfiles()
	{
		int num = 0;
		ProfileSlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			if (!string.IsNullOrEmpty(slots[i].m_ProfileName))
			{
				num++;
			}
		}
		return num;
	}

	private void OnDeleteConfirm()
	{
		string activeProfileName = Profiles.GetActiveProfileName();
		string firstDisabledProfile = GetFirstDisabledProfile();
		int slotIndex = Profiles.GetSlotIndex(activeProfileName);
		Profiles.Delete(activeProfileName);
		if (string.IsNullOrEmpty(firstDisabledProfile))
		{
			Profile profile = CreateNewProfile(Localize.Get("UI_DEFAULT_PROFILE_NAME"), slotIndex, copySettingsFromActiveProfile: true);
			if (profile != null)
			{
				firstDisabledProfile = profile.m_Name;
			}
		}
		if (string.IsNullOrEmpty(firstDisabledProfile))
		{
			Debug.LogWarning("Failed to find a profile to make active after deleting current profile");
			return;
		}
		MakeProfileActive(firstDisabledProfile);
		PopulateSlots();
	}

	private int GetProgressForProfile(string profileName)
	{
		Dictionary<string, CampaignLevelState> dictionary = CampaignProgressSerialize.LoadCampaignProgress(profileName, CampaignProgress.CAMPAIGN_PROGRESS_FILENAME);
		if (dictionary == null)
		{
			return 0;
		}
		int num = 0;
		foreach (KeyValuePair<string, CampaignLevelState> item in dictionary)
		{
			if (item.Value.m_Status == CampaignLevelStatus.PASS || item.Value.m_Status == CampaignLevelStatus.UNDER_BUDGET || item.Value.m_Status == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS)
			{
				num++;
			}
		}
		return num;
	}

	private Sprite GetVehicleSpriteForProfile(string profileName)
	{
		ProfileProxy profileProxy = Profiles.LoadProfileProxy(profileName);
		if (profileProxy == null)
		{
			return GameUI.m_Instance.m_DefaultAvatarSprite;
		}
		return Profiles.GetSpriteForVehicle(profileProxy.m_AvatarAddressable, profileProxy.m_AvatarSkin);
	}

	private void PopulateSlots()
	{
		for (int i = 0; i < m_Slots.Length; i++)
		{
			m_Slots[i].MakeEmptyCard(i);
		}
		List<string> profileNames = Profiles.GetProfileNames();
		if (profileNames == null || profileNames.Count == 0)
		{
			m_Slots[0].MakeCreateNewCard(0);
			return;
		}
		for (int j = 0; j < profileNames.Count; j++)
		{
			int slotIndex = Profiles.GetSlotIndex(profileNames[j]);
			if (slotIndex >= 0 && slotIndex < m_Slots.Length)
			{
				MakeDisabledCard(profileNames[j], slotIndex);
			}
		}
		for (int k = 0; k < m_Slots.Length; k++)
		{
			if (m_Slots[k].IsEmptyCard())
			{
				m_Slots[k].MakeCreateNewCard(k);
				break;
			}
		}
		string activeProfileName = Profiles.GetActiveProfileName();
		int slotIndex2 = Profiles.GetSlotIndex(activeProfileName);
		if (slotIndex2 >= 0 && slotIndex2 < m_Slots.Length)
		{
			MakeActiveCard(activeProfileName, slotIndex2);
		}
	}

	private string GetFirstDisabledProfile()
	{
		ProfileSlot[] slots = m_Slots;
		foreach (ProfileSlot profileSlot in slots)
		{
			if (profileSlot.IsDisabledCard())
			{
				return profileSlot.m_ProfileName;
			}
		}
		return string.Empty;
	}

	private int GetCreateNewSlotIndex()
	{
		for (int i = 0; i < m_Slots.Length; i++)
		{
			if (m_Slots[i].IsCreateNewCard())
			{
				return i;
			}
		}
		return -1;
	}

	private bool MakeProfileActive(string profileName)
	{
		Profile profile = Profiles.LoadProfile(profileName);
		if (profile == null)
		{
			return false;
		}
		Profiles.SetActiveProfile(profile);
		Profiles.LoadActiveProfileProgress();
		Mods.SetActiveModsFromProfile();
		Mods.LoadModsFromProfile(LoadModsFromProfileComplete);
		m_LoadInProgress = true;
		Profiles.m_ActiveProfile.Apply();
		return true;
	}

	private void LoadModsFromProfileComplete()
	{
		m_LoadInProgress = false;
	}

	private void OnClicked(int slotIndex)
	{
		if (m_LoadInProgress)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		if (m_Slots[slotIndex].IsCreateNewCard())
		{
			OnAddProfile();
			return;
		}
		string profileName = m_Slots[slotIndex].m_ProfileName;
		if (!string.IsNullOrEmpty(profileName) && Profiles.GetActiveProfileName().ToLower() != profileName.ToLower())
		{
			MakeProfileActive(profileName);
			PopulateSlots();
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private Profile CreateNewProfile(string profileName, int slotIndex, bool copySettingsFromActiveProfile)
	{
		Profile profile = new Profile();
		profile.Init(profileName);
		if (copySettingsFromActiveProfile)
		{
			profile.CopyAudioSettings(Profiles.m_ActiveProfile);
			profile.CopyGraphicsSettings(Profiles.m_ActiveProfile);
		}
		if (!profile.Write())
		{
			return null;
		}
		if (slotIndex != -1)
		{
			Profiles.WriteSlotIndex(profileName, slotIndex);
		}
		ProfileInfo.WriteActiveProfileName(profileName);
		CampaignWorlds.m_Instance.SetDefaultProgress();
		return profile;
	}

	private void SelectNextProfile()
	{
		for (int i = Profiles.GetSlotIndex(Profiles.GetActiveProfileName()) + 1; i < m_Slots.Length; i++)
		{
			if (m_Slots[i].m_DisabledCard.gameObject.activeInHierarchy)
			{
				OnClicked(i);
				return;
			}
		}
		InterfaceAudio.PlayErrorBeep();
	}

	private void SelectPrevProfile()
	{
		for (int num = Profiles.GetSlotIndex(Profiles.GetActiveProfileName()) - 1; num >= 0; num--)
		{
			if (m_Slots[num].m_DisabledCard.gameObject.activeInHierarchy)
			{
				OnClicked(num);
				return;
			}
		}
		InterfaceAudio.PlayErrorBeep();
	}

	private void SelectUpProfile()
	{
		int slotIndex = Profiles.GetSlotIndex(Profiles.GetActiveProfileName());
		if (slotIndex < NUM_COLUMNS)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		int num = slotIndex - NUM_COLUMNS;
		if (m_Slots[num].m_DisabledCard.gameObject.activeInHierarchy)
		{
			OnClicked(num);
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void SelectDownProfile()
	{
		int slotIndex = Profiles.GetSlotIndex(Profiles.GetActiveProfileName());
		if (slotIndex >= NUM_COLUMNS)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		int num = slotIndex + NUM_COLUMNS;
		if (m_Slots[num].m_DisabledCard.gameObject.activeInHierarchy)
		{
			OnClicked(num);
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void MakeActiveCard(string profileName, int slotIndex)
	{
		int progressForProfile = GetProgressForProfile(profileName);
		Sprite vehicleSpriteForProfile = GetVehicleSpriteForProfile(profileName);
		m_Slots[slotIndex].MakeActiveCard(slotIndex, vehicleSpriteForProfile, profileName, progressForProfile);
	}

	private void MakeDisabledCard(string profileName, int slotIndex)
	{
		int progressForProfile = GetProgressForProfile(profileName);
		Sprite vehicleSpriteForProfile = GetVehicleSpriteForProfile(profileName);
		m_Slots[slotIndex].MakeDisabledCard(slotIndex, vehicleSpriteForProfile, profileName, progressForProfile);
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		int slotIndex = Profiles.GetSlotIndex(Profiles.GetActiveProfileName());
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && slotIndex != -1)
		{
			ProfileSlot profileSlot = m_Slots[slotIndex];
			if (profileSlot != null)
			{
				GameInput.SetVirtualMousePosition(profileSlot.m_ActiveCard.transform.position);
			}
		}
	}
}
