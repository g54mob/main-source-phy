using System;
using System.Collections;
using System.Collections.Generic;
using BesiegeDlc;
using MultithreadCoroutines;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[AddComponentMenu("Water/Controllers/Water Controller")]
public class WaterController : ExternalForceWater
{
	public enum SurfaceTensionType
	{
		minusVel = 0,
		minusVelY = 1,
		WaterUp = 2,
		Reflect = 3
	}

	private enum HeightMapType
	{
		Color = 0,
		GrayScale = 1
	}

	private const int MAX_TRACKED = 128;

	private const int MAX_AUDIO = 3;

	public static WaterController buildInstance;

	public static WaterController simInstance;

	[Header("General Water Variables")]
	public float updateSpeed = 0.25f;

	public float stabilitySkipAmount = 5f;

	public float waterUpForce = 10f;

	public float angularDrag = 1f;

	public float drag = 1f;

	public float surfaceBreak = 2f;

	[HideInInspector]
	public BoxCollider WaterCollider;

	[HideInInspector]
	public bool useCollider;

	public float particleImpactScaleMax = 5000f;

	public float particleImpactScaleMin = 500f;

	public Material waterMaterial;

	public CalmZoneController calmZController;

	[SerializeField]
	protected static WaterLod waterLod;

	[SerializeField]
	protected Transform meshTransform;

	[Header("Audio Variables")]
	public AudioSource audioSrc;

	public AudioClip[] WaterSplashSoundsSmall;

	public AudioClip[] WaterSplashSounds;

	public AudioClip[] WaterSplashSoundsLarge;

	public static float waterPhysicsOffset = 1f;

	public static float timeOffset = 0f;

	public static float globalSpeed = 0f;

	[HideInInspector]
	public Vector3 waterPower;

	[HideInInspector]
	public static int aerodynamicCount = 0;

	private bool gotWaterHeightThisFrame;

	protected ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	protected ParticleSystem.EmitParams foamEmitter = default(ParticleSystem.EmitParams);

	private bool setupDone;

	private static bool startIsDone = false;

	public static bool isDisabled = true;

	private bool setupInProgress;

	private float timeTillUpdateWaterBlocks;

	private Vector3 waterUpDir;

	private Bounds waterBounds;

	private float stabilitySkip;

	private ExternalForceObject EFO;

	private ExternalForceObject FixedEFO;

	private Vector3 newPoint;

	private Vector3 localWaterUp;

	private Quaternion rbRot;

	private Vector3 pos;

	private Vector3 corner;

	private Vector3 vel;

	private float velMagnitude;

	private Vector3 fixedVel;

	private float sArea;

	[HideInInspector]
	public static float waterTransformHeight;

	public static Transform waterTransform;

	private bool firstRun = true;

	private Task advAeroDyn;

	private static AnimationCurve curveExponent = new AnimationCurve();

	[Header("Debug")]
	public SurfaceTensionType sType;

	[Tooltip("Scaled x and z velocity with this number when MinusVelY is selected")]
	public float velYzxScale = 0.25f;

	public bool logDragInterperation;

	public float logVelocity = 2f;

	public float nonLogVelocityScale = 0.2f;

	public bool simulateWave = true;

	public static bool simulateWaves = true;

	private bool uniformSkip;

	[Header("Debug - Wheel Limiter")]
	public bool wheelForceLimiter;

	public float wheelForceValue = 600f;

	public static bool WheelMotorForceLimiter = false;

	public static float WheelMotorForceValue = 600f;

	[Header("Debug - Wing Panel")]
	public bool axialMassChange;

	public float axialMass = 0.1f;

	public static bool WingPalenMassChange = false;

	public static float WingMass = 0.1f;

	[Header("Debug - Special water force for Alone blocks")]
	public bool aloneCheck;

	private static bool hasDefaults;

	private static Texture2D heightMap1;

	private static Texture2D heightMap2;

	private static Vector2 heightMap1Size;

	private static Vector2 heightMap2Size;

	private static float[] heightMapColor1;

	private static Color[] heightMapColor2;

	private static Vector2 heightMapTiling1;

	private static Vector2 heightMapTiling2;

	private Camera cam;

	[Header("Debug - Draws")]
	public bool drawDebugRayGrid;

	private bool flipper;

	private int smallParticles;

	private int mediumParticles;

	private int largeParticles;

	private int splashedQueuedForAudio;

	private Vector3[] splashPositions = new Vector3[128];

	private float splashCombineDistanace = 20f;

	private List<AudioSource> splashes = new List<AudioSource>();

	private int currentSplash;

	private static float waveSpeed;

	private static float wave1Scale;

	private static float wave2Scale;

	private static float detailHeight;

	private static float microDetailHeight;

	private static float bigWaveHeight;

	private static float bigWaveScale;

	private static Vector2 wavePos = new Vector2(0f, 0f);

	private static CalmZoneController calmController;

	public static bool Exist = false;

	private static float meshScale = 1f;

	public static bool WaterBoundsExceedsTop = false;

	public static bool WaterBoundsExceedsBottom = false;

	private static Vector2 calmRelation;

	private static Vector4 fullInt;

	private static uint index = 0u;

	private static uint cellContent = 0u;

	private static float calmSqr = 0f;

	private static float calm = 0f;

	private static float calmRad = 0f;

	private static int i = 0;

	protected static Vector2 waveHeight;

	protected static Vector2 uv1;

	protected static Vector2 uv2;

	protected static Vector2 uv3;

	protected static Vector2 uv4;

	protected static float w;

	protected static float height;

	protected static float height1;

	protected static float height2;

	protected static float height3;

	protected static float height4;

	protected static float height5;

	protected static float height6;

	private static Color black = Color.black;

	private static Color height2Full = black;

	private static float uPixelIndex;

	private static float vPixelIndex;

	private static float ftopLeft = 0f;

	private static float ftopRight = 0f;

	private static float fbottomLeft = 0f;

	private static float fbottomRight = 0f;

	private static float pixelDifferenceInv;

	private static float pixelDifference;

	private static float result1;

	private static float result2;

	private static int uMin;

	private static int uMax;

	private static int vMin;

	private static int vMax;

	private static Color topLeft;

	private static Color topRight;

	private static Color bottomLeft;

	private static Color bottomRight;

	private static Color result1Color;

	private static Color result2Color;

	public static WaterController currentInstance
	{
		get
		{
			return (!(simInstance != null)) ? buildInstance : simInstance;
		}
	}

	public static WaterLod WaterLOD
	{
		get
		{
			return waterLod;
		}
	}

	public static void ResetShaderTime()
	{
		timeOffset = 0f;
		globalSpeed = 1f;
		Shader.SetGlobalFloat("_TimeOffset", 0f);
		Shader.SetGlobalFloat("_TimeSpeed", 1f);
	}

	public static void SetHeight(float h)
	{
		waterTransformHeight = h;
		if ((bool)waterLod)
		{
			Transform transform = waterLod.transform;
			transform.position = new Vector3(transform.position.x, h, transform.position.z);
		}
		if ((bool)buildInstance)
		{
			Transform transform = buildInstance.transform;
			transform.position = new Vector3(transform.position.x, h, transform.position.z);
			buildInstance.calmZController.SetHeight();
		}
	}

	internal void Awake()
	{
		if (!DlcManager.Instance.HasPurchasedDlc(DlcManager.DlcType.Water))
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		if (!DlcManager.Instance.HasPurchasedDlc(DlcManager.DlcType.Water))
		{
			Exist = false;
			SceneManager.LoadScene("INITIALISER", LoadSceneMode.Single);
			UnityEngine.Object.DestroyImmediate(this);
			return;
		}
		waterTransform = base.transform;
		waterTransformHeight = base.transform.position.y;
		Exist = true;
		if (base.isSimulating)
		{
			aerodynamicCount = 0;
		}
	}

