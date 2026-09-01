using UnityEngine;

namespace Shapes;

public class ShapesConfig : ScriptableObject
{
	public enum FragOutputPrecision
	{
		fixed4,
		half4,
		float4
	}

	public enum LocalAAQuality
	{
		Off,
		Medium,
		High
	}

	public enum QuadInterpolationQuality
	{
		Low,
		Medium,
		High2D,
		High
	}

	private static ShapesConfig inst;

	public bool useHdrColorPickers;

	public bool autoConfigureRenderPipeline = true;

	public bool useImmediateModeInstancing;

	public float polylineDefaultPointsPerTurn = 64f;

	public int polylineBezierAngularSumAccuracy = 2;

	public bool pushPopStateInDrawCommands = true;

	public const string TOOLTIP_BOUNDS = "These settings are uh, very esoteric\n*if* you are having trouble with *many* shapes being drawn on screen at the same time,\nmaking the bounds smaller using this parameter might help you optimize your game\n\nThis is like, super technical, so please read every word very carefully below:\nThis value should be set so that *all* shapes using, for instance, the quad mesh (disc, line, rect, etc.),\ncan use *these specific bounds*, so that the bounds would encapsulate the entire shape.\nPractically, this means that these bounds should be set so that it can encapsulate the largest\nshape you have in your project. If this is set too low, larger shapes will pop in/out of existence\n\nThe purpose of this is to gain some benefit in culling, but still keep the benefits of instancing.\nBy default, size is set to a large value of 1 << 16 (65536), practically \"turning off\" frustum culling";

	private const float VERY_LORGE_BOUNDS = 65536f;

	public float boundsSizeQuad = 65536f;

	public float boundsSizeTriangle = 65536f;

	public float boundsSizeSphere = 65536f;

	public float boundsSizeTorus = 65536f;

	public float boundsSizeCuboid = 65536f;

	public float boundsSizeCone = 65536f;

	public float boundsSizeCylinder = 65536f;

	public float boundsSizeCapsule = 65536f;

	public int[] sphereDetail = new int[5] { 1, 2, 5, 7, 12 };

	public Vector2Int[] torusDivsMinorMajor;

	public int[] coneDivs;

	public int[] cylinderDivs;

	public int[] capsuleDivs;

	public FragOutputPrecision FRAG_OUTPUT_V4;

	public LocalAAQuality LOCAL_ANTI_ALIASING_QUALITY;

	public QuadInterpolationQuality QUAD_INTERPOLATION_QUALITY;

	public int NOOTS_ACROSS_SCREEN;

	public static ShapesConfig Instance
	{
		get
		{
			if (inst == null)
			{
				ShapesConfig shapesConfig = Resources.Load<ShapesConfig>("Shapes Config");
				inst = shapesConfig;
			}
			return inst;
		}
	}

	public ShapesConfig()
	{
		Vector2Int[] array = new Vector2Int[5];
		_ = 6;
		_ = 12;
		_ = 24;
		_ = 32;
		_ = 64;
		torusDivsMinorMajor = array;
		coneDivs = new int[5] { 8, 12, 32, 64, 128 };
		cylinderDivs = new int[5] { 8, 12, 32, 64, 128 };
		capsuleDivs = new int[5] { 2, 3, 8, 10, 32 };
		FRAG_OUTPUT_V4 = FragOutputPrecision.half4;
		LOCAL_ANTI_ALIASING_QUALITY = LocalAAQuality.High;
		QUAD_INTERPOLATION_QUALITY = QuadInterpolationQuality.Medium;
		NOOTS_ACROSS_SCREEN = 100;
		base._002Ector();
	}
}
