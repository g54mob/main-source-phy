using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Shapes
{
	public static class Draw
	{
		private delegate void OnPreRenderTmpDelegate(TextMeshProShapes tmp);

		private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

		private static MpbLine2D mpbLine;

		private static MpbPolyline2D mpbPolyline;

		private static MpbPolyline2D mpbPolylineJoins;

		private static MpbPolygon mpbPolygon;

		private static readonly MpbDisc mpbDisc;

		private static readonly MpbRegularPolygon mpbRegularPolygon;

		private static readonly MpbRect mpbRect;

		private static MpbTriangle mpbTriangle;

		private static MpbQuad mpbQuad;

		private static readonly MpbSphere metaMpbSphere;

		private static readonly MpbCone mpbCone;

		private static readonly MpbCuboid mpbCuboid;

		private static MpbTorus mpbTorus;

		private static MpbText mpbText;

		private static OnPreRenderTmpDelegate onPreRenderTmp;

		private static MpbCustomMesh mpbCustomMesh;

		private static MpbTexture mpbTexture;

		private const string OBS_DASH = "As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle";

		private const string OBS_FILL = "As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill";

		private const string OBS_REGPOLRENAME = "For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead";

		private const string OBS_TRIRENAME = "For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead";

		private const string JOINER = ". In addition: ";

		private const string OBS_REGPOLRENAME_AND_FILL = "As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead";

		private const string OBS_DISC_GRADIENT_PREFIX = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.";

		private const string OBS_DISC_GRADIENT_DISC_RADIAL = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )";

		private const string OBS_DISC_GRADIENT_DISC_ANGULAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )";

		private const string OBS_DISC_GRADIENT_DISC_BILINEAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )";

		private const string OBS_DISC_GRADIENT_RING_RADIAL = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )";

		private const string OBS_DISC_GRADIENT_RING_ANGULAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )";

		private const string OBS_DISC_GRADIENT_RING_BILINEAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )";

		private const string OBS_DISC_GRADIENT_PIE_RADIAL = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )";

		private const string OBS_DISC_GRADIENT_PIE_ANGULAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )";

		private const string OBS_DISC_GRADIENT_PIE_BILINEAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )";

		private const string OBS_DISC_GRADIENT_ARC_RADIAL = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )";

		private const string OBS_DISC_GRADIENT_ARC_ANGULAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )";

		private const string OBS_DISC_GRADIENT_ARC_BILINEAR = "As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )";

		private static Matrix4x4 matrix;

		internal static DrawStyle style;

		private static OnPreRenderTmpDelegate OnPreRenderTmp => null;

		public static StateStack Scope => default(StateStack);

		public static Matrix4x4 Matrix
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Matrix4x4);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static MatrixStack MatrixScope => default(MatrixStack);

		public static Vector3 Position
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public static Vector2 Position2D
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		[Obsolete("Please use Draw.Position instead (I done messed up, did a typo, I'm sorry~)", true)]
		public static Vector3 Postition
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		[Obsolete("Please use Draw.Position2D instead (I done messed up, did a typo, I'm sorry~)", true)]
		public static Vector2 Postition2D
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public static Quaternion Rotation
		{
			get
			{
				return default(Quaternion);
			}
			set
			{
			}
		}

		public static float Angle2D
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public static Vector3 Right => default(Vector3);

		public static Vector3 Up => default(Vector3);

		public static Vector3 Forward => default(Vector3);

		public static Vector3 RightBasis => default(Vector3);

		public static Vector3 UpBasis => default(Vector3);

		public static Vector3 ForwardBasis => default(Vector3);

		public static Vector3 LocalScale
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public static StyleStack StyleScope => default(StyleStack);

		public static ColorStack ColorScope => default(ColorStack);

		public static CompareFunction ZTest
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(CompareFunction);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float ZOffsetFactor
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static int ZOffsetUnits
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static ColorWriteMask ColorMask
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(ColorWriteMask);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static CompareFunction StencilComp
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(CompareFunction);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static StencilOp StencilOpPass
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(StencilOp);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static byte StencilRefID
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static byte StencilReadMask
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static byte StencilWriteMask
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Color Color
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Color);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float Opacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static ShapesBlendMode BlendMode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(ShapesBlendMode);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static ScaleMode ScaleMode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(ScaleMode);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static DetailLevel DetailLevel
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(DetailLevel);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float Thickness
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float Radius
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static ThicknessSpace ThicknessSpace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(ThicknessSpace);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static ThicknessSpace RadiusSpace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(ThicknessSpace);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static ThicknessSpace SizeSpace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(ThicknessSpace);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static bool UseGradientFill
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return false;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static GradientFill GradientFill
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(GradientFill);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static FillType GradientFillType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(FillType);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static FillSpace GradientFillSpace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(FillSpace);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Color GradientFillColorStart
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Color);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Color GradientFillColorEnd
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Color);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Vector3 GradientFillLinearStart
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Vector3);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Vector3 GradientFillLinearEnd
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Vector3);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Vector3 GradientFillRadialOrigin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Vector3);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float GradientFillRadialRadius
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static bool UseDashes
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return false;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static DashStyle DashStyle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(DashStyle);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static DashType DashType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(DashType);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static DashSpace DashSpace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(DashSpace);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static DashSnapping DashSnap
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(DashSnapping);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float DashSize
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float DashSizeUniform
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float DashSpacing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float DashOffset
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float DashShapeModifier
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static LineEndCap LineEndCaps
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(LineEndCap);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static LineGeometry LineGeometry
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(LineGeometry);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static PolygonTriangulation PolygonTriangulation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(PolygonTriangulation);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static PolylineGeometry PolylineGeometry
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(PolylineGeometry);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static PolylineJoins PolylineJoins
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(PolylineJoins);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static DiscGeometry DiscGeometry
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(DiscGeometry);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static int RegularPolygonSideCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static RegularPolygonGeometry RegularPolygonGeometry
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(RegularPolygonGeometry);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static TextStyle TextStyle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(TextStyle);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static TMP_FontAsset Font
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return null;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float FontSize
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static FontStyles FontStyle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(FontStyles);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static TextAlign TextAlign
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(TextAlign);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float TextCharacterSpacing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float TextWordSpacing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float TextLineSpacing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float TextParagraphSpacing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Vector4 TextMargins
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Vector4);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static TextWrappingModes TextWrap
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(TextWrappingModes);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static TextOverflowModes TextOverflow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(TextOverflowModes);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static float TextCurvature
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public static Vector2 TextCurvaturePivot
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(Vector2);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Thickness property", true)]
		public static float LineThickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static ThicknessSpace property", true)]
		public static ThicknessSpace LineThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static DashStyle property by default, when UseDashes is enabled", true)]
		public static DashStyle LineDashStyle
		{
			get
			{
				return default(DashStyle);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Radius property", true)]
		public static float DiscRadius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static DashStyle property by default, when UseDashes is enabled", true)]
		public static DashStyle RingDashStyle
		{
			get
			{
				return default(DashStyle);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static GradientFill property by default. If you want to override shape fill per shape, use the draw overload with a fill input", true)]
		public static GradientFill PolygonShapeFill
		{
			get
			{
				return default(GradientFill);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static GradientFill property by default. If you want to override shape fill per shape, use the draw overload with a fill input", true)]
		public static GradientFill RegularPolygonShapeFill
		{
			get
			{
				return default(GradientFill);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static GradientFill property by default. If you want to override shape fill per shape, use the draw overload with a fill input", true)]
		public static GradientFill RectangleShapeFill
		{
			get
			{
				return default(GradientFill);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Thickness property", true)]
		public static float RingThickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static ThicknessSpace property", true)]
		public static ThicknessSpace RingThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static RadiusSpace property", true)]
		public static ThicknessSpace DiscRadiusSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Radius property", true)]
		public static float RegularPolygonRadius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Thickness property", true)]
		public static float RegularPolygonThickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static ThicknessSpace property", true)]
		public static ThicknessSpace RegularPolygonThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static RadiusSpace property", true)]
		public static ThicknessSpace RegularPolygonRadiusSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Thickness property", true)]
		public static float RectangleThickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static ThicknessSpace property", true)]
		public static ThicknessSpace RectangleThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Thickness property", true)]
		public static float TriangleThickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static ThicknessSpace property", true)]
		public static ThicknessSpace TriangleThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static Radius property", true)]
		public static float SphereRadius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static RadiusSpace property", true)]
		public static ThicknessSpace SphereRadiusSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static SizeSpace property", true)]
		public static ThicknessSpace CuboidSizeSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static ThicknessSpace property", true)]
		public static ThicknessSpace TorusThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static RadiusSpace property", true)]
		public static ThicknessSpace TorusRadiusSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		[Obsolete("All shapes now use the same static SizeSpace property", true)]
		public static ThicknessSpace ConeSizeSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		public static DrawCommand Command(Camera cam, RenderPassEvent cameraEvent = RenderPassEvent.BeforeRenderingPostProcessing)
		{
			return null;
		}

		public static void PrepareForIMGUI()
		{
		}

		[OvldGenCallTarget]
		private static void Line_Internal([OvldDefault("LineEndCaps")] LineEndCap endCaps, [OvldDefault("ThicknessSpace")] ThicknessSpace thicknessSpace, Vector3 start, Vector3 end, [OvldDefault("Color")] Color colorStart, [OvldDefault("Color")] Color colorEnd, [OvldDefault("Thickness")] float thickness)
		{
		}

		[OvldGenCallTarget]
		private static void Polyline_Internal(PolylinePath path, [OvldDefault("false")] bool closed, [OvldDefault("PolylineGeometry")] PolylineGeometry geometry, [OvldDefault("PolylineJoins")] PolylineJoins joins, [OvldDefault("Thickness")] float thickness, [OvldDefault("ThicknessSpace")] ThicknessSpace thicknessSpace, [OvldDefault("Color")] Color color)
		{
		}

		[OvldGenCallTarget]
		private static void Polygon_Internal(PolygonPath path, [OvldDefault("PolygonTriangulation")] PolygonTriangulation triangulation, [OvldDefault("Color")] Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OvldGenCallTarget]
		private static void Disc_Internal([OvldDefault("Radius")] float radius, [OvldDefault("Color")] DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OvldGenCallTarget]
		private static void Ring_Internal([OvldDefault("Radius")] float radius, [OvldDefault("Thickness")] float thickness, [OvldDefault("Color")] DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OvldGenCallTarget]
		private static void Pie_Internal([OvldDefault("Radius")] float radius, [OvldDefault("Color")] DiscColors colors, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OvldGenCallTarget]
		private static void Arc_Internal([OvldDefault("Radius")] float radius, [OvldDefault("Thickness")] float thickness, [OvldDefault("Color")] DiscColors colors, float angleRadStart, float angleRadEnd, [OvldDefault("ArcEndCap.None")] ArcEndCap endCaps)
		{
		}

		private static void DiscCore(bool hollow, bool sector, float radius, float thickness, DiscColors colors, float angleRadStart = 0f, float angleRadEnd = 0f, ArcEndCap arcEndCaps = ArcEndCap.None)
		{
		}

		[OvldGenCallTarget]
		private static void RegularPolygon_Internal([OvldDefault("RegularPolygonSideCount")] int sideCount, [OvldDefault("Radius")] float radius, [OvldDefault("Thickness")] float thickness, [OvldDefault("Color")] Color color, bool hollow, [OvldDefault("0f")] float roundness, [OvldDefault("0f")] float angle)
		{
		}

		[OvldGenCallTarget]
		private static void Rectangle_Internal([OvldDefault("BlendMode")] ShapesBlendMode blendMode, [OvldDefault("false")] bool hollow, Rect rect, [OvldDefault("Color")] Color color, [OvldDefault("Thickness")] float thickness, [OvldDefault("default")] Vector4 cornerRadii)
		{
		}

		[OvldGenCallTarget]
		private static void Triangle_Internal(Vector3 a, Vector3 b, Vector3 c, bool hollow, [OvldDefault("Thickness")] float thickness, [OvldDefault("0f")] float roundness, [OvldDefault("Color")] Color colorA, [OvldDefault("Color")] Color colorB, [OvldDefault("Color")] Color colorC)
		{
		}

		[OvldGenCallTarget]
		private static void Quad_Internal(Vector3 a, Vector3 b, Vector3 c, [OvldDefault("a + ( c - b )")] Vector3 d, [OvldDefault("Color")] Color colorA, [OvldDefault("Color")] Color colorB, [OvldDefault("Color")] Color colorC, [OvldDefault("Color")] Color colorD)
		{
		}

		[OvldGenCallTarget]
		private static void Sphere_Internal([OvldDefault("Radius")] float radius, [OvldDefault("Color")] Color color)
		{
		}

		[OvldGenCallTarget]
		private static void Cone_Internal(float radius, float length, [OvldDefault("true")] bool fillCap, [OvldDefault("Color")] Color color)
		{
		}

		[OvldGenCallTarget]
		private static void Cuboid_Internal(Vector3 size, [OvldDefault("Color")] Color color)
		{
		}

		[OvldGenCallTarget]
		private static void Torus_Internal(float radius, float thickness, [OvldDefault("0")] float angleRadStart, [OvldDefault("ShapesMath.TAU")] float angleRadEnd, [OvldDefault("Color")] Color color)
		{
		}

		[OvldGenCallTarget]
		private static void TextRect_Internal([OvldDefault("null")] string content, [OvldDefault("null")] TextElement element, Rect rect, [OvldDefault("Font")] TMP_FontAsset font, [OvldDefault("FontSize")] float fontSize, [OvldDefault("TextAlign")] TextAlign align, [OvldDefault("Color")] Color color)
		{
		}

		[OvldGenCallTarget]
		private static void Text_Internal(bool isRect, [OvldDefault("null")] string content, [OvldDefault("null")] TextElement element, [OvldDefault("default")] Vector2 pivot, [OvldDefault("default")] Vector2 size, [OvldDefault("Font")] TMP_FontAsset font, [OvldDefault("FontSize")] float fontSize, [OvldDefault("TextAlign")] TextAlign align, [OvldDefault("Color")] Color color)
		{
		}

		private static void ApplyTextValuesToInstance(TextMeshProShapes tmp, bool isRect, string content, TMP_FontAsset font, float fontSize, TextAlign align, Vector2 pivot, Vector2 size, Color color)
		{
		}

		private static void Text_Internal(TextMeshPro tmp, IMDrawer.DrawType drawType, int disposeId = -1)
		{
		}

		public static void Mesh(Mesh mesh, Material mat)
		{
		}

		public static void Mesh(Mesh mesh, Material mat, MaterialPropertyBlock mpb)
		{
		}

		private static void CustomMesh_Internal(Mesh mesh, Material mat, MaterialPropertyBlock mpb)
		{
		}

		[OvldGenCallTarget]
		private static void Texture_Internal(Texture texture, Rect rect, Rect uvs, [OvldDefault("Color")] Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Texture_Placement_Internal(Texture texture, (Rect rect, Rect uvs) placement, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OvldGenCallTarget]
		private static void Texture_RectFill_Internal(Texture texture, Rect rect, [OvldDefault("TextureFillMode.ScaleToFit")] TextureFillMode fillMode, [OvldDefault("Color")] Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OvldGenCallTarget]
		private static void Texture_PosSize_Internal(Texture texture, Vector2 center, float size, [OvldDefault("TextureSizeMode.LongestSide")] TextureSizeMode sizeMode, [OvldDefault("Color")] Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, Color colorStart, Color colorEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, LineEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, LineEndCap endCaps, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, LineEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Line(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, PolylineJoins joins)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, PolylineJoins joins)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, float thickness, PolylineJoins joins)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, float thickness, PolylineJoins joins)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, PolylineJoins joins, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, PolylineJoins joins, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, float thickness, PolylineJoins joins, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polyline(PolylinePath path, bool closed, float thickness, PolylineJoins joins, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polygon(PolygonPath path)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polygon(PolygonPath path, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polygon(PolygonPath path, PolygonTriangulation triangulation)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Polygon(PolygonPath path, PolygonTriangulation triangulation, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, int sideCount, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, float radius, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, float radius, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, float radius, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygon(int sideCount, float radius, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, float thickness, float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RegularPolygonBorder(int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Vector3 normal)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Vector3 normal, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Vector3 normal, float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Quaternion rot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Quaternion rot, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(Vector3 pos, Quaternion rot, float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Disc(float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, float radius, float thickness, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Vector3 normal)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Vector3 normal, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Vector3 normal, float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Vector3 normal, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Vector3 normal, float radius, float thickness, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Quaternion rot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Quaternion rot, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Quaternion rot, float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Quaternion rot, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(Vector3 pos, Quaternion rot, float radius, float thickness, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(float radius, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Ring(float radius, float thickness, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pie(float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float thickness, float angleRadStart, float angleRadEnd, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Arc(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, DiscColors colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Rect rect)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Rect rect, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Rect rect, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Rect rect, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Rect rect, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Rect rect, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Rect rect)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Rect rect, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Rect rect, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Rect rect, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Rect rect, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Rect rect, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Rect rect)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Rect rect, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Rect rect, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Rect rect, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Rect rect, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Rect rect, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Rect rect)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Rect rect, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Rect rect, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Rect rect, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Rect rect, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Rect rect, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, RectPivot pivot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, RectPivot pivot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, RectPivot pivot, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, RectPivot pivot, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector2 size, RectPivot pivot, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, RectPivot pivot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, RectPivot pivot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, RectPivot pivot, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, RectPivot pivot, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, float width, float height, RectPivot pivot, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rectangle(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Rect rect, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Rect rect, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Rect rect, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Rect rect, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Rect rect, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Rect rect, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Rect rect, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Rect rect, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Rect rect, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Rect rect, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Rect rect, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Rect rect, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Rect rect, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Rect rect, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Rect rect, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Rect rect, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Rect rect, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Rect rect, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Rect rect, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Rect rect, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, RectPivot pivot, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, RectPivot pivot, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, RectPivot pivot, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, RectPivot pivot, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, float cornerRadius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RectangleBorder(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Triangle(Vector3 a, Vector3 b, Vector3 c)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Color colorA, Color colorB, Color colorC)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Triangle(Vector3 a, Vector3 b, Vector3 c, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Triangle(Vector3 a, Vector3 b, Vector3 c, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Triangle(Vector3 a, Vector3 b, Vector3 c, float roundness, Color colorA, Color colorB, Color colorC)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, Color colorA, Color colorB, Color colorC)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, float thickness, Color colorA, Color colorB, Color colorC)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TriangleBorder(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness, Color colorA, Color colorB, Color colorC)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Quad(Vector3 a, Vector3 b, Vector3 c)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Quad(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Quad(Vector3 a, Vector3 b, Vector3 c, Color colorA, Color colorB, Color colorC, Color colorD)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Quad(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Quad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Quad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color colorA, Color colorB, Color colorC, Color colorD)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(Vector3 pos, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(Vector3 pos, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(Vector3 pos, float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sphere(float radius, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 pos, Vector3 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 pos, Vector3 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 pos, Vector3 normal, Vector3 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 pos, Vector3 normal, Vector3 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 pos, Quaternion rot, Vector3 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 pos, Quaternion rot, Vector3 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cuboid(Vector3 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(Vector3 pos, float size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(Vector3 pos, float size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(Vector3 pos, Vector3 normal, float size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(Vector3 pos, Vector3 normal, float size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(Vector3 pos, Quaternion rot, float size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(Vector3 pos, Quaternion rot, float size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(float size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cube(float size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, float radius, float length)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, float radius, float length, bool fillCap)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, float radius, float length, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, float radius, float length, bool fillCap, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Vector3 normal, float radius, float length)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Vector3 normal, float radius, float length, bool fillCap)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Vector3 normal, float radius, float length, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Vector3 normal, float radius, float length, bool fillCap, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Quaternion rot, float radius, float length)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Quaternion rot, float radius, float length, bool fillCap)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Quaternion rot, float radius, float length, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(Vector3 pos, Quaternion rot, float radius, float length, bool fillCap, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(float radius, float length)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(float radius, float length, bool fillCap)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(float radius, float length, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Cone(float radius, float length, bool fillCap, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Vector3 normal, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Vector3 normal, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Quaternion rot, float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Quaternion rot, float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(float radius, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(float radius, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Torus(float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(TextElement element, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(Vector3 pos, Quaternion rot, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Text(string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector3 pos, Quaternion rot, Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Vector2 pivot, Vector2 size, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(TextElement element, Rect rect, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, float fontSize)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, float fontSize, TMP_FontAsset font)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, float fontSize, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TextRect(Rect rect, string content, TextAlign align, float fontSize, TMP_FontAsset font, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Rect rect, Rect uvs)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Rect rect, Rect uvs, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Rect rect)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Rect rect, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Rect rect, TextureFillMode fillMode)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Rect rect, TextureFillMode fillMode, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Vector2 center, float size)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Vector2 center, float size, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Vector2 center, float size, TextureSizeMode sizeMode)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Texture(Texture texture, Vector2 center, float size, TextureSizeMode sizeMode, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, LineEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, LineEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, LineEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, float thickness, LineEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, LineEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, LineEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, LineEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness, LineEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness, LineEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void LineDashed(Vector3 start, Vector3 end, DashStyle dashStyle, float thickness, LineEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFill(PolygonPath path)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFill(PolygonPath path, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFill(PolygonPath path, PolygonTriangulation triangulation)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFill(PolygonPath path, PolygonTriangulation triangulation, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFillLinear(PolygonPath path, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFillLinear(PolygonPath path, PolygonTriangulation triangulation, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFillRadial(PolygonPath path, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void PolygonFillRadial(PolygonPath path, PolygonTriangulation triangulation, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow()
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollow(int sideCount, float radius, float thickness, float angle, float roundness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, int sideCount, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill()
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, float radius, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, float radius, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, float radius, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFill(int sideCount, float radius, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill()
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle, float roundness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFill(int sideCount, float radius, float thickness, float angle, float roundness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, int sideCount, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(int sideCount, float radius, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillLinear(int sideCount, float radius, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(int sideCount, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(int sideCount, float radius, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(int sideCount, float radius, float thickness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(int sideCount, float radius, float thickness, float angle, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillLinear(int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillStart, Vector3 fillEnd, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, int sideCount, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(int sideCount, float radius, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RegularPolygonFillRadial(int sideCount, float radius, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Vector3 normal, int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 pos, Quaternion rot, int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(int sideCount, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(int sideCount, float radius, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(int sideCount, float radius, float thickness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(int sideCount, float radius, float thickness, float angle, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill. In addition: For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.RegularPolygonBorder instead", true)]
		public static void RegularPolygonHollowFillRadial(int sideCount, float radius, float thickness, float angle, float roundness, Vector3 fillOrigin, float fillRadius, Color fillColorStart, Color fillColorEnd, FillSpace fillSpace = FillSpace.Local)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Vector3 pos, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Vector3 pos, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Vector3 pos, Vector3 normal, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Vector3 pos, Vector3 normal, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Vector3 pos, Quaternion rot, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Vector3 pos, Quaternion rot, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Radial(...) )", true)]
		public static void DiscGradientRadial(float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Vector3 pos, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Vector3 pos, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Vector3 pos, Vector3 normal, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Vector3 pos, Vector3 normal, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Vector3 pos, Quaternion rot, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Vector3 pos, Quaternion rot, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Angular(...) )", true)]
		public static void DiscGradientAngular(float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Vector3 pos, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Vector3 pos, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Vector3 pos, Vector3 normal, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Vector3 pos, Vector3 normal, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Vector3 pos, Quaternion rot, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Vector3 pos, Quaternion rot, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Disc( ..., DiscColors.Bilinear(...) )", true)]
		public static void DiscGradientBilinear(float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, DashStyle dashStyle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, DashStyle dashStyle, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed()
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(DashStyle dashStyle)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(DashStyle dashStyle, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(DashStyle dashStyle, float radius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(DashStyle dashStyle, float radius, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(DashStyle dashStyle, float radius, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingDashed(DashStyle dashStyle, float radius, float thickness, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Vector3 normal, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Vector3 normal, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Vector3 normal, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Quaternion rot, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Quaternion rot, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Vector3 pos, Quaternion rot, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) )", true)]
		public static void RingGradientRadial(float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, DashStyle dashStyle, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(DashStyle dashStyle, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(DashStyle dashStyle, float radius, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientRadialDashed(DashStyle dashStyle, float radius, float thickness, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Vector3 normal, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Vector3 normal, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Vector3 normal, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Quaternion rot, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Quaternion rot, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Vector3 pos, Quaternion rot, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) )", true)]
		public static void RingGradientAngular(float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, DashStyle dashStyle, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(DashStyle dashStyle, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(DashStyle dashStyle, float radius, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientAngularDashed(DashStyle dashStyle, float radius, float thickness, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Vector3 normal, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Vector3 normal, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Vector3 normal, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Quaternion rot, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Quaternion rot, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Vector3 pos, Quaternion rot, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) )", true)]
		public static void RingGradientBilinear(float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(DashStyle dashStyle, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(DashStyle dashStyle, float radius, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Ring( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void RingGradientBilinearDashed(DashStyle dashStyle, float radius, float thickness, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Radial(...) )", true)]
		public static void PieGradientRadial(float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Angular(...) )", true)]
		public static void PieGradientAngular(float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Pie( ..., DiscColors.Bilinear(...) )", true)]
		public static void PieGradientBilinear(float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps)
		{
		}

		[Obsolete("As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color color)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) )", true)]
		public static void ArcGradientRadial(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Radial(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientRadialDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInner, Color colorOuter)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) )", true)]
		public static void ArcGradientAngular(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Angular(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientAngularDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorStart, Color colorEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) )", true)]
		public static void ArcGradientBilinear(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Vector3 normal, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(Vector3 pos, Quaternion rot, DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(DashStyle dashStyle, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, disc gradients are now defined using a DiscColors as the last parameter. Instead, please use Draw.Arc( ..., DiscColors.Bilinear(...) ). In addition: As of Shapes 4.0.0, dash state is now set using the global Draw.UseDashes and Draw.DashStyle", true)]
		public static void ArcGradientBilinearDashed(DashStyle dashStyle, float radius, float thickness, float angleRadStart, float angleRadEnd, ArcEndCap endCaps, Color colorInnerStart, Color colorOuterStart, Color colorInnerEnd, Color colorOuterEnd)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Rect rect)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Rect rect, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Rect rect, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Rect rect, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Rect rect, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Rect rect, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Rect rect, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Rect rect, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Rect rect)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Rect rect, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Rect rect, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Rect rect, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Rect rect, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Rect rect, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector2 size, RectPivot pivot, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, float width, float height, RectPivot pivot, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Rect rect, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Rect rect, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Rect rect, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Rect rect, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Rect rect, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Rect rect, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Rect rect, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Rect rect, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Rect rect, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Vector3 normal, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, Vector2 size, RectPivot pivot, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, float cornerRadius)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, float cornerRadius, GradientFill fill)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii)
		{
		}

		[Obsolete("As of Shapes 4.0.0, color fill is now set using the global Draw.UseGradientFill and Draw.GradientFill", true)]
		public static void RectangleBorderFill(Vector3 pos, Quaternion rot, float width, float height, RectPivot pivot, float thickness, Vector4 cornerRadii, GradientFill fill)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, Color colorA, Color colorB, Color colorC)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, Color colorA, Color colorB, Color colorC)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness, Color color)
		{
		}

		[Obsolete("For consistency, this has been renamed as of Shapes 4.0.0. Please use Draw.TriangleBorder instead", true)]
		public static void TriangleHollow(Vector3 a, Vector3 b, Vector3 c, float thickness, float roundness, Color colorA, Color colorB, Color colorC)
		{
		}

		static Draw()
		{
		}

		public static void ResetAllDrawStates()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Pop()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetMatrix()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PushMatrix()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PopMatrix()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ApplyMatrix(Matrix4x4 matrix)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Translate(float x, float y)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Translate(float x, float y, float z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Translate(Vector2 displacement)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Translate(Vector3 displacement)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rotate(float angle)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rotate(float x, float y, float z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rotate(float angle, Vector3 axis)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Rotate(Quaternion rotation)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scale(float uniformScale)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scale(float x, float y)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scale(float x, float y, float z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scale(Vector2 scale)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Scale(Vector3 scale)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMatrix(Matrix4x4 matrix)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMatrix(Vector3 position, Quaternion rotation, Vector3 scale)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMatrix(Transform transform)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxSetRotationKeepScale(ref Matrix4x4 m, Quaternion rotation)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxRotateZLhs(ref Matrix4x4 rhs, float a)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxTranslateXYZ(ref Matrix4x4 lhs, double x, double y, double z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxTranslateXY(ref Matrix4x4 lhs, double x, double y)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxRotateZ(ref Matrix4x4 lhs, float a)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxScaleXYZ(ref Matrix4x4 m, double x, double y, double z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxScaleXY(ref Matrix4x4 m, double x, double y)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxResetToXYZ(out Matrix4x4 m, float x, float y, float z)
		{
			m = default(Matrix4x4);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxResetToXY(out Matrix4x4 m, float x, float y)
		{
			m = default(Matrix4x4);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxResetToPosXYatAngle(out Matrix4x4 lhs, float x, float y, float a)
		{
			lhs = default(Matrix4x4);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxResetToPosXYatDirection(out Matrix4x4 lhs, float x, float y, Vector2 dir)
		{
			lhs = default(Matrix4x4);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxResetScaleSetAngleZ(ref Matrix4x4 lhs, float a)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void MtxResetScaleSetDirX(ref Matrix4x4 lhs, Vector2 dir)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetStyle()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PushStyle()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PopStyle()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PushColor()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PopColor()
		{
		}

		public static DashStack DashedScope()
		{
			return default(DashStack);
		}

		public static DashStack DashedScope(DashStyle dashStyle)
		{
			return default(DashStack);
		}

		public static GradientFillStack GradientFillScope()
		{
			return default(GradientFillStack);
		}

		public static GradientFillStack GradientFillScope(GradientFill fill)
		{
			return default(GradientFillStack);
		}
	}
}
