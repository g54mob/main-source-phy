using UnityEngine;

namespace FullSail
{
	[CreateAssetMenu(menuName = "Full Sail/Furled Map")]
	public class FurledMap : ScriptableObject
	{
		public enum Mode
		{
			Gradient = 0,
			Curve = 1
		}

		public int width = 256;

		public int height = 32;

		public Mode mode = Mode.Curve;

		public Gradient fill = new Gradient();

		public AnimationCurve fillCrv = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0.1f));
	}
}
