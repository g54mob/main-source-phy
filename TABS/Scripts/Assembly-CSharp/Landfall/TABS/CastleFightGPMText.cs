using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class CastleFightGPMText : TeamSingletons<CastleFightGPMText>
	{
		private TextMeshProUGUI text;

		public override void OnAwake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		public static void SetGPM(float gold, Team team)
		{
			int num = Mathf.FloorToInt(gold);
			TeamSingletons<CastleFightGPMText>.GetTeamInstance(team).text.text = "GPM: " + num;
		}
	}
}
