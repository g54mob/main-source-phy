using UnityEngine;

[CreateAssetMenu(fileName = "new Color Preset", menuName = "TABC/Color Preset", order = 99999)]
public class UIColorPreset : ScriptableObject
{
	public Color m_Color;

	public void UpdateEntireScene()
	{
		UIColorSetter[] array = Object.FindObjectsOfType<UIColorSetter>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ApplyColor();
		}
	}
}
