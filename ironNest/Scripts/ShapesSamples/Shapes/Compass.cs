using UnityEngine;

namespace Shapes
{
	public class Compass : MonoBehaviour
	{
		public Vector2 position;

		public float width;

		[Range(0f, 0.01f)]
		public float lineThickness;

		[Range(0.1f, 2f)]
		public float bendRadius;

		[Range(0.05f, 3.0787609f)]
		public float fieldOfView;

		[Header("Ticks")]
		public int ticksPerQuarterTurn;

		[Range(0f, 0.2f)]
		public float tickSize;

		[Range(0f, 1f)]
		public float tickEdgeFadeFraction;

		[Range(0.01f, 0.26f)]
		public float fontSizeTickLabel;

		[Range(0f, 0.1f)]
		public float tickLabelOffset;

		[Header("Degree Marker")]
		[Range(0.01f, 0.26f)]
		public float fontSizeLookLabel;

		public Vector2 lookAngLabelOffset;

		[Range(0f, 0.05f)]
		public float triangleNootSize;

		private string[] directionLabels;

		public void DrawCompass(Vector3 worldDir)
		{
		}
	}
}
