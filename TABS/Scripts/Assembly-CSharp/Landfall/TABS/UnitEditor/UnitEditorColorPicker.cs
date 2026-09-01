using System;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorColorPicker : MonoBehaviour
	{
		public UnitEditorColorPalette ColorPalette;

		public GameObject Cell;

		public GameObject TeamColorCells;

		public UIMovementAnimation UIMovement;

		private UnitEditorManager.EquipedWrapper clothingWrapper;

		private int submeshIndex;

		private bool preparedToColor;

		private Action<int> onColorComplete;

		public void Start()
		{
			ColorPaletteData[] colors = ColorPalette.Colors;
			for (int i = 0; i < colors.Length; i++)
			{
				GameObject obj = UnityEngine.Object.Instantiate(Cell, Cell.transform.parent);
				obj.SetActive(value: true);
				obj.GetComponent<UnitEditorColorCell>().Setup(colors[i], this);
			}
			TeamColorPaletteData[] teamColors = ColorPalette.TeamColors;
			for (int j = 0; j < teamColors.Length; j++)
			{
				GameObject obj2 = UnityEngine.Object.Instantiate(TeamColorCells, TeamColorCells.transform.parent);
				obj2.SetActive(value: true);
				obj2.GetComponent<UnitEditorColorCell>().Setup(teamColors[j], this);
			}
		}

		public void ShowColorPallete()
		{
			UIMovement.SetState(1);
		}

		public void HideColorPallete()
		{
			UIMovement.SetState(0);
			preparedToColor = false;
		}

		public void SetupClothingToColor(UnitEditorManager.EquipedWrapper clothingWrapper, int submeshIndex, Action<int> onComplete)
		{
			SetupClothingToColor(clothingWrapper, submeshIndex);
			onColorComplete = onComplete;
		}

		public void SetupClothingToColor(UnitEditorManager.EquipedWrapper clothingWrapper, int submeshIndex)
		{
			this.clothingWrapper = clothingWrapper;
			this.submeshIndex = submeshIndex;
			preparedToColor = true;
			ShowColorPallete();
		}

		public void Color(ColorPaletteData colorData)
		{
			UnityEngine.Object.FindObjectOfType<UnitEditorManager>().ColorProp(clothingWrapper, submeshIndex, colorData);
			HideColorPallete();
			if (onColorComplete != null)
			{
				onColorComplete(submeshIndex);
			}
		}

		public void OnColorPreviewEnter(ColorPaletteData colorData)
		{
			UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().BlinkClothes(clothingWrapper, submeshIndex, colorData.m_color, flash: false);
		}

		public void OnColorPreviewExit(ColorPaletteData colorData)
		{
			UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().StopBlinking(clothingWrapper, submeshIndex);
		}
	}
}