	protected override void Start()
	{
		if (!base.isSimulating && base.transform.root != ReferenceMaster.physicsGoalInstance)
		{
			buildInstance = this;
		}
		else
		{
			simInstance = this;
		}
		cam = Camera.main;
		if (waterMaterial == null)
		{
			simulateWave = false;
			Debug.LogError("missing material or wrong material in WaterController");
			return;
		}
		timeTillUpdateWaterBlocks = updateSpeed;
		if (calmZController != null)
		{
			calmController = calmZController;
		}
		else
		{
			calmZController = UnityEngine.Object.FindObjectOfType<CalmZoneController>();
			calmController = calmZController;
		}
		if (waterLod == null)
		{
			waterLod = UnityEngine.Object.FindObjectOfType<WaterLod>();
		}
		waterTransform = base.transform;
		if (waterTransformHeight != 0f && StatMaster.isMP)
		{
			SetHeight(waterTransformHeight);
		}
		else
		{
			SetHeight(base.transform.position.y);
		}
		if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			if (!setupInProgress)
			{
				setupInProgress = true;
				StartCoroutine(Setup());
			}
			isDisabled = false;
			foamEmitter.applyShapeToPosition = false;
			startIsDone = true;
			return;
		}
		base.Start();
		if (!base.isSimulating)
		{
			startIsDone = true;
			return;
		}
		stabilitySkip = 0f;
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			base.enabled = false;
		}
		if (useCollider && WaterCollider != null)
		{
			waterBounds = WaterCollider.bounds;
		}
		UnityEngine.Object.Destroy(WaterCollider, 1f);
		waterUpDir = ((!(base.transform.localScale.x < 0f)) ? base.transform.up : (-base.transform.up));
		waterPower = waterUpDir * waterUpForce;
		if (!setupInProgress)
		{
			setupInProgress = true;
			StartCoroutine(Setup());
		}
		isDisabled = false;
		foamEmitter.applyShapeToPosition = false;
		startIsDone = true;
	}

	public void UpdateBounds()
	{
		waterUpDir = ((!(base.transform.localScale.x < 0f)) ? base.transform.up : (-base.transform.up));
		waterPower = waterUpDir * waterUpForce;
		worldMatrix = base.transform.worldToLocalMatrix;
	}

	private void OnEnable()
	{
		if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			SetDefaults(waterMaterial);
			isDisabled = false;
			return;
		}
		ReferenceMaster.UpdateExtrenalForceArray();
		if (!setupDone && !setupInProgress)
		{
			Start();
		}
		SetDefaults(waterMaterial);
		if (meshTransform == null)
		{
			meshTransform = waterLod.waterLODs[0].LODMeshrenderer[0].transform;
		}
		meshScale = meshTransform.lossyScale.y;
		isDisabled = false;
	}

	public static void SetMaterial(Material m, Material f)
	{
		if (!(buildInstance == null))
		{
			waterLod.SetMaterial(m, f);
			buildInstance.waterMaterial = m;
			buildInstance.calmZController.SetMat(m);
			if ((bool)simInstance)
			{
				simInstance.waterMaterial = m;
			}
			SetDefaults(m);
		}
	}

	private static void SetDefaults(Material waterMaterial = null)
	{
		if (waterMaterial == null)
		{
			waveSpeed = 0f;
			wave1Scale = (wave2Scale = 0f);
			detailHeight = 0f;
			microDetailHeight = 0f;
			bigWaveHeight = 0f;
			bigWaveScale = 0f;
			heightMap1 = (heightMap2 = null);
			heightMapColor1 = new float[65536];
			heightMapColor2 = new Color[65536];
			heightMapTiling1 = (heightMapTiling2 = Vector2.zero);
			hasDefaults = false;
			return;
		}
		if (!hasDefaults)
		{
			Keyframe[] array = new Keyframe[4]
			{
				new Keyframe(0f, 0f),
				default(Keyframe),
				default(Keyframe),
				default(Keyframe)
			};
			array[0].inTangent = 0f;
			array[0].outTangent = -0.01f;
			array[1] = new Keyframe(0.5f, 0.21763764f);
			array[1].inTangent = 0.974f;
			array[1].outTangent = 0.974f;
			array[2] = new Keyframe(1f, 1f);
			array[2].inTangent = 2.219f;
			array[2].outTangent = 2.219f;
			array[3] = new Keyframe(2f, 4.5947933f);
			array[3].inTangent = 5.077f;
			curveExponent = new AnimationCurve(array);
			curveExponent.postWrapMode = WrapMode.Once;
			curveExponent.preWrapMode = WrapMode.PingPong;
		}
		hasDefaults = true;
		waveSpeed = waterMaterial.GetFloat("_WaveSpeed");
		wave1Scale = waterMaterial.GetFloat("_Wave1Scale");
		wave2Scale = waterMaterial.GetFloat("_Wave2Scale");
		detailHeight = waterMaterial.GetFloat("_DetailHeight");
		microDetailHeight = waterMaterial.GetFloat("_MicroDetailHeight");
		bigWaveHeight = waterMaterial.GetFloat("_BigWaveHeight");
		bigWaveScale = waterMaterial.GetFloat("_BigWaveScale");
		heightMap1 = (Texture2D)waterMaterial.GetTexture("_HeightMap");
		heightMap2 = (Texture2D)waterMaterial.GetTexture("_HeightMap2");
		heightMapTiling1 = waterMaterial.GetTextureScale("_HeightMap");
		heightMapTiling2 = waterMaterial.GetTextureScale("_HeightMap2");
		heightMap1Size.x = heightMap1.width;
		heightMap1Size.y = heightMap1.height;
		heightMap2Size.x = heightMap2.width;
		heightMap2Size.y = heightMap2.height;
		GetHeightmapPixelData();
	}

	private IEnumerator Setup()
	{
		yield return null;
		int blockCount = ReferenceMaster.GetAllSimulationBlocks().Count;
		int size = (int)((blockCount <= 8) ? ((float)blockCount) : ((float)blockCount * 0.3f));
		EFOArray = new ExternalForceObject[size];
		worldMatrix = base.transform.worldToLocalMatrix;
		setupDone = true;
		setupInProgress = false;
	}

	protected void Update()
	{
		if ((!StatMaster.isClient || StatMaster.isLocalSim) && setupDone && base.isSimulating)
		{
			simulateWave = simulateWaves;
			WheelMotorForceLimiter = wheelForceLimiter;
			WheelMotorForceValue = wheelForceValue;
			WingPalenMassChange = axialMassChange;
			WingMass = axialMass;
		}
	}

	private void LateUpdate()
	{
		if (largeParticles + mediumParticles + smallParticles > 0)
		{
			PlaySplashSounds();
			smallParticles = 0;
			mediumParticles = 0;
			largeParticles = 0;
		}
		gotWaterHeightThisFrame = false;
	}

	internal void FixedUpdate()
	{
		if (base.isSimulating && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			ClientUpdateWaterBlocks();
		}
		else
		{
			if (!base.isSimulating || !setupDone || StatMaster.GodTools.GravityDisabled)
			{
				return;
			}
			if (stabilitySkip < stabilitySkipAmount)
			{
				stabilitySkip += 1f;
				return;
			}
			float fixedDeltaTime = Time.fixedDeltaTime;
			timeTillUpdateWaterBlocks -= fixedDeltaTime;
			if (timeTillUpdateWaterBlocks > fixedDeltaTime)
			{
				if (flipper)
				{
					CalculateForcePositions(gotWaterHeightThisFrame);
				}
				flipper = !flipper;
			}
			else
			{
				UpdateWaterBlocks();
				flipper = false;
			}
			for (int i = 0; i < ExternalForceObjectCount; i++)
			{
				FixedEFO = EFOArray[i];
				if (object.ReferenceEquals(FixedEFO, null))
				{
					break;
				}
				if (FixedEFO.basicInfo.isDestroyed || FixedEFO.basicInfo.noRigidbody || FixedEFO.waitForUpdate || FixedEFO.basicInfo.AddNoWaterForce || FixedEFO.basicInfo.submergedPercent == 0f)
				{
					continue;
				}
				float num = 1f;
				fixedVel = FixedEFO.basicInfo.Rigidbody.velocity;
				Vector3 force = FixedEFO.force;
				Vector3 force2 = FixedEFO.force2;
				if (FixedEFO.basicInfo.splitBody)
				{
					force.y -= fixedVel.y * 0.5f;
					force2.y -= fixedVel.y * 0.5f;
					FixedEFO.basicInfo.Rigidbody.AddForceAtPosition(force * num, FixedEFO.closestPoint, FixedEFO.forceMode);
					FixedEFO.basicInfo.Rigidbody.AddForceAtPosition(force2 * num, FixedEFO.furthestPoint, FixedEFO.forceMode);
					continue;
				}
				if (FixedEFO.basicInfo is BuildSurface)
				{
					force.y -= fixedVel.y;
					FixedEFO.basicInfo.Rigidbody.AddForceAtPosition(force * num, FixedEFO.closestPoint, FixedEFO.forceMode);
					continue;
				}
				if (FixedEFO.highestPoint > FixedEFO.hightMapValue)
				{
					if (fixedVel.y > FixedEFO.force.y - FixedEFO.force2.y)
					{
						fixedVel.y = FixedEFO.force.y;
						force.y -= fixedVel.y;
					}
					else
					{
						force.y -= fixedVel.y + FixedEFO.force2.y;
					}
					FixedEFO.force2.y = 0f;
					FixedEFO.basicInfo.Rigidbody.AddForceAtPosition(force * num, FixedEFO.closestPoint, FixedEFO.forceMode);
					continue;
				}
				if (FixedEFO.basicInfo.infoType == BasicInfo.BasicInfoType.Block)
				{
					BlockBehaviour blockBehaviour = FixedEFO.basicInfo as BlockBehaviour;
					bool isParented = blockBehaviour.isParented;
					bool flag = false;
					if (!isParented)
					{
						flag = blockBehaviour.gotChildBlocks;
					}
					if (isParented)
					{
						Vector3 center = blockBehaviour.parentBlock.GetCenter();
						Vector3 position = ((!(blockBehaviour.originalMass * 0.5f * (1.5f / blockBehaviour.density) * 100f - Physics.gravity.y * blockBehaviour.originalMass < 0f)) ? (blockBehaviour.GetCenter() - center + center) : (center - blockBehaviour.GetCenter() + center));
						FixedEFO.basicInfo.Rigidbody.AddForceAtPosition(force * num, position, FixedEFO.forceMode);
						continue;
					}
					if (flag)
					{
						if (fixedVel.y > FixedEFO.force.y - FixedEFO.force2.y)
						{
							fixedVel.y = FixedEFO.force.y;
							force.y -= fixedVel.y;
						}
						else
						{
							force.y -= fixedVel.y + FixedEFO.force2.y;
						}
						FixedEFO.force2.y = 0f;
						FixedEFO.basicInfo.Rigidbody.AddForceAtPosition(force * num, blockBehaviour.GetCenter(), FixedEFO.forceMode);
						continue;
					}
				}
				if (fixedVel.y > FixedEFO.force.y - FixedEFO.force2.y)
				{
					fixedVel.y = FixedEFO.force.y;
					force.y -= fixedVel.y;
				}
				else
				{
					force.y -= fixedVel.y + FixedEFO.force2.y;
				}
				FixedEFO.force2.y = 0f;
				FixedEFO.basicInfo.Rigidbody.AddForce(force * num, FixedEFO.forceMode);
			}
		}
	}

	private void WaveDebugRays(float gridsize, float spacing)
	{
		Vector2 vector = new Vector2(0f, 0f);
		for (int i = 0; (float)i < gridsize; i++)
		{
			for (int j = 0; (float)j < gridsize; j++)
			{
				float x = (float)i * spacing + vector.x;
				float z = (float)j * spacing + vector.y;
				float x2 = (float)(i + 1) * spacing + vector.x;
				float z2 = (float)(j + 1) * spacing + vector.y;
				Vector3 start = new Vector3(x, CheckHeightMap(x, z, true), z);
				Vector3 end = new Vector3(x, CheckHeightMap(x, z2, true), z2);
				Vector3 end2 = new Vector3(x2, CheckHeightMap(x2, z, true), z);
				Debug.DrawLine(start, end, Color.red, 0f, true);
				Debug.DrawLine(start, end2, Color.red, 0f, true);
			}
		}
	}

	internal void CalculateForcePositions(bool skipHeightCheck = false)
	{
		for (int i = 0; i < ExternalForceObjectCount; i++)
		{
			EFO = EFOArray[i];
			if (object.ReferenceEquals(EFO, null))
			{
				break;
			}
			if (object.ReferenceEquals(EFO.basicInfo, null))
			{
				continue;
			}
			if (EFO.basicInfo.isDestroyed || EFO.basicInfo.noRigidbody || EFO.basicInfo.isDisabled)
			{
				ExitWater(EFO.basicInfo);
			}
			else if (EFO.basicInfo.splitBody)
			{
				Vector3 worldRBCenter = EFO.basicInfo.WorldRBCenter;
				Vector3 vector = ((!EFO.basicInfo.gotBounds) ? EFO.basicInfo.DefaultBounds.extents : EFO.basicInfo.defaultExtents);
				Vector3 vector2 = zero;
				Vector3 vector3 = vector * 2f;
				int directionToSplit = (int)EFO.basicInfo.directionToSplit;
				int num2;
				int num = (num2 = directionToSplit);
				float num3 = vector3[num2];
				vector3[num] = num3 * 0.5f;
				int num4 = (num2 = directionToSplit);
				num3 = vector2[num2];
				vector2[num4] = num3 + vector[directionToSplit] * 0.5f;
				vector2 = EFO.basicInfo.RBRot * vector2;
				CalculateForcePositionAdvancedFirst(worldRBCenter + vector2, vector3);
				vector2 = zero;
				int num5 = (num2 = directionToSplit);
				num3 = vector2[num2];
				vector2[num5] = num3 - vector[directionToSplit] * 0.5f;
				vector2 = EFO.basicInfo.RBRot * vector2;
				CalculateForcePositionAdvancedSecond(worldRBCenter + vector2, vector3);
			}
			else if (EFO.basicInfo is BuildSurface)
			{
				BuildSurface buildSurface = EFO.basicInfo as BuildSurface;
				EFO.force = (EFO.closestPoint = (EFO.furthestPoint = zero));
				EFO.hightMapValue = (EFO.highestPoint = 0f);
				Vector3 vector4 = zero;
				Vector3 vector5 = zero;
				float num6 = 0f;
				float num7 = 1f;
				Vector3 center = buildSurface.GetCenter();
				Quaternion rotation = buildSurface.transform.rotation;
				for (int j = 0; j < buildSurface.nodes.Length; j++)
				{
					Vector3 vector6 = rotation * buildSurface.DefaultBoundsArray[j].center + center;
					Quaternion quaternion = rotation;
					Vector3 extents = buildSurface._defaultBoundsArray[j].extents;
					CalculateForcePositionCombine(vector6, extents, quaternion);
					vector4 += EFO.force;
					vector5 += EFO.closestPoint;
					num6 += EFO.basicInfo.surfaceAreaToVel;
					num7 += 1f;
				}
				EFO.basicInfo.surfaceAreaToVel = num6;
				EFO.force = vector4 / num7;
				EFO.closestPoint = vector5 / num7 + center;
				if (EFO.closestPoint.sqrMagnitude < 1f || EFO.basicInfo.submergedPercent == 1f)
				{
					EFO.closestPoint = buildSurface.WorldRBCenter;
				}
			}
			else
			{
				CalculateForcePosition(EFO.basicInfo.WorldRBCenter, (!EFO.basicInfo.gotBounds) ? EFO.basicInfo.DefaultBounds.extents : EFO.basicInfo.defaultExtents, skipHeightCheck);
			}
		}
		if (firstRun)
		{
			firstRun = false;
		}
		gotWaterHeightThisFrame = true;
	}

	internal void CalculateForcePosition(Vector3 pos, Vector3 corner, bool skipHeightCheck = false)
	{
		uniformSkip = false;
		bool flag = EFO.basicInfo.infoType == BasicInfo.BasicInfoType.Block && (EFO.basicInfo as BlockBehaviour).isParented;
		if (EFO.basicInfo.uniformlyScaled)
		{
			EFO.hightMapValue = -0.6f + waterTransformHeight;
			EFO.highestPoint = EFO.basicInfo.extentLength + pos.y;
			uniformSkip = EFO.highestPoint < EFO.hightMapValue;
		}
		if (uniformSkip)
		{
			EFO.basicInfo.submergedPercent = 0f;
		}
		else
		{
			EFO.hightMapValue = (EFO.basicInfo.waterDepth = ((!skipHeightCheck) ? IsUnderwater(pos, EFO.basicInfo.extentLength) : EFO.basicInfo.waterDepth));
			rbRot = ((!flag) ? EFO.basicInfo.RBRot : EFO.basicInfo.transform.rotation);
			localWaterUp = Quaternion.Inverse(rbRot) * waterUpDir;
			newPoint.x = ((!(localWaterUp.x > 0f)) ? (0f - corner.x) : corner.x);
			newPoint.y = ((!(localWaterUp.y > 0f)) ? (0f - corner.y) : corner.y);
			newPoint.z = ((!(localWaterUp.z > 0f)) ? (0f - corner.z) : corner.z);
			EFO.furthestPoint = rbRot * newPoint;
			EFO.highestPoint = EFO.furthestPoint.y + pos.y;
			if (EFO.highestPoint <= EFO.hightMapValue)
			{
				EFO.closestPoint = pos;
				EFO.basicInfo.submergedPercent = 0f;
			}
			else
			{
				EFO.basicInfo.submergedPercent = (EFO.highestPoint - EFO.hightMapValue) / Math.Abs(EFO.furthestPoint.y * 2f);
				if (EFO.basicInfo.submergedPercent > 1f)
				{
					EFO.basicInfo.submergedPercent = 1f;
				}
				else if (EFO.basicInfo.submergedPercent < 0f)
				{
					EFO.basicInfo.submergedPercent = 0f;
				}
				localWaterUp *= 0f - EFO.basicInfo.submergedPercent;
				EFO.closestPoint = rbRot * Vector3.Scale(localWaterUp, corner) + pos;
			}
		}
		vel = (EFO.velocity = EFO.basicInfo.Rigidbody.velocity);
		velMagnitude = vel.magnitude;
		EFO.velNormal = ((!(velMagnitude < 5f)) ? NormalizeVector(velMagnitude, vel) : (vel * 0.2f));
		if (uniformSkip)
		{
			EFO.basicInfo.surfaceAreaToVel = (sArea = EFO.basicInfo.MaxAreaSize);
		}
		else
		{
			Quaternion quaternion = Quaternion.FromToRotation(EFO.velNormal, waterUpDir);
			newPoint = quaternion * EFO.furthestPoint;
			Vector3 vector = quaternion * new Vector3(0f - EFO.furthestPoint.x, EFO.furthestPoint.y, 0f - EFO.furthestPoint.z);
			newPoint.x = ((!(newPoint.x < 0f)) ? newPoint.x : (0f - newPoint.x));
			newPoint.z = ((!(newPoint.z < 0f)) ? newPoint.z : (0f - newPoint.z));
			vector.x = ((!(vector.x < 0f)) ? vector.x : (0f - vector.x));
			vector.z = ((!(vector.z < 0f)) ? vector.z : (0f - vector.z));
			newPoint.x = ((!(newPoint.x > vector.x)) ? vector.x : newPoint.x);
			newPoint.z = ((!(newPoint.z > vector.z)) ? vector.z : newPoint.z);
			EFO.basicInfo.surfaceAreaToVel = (sArea = Math.Abs(newPoint.x * newPoint.z));
		}
		if (!EFO.basicInfo.InWater)
		{
			EFO.basicInfo.InWater = true;
			if (!firstRun)
			{
				EmitWaterParticles(new Vector3(pos.x, EFO.hightMapValue, pos.z), EFO.basicInfo, (Math.Abs(vel.y) + velMagnitude) * 0.5f);
			}
			if (vel.y < 0f && EFO.basicInfo.infoType != BasicInfo.BasicInfoType.Projectile && EFO.basicInfo.density < 20f && !flag)
			{
				float num = sArea / EFO.basicInfo.MaxAreaSize;
				num = ((!(num > 1f)) ? num : 1f);
				Vector3 vector2 = zero;
				EFO.dragScale = 1f + Mathf.Clamp01(vel.y * vel.y * 0.0001f) * 200f;
				if (vel.y < -250f)
				{
					EFO.basicInfo.Rigidbody.velocity = new Vector3(EFO.basicInfo.Rigidbody.velocity.x, 0f, EFO.basicInfo.Rigidbody.velocity.z);
				}
				else
				{
					switch (sType)
					{
					case SurfaceTensionType.minusVel:
						vector2 = vel * (surfaceBreak * num);
						EFO.basicInfo.Rigidbody.AddForce(-vector2, ForceMode.Impulse);
						break;
					case SurfaceTensionType.minusVelY:
					{
						Vector3 vector3 = vel;
						vector3.x *= velYzxScale;
						vector3.z *= velYzxScale;
						vector3.y *= 1f + velMagnitude * 0.015f;
						vector2 = vector3 * (surfaceBreak * num) * (1f / EFO.basicInfo.density);
						if (vector2.y < vel.y)
						{
							vector2.y = vel.y;
						}
						EFO.basicInfo.Rigidbody.AddForce(-vector2, ForceMode.VelocityChange);
						break;
					}
					case SurfaceTensionType.Reflect:
						vector2 = Vector3.Reflect(vel, waterUpDir) * surfaceBreak * num;
						EFO.basicInfo.Rigidbody.AddForce(vector2, ForceMode.Impulse);
						break;
					case SurfaceTensionType.WaterUp:
						vector2 = waterUpDir * (surfaceBreak * num);
						EFO.basicInfo.Rigidbody.AddForce(vector2, ForceMode.Impulse);
						break;
					}
				}
			}
		}
		else if (RipplePostProcessing.Active)
		{
			EmitRipple(pos, EFO, corner);
		}
		EFO.basicInfo.submergedPercent = 1f - EFO.basicInfo.submergedPercent;
		if (!flag)
		{
			SetDragCurrentEFO(velMagnitude);
		}
		if (!EFO.basicInfo.AddNoWaterForce)
		{
			EFO.force = waterPower * (EFO.powerScale * EFO.basicInfo.submergedPercent * 0.5f * (1.5f / EFO.basicInfo.density));
		}
		if (EFO.waitForUpdate)
		{
			EFO.waitForUpdate = false;
		}
	}

	internal void CalculateForcePositionAdvancedFirst(Vector3 pos, Vector3 corner)
	{
		rbRot = EFO.basicInfo.RBRot;
		localWaterUp = Quaternion.Inverse(rbRot) * waterUpDir;
		newPoint.x = ((!(localWaterUp.x > 0f)) ? (0f - corner.x) : corner.x);
		newPoint.y = ((!(localWaterUp.y > 0f)) ? (0f - corner.y) : corner.y);
		newPoint.z = ((!(localWaterUp.z > 0f)) ? (0f - corner.z) : corner.z);
		EFO.furthestPoint = rbRot * newPoint;
		EFO.highestPoint = EFO.furthestPoint.y + pos.y;
		EFO.hightMapValue = (EFO.basicInfo.waterDepth = IsUnderwater(pos, EFO.basicInfo.extentLength));
		if (EFO.highestPoint <= EFO.hightMapValue)
		{
			EFO.basicInfo.submergedPercent = 0f;
		}
		else
		{
			EFO.basicInfo.submergedPercent = (EFO.highestPoint - EFO.hightMapValue) / Math.Abs(EFO.furthestPoint.y * 2f);
			if (EFO.basicInfo.submergedPercent > 1f)
			{
				EFO.basicInfo.submergedPercent = 1f;
			}
			else if (EFO.basicInfo.submergedPercent < 0f)
			{
				EFO.basicInfo.submergedPercent = 0f;
			}
			localWaterUp *= 0f - EFO.basicInfo.submergedPercent;
			EFO.closestPoint = rbRot * Vector3.Scale(localWaterUp, corner) + pos;
		}
		vel = (EFO.velocity = EFO.basicInfo.Rigidbody.velocity);
		velMagnitude = vel.magnitude;
		EFO.velNormal = ((!(velMagnitude < 5f)) ? NormalizeVector(velMagnitude, vel) : (vel * 0.2f));
		Quaternion quaternion = Quaternion.FromToRotation(EFO.velNormal, waterUpDir);
		newPoint = quaternion * EFO.furthestPoint;
		Vector3 vector = quaternion * new Vector3(0f - EFO.furthestPoint.x, EFO.furthestPoint.y, 0f - EFO.furthestPoint.z);
		newPoint.x = ((!(newPoint.x < 0f)) ? newPoint.x : (0f - newPoint.x));
		newPoint.z = ((!(newPoint.z < 0f)) ? newPoint.z : (0f - newPoint.z));
		vector.x = ((!(vector.x < 0f)) ? vector.x : (0f - vector.x));
		vector.z = ((!(vector.z < 0f)) ? vector.z : (0f - vector.z));
		newPoint.x = ((!(newPoint.x > vector.x)) ? vector.x : newPoint.x);
		newPoint.z = ((!(newPoint.z > vector.z)) ? vector.z : newPoint.z);
		EFO.basicInfo.surfaceAreaToVel = (sArea = Math.Abs(newPoint.x * newPoint.z));
		if (!EFO.basicInfo.InWater)
		{
			EFO.basicInfo.InWater = true;
			if (!firstRun)
			{
				EmitWaterParticles(new Vector3(pos.x, EFO.hightMapValue, pos.z), EFO.basicInfo, (Mathf.Abs(vel.y) + velMagnitude) * 0.5f);
			}
			if (EFO.basicInfo.submergedPercent != 1f)
			{
				float num = sArea / EFO.basicInfo.MaxAreaSize;
				num = ((!(num > 1f)) ? num : 1f);
				Vector3 vector2 = zero;
				switch (sType)
				{
				case SurfaceTensionType.minusVel:
					vector2 = vel * (surfaceBreak * num);
					EFO.basicInfo.Rigidbody.AddForce(-vector2, ForceMode.Impulse);
					break;
				case SurfaceTensionType.minusVelY:
				{
					Vector3 vector3 = vel;
					vector3.x *= velYzxScale;
					vector3.z *= velYzxScale;
					vector3.y *= 1f + velMagnitude * 0.015f;
					vector2 = vector3 * (surfaceBreak * num) * (1f / EFO.basicInfo.density);
					if (vector2.y < vel.y)
					{
						vector2.y = vel.y;
					}
					EFO.basicInfo.Rigidbody.AddForce(-vector2, ForceMode.VelocityChange);
					break;
				}
				case SurfaceTensionType.Reflect:
					EFO.basicInfo.Rigidbody.AddForce(Vector3.Reflect(vel, waterUpDir) * surfaceBreak * num, ForceMode.Impulse);
					break;
				case SurfaceTensionType.WaterUp:
					vector2 = waterUpDir * (surfaceBreak * num);
					EFO.basicInfo.Rigidbody.AddForce(vector2, ForceMode.Impulse);
					break;
				}
			}
		}
		else if (RipplePostProcessing.Active)
		{
			EmitRipple(pos, EFO, corner);
		}
		EFO.basicInfo.submergedPercent = 1f - EFO.basicInfo.submergedPercent;
		SetDragCurrentEFO(velMagnitude);
		EFO.force = waterPower * (EFO.powerScale * EFO.basicInfo.submergedPercent * 0.5f) * (1.5f / EFO.basicInfo.density);
		if (EFO.waitForUpdate)
		{
			EFO.waitForUpdate = false;
		}
	}

	internal void CalculateForcePositionAdvancedSecond(Vector3 pos, Vector3 corner)
	{
		rbRot = EFO.basicInfo.RBRot;
		localWaterUp = Quaternion.Inverse(rbRot) * waterUpDir;
		newPoint.x = ((!(localWaterUp.x > 0f)) ? (0f - corner.x) : corner.x);
		newPoint.y = ((!(localWaterUp.y > 0f)) ? (0f - corner.y) : corner.y);
		newPoint.z = ((!(localWaterUp.z > 0f)) ? (0f - corner.z) : corner.z);
		EFO.furthestPoint = rbRot * newPoint;
		float num = EFO.furthestPoint.y + pos.y;
		EFO.hightMapValue = IsUnderwater(pos, EFO.basicInfo.extentLength);
		float num2 = 0f;
		Vector3 furthestPoint;
		if (num <= EFO.hightMapValue)
		{
			furthestPoint = pos;
			num2 = 0f;
		}
		else
		{
			num2 = (num - EFO.hightMapValue) / Math.Abs(EFO.furthestPoint.y * 2f);
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			else if (num2 < 0f)
			{
				num2 = 0f;
			}
			localWaterUp *= 0f - num2;
			furthestPoint = rbRot * Vector3.Scale(localWaterUp, corner) + pos;
		}
		EFO.force2 = waterPower * (EFO.powerScale * (1f - num2) * 0.5f) * (1.5f / EFO.basicInfo.density);
		EFO.furthestPoint = furthestPoint;
		if (EFO.waitForUpdate)
		{
			EFO.waitForUpdate = false;
		}
	}

	internal void CalculateForcePositionCombine(Vector3 pos, Vector3 corner, Quaternion rbRot)
	{
		EFO.hightMapValue = (EFO.basicInfo.waterDepth = IsUnderwater(pos, EFO.basicInfo.extentLength));
		localWaterUp = Quaternion.Inverse(rbRot) * waterUpDir;
		newPoint.x = ((!(localWaterUp.x > 0f)) ? (0f - corner.x) : corner.x);
		newPoint.y = ((!(localWaterUp.y > 0f)) ? (0f - corner.y) : corner.y);
		newPoint.z = ((!(localWaterUp.z > 0f)) ? (0f - corner.z) : corner.z);
		EFO.furthestPoint = rbRot * newPoint;
		EFO.highestPoint = EFO.furthestPoint.y + pos.y;
		EFO.basicInfo.submergedPercent = (EFO.highestPoint - EFO.hightMapValue) / Math.Abs(EFO.furthestPoint.y * 2f);
		if (EFO.basicInfo.submergedPercent > 1f)
		{
			EFO.basicInfo.submergedPercent = 1f;
		}
		else if (EFO.basicInfo.submergedPercent < 0f)
		{
			EFO.basicInfo.submergedPercent = 0f;
		}
		localWaterUp *= -1f;
		EFO.closestPoint = rbRot * Vector3.Scale(localWaterUp, corner);
		vel = (EFO.velocity = EFO.basicInfo.Rigidbody.velocity);
		velMagnitude = vel.magnitude;
		EFO.velNormal = ((!(velMagnitude < 5f)) ? NormalizeVector(velMagnitude, vel) : (vel * 0.2f));
		Quaternion quaternion = Quaternion.FromToRotation(EFO.velNormal, waterUpDir);
		newPoint = quaternion * EFO.furthestPoint;
		Vector3 vector = quaternion * new Vector3(0f - EFO.furthestPoint.x, EFO.furthestPoint.y, 0f - EFO.furthestPoint.z);
		newPoint.x = Math.Abs(newPoint.x);
		newPoint.z = Math.Abs(newPoint.z);
		vector.x = Math.Abs(vector.x);
		vector.z = Math.Abs(vector.z);
		newPoint.x = ((!(newPoint.x > vector.x)) ? vector.x : newPoint.x);
		newPoint.z = ((!(newPoint.z > vector.z)) ? vector.z : newPoint.z);
		EFO.basicInfo.surfaceAreaToVel = (sArea = Math.Abs(newPoint.x * newPoint.z));
		if (!EFO.basicInfo.InWater)
		{
			EFO.basicInfo.InWater = true;
			if (!firstRun)
			{
				EmitWaterParticles(new Vector3(pos.x, EFO.hightMapValue, pos.z), EFO.basicInfo, (Math.Abs(vel.y) + velMagnitude) * 0.5f);
			}
			if (EFO.basicInfo.submergedPercent != 1f)
			{
				float num = sArea / EFO.basicInfo.MaxAreaSize;
				num = ((!(num > 1f)) ? num : 1f);
				Vector3 vector2 = zero;
				switch (sType)
				{
				case SurfaceTensionType.minusVel:
					vector2 = vel * (surfaceBreak * num);
					EFO.basicInfo.Rigidbody.AddForce(-vector2, ForceMode.Impulse);
					break;
				case SurfaceTensionType.minusVelY:
				{
					Vector3 vector3 = vel;
					vector3.x *= velYzxScale;
					vector3.z *= velYzxScale;
					vector3.y *= 1f + velMagnitude * 0.015f;
					vector2 = vector3 * (surfaceBreak * num) * (1f / EFO.basicInfo.density);
					if (vector2.y < vel.y)
					{
						vector2.y = vel.y;
					}
					EFO.basicInfo.Rigidbody.AddForce(-vector2, ForceMode.VelocityChange);
					break;
				}
				case SurfaceTensionType.Reflect:
					EFO.basicInfo.Rigidbody.AddForce(Vector3.Reflect(vel, waterUpDir) * surfaceBreak * num, ForceMode.Impulse);
					break;
				case SurfaceTensionType.WaterUp:
					vector2 = waterUpDir * (surfaceBreak * num);
					EFO.basicInfo.Rigidbody.AddForce(vector2, ForceMode.Impulse);
					break;
				}
			}
		}
		else if (RipplePostProcessing.Active)
		{
			EmitRipple(pos, EFO, corner);
		}
		EFO.basicInfo.submergedPercent = 1f - EFO.basicInfo.submergedPercent;
		SetDragCurrentEFO(velMagnitude);
		EFO.force = waterPower * (EFO.powerScale * EFO.basicInfo.submergedPercent * 0.5f * (1.5f / EFO.basicInfo.density));
		if (EFO.waitForUpdate)
		{
			EFO.waitForUpdate = false;
		}
	}

	protected internal void SetDragCurrentEFO(float velMag)
	{
		if (EFO.basicInfo.calcDragInWater)
		{
			sArea = Mathf.Sqrt(sArea);
			float num = EFO.dragScale;
			if (EFO.basicInfo.infoType == BasicInfo.BasicInfoType.Block)
			{
				num = advancedBasePercentageDrag + num * (1f - advancedBasePercentageDrag);
				num = (0.1f * velMag + 0.5f) * num;
			}
			float num2 = ((EFO.basicInfo.waterDragMulti != 0f) ? EFO.basicInfo.waterDragMulti : 1f);
			if (logDragInterperation)
			{
				float num3 = EFO.basicInfo.submergedPercent * drag * sArea * EFO.CounterDrag * num * num2;
				EFO.basicInfo.Rigidbody.drag += num3 - EFO.basicInfo._waterDrag;
				EFO.basicInfo._waterDrag = num3;
			}
			else
			{
				float num4 = EFO.basicInfo.submergedPercent * drag * sArea * nonLogVelocityScale * EFO.CounterDrag * num * num2;
				EFO.basicInfo.Rigidbody.drag += num4 - EFO.basicInfo._waterDrag;
				EFO.basicInfo._waterDrag = num4;
			}
		}
		if (EFO.basicInfo.calcAngularDragInWater)
		{
			float num5 = ((EFO.basicInfo.waterDragMulti != 0f) ? EFO.basicInfo.waterDragMulti : 1f);
			if (logDragInterperation)
			{
				float num6 = EFO.basicInfo.submergedPercent * angularDrag * num5;
				EFO.basicInfo.Rigidbody.angularDrag += num6 - EFO.basicInfo._waterAngularDrag;
				EFO.basicInfo._waterAngularDrag = num6;
			}
			else
			{
				float num7 = EFO.basicInfo.submergedPercent * angularDrag * nonLogVelocityScale * num5;
				EFO.basicInfo.Rigidbody.angularDrag += num7 - EFO.basicInfo._waterAngularDrag;
				EFO.basicInfo._waterAngularDrag = num7;
			}
		}
	}

	protected bool AloneBlock(BasicInfo bInfo)
	{
		if (!aloneCheck)
		{
			return false;
		}
		BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
		if (blockBehaviour != null)
		{
			if (blockBehaviour.ClusterIndex == -1)
			{
				return true;
			}
			for (int i = 0; i < blockBehaviour.jointsToMe.Count; i++)
			{
				if (blockBehaviour.jointsToMe[i] != null)
				{
					return false;
				}
			}
			for (int j = 0; j < blockBehaviour.iJointTo.Count; j++)
			{
				if (blockBehaviour.iJointTo[j] != null)
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	protected internal void UpdateWaterBlocks()
	{
		if (!base.isSimulating)
		{
			return;
		}
		simulateWaves = simulateWave;
		if (dragAffectedBlocks.Length != ReferenceMaster.blocksInSim)
		{
			dragAffectedBlocks = new ExternalForceObject[ReferenceMaster.blocksInSim];
		}
		timeTillUpdateWaterBlocks = updateSpeed;
		ExternalForceObjectCount = (dragArrayIndex = 0);
		for (int i = 0; i < ReferenceMaster.ExternalForceObjectsArray.Length; i++)
		{
			processBasicInfo(ReferenceMaster.ExternalForceObjectsArray[i]);
		}
		for (int i = 0; i < ReferenceMaster.ExternalForceTemp.Count; i++)
		{
			processBasicInfo(ReferenceMaster.ExternalForceTemp[i]);
		}
		if (advancedAreoDynamics && (advAeroDyn == null || advAeroDyn.State != TaskState.Running))
		{
			for (int i = dragArrayIndex; i < dragAffectedBlocks.Length; i++)
			{
				if (dragAffectedBlocks[i] != null)
				{
					dragAffectedBlocks[i].dontCompare = true;
				}
			}
			this.StartCoroutineAsync(AdvancedAreodynamicCheck(), out advAeroDyn);
		}
		CalculateForcePositions(true);
	}

	protected override bool ValidateEFO(BasicInfo b)
	{
		if (b.isDestroyed || b.noRigidbody || b.isKinematic || b.IgnoredByWater || b.isDisabled || !b.isSimulating)
		{
			return false;
		}
		return true;
	}

	internal void processBasicInfo(BasicInfo bInfo)
	{
		if (!ValidateEFO(bInfo))
		{
			return;
		}
		if (bInfo.gotBounds && bInfo.density < 0.01f)
		{
			bInfo.IgnoredByWater = true;
			ExitWater(bInfo);
			return;
		}
		if (bInfo.ShelterAmount == 1f)
		{
			ExitWater(bInfo);
			return;
		}
		if (bInfo.infoType == BasicInfo.BasicInfoType.Block)
		{
			BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
			BlockType type = blockBehaviour.Prefab.Type;
			if (Machine.IsDraggedBlock(type) || type == BlockType.Pin || type == BlockType.CameraBlock)
			{
				ExitWater(bInfo);
				return;
			}
		}
		Vector3 worldRBCenter = bInfo.WorldRBCenter;
		bInfo.waterDepth = IsUnderwater(worldRBCenter, bInfo.extentLength);
		if (bInfo.waterDepth < bInfo.LowestPoint)
		{
			ExitSplash(bInfo);
			ExitWater(bInfo);
		}
		else if (bInfo.infoType != BasicInfo.BasicInfoType.Block)
		{
			AddEFO(worldRBCenter, bInfo, ForceMode.Acceleration, 1f);
		}
		else
		{
			AddEFOVelSpace(worldRBCenter, bInfo, ForceMode.Force, bInfo.originalMassDensity);
		}
	}

	protected internal void ClientUpdateWaterBlocks()
	{
		if (!base.isSimulating)
		{
			return;
		}
		if (dragAffectedBlocks.Length != ReferenceMaster.blocksInSim)
		{
			dragAffectedBlocks = new ExternalForceObject[ReferenceMaster.blocksInSim];
		}
		timeTillUpdateWaterBlocks = updateSpeed;
		ExternalForceObjectCount = (dragArrayIndex = 0);
		for (int i = 0; i < ReferenceMaster.ExternalForceObjectsArray.Length; i++)
		{
			ClientProcessBasicInfo(ReferenceMaster.ExternalForceObjectsArray[i]);
		}
		if (!advancedAreoDynamics || (advAeroDyn != null && advAeroDyn.State == TaskState.Running))
		{
			return;
		}
		for (int i = dragArrayIndex; i < dragAffectedBlocks.Length; i++)
		{
			if (dragAffectedBlocks[i] != null)
			{
				dragAffectedBlocks[i].dontCompare = true;
			}
		}
		this.StartCoroutineAsync(AdvancedAreodynamicCheck(), out advAeroDyn);
	}

	protected bool ClientValidateEFO(BasicInfo b)
	{
		if (b.isDestroyed || b.IgnoredByWater || b.isDisabled || !b.isSimulating)
		{
			return false;
		}
		return true;
	}

	internal void ClientProcessBasicInfo(BasicInfo bInfo)
	{
		if (!ClientValidateEFO(bInfo) || bInfo.infoType != BasicInfo.BasicInfoType.Block)
		{
			return;
		}
		if (bInfo.gotBounds && bInfo.density < 0.01f)
		{
			bInfo.IgnoredByWater = true;
			ExitWater(bInfo);
			return;
		}
		if (bInfo.ShelterAmount == 1f)
		{
			ExitWater(bInfo);
			return;
		}
		BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
		BlockType type = blockBehaviour.Prefab.Type;
		Vector3 center = blockBehaviour.GetCenter();
		bInfo.waterDepth = IsUnderwater(center, bInfo.extentLength);
		if (bInfo.waterDepth < bInfo.ClientLowestPoint)
		{
			ExitSplash(bInfo);
			ExitWater(bInfo);
		}
		else if (Machine.IsDraggedBlock(type) || type == BlockType.Pin || type == BlockType.CameraBlock)
		{
			ExitWater(bInfo);
		}
		else
		{
			bInfo.InWater = true;
			ClientAddEFOVelSpace(center, bInfo, ForceMode.Force, bInfo.originalMassDensity);
		}
	}

	private void ExitWater(BasicInfo bInfo)
	{
		if (bInfo.InWater)
		{
			bInfo.InWater = false;
		}
	}

	private void ExitSplash(BasicInfo bInfo)
	{
		if (!bInfo.noRigidbody && bInfo.InWater)
		{
			float y = bInfo.Rigidbody.velocity.y;
			Vector3 worldRBCenter = bInfo.WorldRBCenter;
			if (worldRBCenter.y > bInfo.waterDepth)
			{
				worldRBCenter.y = bInfo.waterDepth;
			}
			if (y > 5f)
			{
				EmitWaterParticles(worldRBCenter, bInfo, y);
			}
		}
	}

	private void EmitWaterParticles(Vector3 emitPosition, BasicInfo bi, float suppliedVelMag)
	{
		if (suppliedVelMag < particleImpactScaleMin * 0.01f)
		{
			return;
		}
		float num = suppliedVelMag;
		float num2 = 0f;
		switch (bi.infoType)
		{
		case BasicInfo.BasicInfoType.Block:
		{
			num2 = bi.surfaceAreaToVel;
			BlockBehaviour blockBehaviour = bi as BlockBehaviour;
			if (!blockBehaviour.HasParentMachine || blockBehaviour.ClusterIndex == -1)
			{
				break;
			}
			num *= Mathf.Max(1f, blockBehaviour.defaultExtents.sqrMagnitude);
			Machine.SimCluster simCluster = blockBehaviour.ParentMachine.simClusters[blockBehaviour.ClusterIndex];
			if (simCluster.Base == blockBehaviour)
			{
				BlockType type = blockBehaviour.Prefab.Type;
				if (type == BlockType.Bomb || type == BlockType.FlameBall || type == BlockType.Boulder)
				{
					num *= 2f;
					num *= num;
				}
				else if (!(simCluster.CenterOffset.sqrMagnitude > 6f))
				{
					Vector3 vector = simCluster.BaseTransform.TransformPoint(simCluster.CenterOffset);
					emitPosition.x = vector.x;
					emitPosition.z = vector.z;
					num *= (float)(simCluster.count * simCluster.count);
				}
				break;
			}
			blockBehaviour.CreateSimLists();
			int num3 = blockBehaviour.iJointTo.Count + blockBehaviour.jointsToMe.Count;
			switch (blockBehaviour.Prefab.Type)
			{
			case BlockType.WoodenPole:
				if (num3 > 1)
				{
					return;
				}
				break;
			case BlockType.CornerWoodenBlock:
				if (num3 > 3)
				{
					return;
				}
				break;
			default:
				if (num3 > 2)
				{
					return;
				}
				break;
			case BlockType.BuildSurface:
				break;
			}
			break;
		}
		case BasicInfo.BasicInfoType.Projectile:
			num2 = 0.5f;
			num = particleImpactScaleMin + 1f;
			break;
		default:
			num2 = Mathf.Clamp((bi.surfaceAreaToVel + bi.defaultExtents.x) * 0.5f, 0f, 4f);
			num *= num;
			break;
		}
		int num4 = ((!(num < particleImpactScaleMin)) ? ((!(num > particleImpactScaleMax)) ? 1 : 2) : 0);
		EmitWaterParticles(emitPosition, num4);
		if (RipplePostProcessing.Active)
		{
			emitPosition = (emitPosition + bi.GetCenter()) * 0.5f;
			EmitFoam(emitPosition, num2, num);
		}
		if (StatMaster.isMP && bi.SimPhysics)
		{
			NetworkBlock netBlock = bi.NetBlock;
			if (netBlock != null)
			{
				netBlock.Event(NetworkEntity.EntityEvent.WaterSplash, (byte)(num4 + 1));
			}
		}
	}

	public void EmitFoam(Vector3 emitPosition, float size, float impact)
	{
		emitPosition.y = waterTransformHeight;
		foamEmitter.startSize = (0.5f + size * 0.5f) * 20f;
		foamEmitter.startColor = new Color32(byte.MaxValue, (byte)(int)(255f * size), (byte)(int)(255f * size), (byte)(int)(255f * (0.5f + Mathf.InverseLerp(0f, particleImpactScaleMin, impact))));
		foamEmitter.position = emitPosition;
		GlobalParticles.EmitParticle(14, foamEmitter, 1);
	}

	public void EmitRipple(Vector3 emitPosition, ExternalForceObject EFO, Vector3 extents)
	{
		if (RipplePostProcessing.Active && EFO.basicInfo.submergedPercent > 0.2f && EFO.basicInfo.submergedPercent < 0.8f)
		{
			Vector3 velocity = EFO.velocity;
			velocity.y = 0f;
			float sqrMagnitude = velocity.sqrMagnitude;
			if (sqrMagnitude > 10f)
			{
				float num = (extents.x + extents.y) * 0.25f;
				foamEmitter.startSize = (0.5f + num * 0.5f) * 40f;
				foamEmitter.startColor = new Color32(20, (byte)(int)(255f * num * 0.75f), (byte)(int)(255f * num * 1.5f), (byte)(int)Mathf.Clamp(sqrMagnitude * sqrMagnitude * 0.005f, 0f, 100f));
				emitPosition.y = waterTransformHeight;
				foamEmitter.position = emitPosition;
				GlobalParticles.EmitParticle(15, foamEmitter, 1);
			}
		}
	}

	public void EmitRipple(Vector3 emitPosition, float size, float speed)
	{
		if (RipplePostProcessing.Active)
		{
			foamEmitter.startSize = (0.5f + size * 0.5f) * 40f;
			foamEmitter.startColor = new Color32(20, (byte)(int)(255f * size * 0.75f), (byte)(int)(255f * size * 1.5f), (byte)(int)Mathf.Clamp(speed * speed * 0.005f, 0f, 100f));
			emitPosition.y = waterTransformHeight;
			foamEmitter.position = emitPosition;
			GlobalParticles.EmitParticle(15, foamEmitter, 1);
		}
	}

	public void EmitWaterParticles(Vector3 emitPosition, int particleSet)
	{
		if (splashedQueuedForAudio < 128)
		{
			if (cam == null)
			{
				cam = Camera.main;
			}
			if ((emitPosition - cam.transform.position).sqrMagnitude < 10000f)
			{
				splashPositions[splashedQueuedForAudio] = emitPosition;
				splashedQueuedForAudio++;
			}
		}
		GlobalParticles.EmitParticleAmount(4, emitPosition + Vector3.down * UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(1, 4));
		switch (particleSet)
		{
		case 0:
			GlobalParticles.EmitParticleBursts(0, emitPosition);
			smallParticles++;
			break;
		case 1:
			GlobalParticles.EmitParticleBursts(1, emitPosition);
			mediumParticles++;
			break;
		case 2:
			GlobalParticles.EmitParticleBursts(2, emitPosition);
			largeParticles++;
			break;
		default:
			GlobalParticles.EmitParticleBursts(1, emitPosition);
			break;
		}
	}

	public void EmitWakeParticles(Vector3 emitPosition, Vector3 direction)
	{
		emitter.applyShapeToPosition = true;
		emitter.position = emitPosition;
		emitter.velocity = direction * 10f;
		Debug.DrawRay(emitPosition, direction, Color.red, 10f);
		GlobalParticles.EmitParticleBursts(7, emitter);
	}

	private void PlaySplashSounds()
	{
		Vector3 vector = new Vector3(0f, -1000f, 0f);
		Vector3 vector2 = vector;
		Vector3 vector3 = vector;
		Vector3 vector4 = vector;
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		float num3 = float.MaxValue;
		Vector2 vector5 = new Vector2(cam.transform.position.x, cam.transform.position.z);
		for (int i = 0; i < splashedQueuedForAudio; i++)
		{
			vector = splashPositions[i];
			float sqrMagnitude = (new Vector2(vector.x, vector.z) - vector5).sqrMagnitude;
			if (!(sqrMagnitude < num))
			{
				continue;
			}
			if (sqrMagnitude < num2)
			{
				vector2 = vector3;
				num = num2;
				if (sqrMagnitude < num3)
				{
					vector3 = vector4;
					num2 = num3;
					vector4 = vector;
					num3 = sqrMagnitude;
				}
				else
				{
					vector3 = vector;
					num2 = sqrMagnitude;
				}
			}
			else
			{
				vector2 = vector;
				num = sqrMagnitude;
			}
		}
		int num4 = Mathf.Min(splashedQueuedForAudio, 3);
		splashedQueuedForAudio = 0;
		Vector3[] array = new Vector3[3] { vector4, vector3, vector2 };
		float num5 = 0f;
		Vector3 vector6 = zero;
		Vector3 vector7 = zero;
		for (int j = 0; j < num4; j++)
		{
			vector7 -= array[j];
			vector6 += array[j];
		}
		num5 = vector7.magnitude / (float)num4;
		vector6 /= (float)num4;
		int num6 = 0;
		int num7 = 0;
		if (num5 < splashCombineDistanace)
		{
			if (num4 >= 3)
			{
				PlayAudioAtPoint(WaterSplashSoundsLarge[UnityEngine.Random.Range(0, WaterSplashSoundsLarge.Length)], vector6);
				num7++;
			}
			else
			{
				PlayAudioAtPoint(WaterSplashSounds[UnityEngine.Random.Range(0, WaterSplashSounds.Length)], vector6);
				num6++;
			}
		}
		else
		{
			for (int k = 0; k < num4; k++)
			{
				PlayAudioAtPoint(WaterSplashSounds[UnityEngine.Random.Range(0, WaterSplashSounds.Length)], splashPositions[k]);
				num6++;
			}
		}
	}

	private void PlayAudioAtPoint(AudioClip clip, Vector3 pos)
	{
		int num = 16;
		AudioSource audioSource;
		if (splashes.Count < num)
		{
			audioSource = UnityEngine.Object.Instantiate(audioSrc, pos, Quaternion.identity, ReferenceMaster.physicsGoalInstance) as AudioSource;
			splashes.Add(audioSource);
		}
		else
		{
			audioSource = splashes[currentSplash];
			if (audioSource.isPlaying)
			{
				Vector2 vector = new Vector2(cam.transform.position.x, cam.transform.position.z);
				Vector2 vector2 = new Vector2(pos.x, pos.z) - vector;
				Vector2 vector3 = new Vector2(audioSource.transform.position.x, audioSource.transform.position.z) - vector;
				if (vector2.sqrMagnitude > vector3.sqrMagnitude)
				{
					return;
				}
				audioSource.Stop();
			}
			audioSource.transform.position = pos;
		}
		audioSource.clip = clip;
		audioSource.Play();
		currentSplash++;
		currentSplash %= num;
	}

	private void OnDestroy()
	{
		if (buildInstance == null || buildInstance == this)
		{
			waterTransformHeight = -1000f;
		}
		if (base.transform.root != ReferenceMaster.physicsGoalInstance && base.transform.parent != ReferenceMaster.physicsGoalInstance)
		{
			Exist = false;
		}
	}

	public static void Disable()
	{
		if (buildInstance != null)
		{
			buildInstance.DisableWater();
		}
	}

	private void DisableWater()
	{
		setupDone = false;
		setupInProgress = false;
		isDisabled = true;
	}

	public static bool GetInitialWaterState(float posy)
	{
		if (!Exist || isDisabled)
		{
			return false;
		}
		return posy < waterTransformHeight;
	}

	public static bool IsUnderwater(Vector3 pos)
	{
		if (!Exist || isDisabled)
		{
			return false;
		}
		if (!WaterBoundsExceedsBottom && pos.y < waterTransformHeight)
		{
			return true;
		}
		if (!WaterBoundsExceedsTop && pos.y > waterTransformHeight + 10f)
		{
			return false;
		}
		return CheckHeightMap(pos.x, pos.z) > pos.y;
	}

	public static bool IsUnderwater(Vector3 pos, ref bool exitedEarly)
	{
		if (!Exist || isDisabled)
		{
			return false;
		}
		if (!WaterBoundsExceedsBottom && pos.y < waterTransformHeight)
		{
			exitedEarly = true;
			return true;
		}
		if (!WaterBoundsExceedsTop && pos.y > waterTransformHeight + 7f)
		{
			exitedEarly = true;
			return false;
		}
		return CheckHeightMap(pos.x, pos.z) > pos.y;
	}

	public static float IsUnderwater(Vector3 pos, float extendLength, bool includeDetailWaves = false)
	{
		if (!Exist || isDisabled)
		{
			return -1000f;
		}
		if (!WaterBoundsExceedsTop && pos.y - extendLength > waterTransformHeight + 7f)
		{
			return waterTransformHeight;
		}
		if (!WaterBoundsExceedsBottom && pos.y + extendLength < waterTransformHeight)
		{
			return waterTransformHeight;
		}
		return CheckHeightMap(pos.x, pos.z, includeDetailWaves);
	}

	public static float CheckHeightMap(float x, float z, bool includeDetailWaves = false)
	{
		if (!Exist || isDisabled)
		{
			return -1000f;
		}
		return WavePoints(x, z, includeDetailWaves) + waterTransformHeight;
	}

	internal static float WavePoints(float x, float z, bool includeDetailWaves = false)
	{
		wavePos.x = x;
		wavePos.y = z;
		if (meshScale == 1f)
		{
			meshScale = 50f;
		}
		if (!hasDefaults)
		{
			return 0f;
		}
		float minCalm = 0f;
		float maxCalm = 0f;
		Calm(wavePos, ref minCalm, ref maxCalm);
		if (minCalm < 0f)
		{
			float t = Mathf.Clamp01(0f - minCalm);
			float num = Mathf.Lerp(minCalm, maxCalm, t);
			maxCalm = Mathf.Lerp(maxCalm, minCalm, t);
			minCalm = num;
		}
		wavePos /= 60f;
		includeDetailWaves = includeDetailWaves && maxCalm < 0f;
		Vector2 vector = SampleWaveHeight(wavePos, includeDetailWaves);
		if (includeDetailWaves)
		{
			vector.x *= minCalm;
			return vector.x + (vector.y * (1f + (1f - minCalm) * 0.5f) + maxCalm * meshScale * 0.2f);
		}
		if (maxCalm == 0f)
		{
			return (vector.x + vector.y) * minCalm;
		}
		if (maxCalm < 0f)
		{
			return (vector.x + vector.y) * minCalm + maxCalm * meshScale * 0.2f;
		}
		return vector.x + vector.y + maxCalm * meshScale * 0.2f;
	}

	private static uint ReturnBitIndex(uint v)
	{
		if ((v & 1) != 0)
		{
			return 0u;
		}
		uint num = 1u;
		if ((v & 0xFFFF) == 0)
		{
			v >>= 16;
			num += 16;
		}
		if ((v & 0xFF) == 0)
		{
			v >>= 8;
			num += 8;
		}
		if ((v & 0xF) == 0)
		{
			v >>= 4;
			num += 4;
		}
		if ((v & 3) == 0)
		{
			v >>= 2;
			num += 2;
		}
		return num - (v & 1);
	}

	internal static void Calm(Vector2 worldPos, ref float minCalm, ref float maxCalm)
	{
		if (!startIsDone)
		{
			minCalm = 1f;
			maxCalm -= 1f;
			return;
		}
		fullInt = calmController.CellsContains[calmController.GetCellKey(worldPos)];
		minCalm = float.MaxValue;
		maxCalm = 1f;
		if (fullInt.x == 0f && fullInt.y == 0f && fullInt.z == 0f && fullInt.w == 0f)
		{
			minCalm = 1f;
			maxCalm -= 1f;
			return;
		}
		i = 0;
		for (uint num = 0u; num < 4; num++)
		{
			if (num * 24 >= calmController.numerOfZones)
			{
				if (minCalm == float.MaxValue)
				{
					minCalm = 1f;
				}
				maxCalm -= 1f;
				return;
			}
			cellContent = (uint)fullInt[i];
			i++;
			for (uint num2 = 0u; num2 < cellContent; num2++)
			{
				index = cellContent;
				cellContent &= cellContent - 1;
				index -= cellContent;
				index = ReturnBitIndex(index) + num * 24;
				if (index >= calmController.numerOfZones)
				{
					if (minCalm == float.MaxValue)
					{
						minCalm = 1f;
					}
					maxCalm -= 1f;
					return;
				}
				Vector4 vector = calmController.v[index];
				calmRelation.x = vector.x - worldPos.x;
				calmRelation.y = vector.y - worldPos.y;
				calmSqr = calmRelation.x * calmRelation.x + calmRelation.y * calmRelation.y;
				calmRad = vector.z;
				if (!(calmSqr > calmRad))
				{
					calm = calmSqr / calmRad;
					calm = ((calm < 0f) ? 0f : ((!(calm > 1f)) ? calm : 1f));
					calm = Mathf.Lerp(calmController.b[index], 1f, (float)Math.Pow(calm, vector.w));
					if (calm > maxCalm)
					{
						maxCalm = calm;
					}
					if (calm < minCalm)
					{
						minCalm = calm;
					}
				}
			}
		}
		if (minCalm == float.MaxValue)
		{
			minCalm = 1f;
		}
		maxCalm -= 1f;
	}

	internal static Vector2 SampleWaveHeight(Vector2 worldPos, bool includeDetails = false)
	{
		float num = (Time.timeSinceLevelLoad + timeOffset) / 20f * waveSpeed * globalSpeed;
		uv1.x = 0f - num + worldPos.x * heightMapTiling1.x;
		uv1.y = worldPos.y * heightMapTiling1.y;
		height = GetBilinearHeightSample(uv1.x * wave1Scale, uv1.y * wave1Scale, heightMap1Size.x, heightMap1Size.y, HeightMapType.GrayScale) * detailHeight;
		uv2.x = num + worldPos.x * heightMapTiling2.x + height * 0.2f;
		uv2.y = num + worldPos.y * heightMapTiling2.y + height * 0.2f;
		height2Full = black;
		height2 = 0f;
		if (includeDetails)
		{
			height2Full = GetBilinearHeightSampleRGBA(uv2.x * wave2Scale, uv2.y * wave2Scale, heightMap2Size.x, heightMap2Size.y);
			height2 = height2Full.r * detailHeight;
		}
		else
		{
			height2 = GetBilinearHeightSample(uv2.x * wave2Scale, uv2.y * wave2Scale, heightMap2Size.x, heightMap2Size.y, HeightMapType.Color) * detailHeight;
		}
		uv3.x = ((0f - num) * 2f + worldPos.x + height2 * 0.2f) * bigWaveScale;
		uv3.y = (worldPos.y + height2 * 0.2f) * bigWaveScale;
		height3 = GetBilinearHeightSample(uv3.x, uv3.y, heightMap2Size.x, heightMap2Size.y, HeightMapType.Color) * bigWaveHeight;
		uv4.x = (num * 2f + worldPos.x + height3 * 0.2f) * bigWaveScale;
		uv4.y = (num * 2f + worldPos.y + height3 * 0.2f) * bigWaveScale;
		height4 = GetBilinearHeightSample(uv4.x, uv4.y, heightMap1Size.x, heightMap1Size.y, HeightMapType.GrayScale) * bigWaveHeight;
		height = curveExponent.Evaluate(height);
		height2 = curveExponent.Evaluate(height2);
		height3 = curveExponent.Evaluate(height3);
		height4 = curveExponent.Evaluate(height4);
		if (includeDetails)
		{
			height5 = height2Full.b * microDetailHeight * detailHeight;
			height6 = height2Full.g * microDetailHeight * detailHeight;
			height5 = curveExponent.Evaluate(height5);
			height6 = curveExponent.Evaluate(height6);
			waveHeight.x = height3 + height4;
			waveHeight.y = height + height2 + height5 + height6;
		}
		else
		{
			waveHeight.x = height3 + height4;
			waveHeight.y = height + height2;
		}
		waveHeight.x *= meshScale;
		waveHeight.y *= meshScale;
		return waveHeight;
	}

	private static float GetBilinearHeightSample(float u, float v, float width, float height, HeightMapType type)
	{
		uPixelIndex = u - (float)(int)u;
		uPixelIndex = ((!(uPixelIndex < 0f)) ? uPixelIndex : (uPixelIndex + 1f));
		uPixelIndex *= width;
		vPixelIndex = v - (float)(int)v;
		vPixelIndex = ((!(vPixelIndex < 0f)) ? vPixelIndex : (vPixelIndex + 1f));
		vPixelIndex *= height;
		uMin = (int)uPixelIndex;
		uMin = ((uMin < 256) ? uMin : 0);
		uMax = ((uMin != 255) ? (uMin + 1) : 0);
		vMin = (int)vPixelIndex;
		vMin = ((vMin < 256) ? vMin : 0);
		vMax = ((vMin != 255) ? (vMin + 1) : 0) * (int)height;
		vMin *= (int)height;
		switch (type)
		{
		case HeightMapType.GrayScale:
			ftopLeft = heightMapColor1[vMax + uMin];
			ftopRight = heightMapColor1[vMax + uMax];
			fbottomLeft = heightMapColor1[vMin + uMin];
			fbottomRight = heightMapColor1[vMin + uMax];
			break;
		case HeightMapType.Color:
			ftopLeft = heightMapColor2[vMax + uMin].r;
			ftopRight = heightMapColor2[vMax + uMax].r;
			fbottomLeft = heightMapColor2[vMin + uMin].r;
			fbottomRight = heightMapColor2[vMin + uMax].r;
			break;
		}
		pixelDifferenceInv = uPixelIndex - (float)(int)uPixelIndex;
		pixelDifference = 1f - pixelDifferenceInv;
		result1 = pixelDifference * ftopLeft + pixelDifferenceInv * ftopRight;
		result2 = pixelDifference * fbottomLeft + pixelDifferenceInv * fbottomRight;
		pixelDifferenceInv = vPixelIndex - (float)(int)vPixelIndex;
		pixelDifference = 1f - pixelDifferenceInv;
		return pixelDifference * result2 + pixelDifferenceInv * result1;
	}

	private static Color GetBilinearHeightSampleRGBA(float u, float v, float width, float height)
	{
		uPixelIndex = u - (float)(int)u;
		uPixelIndex = ((!(uPixelIndex < 0f)) ? uPixelIndex : (uPixelIndex + 1f));
		uPixelIndex *= width;
		vPixelIndex = v - (float)(int)v;
		vPixelIndex = ((!(vPixelIndex < 0f)) ? vPixelIndex : (vPixelIndex + 1f));
		vPixelIndex *= height;
		uMin = (int)uPixelIndex;
		uMin = ((uMin < 256) ? uMin : 0);
		uMax = ((uMin != 255) ? (uMin + 1) : 0);
		vMin = (int)vPixelIndex;
		vMin = ((vMin < 256) ? vMin : 0);
		vMax = ((vMin != 255) ? (vMin + 1) : 0) * (int)height;
		vMin *= (int)height;
		topLeft = heightMapColor2[vMax + uMin];
		topRight = heightMapColor2[vMax + uMax];
		bottomLeft = heightMapColor2[vMin + uMin];
		bottomRight = heightMapColor2[vMin + uMax];
		pixelDifferenceInv = uPixelIndex - (float)(int)uPixelIndex;
		pixelDifference = 1f - pixelDifferenceInv;
		result1Color = pixelDifference * topLeft + pixelDifferenceInv * topRight;
		result2Color = pixelDifference * bottomLeft + pixelDifferenceInv * bottomRight;
		pixelDifferenceInv = vPixelIndex - (float)(int)vPixelIndex;
		pixelDifference = 1f - pixelDifferenceInv;
		return pixelDifference * result2Color + pixelDifferenceInv * result1Color;
	}

	private static void GetHeightmapPixelData()
	{
		Color[] pixels = heightMap1.GetPixels();
		heightMapColor1 = new float[heightMap1.width * heightMap1.height];
		for (int i = 0; i < heightMap1.width; i++)
		{
			for (int j = 0; j < heightMap1.height; j++)
			{
				heightMapColor1[i * heightMap1.height + j] = pixels[i * heightMap1.height + j].r;
			}
		}
		heightMapColor2 = heightMap2.GetPixels();
	}
}
