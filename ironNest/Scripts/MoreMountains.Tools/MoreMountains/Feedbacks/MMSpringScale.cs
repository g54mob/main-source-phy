using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringScale")]
	public class MMSpringScale : MMSpringVector3Component<Transform>
	{
		public override Vector3 TargetVector3
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		protected override void Initialization()
		{
		}
	}
}
