using UnityEngine;

namespace FullSail
{
	[CreateAssetMenu(menuName = "Full Sail/Furl Map")]
	public class FurlMap : ScriptableObject
	{
		public int width = 256;

		public int height = 256;

		public Gradient fill = new Gradient();

		public AnimationCurve fillCrv = new AnimationCurve(new Keyframe(0f, 0.4f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0.4f));
	}
}
