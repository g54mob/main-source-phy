using System.Collections.Generic;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorClothingTypeGrid : UIComponentMainMenu
	{
		public GameObject clothingTypeCell;

		[SerializeField]
		private Transform listContainer;

		private const string Back = "BUTTON_BACK";

		public override void EnableNavigation()
		{
			base.EnableNavigation();
			SelectFirstChild(ignoreIfAlreadyHasSelected: true);
		}

		public void SpawnUnitBaseButtons(UnitEditorManager.ClothingTypeWrapper[] clothingTypeWrappers, UnitEditorManager unitEditorManager)
		{
			for (int i = 0; i < clothingTypeWrappers.Length; i++)
			{
				UnitEditorClothingTypeButton component = Object.Instantiate(clothingTypeCell, listContainer).GetComponent<UnitEditorClothingTypeButton>();
				component.gameObject.SetActive(value: true);
				component.Initlize(clothingTypeWrappers[i], unitEditorManager);
			}
			IList<Selectable> selectableChildren = listContainer.GetSelectableChildren();
			if (selectableChildren != null && selectableChildren.Count > 0)
			{
				UIHelpers.CreateExplicitLinearNavigation(selectableChildren, horizontal: false);
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();
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
