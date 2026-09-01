using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public struct MMSpringColorEvent
	{
		private static MMSpringColorEvent e;

		public MMChannelData ChannelData;

		public MMSpringComponentBase TargetSpring;

		public SpringCommands Command;

		public Color MoveToValue;

		public Color BumpAmount;

		public Color MoveToRandomValueMin;

		public Color MoveToRandomValueMax;

		public Color BumpAmountRandomValueMin;

		public Color BumpAmountRandomValueMax;

		public bool OverrideDamping;

		public float NewDamping;

		public bool OverrideFrequency;

		public float NewFrequency;

		public static void Trigger(SpringCommands command, MMSpringComponentBase targetSpring, MMChannelData channelData, Color moveToValue = default(Color), Color bumpAmount = default(Color), Color moveToRandomValueMin = default(Color), Color moveToRandomValueMax = default(Color), Color bumpAmountRandomValueMin = default(Color), Color bumpAmountRandomValueMax = default(Color), bool overrideDamping = false, float newDamping = 0f, bool overrideFrequency = false, float newFrequency = 0f)
		{
		}
	}
}
