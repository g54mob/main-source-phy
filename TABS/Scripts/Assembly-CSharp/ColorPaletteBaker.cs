using UnityEngine;
using UnityEngine.UI;

public class ColorPaletteBaker : MonoBehaviour
{
	public UnitEditorColorPalette colorPalette;

	public void BakeColors()
	{
		Image[] componentsInChildren = GetComponentsInChildren<Image>();
		Color[] array = new Color[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			array[i] = componentsInChildren[i].color;
		}
		colorPalette.SetNewColors(array);
	}
}
