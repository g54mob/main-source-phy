using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "Alliance", menuName = "TABC/Alliance")]
	public class Alliance : ScriptableObject
	{
		public string allianceName = "";

		public Color color;

		public Color shadowColor;

		public Sprite sprite;

		public AllianceBonus[] bonuses;

		public string Name
		{
			get
			{
				if (allianceName != "")
				{
					return allianceName;
				}
				return base.name;
			}
			internal set
			{
			}
		}

		internal int GetUnlockedLevels(int testedUnitsInFaction)
		{
			int num = 0;
			for (int i = 0; i < bonuses.Length; i++)
			{
				for (int j = 0; j < bonuses[i].unitsNeeded; j++)
				{
					if (testedUnitsInFaction > 0)
					{
						testedUnitsInFaction--;
						if (j == bonuses[i].unitsNeeded - 1)
						{
							num = i + 1;
						}
					}
				}
			}
			return num - 1;
		}
	}
}
