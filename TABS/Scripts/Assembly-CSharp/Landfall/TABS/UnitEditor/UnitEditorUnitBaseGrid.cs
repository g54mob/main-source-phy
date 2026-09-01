using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorUnitBaseGrid : UIComponentMainMenu
	{
		public GameObject unitBaseButton;

		[SerializeField]
		private Transform listContainer;

		private List<UnitEditorUnitBaseButton> baseButtons = new List<UnitEditorUnitBaseButton>();

		private const string Back = "BUTTON_BACK";

		public void SpawnUnitBaseButtons(UnitEditorManager.UnitBaseWrapper[] unitBaseWrappers, UnitEditorManager unitEditorManager)
		{
			baseButtons.Clear();
			for (int i = 0; i < unitBaseWrappers.Length; i++)
			{
				UnitEditorUnitBaseButton component = Object.Instantiate(unitBaseButton, listContainer).GetComponent<UnitEditorUnitBaseButton>();
				if (component != null)
				{
					component.Initlize(unitBaseWrappers[i], unitEditorManager, i);
					baseButtons.Add(component);
					component.gameObject.SetActive(value: true);
				}
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			if (baseButtons != null && baseButtons.Count > 0)
			{
				baseButtons[0].Select();
			}
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				if (!(gamepadGlyphs == null))
				{
					gamepadGlyphs.UpdateActionNames("Back", "BUTTON_BACK", UnitEditorGamepadGlyphs.Position.Left);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Middle);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
				}
			}
		}
	}
}
