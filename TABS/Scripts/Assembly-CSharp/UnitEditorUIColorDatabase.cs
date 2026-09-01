using UnityEngine;

[CreateAssetMenu(fileName = "UnitEditorUIColorDatabase", menuName = "Landfall/UnitEditor/UIColorDatabase", order = 99999999)]
public class UnitEditorUIColorDatabase : ScriptableObject
{
	public enum ColorMode
	{
		PanelColor = 0,
		ButtonColor = 1,
		Cutout = 2
	}

	public Color PanelColor;

	public Color ButtonColor;

	public Color CutoutColor;

	public void UpdateColors()
	{
		UnitEditorColorImage[] array = Object.FindObjectsOfType<UnitEditorColorImage>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Color(this);
		}
	}

	public Color GetColor(ColorMode colorMode)
	{
		switch (colorMode)
		{
		case ColorMode.ButtonColor:
			return ButtonColor;
		case ColorMode.PanelColor:
			return PanelColor;
		case ColorMode.Cutout:
			return CutoutColor;
		default:
			return Color.red;
		}
	}
}
