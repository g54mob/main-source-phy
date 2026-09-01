using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorGamepadGlyphs : MonoBehaviour
	{
		public enum Position
		{
			Left = 0,
			Middle = 1,
			Right = 2
		}

		[SerializeField]
		private ActionGlyphText left;

		[SerializeField]
		private ActionGlyphText middle;

		[SerializeField]
		private ActionGlyphText right;

		[SerializeField]
		private Color defaultTextColor = Color.white;

		private ActionGlyphText[] positionGlyphs;

		private void Awake()
		{
			if (!(left == null) && !(middle == null))
			{
				_ = right == null;
			}
			positionGlyphs = new ActionGlyphText[3] { left, middle, right };
		}

		public void UpdateActionNames(string action, string text, Position position)
		{
			UpdateActionNames(action, text, position, defaultTextColor);
		}

		public void UpdateActionNames(string action, string text, Position position, Color color)
		{
			if (positionGlyphs != null)
			{
				positionGlyphs[(int)position].UpdateActionNames(action, text);
				positionGlyphs[(int)position].UpdateTextColor(color);
			}
		}

		public void UpdateTextColor(Color color, Position position)
		{
			if (positionGlyphs != null)
			{
				positionGlyphs[(int)position].UpdateTextColor(color);
			}
		}
	}
}
