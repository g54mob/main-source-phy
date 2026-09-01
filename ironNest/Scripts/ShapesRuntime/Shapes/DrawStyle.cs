using UnityEngine;

namespace Shapes
{
	internal struct DrawStyle
	{
		private const float DEFAULT_THICKNESS = 0.05f;

		private const ThicknessSpace DEFAULT_THICKNESS_SPACE = ThicknessSpace.Meters;

		public static DrawStyle @default;

		public RenderState renderState;

		public Color color;

		public ShapesBlendMode blendMode;

		public ScaleMode scaleMode;

		public DetailLevel detailLevel;

		public bool useDashes;

		public DashStyle dashStyle;

		public bool useGradients;

		public GradientFill gradientFill;

		public float radius;

		public float thickness;

		public ThicknessSpace thicknessSpace;

		public ThicknessSpace radiusSpace;

		public ThicknessSpace sizeSpace;

		public LineEndCap lineEndCaps;

		public LineGeometry lineGeometry;

		public PolygonTriangulation polygonTriangulation;

		public PolylineGeometry polylineGeometry;

		public PolylineJoins polylineJoins;

		public DiscGeometry discGeometry;

		public int regularPolygonSideCount;

		public RegularPolygonGeometry regularPolygonGeometry;

		public TextStyle textStyle;
	}
}
