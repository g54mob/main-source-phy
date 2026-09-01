using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class BattleRadialMenuBackgroundHolder : MonoBehaviour
	{
		[SerializeField]
		private Sprite greenBackground;

		[SerializeField]
		private Sprite redBackground;

		[SerializeField]
		private Sprite blueBackground;

		[SerializeField]
		private Sprite green;

		[SerializeField]
		private Sprite red;

		[SerializeField]
		private Sprite blue;

		[SerializeField]
		private Faction addFactionFaction;

		public Sprite GreenBackground => greenBackground;

		public Sprite RedBackground => redBackground;

		public Sprite BlueBackground => blueBackground;

		public Sprite Green => green;

		public Sprite Red => red;

		public Sprite Blue => blue;

		public Faction AddFactionFaction => addFactionFaction;

		private void OnValidate()
		{
			if (greenBackground == null || redBackground == null || blueBackground == null || green == null || red == null || blue == null || addFactionFaction == null)
			{
				Debug.LogWarning("WARNING: BATTLE RADIAL MENU BACKGROUND HOLDER IS MISSING REFERENCES");
			}
		}
	}
}
