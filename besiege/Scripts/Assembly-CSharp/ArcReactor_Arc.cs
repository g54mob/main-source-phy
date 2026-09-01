using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("Arc Reactor Rays/Ray System")]
public class ArcReactor_Arc : MonoBehaviour
{
	public enum PropagationType
	{
		instant = 0,
		globalSpaceSpeed = 1,
		localTimeCurve = 2
	}

	public enum ArcsPlaybackType
	{
		once = 0,
		loop = 1,
		pingpong = 2,
		clamp = 3,
		pingpong_once = 4,
		pingpong_clamp_once = 5
	}

	public enum InterpolationType
	{
		CatmullRom_Splines = 0,
		Linear = 1
	}

	public enum SpatialNoiseType
	{
		TangentRandomization = 0,
		CubicRandomization = 1,
		BrokenTangentRandomization = 2
	}

	public enum OscillationType
	{
		sine_wave = 0,
		rectangular = 1,
		zigzag = 2
	}

	public enum FadeTypes
	{
		none = 0,
		worldspacePoint = 1,
		relativePoint = 2
	}

	[Serializable]
	public class ArcNestingOptions
	{
		public bool Nested;

		public int parentArcIndex;

		public bool combinedNesting;

		public int secondaryArcIndex;

		public float nestingCoef;
	}

	[Serializable]
	public class EaseInOutOptions
	{
		public bool useEaseInOut;

		public AnimationCurve easeInOutCurve;

		public float distance;
	}

	[Serializable]
	public class ArcPropagationOptions
	{
		public PropagationType propagationType;

		public float globalSpeed = 1f;

		public AnimationCurve timeCurve;
	}

	[Serializable]
	public class ArcColorOptions
	{
		public Gradient startColor;

		public bool onlyStartColor = true;

		public Gradient endColor;

		public Gradient coreColor;

		public AnimationCurve coreCurve;

		public float coreJitter;

		public FadeTypes fade;

		public float fadePoint;

		public FadeTypes frontFade;

		public float frontFadePoint;
	}

	[Serializable]
	public class ArcSizeOptions
	{
		public InterpolationType interpolation;

		public AnimationCurve startWidthCurve;

		public bool onlyStartWidth = true;

		public AnimationCurve endWidthCurve;

		public float segmentLength = 10f;

		public bool snapSegmentsToShape;

		public int numberOfSmoothingSegments;

		public int minNumberOfSegments = 1;
	}

	[Serializable]
	public class TextureAnimationOptions
	{
		public Texture shapeTexture;

		public Texture noiseTexture;

		public AnimationCurve noiseCoef;

		public bool animateTexture;

		public float tileSize;

		public float noiseSpeed;
	}

	[Serializable]
	public class ArcSpatialNoiseOptions
	{
		public SpatialNoiseType type;

		public float scale;

		public float scaleMovement;

		public float resetFrequency;

		public int invisiblePriority;
	}

	[Serializable]
	public class ArcLightsOptions
	{
		public bool lights;

		public float lightsRange = 5f;

		public float lightsIntensityMultiplyer = 5f;

		public LightRenderMode renderMode;

		public int priority;
	}

	[Serializable]
	public class OscillationInfo
	{
		public OscillationType type;

		public bool swirl;

		public float planeRotation;

		public float wavelength;

		public bool integerPeriods;

		public WavelengthMetric metric;

		public float amplitude;

		public float phase;

		public float phaseMovementSpeed;

		public int invisiblePriority;
	}

	[Serializable]
	public class ParticleEmissionOptions
	{
		public bool emit;

		public ParticleSystem shurikenPrefab;

		public bool emitAfterRayDeath;

		public float particlesPerMeter;

		public AnimationCurve emissionDuringLifetime;

		public AnimationCurve radiusCoefDuringLifetime;

		public AnimationCurve directionDuringLifetime;

		public float arcColorInfluence = 0.5f;
	}

	public enum WavelengthMetric
	{
		globalSpace = 0,
		localSpace = 1
	}

	[Serializable]
	public class ArcFlaresInfo
	{
		public FlareInfo startFlare;

		public FlareInfo endFlare;

		public bool useNoiseMask;

		public AnimationCurve noiseMaskPowerCurve;
	}

	[Serializable]
	public class FlareInfo
	{
		public bool enabled;

		public Flare flare;

		public float fadeSpeed = 50f;

		public float maxBrightness;

		public float maxBrightnessDistance;

		public float minBrightness;

		public float minBrightnessDistance;

		public LayerMask ignoreLayers = 6;
	}

	[Serializable]
	public class ShiftCurveInfo
	{
		public AnimationCurve shapeCurve;

		public float curveWidth;

		public float planeRotation;

		public WavelengthMetric metric;

		public float curveLength;

		public bool notAffectedByEaseInOut;

		public int invisiblePriority;
	}

	[Serializable]
	public class LineRendererInfo
	{
		public Material material;

		public ArcColorOptions colorOptions;

		public ArcSizeOptions sizeOptions;

		public ArcPropagationOptions propagationOptions;

		public ParticleEmissionOptions[] emissionOptions;

		public ArcSpatialNoiseOptions[] spatialNoise;

		public TextureAnimationOptions textureOptions;

		public ArcLightsOptions lightsOptions;

		public ArcFlaresInfo flaresOptions;

		public ArcNestingOptions nesting;

		public OscillationInfo[] oscillations;

		public ShiftCurveInfo[] shapeCurves;
	}

	private const int maxCalcDetalization = 10;

	public LineRendererInfo[] arcs;

	public Camera currentCamera;

	public float lifetime;

	public ArcsPlaybackType playbackType;

	public bool playbackMessages;

	public GameObject messageReciever;

	public float elapsedTime;

	public bool playBackward;

	public bool freeze;

	public float sizeMultiplier = 1f;

	public InterpolationType interpolation;

	public EaseInOutOptions easeInOutOptions;

	public Transform[] shapeTransforms;

	public Vector3[] shapePoints;

	public bool[] transformsDestructionFlags;

	public bool closedShape;

	public Vector3 oscillationNormal = Vector3.up;

	public bool localSpaceOcillations;

	public float reinitThreshold = 0.5f;

	public int performancePriority;

	public ArcReactorSingleLayer linerendererLayer;

	public bool customSorting;

	public string sortingLayerName;

	public int sortingOrder;

	[NonSerialized]
	public bool currentlyInPool;

	protected Vector3[] resultingShape;

	protected int oldShapeTransformsSize;

	protected float overlap;

	protected float[] noiseOffsets;

	protected float[] noiseScale;

	protected Vector3[,] arcPoints;

	protected Vector3[,] shiftVectors;

	protected Vector3[,] arcTangents;

	protected Quaternion[,] arcTangentsShift;

	protected Vector3[] shapeTangents;

	protected Vector3[][] vertices;

	protected Vector3[][] oldVertices;

	protected ParticleSystem.Particle[][][] particleBuffers;

	protected Transform[,] lightsTransforms;

	protected Light[,] lights;

	protected LineRenderer[] lrends;

	protected int[] segmNums;

	protected int[] vertexCount;

	protected int[] oldVertexCount;

	protected int[] lightsCount;

	protected float shapeLength;

	protected float oldShapeLength;

	protected float[] shapeKeyLocations;

	protected float[] shapeKeyNormalizedLocations;

	protected float[] maxStartWidth;

	protected float[] maxEndWidth;

	protected float[] coreCoefs;

	protected Vector3 oscNormal;

	protected LensFlare startFlare;

	protected LensFlare endFlare;

	protected ParticleSystem[][] emitterSystems;

	protected ArcReactor_EmitterDestructor[][] emitterDestructors;

	public float ShapeLength
	{
		get
		{
			return shapeLength;
		}
	}

	public int PerformancePriority
	{
		get
		{
			return performancePriority;
		}
	}

