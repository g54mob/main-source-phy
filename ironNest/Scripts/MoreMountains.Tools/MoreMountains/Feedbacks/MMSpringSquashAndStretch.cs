using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringSquashAndStretch")]
	public class MMSpringSquashAndStretch : MMSpringFloatComponent<Transform>
	{
		public enum PossibleAxis
		{
			XtoYZ = 0,
			XtoY = 1,
			XtoZ = 2,
			YtoXZ = 3,
			YtoX = 4,
			YtoZ = 5,
			ZtoXZ = 6,
			ZtoX = 7,
			ZtoY = 8
		}

		[MMInspectorGroup("Target", true, 17, false)]
		public PossibleAxis Axis;

		protected Vector3 _newScale;

		protected Vector3 _initialScale;

		protected override void Initialization()
		{
		}

		protected override void ApplyValue(float newValue)
		{
		}

		protected override void GrabCurrentValue()
		{
		}
	}
}
