using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public struct MMSpringFloatEvent
	{
		private static MMSpringFloatEvent e;

		public MMChannelData ChannelData;

		public MMSpringComponentBase TargetSpring;

		public SpringCommands Command;

		public float MoveToValue;

		public float BumpAmount;

		public Vector2 MoveToRandomValue;

		public Vector2 BumpAmountRandomValue;

		public bool OverrideDamping;

		public float NewDamping;

		public bool OverrideFrequency;

		public float NewFrequency;

		public static void Trigger(SpringCommands command, MMSpringComponentBase targetSpring, MMChannelData channelData, float moveToValue = 1f, float bumpAmount = 1f, Vector2 moveToRandomValue = default(Vector2), Vector2 bumpAmountRandomValue = default(Vector2), bool overrideDamping = false, float newDamping = 0.8f, bool overrideFrequency = false, float newFrequency = 5f)
		{
		}
	}
}
