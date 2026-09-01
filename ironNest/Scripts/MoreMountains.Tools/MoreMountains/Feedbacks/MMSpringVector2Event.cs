using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public struct MMSpringVector2Event
	{
		private static MMSpringVector2Event e;

		public MMChannelData ChannelData;

		public MMSpringComponentBase TargetSpring;

		public SpringCommands Command;

		public Vector2 MoveToValue;

		public Vector2 BumpAmount;

		public Vector2 MoveToRandomValueMin;

		public Vector2 MoveToRandomValueMax;

		public Vector2 BumpAmountRandomValueMin;

		public Vector2 BumpAmountRandomValueMax;

		public bool OverrideDamping;

		public Vector2 NewDamping;

		public bool OverrideFrequency;

		public Vector2 NewFrequency;

		public static void Trigger(SpringCommands command, MMSpringComponentBase targetSpring, MMChannelData channelData, Vector2 moveToValue = default(Vector2), Vector2 bumpAmount = default(Vector2), Vector2 moveToRandomValueMin = default(Vector2), Vector2 moveToRandomValueMax = default(Vector2), Vector2 bumpAmountRandomValueMin = default(Vector2), Vector2 bumpAmountRandomValueMax = default(Vector2), bool overrideDamping = false, Vector2 newDamping = default(Vector2), bool overrideFrequency = false, Vector2 newFrequency = default(Vector2))
		{
		}
	}
}
