using UnityEngine;

namespace FullSail
{
	[CreateAssetMenu(menuName = "Full Sail/Ripple Map")]
	public class RippleMap : ScriptableObject
	{
		public int width = 256;

		public int height = 12;

		public bool negate;

		public float amplitude = 1f;

		public float scale = 1f;

		public AnimationCurve waveCrv = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 0f), new Keyframe(0.05f, 1f, 0f, 0f), new Keyframe(0.15f, 0.1f, 0f, 0f), new Keyframe(0.25f, 0.8f, 0f, 0f), new Keyframe(0.35f, 0.3f, 0f, 0f), new Keyframe(0.45f, 0.6f, 0f, 0f), new Keyframe(0.55f, 0.4f, 0f, 0f), new Keyframe(0.65f, 0.55f, 0f, 0f), new Keyframe(0.75f, 0.46f, 0f, 0f), new Keyframe(0.85f, 0.52f, 0f, 0f), new Keyframe(0.99f, 0.5f, 0f, 0f));
	}
}
