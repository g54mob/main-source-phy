using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "NeutralBattle", menuName = "TABC/NeutralBattle", order = 2)]
	public class NeutralBattle : ScriptableObject
	{
		public string challangeName;

		public Sprite challangeImage;

		public BattleLayout battle;
	}
}
