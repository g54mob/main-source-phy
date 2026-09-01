using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class SetDMEditorColor : MonoBehaviour
	{
		public DMEditorColors.ColorState color;

		private void OnValidate()
		{
			Text component = GetComponent<Text>();
			if ((bool)component)
			{
				component.color = DMEditorColors.GetColor(color);
			}
			Image component2 = GetComponent<Image>();
			if ((bool)component2)
			{
				component2.color = DMEditorColors.GetColor(color);
			}
			TextMeshProUGUI component3 = GetComponent<TextMeshProUGUI>();
			if ((bool)component3)
			{
				component3.color = DMEditorColors.GetColor(color);
			}
		}
	}
}
