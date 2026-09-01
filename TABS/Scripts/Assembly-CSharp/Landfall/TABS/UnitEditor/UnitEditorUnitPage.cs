using GamepadUI.StateManager.Core;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorUnitPage : TabbedUIComponent
	{
		public GameObject exitButton;

		public GameObject backButton;

		public GameObject riderButton;

		[SerializeField]
		private GameObject saveTabButton;

		[SerializeField]
		private LocalizeText selectedMovementTypeName;

		[SerializeField]
		private LocalizeText selectedTargetTypeName;

		[SerializeField]
		private Image selectedVoiceBundleIcon;

		[SerializeField]
		private LocalizeText selectedVoiceBundleName;

		[SerializeField]
		private TextMeshProUGUI selectedVoiceBundlePitch;

		[SerializeField]
		private TextMeshProUGUI selectedVoiceBundleVolume;

		[Space]
		[SerializeField]
		private NavigableTMPTextInput nameField;

		private const string Exit = "BUTTON_EXIT";

		public Slider pitchSlider;

		public LocalizeText bundleName;

		public Image bundleIcon;

		public UnitEditorManager manager;

		private VoiceBundle voiceBundle;

		private GameObject previouslySelectedItem;

		private InputService inputService;

		private bool isRiderUnitPage;

		protected override void Awake()
		{
			base.Awake();
			inputService = ServiceLocator.GetService<InputService>();
		}

		public void UpdateMovementType(UnitEditorManager.MovementTypeWrapper movementTypeWrapper)
		{
			selectedMovementTypeName.LocaleID = movementTypeWrapper.DisplayName;
		}

		public void UpdateTargetingType(UnitEditorManager.TargetingTypeWrapper targetTypeWrapper)
		{
			selectedTargetTypeName.LocaleID = targetTypeWrapper.DisplayName;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			nameField.InputDisabled += OnUnitNameFieldDisabled;
			if (!(stateManager is UnitEditorUIManager unitEditorUIManager))
			{
				return;
			}
			UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
			if (!(gamepadGlyphs == null))
			{
				gamepadGlyphs.UpdateActionNames("Back", "BUTTON_EXIT", UnitEditorGamepadGlyphs.Position.Left);
				gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Middle);
				gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
				if (TABS != null && currentPage >= 0 && currentPage < TABS.Length)
				{
					TABS[currentPage].tabSubMenu.OnGainedFocus();
				}
			}
		}

		protected override void OnClose()
		{
			base.OnClose();
			nameField.InputDisabled -= OnUnitNameFieldDisabled;
			if (TABS != null && currentPage >= 0 && currentPage < TABS.Length)
			{
				TABS[currentPage].tabSubMenu.SetFocus(focus: false);
			}
		}

		protected override void OnSubMenuPressedBackButton(UISubMenu menu)
		{
			base.OnSubMenuPressedBackButton(menu);
			if (nameField.IsTextInputEnabled)
			{
				nameField.DisableTextInput();
			}
		}

		protected override void Update()
		{
			base.Update();
			if (UnitEditorManager.isTestingUnit)
			{
				return;
			}
			if (!nameField.IsTextInputEnabled && playerActions.m_back.WasPressed)
			{
				manager.DiscardUnit();
			}
			if (playerActions.m_editUnitName.WasPressed && !ServiceLocator.GetService<IPlatformUtils>().IsUIOpenOrLostFocus)
			{
				if (nameField.IsTextInputEnabled)
				{
					DisableUnitNameField();
				}
				else
				{
					EnableUnitNameField();
				}
			}
		}

		protected override bool IsTabValid(int index)
		{
			if (!base.IsTabValid(index))
			{
				return false;
			}
			if (!isRiderUnitPage)
			{
				return true;
			}
			return !(TABS[index].tabSubMenu is UnitEditorSaveScreen);
		}

		public override void SwitchTab(int index)
		{
			if (!nameField.IsTextInputEnabled)
			{
				base.SwitchTab(index);
			}
		}

		private void EnableUnitNameField()
		{
			EventSystem current = EventSystem.current;
			if (!(current == null))
			{
				GameObject currentSelectedGameObject = current.currentSelectedGameObject;
				if (currentSelectedGameObject != null && currentSelectedGameObject.transform.IsChildOf(TABS[currentPage].tabSubMenu.transform))
				{
					previouslySelectedItem = currentSelectedGameObject;
				}
				if (nameField != null)
				{
					nameField.EnableTextInput();
				}
			}
		}

		private void DisableUnitNameField()
		{
			nameField.DisableTextInput();
			OnUnitNameFieldDisabled();
		}

		private void OnUnitNameFieldDisabled()
		{
			if (EventSystem.current != null && previouslySelectedItem != null)
			{
				EventSystem.current.SetSelectedGameObject(previouslySelectedItem);
			}
		}

		public void EquipVoiceBundle(VoiceBundle bundle)
		{
			bundleName.LocaleID = bundle.Entity.Name;
			bundleIcon.sprite = bundle.Entity.SpriteIcon;
			voiceBundle = bundle;
		}

		public void PlaySound()
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(voiceBundle.VocalRef, 5f, Vector3.zero, SoundEffectVariations.MaterialType.Default, null, pitchSlider.value);
		}

		public void SetPitch(float pitch)
		{
			pitchSlider.value = pitch;
		}

		public float GetPitch()
		{
			return pitchSlider.value;
		}

		public void Setup(bool isSubUnit = false)
		{
			bool num = inputService.CurrentInputType == InputType.Controller;
			riderButton.SetActive(!isSubUnit);
			if (num)
			{
				exitButton.SetActive(value: false);
				backButton.SetActive(value: false);
			}
			else
			{
				exitButton.SetActive(!isSubUnit);
				backButton.SetActive(isSubUnit);
			}
		}

		public void SetSavePageVisable(bool v)
		{
			isRiderUnitPage = !v;
			saveTabButton.SetActive(v);
		}
	}
}
