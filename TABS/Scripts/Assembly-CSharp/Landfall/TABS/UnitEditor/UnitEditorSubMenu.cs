using GamepadUI.StateManager.Core;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorSubMenu : UISubMenu
	{
		private bool waitedForFrame;

		private bool updateGlyphs = true;

		protected const string Exit = "BUTTON_EXIT";

		private const float IncrementHorizontalAxisThreshold = 0.2f;

		private const float VerticalMovementThreshold = 0.2f;

		protected UnitEditorSelectableItem SelectedItem { get; set; }

		public override void Open()
		{
			base.Open();
			if (updateGlyphs && stateManager is UnitEditorUIManager unitEditorUIManager)
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

		public void SetUpdateGlyphs(bool shouldUpdate)
		{
			updateGlyphs = shouldUpdate;
		}

		protected virtual void OnItemSelected(UnitEditorSelectableItem item)
		{
			SelectedItem = item;
		}

		protected virtual void OnItemDeselected(UnitEditorSelectableItem item)
		{
			if (SelectedItem != null && SelectedItem == item)
			{
				SelectedItem = null;
			}
		}

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (SelectedItem == null || UnitEditorManager.isTestingUnit)
			{
				return;
			}
			if (playerActions.m_uiNavigation.WasPressed && !waitedForFrame)
			{
				waitedForFrame = true;
			}
			else
			{
				if (!playerActions.m_uiNavigation.IsPressed || !waitedForFrame)
				{
					return;
				}
				float x = playerActions.m_uiNavigation.Vector.x;
				float y = playerActions.m_uiNavigation.Vector.y;
				bool num = Mathf.Abs(x) < 0.2f;
				bool flag = Mathf.Abs(y) >= 0.2f;
				if (!(num || flag))
				{
					if (x > 0f)
					{
						PerformIncreaseAction();
					}
					else
					{
						PerformDecreaseAction();
					}
					waitedForFrame = false;
				}
			}
		}

		protected virtual void PerformIncreaseAction()
		{
		}

		protected virtual void PerformDecreaseAction()
		{
		}
	}
}
