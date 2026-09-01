using System.Runtime.CompilerServices;

namespace Shapes
{
	internal static class ShapesMaterialUtils
	{
		public static readonly int propZTest;

		public static readonly int propZTestTMP;

		public static readonly int propZOffsetFactor;

		public static readonly int propZOffsetUnits;

		public static readonly int propColorMask;

		public static readonly int propStencilComp;

		public static readonly int propStencilOpPass;

		public static readonly int propStencilID;

		public static readonly int propStencilIDTMP;

		public static readonly int propStencilReadMask;

		public static readonly int propStencilWriteMask;

		public static readonly int propBaseColor;

		public static readonly int propColor;

		public static readonly int propScaleMode;

		public static readonly int propColorEnd;

		public static readonly int propColorOuterStart;

		public static readonly int propColorInnerEnd;

		public static readonly int propColorOuterEnd;

		public static readonly int propColorB;

		public static readonly int propColorC;

		public static readonly int propColorD;

		public static readonly int propPointStart;

		public static readonly int propPointEnd;

		public static readonly int propA;

		public static readonly int propB;

		public static readonly int propC;

		public static readonly int propD;

		public static readonly int propRect;

		public static readonly int propRadius;

		public static readonly int propCornerRadii;

		public static readonly int propLength;

		public static readonly int propBorder;

		public static readonly int propSides;

		public static readonly int propAng;

		public static readonly int propRoundness;

		public static readonly int propAngStart;

		public static readonly int propAngEnd;

		public static readonly int propRoundCaps;

		public static readonly int propThickness;

		public static readonly int propThicknessSpace;

		public static readonly int propRadiusSpace;

		public static readonly int propDashSize;

		public static readonly int propDashOffset;

		public static readonly int propDashSpacing;

		public static readonly int propDashType;

		public static readonly int propDashSpace;

		public static readonly int propDashSnap;

		public static readonly int propDashShapeModifier;

		public static readonly int propSize;

		public static readonly int propSizeSpace;

		public static readonly int propAlignment;

		public static readonly int propFillType;

		public static readonly int propFillStart;

		public static readonly int propFillEnd;

		public static readonly int propFillSpace;

		public static readonly int propMainTex;

		public static readonly int propUvs;

		public static readonly int propScreenParams;

		private static readonly ShapesMaterials matDisc;

		private static readonly ShapesMaterials matCircleSector;

		private static readonly ShapesMaterials matRing;

		private static readonly ShapesMaterials matRingSector;

		private static readonly ShapesMaterials matRectSimple;

		private static readonly ShapesMaterials matRectRounded;

		private static readonly ShapesMaterials matRectBorder;

		private static readonly ShapesMaterials matRectBorderRounded;

		public static readonly ShapesMaterials matTriangle;

		public static readonly ShapesMaterials matQuad;

		public static readonly ShapesMaterials matSphere;

		public static readonly ShapesMaterials matCone;

		public static readonly ShapesMaterials matCuboid;

		public static readonly ShapesMaterials matTorus;

		public static readonly ShapesMaterials matPolygon;

		public static readonly ShapesMaterials matRegularPolygon;

		public static readonly ShapesMaterials matTexture;

		private static readonly ShapesMaterials[] matsLine;

		private static readonly ShapesMaterials[] matsLine3D;

		private static readonly ShapesMaterials[] matsPolyline;

		private static readonly ShapesMaterials[] matsPolylineJoin;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ShapesMaterials GetDiscMaterial(bool hollow, bool sector)
		{
			return null;
		}

		public static ShapesMaterials GetDiscMaterial(DiscType type)
		{
			return null;
		}

		public static ShapesMaterials GetRectMaterial(bool hollow, bool rounded)
		{
			return null;
		}

		public static ShapesMaterials GetRectMaterial(Rectangle.RectangleType type)
		{
			return null;
		}

		public static ShapesMaterials GetPolylineMat(PolylineJoins join)
		{
			return null;
		}

		public static ShapesMaterials GetPolylineJoinsMat(PolylineJoins join)
		{
			return null;
		}

		public static ShapesMaterials GetLineMat(LineGeometry geometry, LineEndCap cap)
		{
			return null;
		}
	}
}