	public static Vector3 HermiteCurvePoint(float t, Vector3 p0, Vector3 m0, Vector3 p1, Vector3 m1)
	{
		float num = t * t;
		float num2 = t * t * t;
		return (2f * num2 - 3f * num + 1f) * p0 + (num2 - 2f * num + t) * m0 + (-2f * num2 + 3f * num) * p1 + (num2 - num) * m1;
	}

	public void FillResultingShape()
	{
		if (resultingShape == null)
		{
			resultingShape = new Vector3[0];
		}
		if (shapePoints != null && shapeTransforms != null)
		{
			if (Mathf.Max(shapeTransforms.Length, shapePoints.Length) != resultingShape.Length)
			{
				Array.Resize(ref resultingShape, Mathf.Max(shapeTransforms.Length, shapePoints.Length));
			}
			for (int i = 0; i < resultingShape.Length; i++)
			{
				if (shapeTransforms.Length > i && shapeTransforms[i] != null)
				{
					resultingShape[i] = shapeTransforms[i].position;
				}
				else
				{
					resultingShape[i] = shapePoints[i];
				}
			}
		}
		else if (shapeTransforms != null)
		{
			if (shapeTransforms.Length != resultingShape.Length)
			{
				Array.Resize(ref resultingShape, shapeTransforms.Length);
			}
			for (int j = 0; j < resultingShape.Length; j++)
			{
				resultingShape[j] = shapeTransforms[j].position;
			}
		}
		else if (shapePoints != null)
		{
			if (shapePoints.Length != resultingShape.Length)
			{
				Array.Resize(ref resultingShape, shapePoints.Length);
			}
			for (int k = 0; k < resultingShape.Length; k++)
			{
				resultingShape[k] = shapePoints[k];
			}
		}
	}

	public static Material GetDefaultMaterial()
	{
		return new Material(Shader.Find("ArcReactor/Additive_core_higlight"));
	}

	public void SetPerformancePriority(int newPriority)
	{
		if (lightsCount == null || performancePriority == newPriority)
		{
			return;
		}
		performancePriority = newPriority;
		for (int i = 0; i < arcs.Length; i++)
		{
			if (arcs[i].lightsOptions.lights && lightsCount[i] > 0)
			{
				for (int j = 0; j < lightsCount[i]; j++)
				{
					lights[i, j].enabled = arcs[i].lightsOptions.priority <= performancePriority;
				}
			}
		}
	}

	protected Vector3 CalculateCurveShift(Vector3 direction, float position, int arcInd)
	{
		Vector3 zero = Vector3.zero;
		ShiftCurveInfo[] shapeCurves = arcs[arcInd].shapeCurves;
		foreach (ShiftCurveInfo shiftCurveInfo in shapeCurves)
		{
			if (lrends[arcInd].isVisible || shiftCurveInfo.invisiblePriority <= performancePriority)
			{
				float num = ((shiftCurveInfo.metric != WavelengthMetric.localSpace) ? (shiftCurveInfo.shapeCurve.Evaluate(position / shiftCurveInfo.curveLength) * shiftCurveInfo.curveWidth) : (shiftCurveInfo.shapeCurve.Evaluate(position / shapeLength) * shiftCurveInfo.curveWidth));
				Quaternion quaternion = Quaternion.AngleAxis(shiftCurveInfo.planeRotation, direction);
				Vector3 vector = Vector3.Cross(direction, oscNormal);
				if (shiftCurveInfo.notAffectedByEaseInOut)
				{
					zero += quaternion * vector.normalized * num;
				}
				else
				{
					zero += quaternion * vector.normalized * num * GetShiftCoef(position / shapeLength);
				}
			}
		}
		return zero * sizeMultiplier;
	}

	protected Vector3 CalculateOscillationShift(Vector3 direction, float position, int arcInd)
	{
		Vector3 zero = Vector3.zero;
		OscillationInfo[] oscillations = arcs[arcInd].oscillations;
		foreach (OscillationInfo oscillationInfo in oscillations)
		{
			if (!lrends[arcInd].isVisible && oscillationInfo.invisiblePriority > performancePriority)
			{
				continue;
			}
			float num = oscillationInfo.wavelength * sizeMultiplier;
			float num2 = num;
			if (oscillationInfo.integerPeriods && oscillationInfo.metric == WavelengthMetric.globalSpace)
			{
				num2 = shapeLength / Mathf.Ceil(shapeLength / num);
			}
			if (oscillationInfo.integerPeriods && oscillationInfo.metric == WavelengthMetric.localSpace)
			{
				num2 = 1f / Mathf.Ceil(1f / num);
			}
			float num3 = ((oscillationInfo.metric != WavelengthMetric.globalSpace) ? (oscillationInfo.phase * ((float)Math.PI / 180f) + (position / shapeLength - num2 * (float)(int)(position / shapeLength / num2)) / num2 * (float)Math.PI * 2f) : (oscillationInfo.phase * ((float)Math.PI / 180f) + (position - num2 * (float)(int)(position / num2)) / num2 * (float)Math.PI * 2f));
			float num4;
			switch (oscillationInfo.type)
			{
			case OscillationType.sine_wave:
				num4 = oscillationInfo.amplitude * Mathf.Sin(num3);
				break;
			case OscillationType.rectangular:
				num4 = ((!(num3 * 57.29578f % 360f > 180f)) ? oscillationInfo.amplitude : (0f - oscillationInfo.amplitude));
				break;
			case OscillationType.zigzag:
				num4 = oscillationInfo.amplitude * (Mathf.Abs(num3 * 57.29578f % 180f / 45f - 2f) - 1f);
				break;
			default:
				num4 = 0f;
				break;
			}
			Quaternion quaternion = Quaternion.AngleAxis(oscillationInfo.planeRotation, direction);
			Vector3 vector = Vector3.Cross(direction, oscNormal);
			zero += quaternion * vector.normalized * num4;
			if (oscillationInfo.swirl)
			{
				num3 = ((oscillationInfo.metric != WavelengthMetric.globalSpace) ? ((oscillationInfo.phase + 90f) * ((float)Math.PI / 180f) + (position / shapeLength - num2 * (float)(int)(position / shapeLength / num2)) / num2 * (float)Math.PI * 2f) : ((oscillationInfo.phase + 90f) * ((float)Math.PI / 180f) + (position - num2 * (float)(int)(position / num2)) / num2 * (float)Math.PI * 2f));
				switch (oscillationInfo.type)
				{
				case OscillationType.sine_wave:
					num4 = oscillationInfo.amplitude * Mathf.Sin(num3);
					break;
				case OscillationType.rectangular:
					num4 = ((!(num3 * 57.29578f % 360f > 180f)) ? oscillationInfo.amplitude : (0f - oscillationInfo.amplitude));
					break;
				case OscillationType.zigzag:
					num4 = oscillationInfo.amplitude * (Mathf.Abs(num3 * 57.29578f % 180f / 45f - 2f) - 1f);
					break;
				default:
					num4 = 0f;
					break;
				}
				quaternion = Quaternion.AngleAxis(oscillationInfo.planeRotation + 90f, direction);
				zero += quaternion * vector.normalized * num4;
			}
		}
		return zero * sizeMultiplier;
	}

