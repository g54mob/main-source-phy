using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	[CreateAssetMenu(fileName = "PlayerProfiles", menuName = "Project Mars/Player Profile Configuration", order = 1)]
	public class PlayerProfileConfiguration : ScriptableObject
	{
		[SerializeField]
		private List<PlayerProfile> profiles;

		public List<PlayerProfile> Profiles => profiles;
	}
}
