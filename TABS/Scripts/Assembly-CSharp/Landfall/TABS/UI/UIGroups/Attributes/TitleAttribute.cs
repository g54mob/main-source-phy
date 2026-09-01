using System;

namespace Landfall.TABS.UI.UIGroups.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class TitleAttribute : Attribute
	{
		private string m_title;

		public string Title => m_title;

		public TitleAttribute(string title)
		{
			m_title = title;
		}
	}
}
