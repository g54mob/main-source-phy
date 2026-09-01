using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class CastleFightGoldText : TeamSingletons<CastleFightGoldText>
	{
		private TextMeshProUGUI text;

		public override void OnAwake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		public static void SetGold(float gold, Team team)
		{
			int num = Mathf.FloorToInt(gold);
			TeamSingletons<CastleFightGoldText>.GetTeamInstance(team).text.text = "Gold: " + num;
		}
	}
}
