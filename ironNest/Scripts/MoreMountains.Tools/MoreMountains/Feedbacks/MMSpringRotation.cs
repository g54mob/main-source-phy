using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringRotation")]
	public class MMSpringRotation : MMSpringVector3Component<Transform>
	{
		public enum Spaces
		{
			Local = 0,
			World = 1
		}

		[MMInspectorGroup("Target", true, 17, false)]
		public Spaces Space;

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
	}
}
