using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringCameraFieldOfView")]
	public class MMSpringCameraFieldOfView : MMSpringFloatComponent<Camera>
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
