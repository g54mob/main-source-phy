using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "UI Style", menuName = "TABS/UI/UI Style", order = 0)]
	public class UIStyle : ScriptableObject
	{
		public Color m_HighlightedColor;

		public Color m_BackgroundColor;

		public Color m_OutlineColor;

		public Color m_DisabledColor;

		public Color m_TextColor;
	}
}
