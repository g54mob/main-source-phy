using UnityEngine;

namespace MoreMountains.Tools
{
	public struct MMSoundManagerSoundControlEvent
	{
		public int SoundID;

		public MMSoundManagerSoundControlEventTypes MMSoundManagerSoundControlEventType;

		public AudioSource TargetSource;

		private static MMSoundManagerSoundControlEvent e;

		public MMSoundManagerSoundControlEvent(MMSoundManagerSoundControlEventTypes eventType, int soundID, AudioSource source = null)
		{
			SoundID = 0;
			MMSoundManagerSoundControlEventType = default(MMSoundManagerSoundControlEventTypes);
			TargetSource = null;
		}

		public static void Trigger(MMSoundManagerSoundControlEventTypes eventType, int soundID, AudioSource source = null)
		{
		}
	}
}
