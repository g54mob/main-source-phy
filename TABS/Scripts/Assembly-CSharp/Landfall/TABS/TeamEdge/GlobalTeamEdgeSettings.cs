using UnityEngine;

namespace Landfall.TABS.TeamEdge
{
	[CreateAssetMenu(fileName = "GlobalTeamEdgeSettings", menuName = "Landfall/GlobalTeamEdgeSettings", order = 999999999)]
	public class GlobalTeamEdgeSettings : ScriptableObject
	{
		public TeamEdgeTypeSettings LineSettings;

		public TeamEdgeTypeSettings CircleSettings;

		private static GlobalTeamEdgeSettings instance;

		private static GlobalTeamEdgeSettings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Resources.Load("GlobalTeamEdgeSettings") as GlobalTeamEdgeSettings;
				}
				return instance;
			}
		}

		public static TeamEdgeTypeSettings GetSettings(EdgeType edgeType)
		{
			switch (edgeType)
			{
			case EdgeType.Line:
				return Instance.LineSettings;
			case EdgeType.Circle:
				return Instance.CircleSettings;
			default:
				return Instance.LineSettings;
			}
		}
	}
}
