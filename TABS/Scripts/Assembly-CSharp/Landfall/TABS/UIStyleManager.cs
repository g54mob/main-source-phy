using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "UI Style Manager", menuName = "TABS/UI/Style Manager", order = 0)]
	public class UIStyleManager : ScriptableObject
	{
		public UIStyle m_UIStyle;

		private static UIStyleManager instance;

		public static UIStyle GetStyle()
		{
			if (instance == null)
			{
				instance = (UIStyleManager)Resources.Load("UI Style Manager");
			}
			return instance.m_UIStyle;
		}
	}
}
