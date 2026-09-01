using System;

namespace Landfall.TABS.UI.UIGroups.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LockReferenceToTeamAttribute : Attribute
	{
		private TeamLock m_teamLock;

		public TeamLock TeamLock => m_teamLock;

		public LockReferenceToTeamAttribute(TeamLock teamLock)
		{
			m_teamLock = teamLock;
		}
	}
}
