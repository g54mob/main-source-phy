using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class IMColorPickerRenderer : ImmediateModeShapeDrawer
	{
		[Header("Color value")]
		[Range(0f, 1f)]
		public float hue;

		[Range(0f, 1f)]
		public float saturation;

		[Range(0f, 1f)]
		public float value;

		[Header("Styling")]
		[Range(0f, 0.3f)]
		public float hueStripThickness;

		[Range(0f, 0.1f)]
		public float outline;

		[Range(0f, 0.1f)]
		public float quadMargin;

		[Range(0f, 1.5f)]
		public float hueDotScale;

		public Vector2 labelSize;

		private PolylinePath hueStripPath;

		public Color CurrentPureColor => default(Color);

		public Color CurrentColor => default(Color);

		public float QuadScale => 0f;

		public Rect QuadRect => default(Rect);

		public float HueStripRadiusOuter => 0f;

		public float HueStripRadiusInner => 0f;

		public static Vector2 HueToVector(float hue)
		{
			return default(Vector2);
		}

		public static float VectorToHue(Vector2 v)
		{
			return 0f;
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
		}

		public override void DrawShapes(Camera cam)
		{
		}

		private void ConstructHueStripPolyline()
		{
		}
	}
}
