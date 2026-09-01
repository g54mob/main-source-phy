using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "AllianceDatabase", menuName = "TABC/AllianceDatabase")]
	public class AllianceDatabase : ScriptableObject
	{
		public Alliance[] alliances;
	}
}
