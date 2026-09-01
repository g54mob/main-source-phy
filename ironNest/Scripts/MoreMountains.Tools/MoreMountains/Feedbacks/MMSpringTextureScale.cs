using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringTextureScale")]
	public class MMSpringTextureScale : MMSpringVector2Component<Renderer>
	{
		public override Vector2 TargetVector2
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}
	}
}