	protected void CalculateShape()
	{
		FillResultingShape();
		if (oldShapeTransformsSize != resultingShape.Length)
		{
			SetShapeArrays();
		}
		if (closedShape)
		{
			shapeLength = 0f;
			for (int i = 0; i < resultingShape.Length - 1; i++)
			{
				shapeKeyLocations[i] = shapeLength;
				shapeLength += (resultingShape[i] - resultingShape[i + 1]).magnitude;
			}
			shapeKeyLocations[resultingShape.Length - 1] = shapeLength;
			float magnitude = (resultingShape[0] - resultingShape[resultingShape.Length - 1]).magnitude;
			shapeLength += magnitude;
			shapeKeyLocations[resultingShape.Length] = shapeLength;
			shapeLength += overlap;
		}
		else
		{
			shapeLength = 0f;
			for (int j = 0; j < resultingShape.Length - 1; j++)
			{
				shapeKeyLocations[j] = shapeLength;
				shapeLength += (resultingShape[j] - resultingShape[j + 1]).magnitude;
			}
			shapeKeyLocations[resultingShape.Length - 1] = shapeLength;
		}
		for (int k = 0; k < shapeKeyLocations.Length; k++)
		{
			shapeKeyNormalizedLocations[k] = shapeKeyLocations[k] / shapeLength;
		}
		if (interpolation == InterpolationType.CatmullRom_Splines)
		{
			if (closedShape)
			{
				for (int l = 0; l < resultingShape.Length; l++)
				{
					shapeTangents[l] = (resultingShape[AddCyclicShift(l, 1, resultingShape.Length - 1)] - resultingShape[AddCyclicShift(l, -1, resultingShape.Length - 1)]) / 2f;
				}
			}
			else
			{
				shapeTangents[0] = resultingShape[1] - resultingShape[0];
				shapeTangents[resultingShape.Length - 1] = resultingShape[resultingShape.Length - 1] - resultingShape[resultingShape.Length - 2];
				for (int m = 1; m < resultingShape.Length - 1; m++)
				{
					shapeTangents[m] = (resultingShape[m + 1] - resultingShape[m - 1]) / 2f;
				}
			}
		}
		if (oldShapeLength == 0f || Mathf.Abs((oldShapeLength - shapeLength) / shapeLength) > reinitThreshold)
		{
			Initialize();
		}
	}

	protected int AddCyclicShift(int a, int b, int size)
	{
		int num = a + b;
		if (num < 0)
		{
			return num + size + 1;
		}
		if (num > size)
		{
			return num - size - 1;
		}
		return num;
	}

	protected float AddCyclicShift(float a, float b, float size)
	{
		float num = a + b;
		if (num < 0f)
		{
			return num + size;
		}
		if (num > size)
		{
			return num - size;
		}
		return num;
	}

