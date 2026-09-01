using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ProfileEdit : MonoBehaviour
{
	[Header("Header")]
	public Image m_SelectedVehicleIcon;

	public TextMeshProUGUI m_ProgressText;

	public TMP_InputField m_InputField;

	public Button m_InputFieldGamepadButton;

	[Header("Icons")]
	public GameObject m_IconGrid;

	public GameObject m_IconPrefab;

	[Header("Swatches")]
	public Image m_SwatchGlow;

	public GameObject m_SwatchGrid;

	public ProfileAvatarSwatch[] m_Swatches;

	private ProfileAvatarSwatch m_SelectedSwatch;

	[Header("Bottom Buttons")]
	public Button m_OK;

	public Button m_Cancel;

	private string m_ProfileName;

	private string m_AvatarAddressable;

	private ProfileAvatarChoice m_UpdateProfileToChoice;

	private List<ProfileAvatarChoice> m_Choices = new List<ProfileAvatarChoice>();

	private bool m_InstantiatedChoices;

	private int m_ChoiceIndex;

	private readonly int NUM_COLUMNS = 9;

	private void Start()
	{
		m_OK.onClick.AddListener(OnOK);
		m_Cancel.onClick.AddListener(OnOK);
		m_InputFieldGamepadButton.onClick.AddListener(OnInputFieldGamepadButton);
		m_InputField.onValueChanged.AddListener(delegate
		{
			OnValueChanged();
		});
		m_InputField.characterLimit = Profiles.NAME_CHARACTER_LIMIT;
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.Save();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_SAVE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.Restore();
		m_InputField.onSelect.RemoveAllListeners();
	}

	private void Update()
	{
		ProcessInput();
		if (m_SelectedSwatch != null)
		{
			m_SwatchGlow.transform.position = m_SelectedSwatch.transform.position;
		}
	}

	public void Open(Profile profile, int progress)
	{
		base.gameObject.SetActive(value: true);
		Init(profile, progress);
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_ProfileSelect.HideElementsWhenEditActive(hide: false);
		InterfaceAudio.Play("ui_window_close");
	}

	public void UpdateForCurrentDevice()
	{
		m_InputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_InputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public bool InputFieldHasFocus()
	{
		if (m_InputField.gameObject.activeInHierarchy)
		{
			return m_InputField.isFocused;
		}
		return false;
	}

	private void Init(Profile profile, int progress)
	{
		if (!m_InstantiatedChoices)
		{
			InstantiatedChoices(profile);
			m_InstantiatedChoices = true;
		}
		m_ProgressText.text = progress.ToString();
		m_SelectedVehicleIcon.sprite = Profiles.GetSpriteForVehicle(profile.m_AvatarAddressable, profile.m_AvatarSkin);
		InitSwatchesForStub(profile.m_AvatarAddressable, profile.m_AvatarSkin);
		m_InputField.text = profile.m_Name;
		m_ProfileName = profile.m_Name;
		m_AvatarAddressable = profile.m_AvatarAddressable;
		m_UpdateProfileToChoice = null;
		ProfileAvatarChoice profileAvatarChoice = GetChoiceMatchingAddressable(profile.m_AvatarAddressable);
		if (profileAvatarChoice == null)
		{
			profileAvatarChoice = m_Choices[0];
		}
		HighlightChoice(profileAvatarChoice);
	}

	private void InstantiatedChoices(Profile profile)
	{
		int num = 0;
		VehicleStub[] stubs = VehicleStubs.m_Instance.m_Stubs;
		foreach (VehicleStub vehicleStub in stubs)
		{
			if (vehicleStub.m_Skins.Length != 0 && vehicleStub.m_Skins[0].m_Icon != null)
			{
				GameObject gameObject = Object.Instantiate(m_IconPrefab, m_IconGrid.transform);
				if (gameObject != null)
				{
					int num2 = Random.Range(0, vehicleStub.m_Skins.Length);
					string skinLocID = ((vehicleStub.m_PrefabAddress == profile.m_AvatarAddressable) ? profile.m_AvatarSkin : vehicleStub.m_Skins[num2].m_DisplayNameLocID);
					ProfileAvatarChoice component = gameObject.GetComponent<ProfileAvatarChoice>();
					m_Choices.Add(component);
					component.Init(num++, vehicleStub.m_PrefabAddress, skinLocID, OnChoiceClicked);
				}
			}
		}
	}

	private void OnChoiceClicked(int choiceIndex)
	{
		if (choiceIndex < 0 || choiceIndex >= m_Choices.Count)
		{
			Debug.LogWarning($"Choice index '{choiceIndex}' out of range");
			return;
		}
		ProfileAvatarChoice profileAvatarChoice = m_Choices[choiceIndex];
		m_SelectedVehicleIcon.sprite = profileAvatarChoice.GetSprite();
		m_AvatarAddressable = profileAvatarChoice.m_VehicleAddressable;
		m_UpdateProfileToChoice = profileAvatarChoice;
		InitSwatchesForStub(profileAvatarChoice.m_VehicleAddressable, profileAvatarChoice.m_VehicleSkinLocID);
		HighlightChoice(profileAvatarChoice);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void HighlightChoice(ProfileAvatarChoice highlightChoice)
	{
		foreach (ProfileAvatarChoice choice in m_Choices)
		{
			choice.Highlight(choice == highlightChoice);
			if (choice == highlightChoice)
			{
				m_ChoiceIndex = m_Choices.IndexOf(choice);
			}
		}
	}

	private ProfileAvatarChoice GetChoiceMatchingAddressable(string addressable)
	{
		foreach (ProfileAvatarChoice choice in m_Choices)
		{
			if (choice.m_VehicleAddressable == addressable)
			{
				return choice;
			}
		}
		return null;
	}

	private void OnOK()
	{
		string text = m_InputField.text.Trim();
		if (string.IsNullOrEmpty(text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_PROFILE_INVALID_NAME"));
			return;
		}
		if (m_ProfileName != text)
		{
			if (!Profiles.Move(m_ProfileName, text))
			{
				PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_PROFILE_RENAME_FAILED"));
				return;
			}
			Profiles.RenameActiveProfile(text);
		}
		if (m_UpdateProfileToChoice != null)
		{
			Profiles.m_ActiveProfile.m_AvatarAddressable = m_UpdateProfileToChoice.m_VehicleAddressable;
			Profiles.m_ActiveProfile.m_AvatarSkin = m_UpdateProfileToChoice.m_VehicleSkinLocID;
			Profiles.SaveActiveProfile();
			m_UpdateProfileToChoice = null;
		}
		GameUI.m_Instance.m_ProfileSelect.ForceRefresh();
		Close();
	}

	private void OnCancel()
	{
		Close();
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				OnOK();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT))
			{
				SelectNextIcon();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_LEFT))
			{
				SelectPrevIcon();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
			{
				SelectUpIcon();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
			{
				SelectDownIcon();
			}
		}
	}

	public void InitSwatchesForStub(string vehicleAddressable, string vehicleSkin)
	{
		VehicleStub stubByAddressable = VehicleStubs.GetStubByAddressable(vehicleAddressable);
		if (stubByAddressable == null)
		{
			Debug.LogWarning("Couldn't find vehicle stub for addressable '" + vehicleAddressable + "'");
			return;
		}
		int num = Mathf.Min(stubByAddressable.m_Skins.Length, m_Swatches.Length);
		m_SwatchGrid.SetActive(num > 0);
		m_SwatchGlow.gameObject.SetActive(num > 0);
		for (int i = 0; i < num; i++)
		{
			m_Swatches[i].gameObject.SetActive(value: true);
			m_Swatches[i].Init(stubByAddressable.m_Skins[i], SwatchSelected);
		}
		for (int j = num; j < m_Swatches.Length; j++)
		{
			m_Swatches[j].gameObject.SetActive(value: false);
		}
		for (int k = 0; k < num; k++)
		{
			if (m_Swatches[k].m_VehicleSkin.m_DisplayNameLocID == vehicleSkin)
			{
				SwatchSelected(m_Swatches[k]);
				return;
			}
		}
		SwatchSelected(m_Swatches[0]);
	}

	private void SwatchSelected(ProfileAvatarSwatch selectedSwatch)
	{
		ProfileAvatarSwatch[] swatches = m_Swatches;
		foreach (ProfileAvatarSwatch profileAvatarSwatch in swatches)
		{
			if (profileAvatarSwatch.m_VehicleSkin == null)
			{
				continue;
			}
			profileAvatarSwatch.Highlight(profileAvatarSwatch == selectedSwatch);
			if (!(profileAvatarSwatch == selectedSwatch))
			{
				continue;
			}
			m_SwatchGlow.color = new Color(selectedSwatch.m_VehicleSkin.GetColorForUI().r, selectedSwatch.m_VehicleSkin.GetColorForUI().g, selectedSwatch.m_VehicleSkin.GetColorForUI().b, 0.5882353f);
			m_SwatchGlow.transform.position = selectedSwatch.transform.position;
			m_SelectedSwatch = profileAvatarSwatch;
			m_SelectedVehicleIcon.sprite = profileAvatarSwatch.m_VehicleSkin.m_Icon;
			foreach (ProfileAvatarChoice choice in m_Choices)
			{
				if (choice.m_VehicleAddressable == m_AvatarAddressable)
				{
					choice.m_VehicleSkinLocID = profileAvatarSwatch.m_VehicleSkin.m_DisplayNameLocID;
					choice.m_Icon.sprite = profileAvatarSwatch.m_VehicleSkin.m_Icon;
					m_UpdateProfileToChoice = choice;
					break;
				}
			}
		}
	}

	private void SelectNextIcon()
	{
		if (m_ChoiceIndex < m_Choices.Count - 1)
		{
			OnChoiceClicked(m_ChoiceIndex + 1);
			ForceGamepadCursorToSelecctedSlot();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void SelectPrevIcon()
	{
		if (m_ChoiceIndex > 0)
		{
			OnChoiceClicked(m_ChoiceIndex - 1);
			ForceGamepadCursorToSelecctedSlot();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void SelectUpIcon()
	{
		if (m_ChoiceIndex - NUM_COLUMNS >= 0)
		{
			OnChoiceClicked(m_ChoiceIndex - NUM_COLUMNS);
			ForceGamepadCursorToSelecctedSlot();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void SelectDownIcon()
	{
		if (m_ChoiceIndex + NUM_COLUMNS < m_Choices.Count)
		{
			OnChoiceClicked(m_ChoiceIndex + NUM_COLUMNS);
			ForceGamepadCursorToSelecctedSlot();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_ChoiceIndex != -1)
		{
			ProfileAvatarChoice profileAvatarChoice = m_Choices[m_ChoiceIndex];
			if (profileAvatarChoice != null)
			{
				GameInput.SetVirtualMousePosition(profileAvatarChoice.m_Icon.transform.position);
			}
		}
	}

	private void OnInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_InputField.text, m_InputField.characterLimit, Localize.Get("UI_PROFILE_RENAME"), multiline: false, OnProfileNameEntered);
	}

	private void OnProfileNameEntered(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			m_InputField.text = Utils.RemoveInvalidCharsFromFilename(name);
			m_InputField.text = Utils.RemoveInvalidCharsFromPath(m_InputField.text);
		}
	}

	private void OnValueChanged()
	{
		m_InputField.text = Utils.RemoveInvalidCharsFromFilename(m_InputField.text);
		m_InputField.text = Utils.RemoveInvalidCharsFromPath(m_InputField.text);
	}
}
