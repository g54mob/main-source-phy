using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringCameraOrthographicSize")]
	public class MMSpringCameraOrthographicSize : MMSpringFloatComponent<Camera>
	{
		public override float TargetFloat
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}
	}
}
