using TFBGames;
using UIStateManager;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorBasePageUI : UnitEditorSubMenu
	{
		[SerializeField]
		private Slider pitchSlider;

		[SerializeField]
		private UnitEditorSelectableItem unitBase;

		[SerializeField]
		private UnitEditorSelectableVoiceItem voiceSelect;

		[SerializeField]
		private UnitEditorSelectableItem movementType;

		[SerializeField]
		private UnitEditorSelectableItem targettingType;

		[SerializeField]
		private UnitEditorSelectableItem rider;

		[SerializeField]
		private Transform contentParent;

		private const float PitchIncrement = 0.1f;

		private const string Remove = "BUTTON_REMOVE";

		private const string Play = "BUTTON_PLAY";

		private const string Edit = "BUTTON_EDIT";

		public override void Open()
		{
			base.Open();
			unitBase.Selected += OnItemSelected;
			voiceSelect.Selected += OnItemSelected;
			movementType.Selected += OnItemSelected;
			targettingType.Selected += OnItemSelected;
			rider.Selected += OnItemSelected;
			UpdateGlyphs();
		}

		public override void Close()
		{
			base.Close();
			unitBase.Selected -= OnItemSelected;
			voiceSelect.Selected -= OnItemSelected;
			movementType.Selected -= OnItemSelected;
			targettingType.Selected -= OnItemSelected;
			rider.Selected -= OnItemSelected;
			base.SelectedItem = null;
		}

		public override void Init(InterfaceStateManager interfaceStateManager)
		{
			base.Init(interfaceStateManager);
			if (rider is UnitEditorSelectableRiderItem unitEditorSelectableRiderItem)
			{
				unitEditorSelectableRiderItem.RiderUI.SetInterfaceStateManager(interfaceStateManager);
			}
		}

		protected override void OnItemSelected(UnitEditorSelectableItem item)
		{
			base.OnItemSelected(item);
			if (!(stateManager is UnitEditorUIManager unitEditorUIManager))
			{
				return;
			}
			UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
			if (gamepadGlyphs == null)
			{
				return;
			}
			gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Middle);
			gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
			UnitEditorSelectableItem selectedItem = base.SelectedItem;
			if ((object)selectedItem == null)
			{
				return;
			}
			if (!(selectedItem is UnitEditorSelectableRiderItem unitEditorSelectableRiderItem))
			{
				if (selectedItem is UnitEditorSelectableVoiceItem)
				{
					gamepadGlyphs.UpdateActionNames("Preview Unit Voice", "BUTTON_PLAY", UnitEditorGamepadGlyphs.Position.Middle);
				}
			}
			else if (unitEditorSelectableRiderItem.RiderUI.HasRider)
			{
				gamepadGlyphs.UpdateActionNames("Remove Rider", "BUTTON_REMOVE", UnitEditorGamepadGlyphs.Position.Middle);
				gamepadGlyphs.UpdateActionNames("Edit Rider", "BUTTON_EDIT", UnitEditorGamepadGlyphs.Position.Right);
			}
		}

		private void Start()
		{
			UIHelpers.CreateExplicitLinearNavigation(contentParent.GetSelectableChildren(), horizontal: false);
		}

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (!UnitEditorManager.isTestingUnit)
			{
				if (playerActions.m_previewUnitVoice.WasPressed && base.SelectedItem is UnitEditorSelectableVoiceItem unitEditorSelectableVoiceItem)
				{
					unitEditorSelectableVoiceItem.PreviewVoice();
				}
				if ((bool)playerActions.m_editRider && base.SelectedItem is UnitEditorSelectableRiderItem unitEditorSelectableRiderItem)
				{
					unitEditorSelectableRiderItem.EditRider();
				}
				if ((bool)playerActions.m_removeRider && base.SelectedItem is UnitEditorSelectableRiderItem unitEditorSelectableRiderItem2)
				{
					unitEditorSelectableRiderItem2.RemoveRider();
				}
			}
		}

		protected override void PerformIncreaseAction()
		{
			base.PerformIncreaseAction();
			if (base.SelectedItem is UnitEditorSelectableVoiceItem && pitchSlider != null)
			{
				pitchSlider.value += 0.1f;
			}
		}

		protected override void PerformDecreaseAction()
		{
			base.PerformDecreaseAction();
			if (base.SelectedItem is UnitEditorSelectableVoiceItem && pitchSlider != null)
			{
				pitchSlider.value -= 0.1f;
			}
		}

		private void UpdateGlyphs()
		{
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				if (!(gamepadGlyphs == null))
				{
					gamepadGlyphs.UpdateActionNames("Back", "BUTTON_EXIT", UnitEditorGamepadGlyphs.Position.Left);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Middle);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
				}
			}
		}
	}
}
