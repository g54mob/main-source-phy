using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public struct MMSpringVector3Event
	{
		private static MMSpringVector3Event e;

		public MMChannelData ChannelData;

		public MMSpringComponentBase TargetSpring;

		public SpringCommands Command;

		public Vector3 MoveToValue;

		public Vector3 BumpAmount;

		public Vector3 MoveToRandomValueMin;

		public Vector3 MoveToRandomValueMax;

		public Vector3 BumpAmountRandomValueMin;

		public Vector3 BumpAmountRandomValueMax;

		public bool OverrideDamping;

		public Vector3 NewDamping;

		public bool OverrideFrequency;

		public Vector3 NewFrequency;

		public static void Trigger(SpringCommands command, MMSpringComponentBase targetSpring, MMChannelData channelData, Vector3 moveToValue = default(Vector3), Vector3 bumpAmount = default(Vector3), Vector3 moveToRandomValueMin = default(Vector3), Vector3 moveToRandomValueMax = default(Vector3), Vector3 bumpAmountRandomValueMin = default(Vector3), Vector3 bumpAmountRandomValueMax = default(Vector3), bool overrideDamping = false, Vector3 newDamping = default(Vector3), bool overrideFrequency = false, Vector3 newFrequency = default(Vector3))
		{
		}
	}
}
