using UnityEngine;

namespace Shapes
{
	public class Crosshair : MonoBehaviour
	{
		[Header("Style")]
		[Range(0f, 0.05f)]
		public float crosshairCrossInnerRad;

		[Range(0f, 0.05f)]
		public float crosshairCrossOuterRad;

		[Range(0f, 0.05f)]
		public float crosshairCrossThickness;

		[Range(0f, 0.05f)]
		public float crosshairHitCrossInnerRad;

		[Range(0f, 0.05f)]
		public float crosshairHitCrossOuterRad;

		[Range(0f, 0.05f)]
		public float crosshairHitCrossThickness;

		[Header("Animation")]
		[Range(0f, 1f)]
		public float scaleFire;

		public Decayer fireDecayer;

		public Decayer hitDecayer;

		public void Fire()
		{
		}

		public void FireHit()
		{
		}

		public void UpdateCrosshairDecay()
		{
		}

		public void DrawCrosshair()
		{
		}
	}
}
