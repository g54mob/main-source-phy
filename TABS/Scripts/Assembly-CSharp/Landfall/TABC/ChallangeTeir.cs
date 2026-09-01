using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "ChallangeTeir", menuName = "TABC/ChallangeTeir", order = 2)]
	public class ChallangeTeir : ScriptableObject
	{
		public NeutralBattle[] battles;

		public Item[] items;
	}
}
