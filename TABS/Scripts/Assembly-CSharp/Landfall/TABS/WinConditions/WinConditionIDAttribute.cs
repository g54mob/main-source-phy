using System;

namespace Landfall.TABS.WinConditions
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class WinConditionIDAttribute : Attribute
	{
		private string m_displayName;

		private bool m_isExclusive = true;

		public string DisplayName => m_displayName;

		public bool IsExclusive => m_isExclusive;

		public WinConditionIDAttribute(string displayName, bool isExclusive = true)
		{
			m_displayName = displayName;
			m_isExclusive = isExclusive;
		}
	}
}