	protected Quaternion RandomXYQuaternion(float angle)
	{
		if (angle > 0f)
		{
			return Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0f - angle, angle), UnityEngine.Random.Range(0f - angle, angle), 0f));
		}
		return Quaternion.identity;
	}

	protected void SetArcShape(int n)
	{
		float num = 1f + overlap / shapeLength;
		int num2 = 1;
		for (int i = 0; i < arcs[n].spatialNoise.Length; i++)
		{
			switch (arcs[n].spatialNoise[i].type)
			{
			case SpatialNoiseType.CubicRandomization:
				if (UnityEngine.Random.value > arcs[n].spatialNoise[i].resetFrequency * Time.deltaTime)
				{
					num2 = 1;
					if (closedShape)
					{
						num2 = 0;
					}
					for (int k = 0; k < segmNums[n] + num2; k++)
					{
						shiftVectors[n, k] += RandomVector3(arcs[n].spatialNoise[i].scaleMovement * Time.deltaTime * 60f) * GetShiftCoef((float)k / (float)segmNums[n]);
					}
				}
				else
				{
					ResetArcNoise(n, i);
				}
				break;
			case SpatialNoiseType.TangentRandomization:
				if (UnityEngine.Random.value > arcs[n].spatialNoise[i].resetFrequency * Time.deltaTime)
				{
					num2 = 1;
					if (closedShape)
					{
						num2 = 0;
					}
					for (int l = 0; l < segmNums[n] + num2; l++)
					{
						arcTangentsShift[n, l * 2] = arcTangentsShift[n, l * 2] * RandomXYQuaternion(arcs[n].spatialNoise[i].scaleMovement * GetShiftCoef((float)l / (float)segmNums[n]));
						arcTangentsShift[n, l * 2 + 1] = arcTangentsShift[n, l * 2];
					}
				}
				else
				{
					ResetArcNoise(n, i);
				}
				break;
			case SpatialNoiseType.BrokenTangentRandomization:
				if (UnityEngine.Random.value > arcs[n].spatialNoise[i].resetFrequency * Time.deltaTime)
				{
					num2 = 1;
					if (closedShape)
					{
						num2 = 0;
					}
					for (int j = 0; j < segmNums[n] + num2; j++)
					{
						arcTangentsShift[n, j * 2] = arcTangentsShift[n, j * 2] * RandomXYQuaternion(arcs[n].spatialNoise[i].scaleMovement * GetShiftCoef((float)j / (float)segmNums[n]));
						arcTangentsShift[n, j * 2 + 1] = arcTangentsShift[n, j * 2 + 1] * RandomXYQuaternion(arcs[n].spatialNoise[i].scaleMovement * GetShiftCoef((float)j / (float)segmNums[n]));
					}
				}
				else
				{
					ResetArcNoise(n, i);
				}
				break;
			}
		}
		num2 = 1;
		if (closedShape)
		{
			num2 = 0;
		}
		if (arcs[n].nesting.Nested && !arcs[n].nesting.combinedNesting)
		{
			for (int m = 0; m < segmNums[n] + num2; m++)
			{
				arcPoints[n, m] = GetArcPoint((float)m / (float)segmNums[n] * num, arcs[n].nesting.parentArcIndex) + shiftVectors[n, m] * sizeMultiplier;
			}
		}
		else if (arcs[n].nesting.Nested && arcs[n].nesting.combinedNesting)
		{
			for (int num3 = 0; num3 < segmNums[n] + num2; num3++)
			{
				arcPoints[n, num3] = Vector3.Lerp(GetArcPoint((float)num3 / (float)segmNums[n] * num, arcs[n].nesting.parentArcIndex), GetArcPoint(Mathf.Clamp01((float)num3 / (float)segmNums[n] * num - 0.001f), arcs[n].nesting.secondaryArcIndex), arcs[n].nesting.nestingCoef) + shiftVectors[n, num3] * sizeMultiplier;
			}
		}
		else
		{
			for (int num4 = 0; num4 < segmNums[n] + num2; num4++)
			{
				arcPoints[n, num4] = CalcShapePoint((float)num4 / (float)segmNums[n] * num) + shiftVectors[n, num4] * sizeMultiplier;
			}
		}
		if (arcs[n].sizeOptions.interpolation != InterpolationType.CatmullRom_Splines)
		{
			return;
		}
		if (closedShape)
		{
			for (int num5 = 0; num5 < segmNums[n]; num5++)
			{
				arcTangents[n, num5] = (arcPoints[n, AddCyclicShift(num5, 1, segmNums[n] - 1)] - arcPoints[n, AddCyclicShift(num5, -1, segmNums[n] - 1)]) / 2f;
			}
			return;
		}
		arcTangents[n, 0] = arcPoints[n, 1] - arcPoints[n, 0];
		arcTangents[n, segmNums[n]] = arcPoints[n, segmNums[n]] - arcPoints[n, segmNums[n] - 1];
		for (int num6 = 1; num6 < segmNums[n]; num6++)
		{
			arcTangents[n, num6] = (arcPoints[n, num6 + 1] - arcPoints[n, num6 - 1]) / 2f;
		}
	}

	protected Vector3 CalcArcPoint(float point, int n)
	{
		int num = 0;
		int num2 = 1;
		if (closedShape)
		{
			num = Mathf.FloorToInt(point * (float)segmNums[n]);
			if (point == 1f)
			{
				num--;
			}
			num2 = ((num != segmNums[n] - 1) ? (num + 1) : 0);
		}
		else
		{
			num = Mathf.FloorToInt(point * (float)segmNums[n]);
			if (point != 1f)
			{
				num2 = num + 1;
			}
			else
			{
				num2 = num;
				num--;
			}
		}
		switch (arcs[n].sizeOptions.interpolation)
		{
		case InterpolationType.CatmullRom_Splines:
			return HermiteCurvePoint(point * (float)segmNums[n] - (float)num, arcPoints[n, num], arcTangentsShift[n, num * 2] * arcTangents[n, num], arcPoints[n, num2], arcTangentsShift[n, num2 * 2 + 1] * arcTangents[n, num2]);
		case InterpolationType.Linear:
			return arcPoints[n, num] + (arcPoints[n, num2] - arcPoints[n, num]) * (point * (float)segmNums[n] - (float)num);
		default:
			return arcPoints[n, num] + (arcPoints[n, num2] - arcPoints[n, num]) * (point * (float)segmNums[n] - (float)num);
		}
	}

	public Vector3 CalcShapePoint(float point)
	{
		float num = point * shapeLength;
		int num2 = 0;
		int num3 = 1;
		float num4 = 0f;
		for (int i = 0; i < shapeKeyLocations.Length - 1; i++)
		{
			if (num > shapeKeyLocations[i] && num <= shapeKeyLocations[i + 1])
			{
				num2 = i;
				num3 = i + 1;
				num4 = 1f - (shapeKeyLocations[i + 1] - num) / (shapeKeyLocations[i + 1] - shapeKeyLocations[i]);
				break;
			}
		}
		if (closedShape && num3 == shapeKeyLocations.Length - 1)
		{
			num2 = resultingShape.Length - 1;
			num3 = 0;
		}
		switch (interpolation)
		{
		case InterpolationType.CatmullRom_Splines:
			return HermiteCurvePoint(num4, resultingShape[num2], shapeTangents[num2], resultingShape[num3], shapeTangents[num3]);
		case InterpolationType.Linear:
			return resultingShape[num2] + (resultingShape[num3] - resultingShape[num2]) * num4;
		default:
			return Vector3.zero;
		}
	}

	public Vector3 GetArcPoint(float point, int arcIndex)
	{
		float num = point * (float)(vertexCount[arcIndex] - 1);
		int num2 = Mathf.Clamp(Mathf.FloorToInt(num), 0, vertexCount[arcIndex] - 1);
		int num3 = Mathf.Clamp(Mathf.CeilToInt(num), 0, vertexCount[arcIndex] - 1);
		float num4 = num - Mathf.Floor(num);
		Vector3 vector = ((!(vertices[arcIndex][num2] == Vector3.zero)) ? vertices[arcIndex][num2] : CalcArcPoint(point, arcIndex));
		Vector3 vector2 = ((!(vertices[arcIndex][num3] == Vector3.zero)) ? vertices[arcIndex][num3] : CalcArcPoint(point, arcIndex));
		return vector * (1f - num4) + vector2 * num4;
	}

	public Vector3 GetOldArcPoint(float point, int arcIndex)
	{
		float num = point * (float)(oldVertexCount[arcIndex] - 1);
		int num2 = Mathf.Clamp(Mathf.FloorToInt(num), 0, oldVertexCount[arcIndex] - 1);
		int num3 = Mathf.Clamp(Mathf.CeilToInt(num), 0, oldVertexCount[arcIndex] - 1);
		float num4 = num - Mathf.Floor(num);
		Vector3 vector = ((!(oldVertices[arcIndex][num2] == Vector3.zero)) ? oldVertices[arcIndex][num2] : CalcArcPoint(point, arcIndex));
		Vector3 vector2 = ((!(oldVertices[arcIndex][num3] == Vector3.zero)) ? oldVertices[arcIndex][num3] : CalcArcPoint(point, arcIndex));
		return vector * (1f - num4) + vector2 * num4;
	}

	public float GetShiftCoef(float point)
	{
		if (easeInOutOptions.useEaseInOut)
		{
			float num = point * shapeLength;
			if (num > easeInOutOptions.distance / 2f && num < shapeLength - easeInOutOptions.distance / 2f)
			{
				return easeInOutOptions.easeInOutCurve.Evaluate(0.5f);
			}
			if (num < easeInOutOptions.distance / 2f)
			{
				return easeInOutOptions.easeInOutCurve.Evaluate(num / easeInOutOptions.distance);
			}
			return easeInOutOptions.easeInOutCurve.Evaluate(1f - (shapeLength - num) / easeInOutOptions.distance);
		}
		return 1f;
	}

	public void ResetArc(int n)
	{
		for (int i = 0; i < arcs[n].spatialNoise.Length; i++)
		{
			ResetArcNoise(n, i);
		}
		if (arcs[n].nesting.Nested && !arcs[n].nesting.combinedNesting)
		{
			for (int j = 0; j < segmNums[n]; j++)
			{
				float point = (float)j / (float)segmNums[n];
				arcPoints[n, j] = GetArcPoint(point, arcs[n].nesting.parentArcIndex) + shiftVectors[n, j] * sizeMultiplier;
			}
		}
		else if (arcs[n].nesting.Nested && arcs[n].nesting.combinedNesting)
		{
			for (int k = 0; k < segmNums[n]; k++)
			{
				float point = (float)k / (float)segmNums[n];
				arcPoints[n, k] = Vector3.Lerp(GetArcPoint(point, arcs[n].nesting.parentArcIndex), GetArcPoint(Mathf.Clamp01(point - 0.001f), arcs[n].nesting.secondaryArcIndex), arcs[n].nesting.nestingCoef) + shiftVectors[n, k] * sizeMultiplier;
			}
		}
		else
		{
			for (int l = 0; l < segmNums[n]; l++)
			{
				float point = (float)l / (float)segmNums[n];
				arcPoints[n, l] = CalcShapePoint(point) + shiftVectors[n, l] * sizeMultiplier;
			}
		}
	}

	public void ResetArcNoise(int n, int noiseInd)
	{
		switch (arcs[n].spatialNoise[noiseInd].type)
		{
		case SpatialNoiseType.CubicRandomization:
		{
			for (int j = 0; j <= segmNums[n]; j++)
			{
				shiftVectors[n, j] = RandomVector3(arcs[n].spatialNoise[noiseInd].scale) * GetShiftCoef((float)j / (float)segmNums[n]);
			}
			break;
		}
		case SpatialNoiseType.TangentRandomization:
		{
			for (int k = 0; k <= segmNums[n]; k++)
			{
				arcTangentsShift[n, k * 2] = RandomXYQuaternion(arcs[n].spatialNoise[noiseInd].scale * GetShiftCoef((float)k / (float)segmNums[n]));
				arcTangentsShift[n, k * 2 + 1] = arcTangentsShift[n, k * 2];
			}
			break;
		}
		case SpatialNoiseType.BrokenTangentRandomization:
		{
			for (int i = 0; i <= segmNums[n]; i++)
			{
				arcTangentsShift[n, i * 2] = RandomXYQuaternion(arcs[n].spatialNoise[noiseInd].scale * GetShiftCoef((float)i / (float)segmNums[n]));
				arcTangentsShift[n, i * 2 + 1] = RandomXYQuaternion(arcs[n].spatialNoise[noiseInd].scale * GetShiftCoef((float)i / (float)segmNums[n]));
			}
			break;
		}
		}
	}

	protected float GetFlareBrightness(Vector3 currentCameraPosition, Vector3 flarePosition, FlareInfo flInfo, float multiplier = 1f)
	{
		float num = Mathf.Clamp((currentCameraPosition - flarePosition).magnitude, flInfo.maxBrightnessDistance, flInfo.minBrightnessDistance) - flInfo.maxBrightnessDistance;
		return Mathf.Lerp(flInfo.maxBrightness, flInfo.minBrightness, num / (flInfo.minBrightnessDistance - flInfo.maxBrightnessDistance)) * multiplier;
	}

	protected void SetFlares(int n)
	{
		float num = 1f;
		if (arcs[n].flaresOptions.startFlare.enabled)
		{
			startFlare.transform.position = resultingShape[0];
			if (arcs[n].flaresOptions.useNoiseMask)
			{
				num = arcs[n].flaresOptions.noiseMaskPowerCurve.Evaluate(noiseOffsets[n]);
			}
			startFlare.brightness = GetFlareBrightness(currentCamera.transform.position, resultingShape[0], arcs[n].flaresOptions.startFlare, arcs[n].sizeOptions.startWidthCurve.Evaluate(elapsedTime / lifetime) / maxStartWidth[n]) * num;
			startFlare.color = arcs[n].colorOptions.startColor.Evaluate(elapsedTime / lifetime);
		}
		if (arcs[n].flaresOptions.endFlare.enabled)
		{
			endFlare.transform.position = resultingShape[resultingShape.Length - 1];
			if (arcs[n].flaresOptions.useNoiseMask)
			{
				num = arcs[n].flaresOptions.noiseMaskPowerCurve.Evaluate(AddCyclicShift(noiseScale[n] - Mathf.Floor(noiseScale[n]), noiseOffsets[n], 1f));
			}
			if (arcs[n].sizeOptions.onlyStartWidth)
			{
				endFlare.brightness = GetFlareBrightness(currentCamera.transform.position, resultingShape[resultingShape.Length - 1], arcs[n].flaresOptions.endFlare, arcs[n].sizeOptions.startWidthCurve.Evaluate(elapsedTime / lifetime) / maxStartWidth[n]) * num;
			}
			else
			{
				endFlare.brightness = GetFlareBrightness(currentCamera.transform.position, resultingShape[resultingShape.Length - 1], arcs[n].flaresOptions.endFlare, arcs[n].sizeOptions.endWidthCurve.Evaluate(elapsedTime / lifetime) / maxEndWidth[n]) * num;
			}
			if (arcs[n].colorOptions.onlyStartColor)
			{
				endFlare.color = arcs[n].colorOptions.startColor.Evaluate(elapsedTime / lifetime);
			}
			else
			{
				endFlare.color = arcs[n].colorOptions.endColor.Evaluate(elapsedTime / lifetime);
			}
		}
	}

	public void Initialize()
	{
		oldShapeLength = shapeLength;
		bool flag = false;
		for (int i = 0; i < arcs.Length; i++)
		{
			for (int j = 0; j < arcs[i].emissionOptions.Length; j++)
			{
				if (emitterSystems[i][j] == null && arcs[i].emissionOptions[j].shurikenPrefab != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(arcs[i].emissionOptions[j].shurikenPrefab.gameObject);
					gameObject.name = "EmitterObject " + base.gameObject.name + " " + i + "," + j;
					emitterSystems[i][j] = gameObject.GetComponent<ParticleSystem>();
					ParticleSystem.EmissionModule emission = emitterSystems[i][j].emission;
					if (emission.enabled)
					{
						emission.enabled = false;
					}
					if (!arcs[i].emissionOptions[j].emitAfterRayDeath)
					{
						gameObject.transform.parent = base.transform;
					}
					else
					{
						emitterDestructors[i][j] = gameObject.AddComponent<ArcReactor_EmitterDestructor>();
						emitterDestructors[i][j].partSystem = emitterSystems[i][j];
						emitterDestructors[i][j].enabled = false;
					}
					gameObject.transform.position = base.transform.position;
					gameObject.transform.rotation = base.transform.rotation;
				}
			}
			if (arcs[i].lightsOptions.lights)
			{
				for (int k = 0; k < lightsCount[i]; k++)
				{
					UnityEngine.Object.Destroy(lights[i, k].gameObject);
				}
			}
			flag |= arcs[i].lightsOptions.lights;
			lightsCount[i] = Mathf.Max((int)(shapeLength * 2f / arcs[i].lightsOptions.lightsRange + 1f), 2);
			segmNums[i] = Mathf.Max((int)(shapeLength / (arcs[i].sizeOptions.segmentLength * sizeMultiplier)) + arcs[i].sizeOptions.minNumberOfSegments, 2);
			vertexCount[i] = segmNums[i] * (arcs[i].sizeOptions.numberOfSmoothingSegments + 1) + 1;
			oldVertexCount[i] = vertexCount[i];
			oldVertices[i] = new Vector3[vertexCount[i]];
			vertices[i] = new Vector3[vertexCount[i]];
			lrends[i].SetVertexCount(vertexCount[i]);
			if (arcs[i].flaresOptions.startFlare.enabled && startFlare == null)
			{
				GameObject gameObject2 = new GameObject(base.gameObject.name + "_Start_flare");
				gameObject2.transform.parent = base.transform;
				startFlare = gameObject2.gameObject.AddComponent<LensFlare>();
				startFlare.flare = arcs[i].flaresOptions.startFlare.flare;
				startFlare.fadeSpeed = arcs[i].flaresOptions.startFlare.fadeSpeed;
			}
			if (arcs[i].flaresOptions.endFlare.enabled && endFlare == null)
			{
				GameObject gameObject3 = new GameObject(base.gameObject.name + "_End_flare");
				gameObject3.transform.parent = base.transform;
				endFlare = gameObject3.gameObject.AddComponent<LensFlare>();
				endFlare.flare = arcs[i].flaresOptions.endFlare.flare;
				endFlare.fadeSpeed = arcs[i].flaresOptions.endFlare.fadeSpeed;
			}
		}
		arcPoints = new Vector3[arcs.Length, segmNums.Max() + 2];
		shiftVectors = new Vector3[arcs.Length, segmNums.Max() + 2];
		arcTangents = new Vector3[arcs.Length, segmNums.Max() + 2];
		arcTangentsShift = new Quaternion[arcs.Length, segmNums.Max() * 2 + 2];
		for (int l = 0; l < arcs.Length; l++)
		{
			ResetArc(l);
		}
		if (!flag)
		{
			return;
		}
		lights = new Light[arcs.Length, lightsCount.Max()];
		lightsTransforms = new Transform[arcs.Length, lightsCount.Max() + 1];
		for (int m = 0; m < arcs.Length; m++)
		{
			if (arcs[m].lightsOptions.lights)
			{
				for (int n = 0; n < lightsCount[m]; n++)
				{
					GameObject gameObject4 = new GameObject("ArcLight");
					gameObject4.transform.parent = base.transform;
					lightsTransforms[m, n] = gameObject4.transform;
					lights[m, n] = gameObject4.AddComponent<Light>();
					lights[m, n].type = LightType.Point;
					lights[m, n].renderMode = arcs[m].lightsOptions.renderMode;
					lights[m, n].range = arcs[m].lightsOptions.lightsRange;
				}
			}
		}
	}

	protected void SetShapeArrays()
	{
		int num = (oldShapeTransformsSize = Mathf.Max(shapeTransforms.Length, shapePoints.Length));
		if (closedShape)
		{
			shapeKeyLocations = new float[num + 1];
			shapeKeyNormalizedLocations = new float[num + 1];
		}
		else
		{
			shapeKeyLocations = new float[num];
			shapeKeyNormalizedLocations = new float[num];
		}
		shapeTangents = new Vector3[num];
	}

	private void Start()
	{
		if (Mathf.Max(shapeTransforms.Length, shapePoints.Length) < 2)
		{
			Debug.LogError(base.gameObject.name + " : There should be at least 2 shape transforms or points for correct shape calculation. Deactivating component.");
			base.enabled = false;
			return;
		}
		if (arcs.Length == 0)
		{
			Debug.LogError(base.gameObject.name + " : No arcs set up. Deactivating component.");
			base.enabled = false;
			return;
		}
		if (lifetime == 0f)
		{
			Debug.LogWarning(base.gameObject.name + " : Lifetime set to zero. That's a waste of a perfectly good component.");
		}
		if (oscillationNormal == Vector3.zero)
		{
			Debug.LogWarning(base.gameObject.name + " : Oscillation normal set to zero. Oscillation planes will be unpredictable.");
		}
		if (easeInOutOptions.useEaseInOut && easeInOutOptions.distance == 0f)
		{
			Debug.LogWarning(base.gameObject.name + " : EaseInOut enabled but it's distance set to zero. It will have no effect except performance hit.");
		}
		for (int i = 0; i < arcs.Length; i++)
		{
			if ((arcs[i].flaresOptions.startFlare.enabled || arcs[i].flaresOptions.endFlare.enabled) && currentCamera == null)
			{
				currentCamera = Camera.main;
			}
			if (arcs[i].sizeOptions.segmentLength <= 0f)
			{
				Debug.LogWarning(base.gameObject.name + " : Segment Length of Arc #" + i + " is set to zero or lower. It would cause unexpected behaviour or division by zero errors.");
			}
			if (arcs[i].colorOptions.startColor.colorKeys.Length == 2 && arcs[i].colorOptions.startColor.colorKeys[0].color == new Color(0f, 0f, 0f, 255f) && arcs[i].colorOptions.startColor.colorKeys[0].time == 0f && arcs[i].colorOptions.startColor.colorKeys[1].color == new Color(0f, 0f, 0f, 255f) && arcs[i].colorOptions.startColor.colorKeys[1].time == 1f && arcs[i].colorOptions.startColor.alphaKeys.Length == 2 && arcs[i].colorOptions.startColor.alphaKeys[0].alpha == 0f && arcs[i].colorOptions.startColor.alphaKeys[0].time == 0f && arcs[i].colorOptions.startColor.alphaKeys[1].alpha == 0f && arcs[i].colorOptions.startColor.alphaKeys[1].time == 1f)
			{
				Debug.LogWarning(base.gameObject.name + " : Start color gradient has not been assigned to Arc #" + i + ", arc probably wouldn't be visible. Set color options to see the arc.");
			}
			if (arcs[i].sizeOptions.segmentLength == 0f)
			{
				Debug.LogWarning(base.gameObject.name + " : Segment length of Arc #" + i + " is set to zero, arc will always be consisting of only 2 vertexes");
			}
			if (arcs[i].sizeOptions.startWidthCurve.keys.Length == 0 && (arcs[i].sizeOptions.onlyStartWidth || arcs[i].sizeOptions.endWidthCurve.keys.Length == 0))
			{
				Debug.LogWarning(base.gameObject.name + " : Width curves has not been assigned to Arc #" + i + ", setting default curves.");
				arcs[i].sizeOptions.startWidthCurve.AddKey(0f, 0.5f);
				if (!arcs[i].sizeOptions.onlyStartWidth)
				{
					arcs[i].sizeOptions.endWidthCurve.AddKey(0f, 0.5f);
				}
			}
			if (arcs[i].material == null)
			{
				Debug.LogWarning(base.gameObject.name + " : Material have not been assigned to Arc #" + i + ", setting default material.");
				arcs[i].material = GetDefaultMaterial();
			}
			if (arcs[i].nesting.Nested && arcs[i].nesting.parentArcIndex > i)
			{
				Debug.LogWarning(base.gameObject.name + " : Arc #" + i + " is nested to arc with higher index. That's not recommended because of vertex caching.");
			}
			for (int j = 0; j < arcs[i].oscillations.Length; j++)
			{
				if (arcs[i].oscillations[j].amplitude == 0f)
				{
					Debug.LogWarning(base.gameObject.name + " : Amplitude of oscillation #" + j + " of Arc #" + i + " set to zero. It will have no effect except performance hit");
				}
				if (arcs[i].oscillations[j].wavelength == 0f)
				{
					Debug.LogError(base.gameObject.name + " : Wavelength of oscillation #" + j + " of Arc #" + i + " set to zero. That makes no mathematical sense. Disabling component");
					base.enabled = false;
					return;
				}
			}
		}
		emitterSystems = new ParticleSystem[arcs.Length][];
		particleBuffers = new ParticleSystem.Particle[arcs.Length][][];
		emitterDestructors = new ArcReactor_EmitterDestructor[arcs.Length][];
		for (int k = 0; k < arcs.Length; k++)
		{
			emitterSystems[k] = new ParticleSystem[arcs[k].emissionOptions.Length];
			emitterDestructors[k] = new ArcReactor_EmitterDestructor[arcs[k].emissionOptions.Length];
			particleBuffers[k] = new ParticleSystem.Particle[arcs[k].emissionOptions.Length][];
			for (int l = 0; l < arcs[k].emissionOptions.Length; l++)
			{
				particleBuffers[k][l] = new ParticleSystem.Particle[arcs[k].emissionOptions[l].shurikenPrefab.maxParticles];
			}
		}
		lrends = new LineRenderer[arcs.Length];
		segmNums = new int[arcs.Length];
		lightsCount = new int[arcs.Length];
		vertexCount = new int[arcs.Length];
		oldVertexCount = new int[arcs.Length];
		noiseOffsets = new float[arcs.Length];
		noiseScale = new float[arcs.Length];
		maxStartWidth = new float[arcs.Length];
		maxEndWidth = new float[arcs.Length];
		coreCoefs = new float[arcs.Length];
		vertices = new Vector3[arcs.Length][];
		oldVertices = new Vector3[arcs.Length][];
		SetShapeArrays();
		for (int m = 0; m < arcs.Length; m++)
		{
			GameObject gameObject = new GameObject("ArcLineRenderer");
			gameObject.transform.parent = base.transform;
			gameObject.layer = linerendererLayer.LayerIndex;
			lrends[m] = gameObject.AddComponent<LineRenderer>();
			lrends[m].material = arcs[m].material;
			lrends[m].shadowCastingMode = ShadowCastingMode.Off;
			lrends[m].receiveShadows = false;
			if (customSorting)
			{
				lrends[m].sortingLayerName = sortingLayerName;
				lrends[m].sortingOrder = sortingOrder;
			}
			if (arcs[m].textureOptions.shapeTexture != null)
			{
				lrends[m].material.SetTexture("_MainTex", arcs[m].textureOptions.shapeTexture);
			}
			if (arcs[m].textureOptions.noiseTexture != null)
			{
				lrends[m].material.SetTexture("_NoiseMask", arcs[m].textureOptions.noiseTexture);
			}
			float num = 0f;
			if (arcs[m].flaresOptions.startFlare.enabled)
			{
				for (int n = 0; n <= 10; n++)
				{
					if (num < arcs[m].sizeOptions.startWidthCurve.Evaluate((float)n / 10f))
					{
						num = arcs[m].sizeOptions.startWidthCurve.Evaluate((float)n / 10f);
					}
				}
				maxStartWidth[m] = num;
			}
			if (!arcs[m].flaresOptions.endFlare.enabled)
			{
				continue;
			}
			if (arcs[m].sizeOptions.onlyStartWidth)
			{
				if (arcs[m].flaresOptions.startFlare.enabled)
				{
					maxEndWidth[m] = maxStartWidth[m];
					continue;
				}
				for (int num2 = 0; num2 <= 10; num2++)
				{
					if (num < arcs[m].sizeOptions.startWidthCurve.Evaluate((float)num2 / 10f))
					{
						num = arcs[m].sizeOptions.startWidthCurve.Evaluate((float)num2 / 10f);
					}
				}
				maxStartWidth[m] = num;
				maxEndWidth[m] = maxStartWidth[m];
				continue;
			}
			num = 0f;
			for (int num3 = 0; num3 <= 10; num3++)
			{
				if (num < arcs[m].sizeOptions.endWidthCurve.Evaluate((float)num3 / 10f))
				{
					num = arcs[m].sizeOptions.endWidthCurve.Evaluate((float)num3 / 10f);
				}
			}
			maxEndWidth[m] = num;
		}
		CalculateShape();
		if (ArcReactor_Manager.Instance != null)
		{
			ArcReactor_Manager.Instance.AddArcSystem(this);
		}
	}

	public Vector3 RandomVector3(float range)
	{
		return new Vector3(UnityEngine.Random.Range(0f - range, range), UnityEngine.Random.Range(0f - range, range), UnityEngine.Random.Range(0f - range, range));
	}

	public void DestroyArc()
	{
		for (int i = 0; i < Mathf.Min(shapeTransforms.Length, transformsDestructionFlags.Length); i++)
		{
			if (transformsDestructionFlags[i])
			{
				UnityEngine.Object.Destroy(shapeTransforms[i].gameObject);
			}
		}
		for (int j = 0; j < arcs.Length; j++)
		{
			for (int k = 0; k < arcs[j].emissionOptions.Length; k++)
			{
				if (arcs[j].emissionOptions[k].emitAfterRayDeath)
				{
					emitterDestructors[j][k].onlyDisable = false;
					emitterDestructors[j][k].enabled = true;
				}
			}
		}
		if (playbackMessages)
		{
			messageReciever.SendMessage("ArcReactorPlayback", this);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void DisableArc()
	{
		for (int i = 0; i < Mathf.Min(shapeTransforms.Length, transformsDestructionFlags.Length); i++)
		{
			if (transformsDestructionFlags[i])
			{
				UnityEngine.Object.Destroy(shapeTransforms[i].gameObject);
			}
		}
		for (int j = 0; j < arcs.Length; j++)
		{
			for (int k = 0; k < arcs[j].emissionOptions.Length; k++)
			{
				if (arcs[j].emissionOptions[k].emitAfterRayDeath)
				{
					emitterDestructors[j][k].onlyDisable = true;
					emitterDestructors[j][k].enabled = true;
				}
			}
		}
		if (playbackMessages)
		{
			messageReciever.SendMessage("ArcReactorPlayback", this);
		}
		base.gameObject.SetActive(false);
	}

	public void EnableArc()
	{
		for (int i = 0; i < arcs.Length; i++)
		{
			for (int j = 0; j < arcs[i].emissionOptions.Length; j++)
			{
				emitterSystems[i][j].gameObject.SetActive(true);
				if (emitterDestructors[i][j] != null)
				{
					emitterDestructors[i][j].enabled = false;
				}
			}
		}
		base.gameObject.SetActive(true);
	}

	private void Update()
	{
		for (int i = 0; i < arcs.Length; i++)
		{
			OscillationInfo[] oscillations = arcs[i].oscillations;
			foreach (OscillationInfo oscillationInfo in oscillations)
			{
				oscillationInfo.phase += oscillationInfo.phaseMovementSpeed * Time.deltaTime;
				if (oscillationInfo.phase > 360f)
				{
					oscillationInfo.phase -= 360f;
				}
				if (oscillationInfo.phase < 0f)
				{
					oscillationInfo.phase += 360f;
				}
			}
		}
		if (!freeze)
		{
			if (!playBackward)
			{
				elapsedTime += Time.deltaTime;
			}
			else
			{
				elapsedTime -= Time.deltaTime;
			}
		}
		if (elapsedTime > lifetime)
		{
			switch (playbackType)
			{
			case ArcsPlaybackType.once:
				if (playbackMessages)
				{
					messageReciever.SendMessage("ArcReactorPlayback", this);
				}
				if (ArcReactor_PoolManager.Instance != null)
				{
					ArcReactor_PoolManager.Instance.SetEntityAsFree(this);
				}
				else
				{
					DestroyArc();
				}
				break;
			case ArcsPlaybackType.loop:
				elapsedTime -= lifetime;
				if (playbackMessages)
				{
					messageReciever.SendMessage("ArcReactorPlayback", this);
				}
				break;
			case ArcsPlaybackType.pingpong:
				playBackward = true;
				elapsedTime = lifetime;
				if (playbackMessages)
				{
					messageReciever.SendMessage("ArcReactorPlayback", this);
				}
				break;
			case ArcsPlaybackType.clamp:
				elapsedTime = lifetime;
				freeze = true;
				if (playbackMessages)
				{
					messageReciever.SendMessage("ArcReactorPlayback", this);
				}
				break;
			case ArcsPlaybackType.pingpong_once:
				playBackward = true;
				elapsedTime = lifetime;
				if (playbackMessages)
				{
					messageReciever.SendMessage("ArcReactorPlayback", this);
				}
				break;
			case ArcsPlaybackType.pingpong_clamp_once:
				elapsedTime = lifetime;
				freeze = true;
				playBackward = true;
				if (playbackMessages)
				{
					messageReciever.SendMessage("ArcReactorPlayback", this);
				}
				break;
			}
		}
		if (!(elapsedTime < 0f))
		{
			return;
		}
		playBackward = false;
		elapsedTime = 0f;
		if (playbackType == ArcsPlaybackType.pingpong_clamp_once || playbackType == ArcsPlaybackType.pingpong_once)
		{
			if (playbackMessages)
			{
				messageReciever.SendMessage("ArcReactorPlayback", this);
			}
			if (ArcReactor_PoolManager.Instance != null)
			{
				ArcReactor_PoolManager.Instance.SetEntityAsFree(this);
			}
			else
			{
				DestroyArc();
			}
		}
	}

	public Vector3 GetArcEndPosition(int arcIndex)
	{
		return GetArcPoint(GetArcEndPoint(arcIndex), arcIndex);
	}

	public float GetArcEndPoint(int arcIndex)
	{
		switch (arcs[arcIndex].propagationOptions.propagationType)
		{
		case PropagationType.globalSpaceSpeed:
			return Mathf.Min((float)vertexCount[arcIndex] * arcs[arcIndex].propagationOptions.globalSpeed * elapsedTime / shapeLength, vertexCount[arcIndex]) / (float)vertexCount[arcIndex];
		case PropagationType.localTimeCurve:
			return Mathf.Clamp01(arcs[arcIndex].propagationOptions.timeCurve.Evaluate(elapsedTime / lifetime));
		case PropagationType.instant:
			return 1f;
		default:
			return 1f;
		}
	}

	public void ExcludeFromPool()
	{
		if (ArcReactor_PoolManager.Instance != null)
		{
			ArcReactor_PoolManager.Instance.activeEntities.Remove(this);
		}
	}

	private void LateUpdate()
	{
		float time = elapsedTime / lifetime;
		CalculateShape();
		if (localSpaceOcillations)
		{
			oscNormal = base.transform.rotation * oscillationNormal;
		}
		else
		{
			oscNormal = oscillationNormal;
		}
		for (int i = 0; i < arcs.Length; i++)
		{
			vertices[i].CopyTo(oldVertices[i], 0);
			Color color = arcs[i].colorOptions.startColor.Evaluate(time);
			Color color2 = ((!arcs[i].colorOptions.onlyStartColor) ? arcs[i].colorOptions.endColor.Evaluate(time) : color);
			Color color3 = arcs[i].colorOptions.coreColor.Evaluate(time);
			lrends[i].material.SetColor("_StartColor", color);
			lrends[i].material.SetColor("_EndColor", color2);
			lrends[i].material.SetColor("_CoreColor", color3);
			if (arcs[i].colorOptions.coreJitter > 0f)
			{
				coreCoefs[i] = arcs[i].colorOptions.coreCurve.Evaluate(time) + UnityEngine.Random.Range((0f - arcs[i].colorOptions.coreJitter) * 0.5f, arcs[i].colorOptions.coreJitter * 0.5f);
				lrends[i].material.SetFloat("_CoreCoef", coreCoefs[i]);
			}
			else
			{
				coreCoefs[i] = arcs[i].colorOptions.coreCurve.Evaluate(time);
				lrends[i].material.SetFloat("_CoreCoef", coreCoefs[i]);
			}
			switch (arcs[i].colorOptions.fade)
			{
			case FadeTypes.none:
				lrends[i].material.SetFloat("_FadeLevel", 0.001f);
				break;
			case FadeTypes.relativePoint:
				lrends[i].material.SetFloat("_FadeLevel", Mathf.Max(arcs[i].colorOptions.fadePoint, 0.001f));
				break;
			case FadeTypes.worldspacePoint:
				lrends[i].material.SetFloat("_FadeLevel", Mathf.Max(Mathf.Clamp01(arcs[i].colorOptions.fadePoint / shapeLength), 0.001f));
				break;
			}
			switch (arcs[i].colorOptions.frontFade)
			{
			case FadeTypes.none:
				lrends[i].material.SetFloat("_FrontFadeLevel", 0.001f);
				break;
			case FadeTypes.relativePoint:
				lrends[i].material.SetFloat("_FrontFadeLevel", Mathf.Max(arcs[i].colorOptions.frontFadePoint, 0.001f));
				break;
			case FadeTypes.worldspacePoint:
				lrends[i].material.SetFloat("_FrontFadeLevel", Mathf.Max(Mathf.Clamp01(arcs[i].colorOptions.frontFadePoint / shapeLength), 0.001f));
				break;
			}
			float num = arcs[i].sizeOptions.startWidthCurve.Evaluate(time) * sizeMultiplier;
			float num2 = ((!arcs[i].sizeOptions.onlyStartWidth) ? (arcs[i].sizeOptions.endWidthCurve.Evaluate(time) * sizeMultiplier) : num);
			lrends[i].SetWidth(num, num2);
			float num3 = vertexCount[i];
			switch (arcs[i].propagationOptions.propagationType)
			{
			case PropagationType.globalSpaceSpeed:
				num3 = Mathf.Min((float)vertexCount[i] * arcs[i].propagationOptions.globalSpeed * elapsedTime / shapeLength, vertexCount[i]);
				lrends[i].SetVertexCount(Mathf.CeilToInt(num3));
				break;
			case PropagationType.localTimeCurve:
				num3 = Mathf.Min((float)vertexCount[i] * arcs[i].propagationOptions.timeCurve.Evaluate(time), vertexCount[i]);
				lrends[i].SetVertexCount(Mathf.Max(Mathf.CeilToInt(num3), 0));
				break;
			}
			if (arcs[i].textureOptions.noiseTexture != null)
			{
				lrends[i].material.SetFloat("_NoiseCoef", arcs[i].textureOptions.noiseCoef.Evaluate(time));
				if (arcs[i].textureOptions.animateTexture)
				{
					noiseOffsets[i] += arcs[i].textureOptions.noiseSpeed * Time.deltaTime;
					if (noiseOffsets[i] > 1f)
					{
						noiseOffsets[i] -= 1f;
					}
					if (noiseOffsets[i] < 0f)
					{
						noiseOffsets[i] += 1f;
					}
					noiseScale[i] = num3 / (float)vertexCount[i] * shapeLength / arcs[i].textureOptions.tileSize;
					lrends[i].material.SetTextureScale("_NoiseMask", new Vector2(noiseScale[i], 1f));
					lrends[i].material.SetTextureOffset("_NoiseMask", new Vector2(noiseOffsets[i], 1f));
				}
				else
				{
					noiseScale[i] = num3 / (float)vertexCount[i] * shapeLength / arcs[i].textureOptions.tileSize;
					lrends[i].material.SetTextureScale("_NoiseMask", new Vector2(noiseScale[i], 1f));
				}
			}
			SetFlares(i);
			SetArcShape(i);
			Vector3 vector = CalcArcPoint(0f, i);
			Vector3 zero = Vector3.zero;
			Vector3 direction = Vector3.zero;
			int num4 = 1;
			for (int j = 0; (float)j < num3 - 1f; j++)
			{
				float num5 = (float)j / (float)vertexCount[i];
				if (arcs[i].sizeOptions.snapSegmentsToShape && (double)(Mathf.Abs(shapeKeyNormalizedLocations[num4] - num5) * (float)vertexCount[i]) < 0.5)
				{
					num5 = shapeKeyNormalizedLocations[num4];
					vector = shapeTransforms[num4].position;
					num4++;
				}
				zero = CalcArcPoint((float)(j + 1) / (float)vertexCount[i], i);
				direction = zero - vector;
				vertices[i][j] = vector + CalculateOscillationShift(direction, num5 * shapeLength, i) * GetShiftCoef(num5) + CalculateCurveShift(direction, num5 * ShapeLength, i);
				lrends[i].SetPosition(j, vertices[i][j]);
				vector = zero;
			}
			if (Mathf.CeilToInt(num3) > 0 && Mathf.CeilToInt(num3) <= vertexCount[i])
			{
				vertices[i][Mathf.CeilToInt(num3) - 1] = CalculateOscillationShift(direction, shapeLength * num3 / (float)vertexCount[i], i) * GetShiftCoef(num3 / (float)vertexCount[i]) + CalcArcPoint(num3 / (float)vertexCount[i], i);
				lrends[i].SetPosition(Mathf.CeilToInt(num3) - 1, vertices[i][Mathf.CeilToInt(num3) - 1]);
			}
			for (int k = 0; k < arcs[i].emissionOptions.Length; k++)
			{
				if (arcs[i].emissionOptions[k].emit)
				{
					int value = (int)(UnityEngine.Random.value + num3 / (float)vertexCount[i] * shapeLength * arcs[i].emissionOptions[k].particlesPerMeter * Time.deltaTime * arcs[i].emissionOptions[k].emissionDuringLifetime.Evaluate(time));
					float num6 = num3 / (float)vertexCount[i];
					float num7 = arcs[i].emissionOptions[k].radiusCoefDuringLifetime.Evaluate(time);
					float num8 = arcs[i].emissionOptions[k].directionDuringLifetime.Evaluate(time);
					float num9 = 0f;
					Vector3 one = Vector3.one;
					Vector3 vector2 = ((emitterSystems[i][k].simulationSpace != ParticleSystemSimulationSpace.Local) ? Vector3.zero : (-emitterSystems[i][k].transform.position));
					Color a = color;
					Color b = color2;
					int particles = emitterSystems[i][k].GetParticles(particleBuffers[i][k]);
					value = Mathf.Clamp(value, 0, emitterSystems[i][k].maxParticles - particles);
					emitterSystems[i][k].Emit(value);
					emitterSystems[i][k].GetParticles(particleBuffers[i][k]);
					for (int l = 0; l < value; l++)
					{
						num9 = 0.001f + UnityEngine.Random.value * (num6 - 0.002f);
						one = UnityEngine.Random.rotation * Vector3.forward;
						float num10 = Mathf.Lerp(num, num2, num9) * num7;
						Vector3 arcPoint = GetArcPoint(num9, i);
						Vector3 normalized = (GetArcPoint(num9 + 0.001f, i) - arcPoint).normalized;
						particleBuffers[i][k][particles + l].position = Vector3.Lerp(arcPoint, GetOldArcPoint(num9, i), UnityEngine.Random.value) + one * num10 * sizeMultiplier + vector2;
						particleBuffers[i][k][particles + l].startSize *= sizeMultiplier;
						particleBuffers[i][k][particles + l].startColor = Color.Lerp(particleBuffers[i][k][particles + l].startColor, Color.Lerp(a, b, num9), arcs[i].emissionOptions[k].arcColorInfluence);
						particleBuffers[i][k][particles + l].velocity = (one * (1f - Mathf.Clamp01(Mathf.Abs(num8))) + normalized * num8) * particleBuffers[i][k][particles + l].velocity.magnitude;
					}
					emitterSystems[i][k].SetParticles(particleBuffers[i][k], particles + value);
				}
			}
			if (!arcs[i].lightsOptions.lights || arcs[i].lightsOptions.priority > performancePriority)
			{
				continue;
			}
			for (int m = 0; m < lightsCount[i]; m++)
			{
				if ((float)m / (float)lightsCount[i] <= num3 / (float)vertexCount[i])
				{
					lights[i, m].enabled = true;
					Color a2 = (arcs[i].colorOptions.onlyStartColor ? color : Color.Lerp(color, color2, (float)m / (float)(lightsCount[i] - 1)));
					lights[i, m].color = Color.Lerp(a2, color3, coreCoefs[i] / 2f);
					if (!arcs[i].sizeOptions.onlyStartWidth)
					{
						lights[i, m].intensity = arcs[i].lightsOptions.lightsIntensityMultiplyer * Mathf.Lerp(num, num2, (float)m / (float)(segmNums[i] + 1));
					}
					else
					{
						lights[i, m].intensity = arcs[i].lightsOptions.lightsIntensityMultiplyer * num;
					}
					lightsTransforms[i, m].position = GetArcPoint((float)m / (float)(lightsCount[i] - 1), i);
				}
				else
				{
					lights[i, m].enabled = false;
				}
			}
		}
	}
}
