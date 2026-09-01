using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class SetDMEditorButtonColors : MonoBehaviour
	{
		public DMEditorColors.ColorState normalColor;

		public DMEditorColors.ColorState highlightedColor;

		public DMEditorColors.ColorState pressedColor;

		public DMEditorColors.ColorState disabledColor;

		private void OnValidate()
		{
			Button component = GetComponent<Button>();
			if ((bool)component)
			{
				ColorBlock colors = component.colors;
				colors.normalColor = DMEditorColors.GetColor(normalColor);
				colors.highlightedColor = DMEditorColors.GetColor(highlightedColor);
				colors.pressedColor = DMEditorColors.GetColor(pressedColor);
				colors.disabledColor = DMEditorColors.GetColor(disabledColor);
				component.colors = colors;
			}
		}
	}
}
