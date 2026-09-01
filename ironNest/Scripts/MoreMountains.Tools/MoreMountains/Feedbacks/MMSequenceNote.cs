using System;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSequenceNote
	{
		public float Timestamp;

		public int ID;

		public virtual MMSequenceNote Copy()
		{
			return null;
		}
	}
}
