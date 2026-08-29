using UnityEngine;

namespace FullSail
{
	[CreateAssetMenu(menuName = "Full Sail/Move Map")]
	public class MoveMap : ScriptableObject
	{
		public int width = 256;

		public int height = 256;

		public float fixFalloff = 0.1f;

		public float fixFalloffPow = 0.2f;

		public float fixTL = 1f;

		public float fixTR = 1f;

		public float fixBL = 1f;

		public float fixBR = 1f;

		public bool useFill;

		public Gradient fill = new Gradient();

		public float fillRepeat = 1f;

		public bool useVertFill;

		public Gradient vertFill = new Gradient();

		public float vertFillRepeat = 1f;

		public bool useCurves = true;

		public AnimationCurve fillCrvX = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		public AnimationCurve fillCrvY = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		public bool useEdgeCurves;

		public AnimationCurve fillCrvX1 = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		public AnimationCurve fillCrvY1 = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		public bool useBlob;

		public Vector2 blobPos = new Vector3(0.5f, 0.5f);

		public float blobRadius = 0.25f;

		public float blobAmt = 0.1f;

		public float blobFalloff = 0.1f;

		public float blobFalloffPow = 0.2f;

		public Texture2D baseMap;
	}
}
