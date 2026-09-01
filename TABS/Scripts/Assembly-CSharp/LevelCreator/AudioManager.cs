using UnityEngine;

namespace LevelCreator
{
	public class AudioManager : MonoBehaviour
	{
		public static readonly string[] MusicClips = new string[6] { "Medieval", "Tribal", "Renaissance", "Pirate", "Dynasty", "Viking" };

		public static readonly string[] LocalizedMusicClips = new string[6] { "FACTION_MEDIEVAL", "FACTION_TRIBAL", "FACTION_RENAISSANCE", "FACTION_PIRATE", "FACTION_ASIA", "FACTION_VIKING" };

		private static AudioManager internalInstance;

		private void Awake()
		{
			internalInstance = this;
		}

		public static void SetClip(int index)
		{
			index = Mathf.Clamp(index, 0, MusicClips.Length - 1);
			if (internalInstance != null)
			{
				internalInstance.PlayClip(MusicClips[index]);
			}
		}

		public static void SetClip(string songRef)
		{
			if (internalInstance != null)
			{
				internalInstance.PlayClip(songRef);
			}
		}

		private void PlayClip(string songRef)
		{
			ServiceLocator.GetService<MusicHandler>().PlaySong(songRef + "/Placement");
		}
	}
}
