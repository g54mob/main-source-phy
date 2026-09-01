using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[CreateAssetMenu(menuName = "MoreMountains/MMChannel", fileName = "MMChannel")]
	public class MMChannel : ScriptableObject
	{
		public static bool Match(MMChannelData dataA, MMChannelData dataB)
		{
			return false;
		}

		public static bool Match(MMChannelData dataA, MMChannelModes modeB, int channelB, MMChannel channelDefinitionB)
		{
			return false;
		}
	}
}
