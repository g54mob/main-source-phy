using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly;
using Poly.Collide;
using Poly.Collide.Unity;
using Poly.Determinism;
using Poly.Game;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	private struct T3
	{
		public Vector3 pos;

		public Quaternion q;
	}

	public VehicleStub m_Stub;

	public MeshRenderer m_MeshRenderer;

	public Transform m_ScalingTransform;

	[Header("Animation")]
	public Animator[] m_SpeedBasedAnimations;

	[Header("Physics")]
	public GameObject m_PhysicsPrefab;

	public Transform m_GameplayTriggers;

	public BoxCollider m_StaticBoundingBox;

	[Header("Skins")]
	public Renderer[] m_ExtraSkinMeshes;

	[Header("FX")]
	public GameObject m_DayLights;

	public GameObject m_NightLights;

	public GameObject m_SecondaryDayLights;

	public GameObject m_SecondaryNightLights;

	public GameObject m_WheelFillMesh;

	public GameObject m_WheelFillMeshBack;

	public VehicleSplashSize m_SplashSize;

	[Header("UI")]
	public SplineComputer m_OutlineSplineComputer;

	public SpriteRenderer m_OutlineSprite;

	[Header("SFX")]
	public VehicleAudio m_VehicleAudio;

	[NonSerialized]
	public List<Checkpoint> m_Checkpoints = new List<Checkpoint>();

	[NonSerialized]
	public List<Checkpoint> m_RemainingCheckpoints = new List<Checkpoint>();

	[NonSerialized]
	public List<string> m_CheckpointGuids = new List<string>();

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public float m_RotationDegrees;

	[NonSerialized]
	public float m_TimeDelaySeconds;

	[NonSerialized]
	public bool m_Flipped;

	[NonSerialized]
	public bool m_OrderedCheckpoints;

	[NonSerialized]
	public Vector3 m_SpawnPos;

	[NonSerialized]
	public Quaternion m_SpawnRot;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public bool m_ReachedVictoryFlag;

	[NonSerialized]
	public bool m_ReachedStopCheckpoint;

	[NonSerialized]
	public float m_NumSecondsNoProgressWithMotorOn;

	[NonSerialized]
	public float m_Acceleration;

	[NonSerialized]
	public float m_TargetSpeed;

	[NonSerialized]
	public float m_Mass;

	[NonSerialized]
	public float m_BrakingForceMultiplier;

	[NonSerialized]
	public bool m_IdleOnDownhill;

	[NonSerialized]
	public float m_DesiredAcceleration;

	[NonSerialized]
	public float m_ShocksMultiplier;

	[NonSerialized]
	public Vector3 m_OriginalScale;

	[NonSerialized]
	public VehicleSyncTarget m_VehicleSyncTargetChassis;

	[NonSerialized]
	public Poly.Physics.Vehicle m_PhysicsPrefabInstantiated;

	[NonSerialized]
	public string m_SkinID;

	[NonSerialized]
	public float m_ForceShowMeshTimer;

	[NonSerialized]
	public string m_ModId;

	[NonSerialized]
	public Vector2 m_PrevFollow;

	[NonSerialized]
	public Vector2 m_FollowOffset;

	[NonSerialized]
	public GameObject m_CenterOfMassIcon;

	[NonSerialized]
	public Renderer[] m_Renderers;

	internal bool m_isRenderingEnabled = true;

	private VehicleSyncTarget[] m_SyncTargets;

	private Poly.Physics.Vehicle m_Physics;

	private BusArticulationAnimator m_articulationAnimator;

	private GameObject m_PhysicsChassis;

	private Vector3 m_LastPos;

	private float m_SecondsSinceVictoryFlagReached;

	private float m_Speed;

	private Vector3 m_OriginalOutlineSpriteLocalPos;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private bool m_PlayedSplashFX;

	private bool m_WheelsUnderWaterAtSimulationStart;

	private bool m_LightsAreOn;

	private float m_StartFollowCamTime;

	private readonly float VEHICLE_FOLLOW_CAMERA_DELAY_SECONDS = 0.75f;

	private List<VehicleNightLight> m_NightLightList = new List<VehicleNightLight>();

	private List<VehicleNightLight> m_SecondaryNightLightList = new List<VehicleNightLight>();

	private ParticleSystem m_SplashParticleSystem;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	internal List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	public const bool m_RotateVisualMeshesWhenFlipped180 = true;

	private bool m_AreVisualMeshTransformsFlipped;

	private VehicleUGC m_UgcComponent;

	private SpeedAnimState m_SpeedAnimState;

	private const string START_ANIM_NAME = "start";

	private const string STOP_ANIM_NAME = "stop";

	private const string STOPPED_STATE_NAME = "stopped";

	private VehicleWheelsLine m_VehicleWheelsLine;

	public float Speed => m_Speed;

	public Poly.Physics.Vehicle Physics => m_Physics;

	private void Awake()
	{
		SetDefaultPhysicsProperties();
		m_SandboxItem = GetComponent<SandboxItem>();
		m_SandboxItem.MaybeCreateSandboxLabel();
		m_UgcComponent = GetComponent<VehicleUGC>();
		if (m_UgcComponent != null)
		{
			m_UgcComponent.m_PhysicsParentTransform.gameObject.SetActive(value: false);
			m_UgcComponent.CreateSplines(this);
		}
		m_GameplayTriggers = VehicleSyncUtil.SplitVehicleSyncTargetsIntoVisualAndPhysics((m_ScalingTransform != null) ? m_ScalingTransform : base.transform);
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		SkinnedMeshRenderer[] componentsInChildren2 = GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: true);
		m_Renderers = new Renderer[componentsInChildren.Length + componentsInChildren2.Length];
		Array.Copy(componentsInChildren, m_Renderers, componentsInChildren.Length);
		Array.Copy(componentsInChildren2, 0, m_Renderers, componentsInChildren.Length, componentsInChildren2.Length);
		if ((bool)m_NightLights)
		{
			Light[] componentsInChildren3 = m_NightLights.GetComponentsInChildren<Light>(includeInactive: true);
			foreach (Light light in componentsInChildren3)
			{
				m_NightLightList.Add(new VehicleNightLight(light));
			}
		}
		if ((bool)m_SecondaryNightLights)
		{
			Light[] componentsInChildren3 = m_NightLights.GetComponentsInChildren<Light>(includeInactive: true);
			foreach (Light light2 in componentsInChildren3)
			{
				m_SecondaryNightLightList.Add(new VehicleNightLight(light2));
			}
		}
		if (m_OutlineSprite != null)
		{
			m_OutlineSprite.gameObject.SetActive(value: false);
			m_OriginalOutlineSpriteLocalPos = m_OutlineSprite.transform.localPosition;
		}
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		Utils.SetLayerOnAllRecursive(base.gameObject, Utils.VEHICLE_LAYER);
		m_StaticBoundingBox.gameObject.layer = Utils.DEFAULT_LAYER;
		if ((bool)m_UgcComponent)
		{
			m_SandboxItem.MaybeCreateSandboxLabel();
		}
		Utils.SetLayerOnAllRecursive(m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
		if (!m_articulationAnimator)
		{
			m_articulationAnimator = GetComponentInChildren<BusArticulationAnimator>();
			if ((bool)m_articulationAnimator)
			{
				m_articulationAnimator.isFlipped = m_Flipped;
				m_articulationAnimator.Init();
			}
		}
		m_OriginalScale = ((m_ScalingTransform != null) ? m_ScalingTransform.localScale : Vector3.one);
		if ((bool)m_UgcComponent)
		{
			GameObject gameObject = m_UgcComponent.CreatePhysicsObject(this, Vector3.zero, Quaternion.identity);
			m_PhysicsPrefabInstantiated = gameObject.GetComponent<Poly.Physics.Vehicle>();
		}
		else
		{
			m_PhysicsPrefabInstantiated = UnityEngine.Object.Instantiate(m_PhysicsPrefab).GetComponent<Poly.Physics.Vehicle>();
		}
		m_VehicleSyncTargetChassis = m_MeshRenderer.GetComponent<VehicleSyncTarget>();
		if (m_VehicleSyncTargetChassis == null)
		{
			m_VehicleSyncTargetChassis = m_MeshRenderer.GetComponentInParent<VehicleSyncTarget>();
		}
		m_PhysicsPrefabInstantiated.gameObject.SetActive(value: false);
		if (m_ScalingTransform != null)
		{
			float num = m_ScalingTransform.localScale.x / m_OriginalScale.x;
			m_PhysicsPrefabInstantiated.transform.localScale = new Vector3(num * m_PhysicsPrefabInstantiated.m_OriginalScale.x, num * m_PhysicsPrefabInstantiated.m_OriginalScale.y, num * m_PhysicsPrefabInstantiated.m_OriginalScale.z);
		}
		m_StartFollowCamTime = float.MaxValue;
	}

	private void OnEnable()
	{
		if (m_Stub != null)
		{
			VehicleSkin[] skins = m_Stub.m_Skins;
			for (int i = 0; i < skins.Length; i++)
			{
				skins[i].DoOnEnable();
			}
		}
	}

	private void OnDestroy()
	{
		VehicleSkin[] skins = m_Stub.m_Skins;
		for (int i = 0; i < skins.Length; i++)
		{
			skins[i].DoOnDestroy();
		}
		if (Vehicles.m_Vehicles.Contains(this))
		{
			Vehicles.m_Vehicles.Remove(this);
		}
		if ((bool)m_Physics)
		{
			UnityEngine.Object.Destroy(m_Physics.gameObject);
			m_Physics = null;
			m_PhysicsChassis = null;
		}
		if ((bool)m_PhysicsPrefabInstantiated)
		{
			UnityEngine.Object.Destroy(m_PhysicsPrefabInstantiated.gameObject);
			m_PhysicsPrefabInstantiated = null;
		}
		if (m_VehicleWheelsLine != null)
		{
			m_VehicleWheelsLine.Destroy();
		}
		if (m_CenterOfMassIcon != null)
		{
			UnityEngine.Object.Destroy(m_CenterOfMassIcon);
		}
	}

	public void UpdateManual()
	{
		if (m_ReachedVictoryFlag)
		{
			m_SecondsSinceVictoryFlagReached += Time.deltaTime;
			MaybeTurnOffLights();
		}
		MaybePlaySplashFX();
		UpdateFloatingText();
		UpdateNoProgressTimer();
		bool rotate = (m_PhysicsChassis ? (m_PhysicsChassis.transform.localScale.x < 0f) : m_Flipped);
		SyncVisual(rotate);
		if ((bool)m_Physics && !m_Physics.isVisible && m_isRenderingEnabled)
		{
			DisableMeshRendering();
		}
		if (IsSimulating())
		{
			MaybePlaySpeedAnimations();
			if (Time.unscaledTime > m_StartFollowCamTime)
			{
				VehicleFollow.MaybeStartFollowing(this);
				m_StartFollowCamTime = float.MaxValue;
			}
		}
		if ((bool)m_articulationAnimator)
		{
			m_articulationAnimator.UpdateArticulation();
		}
	}

	public void FixedUpdateManual()
	{
		if ((bool)m_Physics && m_Physics.isVisible)
		{
			SyncPositionAndRotation();
			float num = Vector3.Distance(base.transform.position, m_LastPos) / Time.fixedDeltaTime;
			if (!Mathf.Approximately(num, 0f) || !(m_Speed > 0.05f))
			{
				m_Speed = num;
			}
			m_LastPos = base.transform.position;
		}
	}

	public void SetDefaultPhysicsProperties()
	{
		Poly.Physics.Vehicle component = m_PhysicsPrefab.GetComponent<Poly.Physics.Vehicle>();
		m_TargetSpeed = component.targetVelocity;
		m_Mass = component.mass;
		m_Acceleration = component.acceleration;
		m_BrakingForceMultiplier = component.brakingForceMultiplier;
		m_IdleOnDownhill = component.idleOnDownhill;
		m_DesiredAcceleration = component.desiredAcceleration;
		m_ShocksMultiplier = component.shocksMultiplier;
	}

	public void TouchedVictoryFlag()
	{
		SetPhysicsVehicleTargetSpeed(0f);
		m_SecondsSinceVictoryFlagReached = 0f;
		m_ReachedVictoryFlag = true;
	}

	public void TouchedStopCheckpoint()
	{
		SetPhysicsVehicleTargetSpeed(0f);
		m_ReachedStopCheckpoint = true;
	}

	public int NumCheckpointsRemaining()
	{
		return m_RemainingCheckpoints.Count;
	}

	public void EnablePhysics()
	{
		if ((bool)m_Physics)
		{
			return;
		}
		GameObject gameObject = null;
		gameObject = ((!m_UgcComponent) ? UnityEngine.Object.Instantiate(m_PhysicsPrefab, m_SpawnPos, m_SpawnRot) : m_UgcComponent.CreatePhysicsObject(this, m_SpawnPos, m_SpawnRot));
		if (!gameObject)
		{
			return;
		}
		m_Physics = gameObject.GetComponent<Poly.Physics.Vehicle>();
		if ((bool)m_Physics)
		{
			if (m_ScalingTransform != null)
			{
				float num = m_ScalingTransform.localScale.x / m_OriginalScale.x;
				m_Physics.transform.localScale = new Vector3(num * m_Physics.m_OriginalScale.x, num * m_Physics.m_OriginalScale.y, num * m_Physics.m_OriginalScale.z);
			}
			m_Physics.FlipChassisAndJoints_EditorMode(m_Flipped, (Vec2)m_SpawnPos, (Vec2)(m_SpawnRot * Vector3.right));
			SetPhysicsVehicleProperties();
			SetPhysicsVehicleTargetSpeed(0f, soundControl: false);
			SetUpSync(gameObject);
			m_LastPos = base.transform.position;
			if ((bool)WheelsUnderWater())
			{
				m_WheelsUnderWaterAtSimulationStart = true;
			}
		}
	}

	public void PhysicsVehicleFlip()
	{
		if ((bool)m_Physics)
		{
			m_Physics.isFlipped = !m_Physics.isFlipped;
		}
		if ((bool)m_Physics && (bool)m_articulationAnimator)
		{
			m_articulationAnimator.isFlipped = m_Physics.isFlipped;
		}
	}

	public void StartSimulation()
	{
		if (m_Physics == null)
		{
			Debug.LogWarningFormat("Tried to start vehicle simulation without physics enabled");
			return;
		}
		if (!m_Physics.isVisible)
		{
			m_Physics.isVisible = true;
			EnableMeshRendering();
		}
		PlayStopAnimationImmediate();
		m_SpeedAnimState = SpeedAnimState.STOPPED;
		MaybeTurnOnLights();
		SetPhysicsVehicleTargetSpeed(m_TargetSpeed);
		if (GameStateManager.GetState() != GameState.SIM)
		{
			return;
		}
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(base.gameObject);
		if (!PreferredVehicleInStage(stageWithUnit, m_Guid))
		{
			if (Vehicles.m_Vehicles.IndexOf(this) == 0)
			{
				VehicleFollow.MaybeStartFollowing(this);
				m_StartFollowCamTime = float.MaxValue;
			}
			else if (!VehicleSimulatingInStage(stageWithUnit, m_Guid))
			{
				m_StartFollowCamTime = Time.unscaledTime + VEHICLE_FOLLOW_CAMERA_DELAY_SECONDS;
			}
		}
	}

	public void SetPhysicsVehicleTargetSpeed(float speed, bool soundControl = true)
	{
		if (!m_Physics)
		{
			return;
		}
		m_Physics.targetVelocity = speed;
		if (m_Physics._topSpeedMultiplier != 0f)
		{
			m_Physics.topSpeed = m_TargetSpeed * m_Physics._topSpeedMultiplier;
		}
		if (soundControl && m_VehicleAudio != null)
		{
			if (speed >= 0.1f)
			{
				m_VehicleAudio.StartEngineSound(this);
			}
			else
			{
				m_VehicleAudio.StopEngineSound(playStopSound: true);
			}
		}
	}

	public bool IsSimulating()
	{
		return m_Physics != null;
	}

	public void Restore()
	{
		if ((bool)m_Physics && !m_Physics.isVisible)
		{
			m_Physics.isVisible = true;
			EnableMeshRendering();
		}
		m_ReachedVictoryFlag = false;
		m_ReachedStopCheckpoint = false;
		m_Speed = 0f;
		m_NumSecondsNoProgressWithMotorOn = 0f;
		m_WheelsUnderWaterAtSimulationStart = false;
		m_PlayedSplashFX = false;
		RestoreVisualSync();
		PlayStopAnimationImmediate();
		m_SpeedAnimState = SpeedAnimState.STOPPED;
		if ((bool)m_articulationAnimator)
		{
			m_articulationAnimator.UpdateArticulation();
		}
		m_RemainingCheckpoints.Clear();
	}

	public void ResetCheckpoints()
	{
		m_RemainingCheckpoints.Clear();
		m_RemainingCheckpoints.AddRange(m_Checkpoints);
	}

	public void EndSimulation()
	{
		if ((bool)m_Physics)
		{
			if (!m_Physics.isVisible)
			{
				m_Physics.isVisible = true;
				EnableMeshRendering();
			}
			if (m_VehicleAudio != null)
			{
				m_VehicleAudio.StopEngineSound();
			}
			UnityEngine.Object.Destroy(m_Physics.gameObject);
			m_Physics = null;
			m_PhysicsChassis = null;
		}
	}

	public string GetTextMeshString()
	{
		return EventTimelines.GetStageLabelForUnit(base.gameObject);
	}

	public void TurnLightsOn()
	{
		if ((bool)m_DayLights)
		{
			m_DayLights.SetActive(value: false);
		}
		if ((bool)m_NightLights)
		{
			m_NightLights.SetActive(value: true);
		}
		if ((bool)m_SecondaryDayLights)
		{
			m_SecondaryDayLights.SetActive(value: false);
		}
		if ((bool)m_SecondaryNightLights)
		{
			m_SecondaryNightLights.SetActive(value: true);
		}
		if (Theme.m_Instance.m_ThemeStub.m_ThemeTimeOfDay == ThemeTimeOfDay.NIGHT)
		{
			RefreshNightLightIntensity();
		}
		m_LightsAreOn = true;
	}

	public void RefreshNightLightIntensity()
	{
		float pointScale = (Mathf.Approximately(Vehicles.m_PointScale, 1f) ? Theme.m_Instance.m_ThemeStub.m_VehicleNightPointLightsScale : Vehicles.m_PointScale);
		float spotScale = (Mathf.Approximately(Vehicles.m_SpotScale, 1f) ? Theme.m_Instance.m_ThemeStub.m_VehicleNightSpotLightsScale : Vehicles.m_SpotScale);
		ScaleLightIntensity(m_NightLightList, pointScale, spotScale);
		ScaleLightIntensity(m_SecondaryNightLightList, pointScale, spotScale);
	}

	public void TurnLightsOff()
	{
		if ((bool)m_DayLights)
		{
			m_DayLights.SetActive(value: true);
			Light[] componentsInChildren = m_DayLights.GetComponentsInChildren<Light>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.SetActive(value: false);
			}
		}
		if ((bool)m_NightLights)
		{
			m_NightLights.SetActive(value: false);
		}
		if ((bool)m_SecondaryDayLights)
		{
			m_SecondaryDayLights.SetActive(value: true);
		}
		if ((bool)m_SecondaryNightLights)
		{
			m_SecondaryNightLights.SetActive(value: false);
		}
		m_LightsAreOn = false;
	}

	public bool CanPickUpCheckpoint(Checkpoint checkpoint)
	{
		if (!m_RemainingCheckpoints.Contains(checkpoint))
		{
			return false;
		}
		if (m_OrderedCheckpoints && checkpoint != m_RemainingCheckpoints[0])
		{
			return false;
		}
		return true;
	}

	public void PickUpCheckpoint(Checkpoint checkpoint)
	{
		if (CanPickUpCheckpoint(checkpoint))
		{
			DeterminismLog.LogEvent(null, Poly.Determinism.EventType.PickUpCheckpoint);
			m_RemainingCheckpoints.Remove(checkpoint);
		}
	}

	public bool HasPickedUpCheckpoint(Checkpoint checkpoint)
	{
		return !m_RemainingCheckpoints.Contains(checkpoint);
	}

	public void ResolveCheckpointGuids()
	{
		m_Checkpoints.Clear();
		foreach (string checkpointGuid in m_CheckpointGuids)
		{
			Checkpoint checkpoint = Checkpoints.FindByGuid(checkpointGuid);
			if ((bool)checkpoint)
			{
				m_Checkpoints.Add(checkpoint);
			}
		}
	}

	public void SetFlagAndCheckpointColor()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.SetColor(GetFlagColor());
		}
		VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.FindTriggerThatStopsVehicle(m_Guid);
		if ((bool)vehicleStopTrigger)
		{
			vehicleStopTrigger.SetFlagColor(GetFlagColor());
		}
	}

	public void SyncGameplayTriggers()
	{
		if (!m_Physics || m_SyncTargets == null || !Bridge.IsSimulating())
		{
			return;
		}
		if ((bool)m_GameplayTriggers && (bool)m_PhysicsChassis)
		{
			m_GameplayTriggers.localScale = new Vector3(Mathf.Sign(m_PhysicsChassis.transform.localScale.x) * Mathf.Abs(m_GameplayTriggers.localScale.x), m_GameplayTriggers.localScale.y, m_GameplayTriggers.localScale.z);
		}
		VehicleSyncTarget[] syncTargets = m_SyncTargets;
		foreach (VehicleSyncTarget vehicleSyncTarget in syncTargets)
		{
			if ((bool)vehicleSyncTarget && vehicleSyncTarget.m_type == VehicleSyncTarget.Type.GameplayTrigger)
			{
				vehicleSyncTarget.Sync(interpolate: false);
			}
		}
	}

	public void SyncVisual(bool rotate180)
	{
		if (!m_Physics || m_SyncTargets == null || !Bridge.IsSimulating())
		{
			return;
		}
		VehicleSyncTarget[] syncTargets = m_SyncTargets;
		foreach (VehicleSyncTarget vehicleSyncTarget in syncTargets)
		{
			if ((bool)vehicleSyncTarget && (vehicleSyncTarget.m_type == VehicleSyncTarget.Type.VisualMesh || vehicleSyncTarget.m_type == VehicleSyncTarget.Type.Invalid))
			{
				vehicleSyncTarget.Sync(interpolate: true, rotate180);
			}
		}
	}

	public void RestoreVisualSync()
	{
		base.transform.position = m_SpawnPos;
		base.transform.rotation = m_SpawnRot;
		SetLocalScale(m_Flipped);
		if (m_SyncTargets == null)
		{
			return;
		}
		VehicleSyncTarget[] syncTargets = m_SyncTargets;
		foreach (VehicleSyncTarget vehicleSyncTarget in syncTargets)
		{
			if ((bool)vehicleSyncTarget)
			{
				vehicleSyncTarget.RestoreDefaultTransform();
			}
		}
	}

	public void SetLocalScale(bool flipped)
	{
		if (flipped ^ m_AreVisualMeshTransformsFlipped)
		{
			m_MeshRenderer.transform.localScale = new Vector3(0f - Mathf.Abs(m_MeshRenderer.transform.localScale.x), m_MeshRenderer.transform.localScale.y, m_MeshRenderer.transform.localScale.z);
			VehicleSyncTarget[] componentsInChildren = m_MeshRenderer.GetComponentsInChildren<VehicleSyncTarget>();
			if (m_UgcComponent != null)
			{
				componentsInChildren = m_UgcComponent.GetComponentsInChildren<VehicleSyncTarget>();
			}
			Dictionary<VehicleSyncTarget, T3> dictionary = new Dictionary<VehicleSyncTarget, T3>();
			VehicleSyncTarget[] array = componentsInChildren;
			foreach (VehicleSyncTarget vehicleSyncTarget in array)
			{
				if (vehicleSyncTarget.m_type == VehicleSyncTarget.Type.VisualMesh || vehicleSyncTarget.m_type == VehicleSyncTarget.Type.Invalid)
				{
					dictionary.Add(vehicleSyncTarget, new T3
					{
						pos = vehicleSyncTarget.transform.position,
						q = vehicleSyncTarget.transform.rotation
					});
				}
			}
			m_MeshRenderer.transform.localScale = new Vector3(Mathf.Abs(m_MeshRenderer.transform.localScale.x), m_MeshRenderer.transform.localScale.y, m_MeshRenderer.transform.localScale.z);
			array = componentsInChildren;
			foreach (VehicleSyncTarget vehicleSyncTarget2 in array)
			{
				if (vehicleSyncTarget2.m_type == VehicleSyncTarget.Type.VisualMesh || vehicleSyncTarget2.m_type == VehicleSyncTarget.Type.Invalid)
				{
					T3 t = dictionary[vehicleSyncTarget2];
					vehicleSyncTarget2.transform.position = t.pos;
					vehicleSyncTarget2.transform.rotation = t.q * Quaternion.AngleAxis(180f, Vector3.up);
				}
			}
			m_AreVisualMeshTransformsFlipped = flipped;
			if ((bool)m_articulationAnimator)
			{
				m_articulationAnimator.isFlipped = m_AreVisualMeshTransformsFlipped;
				m_articulationAnimator.UpdateArticulation();
			}
		}
		m_StaticBoundingBox.transform.localRotation = Quaternion.Euler(0f, flipped ? 180f : 0f, 0f);
		if (m_OutlineSplineComputer != null)
		{
			m_OutlineSplineComputer.transform.localScale = new Vector3(flipped ? (0f - Mathf.Abs(m_OutlineSplineComputer.transform.localScale.x)) : Mathf.Abs(m_OutlineSplineComputer.transform.localScale.x), m_OutlineSplineComputer.transform.localScale.y, m_OutlineSplineComputer.transform.localScale.z);
		}
		if (m_OutlineSprite != null)
		{
			m_OutlineSprite.transform.localScale = new Vector3(flipped ? (0f - Mathf.Abs(m_OutlineSprite.transform.localScale.x)) : Mathf.Abs(m_OutlineSprite.transform.localScale.x), m_OutlineSprite.transform.localScale.y, m_OutlineSprite.transform.localScale.z);
			m_OutlineSprite.transform.localPosition = new Vector3(flipped ? (0f - m_OriginalOutlineSpriteLocalPos.x) : m_OriginalOutlineSpriteLocalPos.x, m_OriginalOutlineSpriteLocalPos.y, m_OriginalOutlineSpriteLocalPos.z);
		}
		if ((bool)m_GameplayTriggers)
		{
			m_GameplayTriggers.localScale = new Vector3(flipped ? (0f - Mathf.Abs(m_GameplayTriggers.localScale.x)) : Mathf.Abs(m_GameplayTriggers.localScale.x), m_GameplayTriggers.localScale.y, m_GameplayTriggers.localScale.z);
		}
		if ((bool)m_SandboxItem)
		{
			m_SandboxItem.SetFloatingTextToDefaultPosition();
		}
	}

	public void SyncPositionAndRotation()
	{
		if ((bool)m_PhysicsChassis)
		{
			base.transform.position = m_PhysicsChassis.transform.position;
			base.transform.rotation = m_PhysicsChassis.transform.rotation;
		}
		SyncGameplayTriggers();
	}

	public bool HasModifiedPhysicsProperties()
	{
		Poly.Physics.Vehicle component = m_PhysicsPrefab.GetComponent<Poly.Physics.Vehicle>();
		if (!Mathf.Approximately(Mathf.Abs(m_TargetSpeed), component.targetVelocity))
		{
			return true;
		}
		if (!Mathf.Approximately(m_Mass, component.mass))
		{
			return true;
		}
		if (!Mathf.Approximately(m_Acceleration, component.acceleration))
		{
			return true;
		}
		if (!Mathf.Approximately(m_BrakingForceMultiplier, component.brakingForceMultiplier))
		{
			return true;
		}
		if (!Mathf.Approximately(m_DesiredAcceleration, component.desiredAcceleration))
		{
			return true;
		}
		if (!Mathf.Approximately(m_ShocksMultiplier, component.shocksMultiplier))
		{
			return true;
		}
		if (m_IdleOnDownhill != component.idleOnDownhill)
		{
			return true;
		}
		return false;
	}

	public void CopyPhysicsPropertiesFrom(Vehicle source)
	{
		m_TargetSpeed = source.m_TargetSpeed;
		m_Mass = source.m_Mass;
		m_Acceleration = source.m_Acceleration;
		m_BrakingForceMultiplier = source.m_BrakingForceMultiplier;
		m_IdleOnDownhill = source.m_IdleOnDownhill;
		m_DesiredAcceleration = source.m_DesiredAcceleration;
		m_ShocksMultiplier = source.m_ShocksMultiplier;
	}

	public void EnableOutline()
	{
		m_SandboxItem.m_OutlineGroup.EnableOutline();
		if (m_OutlineSprite != null)
		{
			m_OutlineSprite.gameObject.SetActive(value: true);
		}
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
		if (m_OutlineSprite != null)
		{
			m_OutlineSprite.gameObject.SetActive(value: false);
		}
	}

	public void EnableMeshRendering()
	{
		Renderer[] renderers = m_Renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}
		m_isRenderingEnabled = true;
	}

	public void UpdateOutline()
	{
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			m_ForceShowMeshTimer -= Time.unscaledDeltaTime;
			if (m_ForceShowMeshTimer > 0f)
			{
				if (!m_isRenderingEnabled)
				{
					EnableMeshRendering();
					DisableOutline();
				}
			}
			else if (m_isRenderingEnabled)
			{
				DisableMeshRendering();
				EnableOutline();
			}
		}
		else if (!m_isRenderingEnabled)
		{
			EnableMeshRendering();
		}
		if (!m_HasCreatedOutline && m_OutlineSprite == null)
		{
			m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (m_Outline != null && m_SandboxItem.IsOutlineDirty() && GameStateManager.GetState() == GameState.SANDBOX)
		{
			m_SandboxItem.UpdateOutlineFromSpline(m_Outline, m_OutlineSplineComputer);
			m_SandboxItem.SetOutlineDirty(dirty: false);
		}
		if (m_OutlineSprite != null && m_SandboxItem.IsOutlineDirty() && m_ForceShowMeshTimer < 0f)
		{
			if (GameStateManager.GetState() == GameState.SANDBOX)
			{
				EnableSpriteOutline();
			}
			m_SandboxItem.SetOutlineDirty(dirty: false);
		}
	}

	public void EnterSandboxMode()
	{
		m_ForceShowMeshTimer = 0f;
		DisableMeshRendering();
		EnableSpriteOutline();
	}

	public void EnableSpriteOutline()
	{
		if (m_OutlineSprite != null)
		{
			m_OutlineSprite.gameObject.SetActive(value: true);
		}
	}

	public void SetSpriteOutlineColor(Color color)
	{
		if (m_OutlineSprite != null)
		{
			m_OutlineSprite.color = color;
		}
	}

	public void UpdatePolygonShapes()
	{
		m_PolygonShapes.Clear();
		if ((bool)m_PhysicsPrefabInstantiated)
		{
			Transform2 shapeOrigin = ((Transform3)base.transform).inverse;
			PolygonCollider[] componentsInChildren = m_PhysicsPrefabInstantiated.GetComponentsInChildren<PolygonCollider>(includeInactive: true);
			foreach (PolygonCollider polygonCollider in componentsInChildren)
			{
				m_PolygonShapes.AddRange(polygonCollider.CreateConvexPolygons(in shapeOrigin, m_Flipped));
			}
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
	}

	public Vector2 ComputeCenterOfMassOfTheFirstChassisOfTheVehicleOnly()
	{
		CenterOfMassModifier componentInChildren = m_PhysicsPrefabInstantiated.GetComponentInChildren<CenterOfMassModifier>();
		if ((bool)componentInChildren)
		{
			return base.transform.TransformPoint(componentInChildren.transform.position);
		}
		m_PhysicsPrefabInstantiated.Init_GatherChassisAndWheels();
		PolygonCollider[] componentsInChildren = m_PhysicsPrefabInstantiated.chassis[0].GetComponentsInChildren<PolygonCollider>(includeInactive: true);
		Transform2 shapeOrigin = ((Transform3)base.transform).inverse;
		List<PolygonShape> list = new List<PolygonShape>();
		PolygonCollider[] array = componentsInChildren;
		foreach (PolygonCollider polygonCollider in array)
		{
			list.AddRange(polygonCollider.CreateConvexPolygons(in shapeOrigin, m_Flipped));
		}
		Shape[] shapes = list.ToArray();
		return InertiaComputer.ComputeInfoFromShapes(shapes).com;
	}

	public void Debug_VisualizePolygonShapes()
	{
		foreach (PolygonShape polygonShape in m_PolygonShapes)
		{
			polygonShape.DrawGizmos(Transform2.identity);
		}
	}

	public bool OverlapsRect(Rect rect)
	{
		PolygonShape polygonShape = PolygonShape.FromRect(rect.center, rect.size);
		polygonShape.radius = 0f;
		return Utils.PolygonShapeOverlapsShapes(polygonShape, m_PolygonShapes);
	}

	public bool OverlapsPolygonShape(PolygonShape shape)
	{
		return Utils.PolygonShapeOverlapsShapes(shape, m_PolygonShapes);
	}

	public string GetVehicleInfoMass()
	{
		return Utils.FormatWeight(m_Mass * BridgePhysics.KgToPg);
	}

	public string GetVehicleInfoSpeed()
	{
		return Utils.FormatSpeed(m_TargetSpeed);
	}

	public string GetVehicleInfoAcceleration()
	{
		return Utils.FormatAcceleration(m_DesiredAcceleration);
	}

	public string GetVehicleInfoHorsePower()
	{
		return Utils.FormatAcceleration(m_Acceleration);
	}

	public void ResolveOverlap()
	{
		bool flag = false;
		bool flag2;
		do
		{
			flag2 = false;
			foreach (Vehicle vehicle in Vehicles.m_Vehicles)
			{
				if (vehicle.gameObject.activeInHierarchy && vehicle != this && OverlapsVehicle(vehicle))
				{
					base.transform.Translate(0f - GameGrid.m_Spacing, 0f, 0f, Space.World);
					flag2 = true;
					flag = true;
				}
			}
		}
		while (flag2);
		if (flag)
		{
			base.transform.Translate(0f - GameGrid.m_Spacing, 0f, 0f, Space.World);
			UpdatePolygonShapes();
		}
	}

	public WaterBlock WheelsUnderWater()
	{
		if (m_SyncTargets == null)
		{
			return null;
		}
		if (SandboxSettings.m_NoWater)
		{
			return null;
		}
		VehicleSyncTarget[] syncTargets = m_SyncTargets;
		foreach (VehicleSyncTarget vehicleSyncTarget in syncTargets)
		{
			if (vehicleSyncTarget.m_VehicleSyncPart >= VehicleSyncPart.WHEELS_BEGIN && vehicleSyncTarget.m_VehicleSyncPart < VehicleSyncPart.WHEELS_END && vehicleSyncTarget.m_type == VehicleSyncTarget.Type.GameplayTrigger)
			{
				WaterBlock waterBlock = WaterBlocks.PositionInWater(vehicleSyncTarget.transform.position);
				if ((bool)waterBlock)
				{
					return waterBlock;
				}
			}
		}
		return null;
	}

	public WaterBlock CenterPointUnderWater()
	{
		if (m_SyncTargets == null)
		{
			return null;
		}
		if (SandboxSettings.m_NoWater)
		{
			return null;
		}
		WaterBlock waterBlock = WaterBlocks.PositionInWater(m_StaticBoundingBox.transform.position);
		if ((bool)waterBlock)
		{
			return waterBlock;
		}
		return null;
	}

	public void DisableMeshRendering()
	{
		Renderer[] renderers = m_Renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
		if ((bool)m_SandboxItem.m_Label)
		{
			m_SandboxItem.m_Label.m_Text.GetComponent<MeshRenderer>().enabled = true;
		}
		m_isRenderingEnabled = false;
	}

	public void TurnWheelFillMeshOn()
	{
		if (m_WheelFillMesh != null)
		{
			m_WheelFillMesh.SetActive(value: true);
		}
		if (m_WheelFillMeshBack != null)
		{
			m_WheelFillMeshBack.SetActive(value: true);
		}
	}

	public void TurnWheelFillMeshOff()
	{
		if (m_WheelFillMesh != null)
		{
			m_WheelFillMesh.SetActive(value: false);
		}
		if (m_WheelFillMeshBack != null)
		{
			m_WheelFillMeshBack.SetActive(value: false);
		}
	}

	public Bounds ComputeBounds()
	{
		return m_StaticBoundingBox.bounds;
	}

	public void Desaturate(bool on)
	{
		m_MeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_Common.SATURATION_SHADER_ID, on ? 0f : 1f);
		Renderer[] renderers = m_Renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public Vehicle Duplicate(Vector3 offset)
	{
		VehicleProxy vehicleProxy = new VehicleProxy(this);
		vehicleProxy.m_Pos += Utils.V3toV2(offset);
		vehicleProxy.m_Guid = Utils.GenerateUniqueId();
		Vehicle vehicle = Vehicles.CreateVehicleFromProxy(vehicleProxy, SandboxLayout.CURRENT_VERSION);
		if (vehicle == null)
		{
			return null;
		}
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(base.gameObject);
		if (stageWithUnit != null)
		{
			stageWithUnit.AddUnit(vehicle.gameObject, EventUnitType.VEHICLE);
		}
		SandboxItems.CreateGoalTriggerForVehicle(vehicle);
		vehicle.m_CheckpointGuids.Clear();
		return vehicle;
	}

	public void MaybeLoadCurrentSkinTexture()
	{
		VehicleSkin currentSkin = GetCurrentSkin();
		if (currentSkin != null && currentSkin.m_Texture == null && !string.IsNullOrEmpty(currentSkin.m_PathToTexture))
		{
			Texture2D texture2D = new Texture2D(2, 2);
			byte[] array = Utils.ReadAllBytes(currentSkin.m_PathToTexture);
			if (array != null && array.Length != 0 && texture2D.LoadImage(array))
			{
				currentSkin.m_Texture = texture2D;
			}
		}
	}

	public void UploadCurrentSkinToShader()
	{
		VehicleSkin currentSkin = GetCurrentSkin();
		if (currentSkin != null && currentSkin.m_Texture != null)
		{
			m_MeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
			m_MaterialPropertyBlock.SetTexture(ShaderVariables_Common.ALBEDO_SHADER_ID, currentSkin.m_Texture);
			m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
			Renderer[] extraSkinMeshes = m_ExtraSkinMeshes;
			for (int i = 0; i < extraSkinMeshes.Length; i++)
			{
				extraSkinMeshes[i].SetPropertyBlock(m_MaterialPropertyBlock);
			}
		}
	}

	public void ApplyRandomSkin()
	{
		if (m_Stub.m_Skins.Length < 2)
		{
			return;
		}
		List<VehicleSkin> list = new List<VehicleSkin>(m_Stub.m_Skins);
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (!vehicle.gameObject.activeInHierarchy || vehicle.m_Stub.m_DisplayNameLocID != m_Stub.m_DisplayNameLocID || vehicle == this)
			{
				continue;
			}
			foreach (VehicleSkin item in list)
			{
				if (item.m_ID == vehicle.m_SkinID)
				{
					list.Remove(item);
					break;
				}
			}
		}
		VehicleSkin vehicleSkin = ((list.Count == 0) ? m_Stub.m_Skins[UnityEngine.Random.Range(0, m_Stub.m_Skins.Length)] : list[UnityEngine.Random.Range(0, list.Count)]);
		m_SkinID = vehicleSkin.m_ID;
		MaybeLoadCurrentSkinTexture();
		UploadCurrentSkinToShader();
	}

	public VehicleSkin GetCurrentSkin()
	{
		List<VehicleSkin> skinsForVehicle = VehicleSkins.GetSkinsForVehicle(this);
		if (skinsForVehicle == null)
		{
			return null;
		}
		foreach (VehicleSkin item in skinsForVehicle)
		{
			if (item.m_ID == m_SkinID)
			{
				return item;
			}
		}
		if (skinsForVehicle.Count <= 0)
		{
			return null;
		}
		return skinsForVehicle[0];
	}

	public Color GetFlagColor()
	{
		VehicleSkin currentSkin = GetCurrentSkin();
		if (!(currentSkin != null))
		{
			return Color.white;
		}
		return currentSkin.m_FlagColor;
	}

	public void ForceShowMeshBriefly()
	{
		m_ForceShowMeshTimer = Vehicles.FORCE_SHOW_VEHICLE_MESH_SECONDS;
	}

	public Sprite GetIcon()
	{
		VehicleSkin currentSkin = GetCurrentSkin();
		if (!(currentSkin != null) || !(currentSkin.m_Icon != null))
		{
			return m_Stub.m_Icon;
		}
		return currentSkin.m_Icon;
	}

	public void MaybeShowVehicleWheelsLine()
	{
		if ((GameStateManager.GetState() != GameState.SANDBOX || GameStateSandbox.m_CameraInTransition || !SandboxSelectionSet.IsSelected(m_SandboxItem)) && m_VehicleWheelsLine != null)
		{
			m_VehicleWheelsLine.SetActive(active: false);
			return;
		}
		if (m_VehicleWheelsLine == null)
		{
			m_VehicleWheelsLine = new VehicleWheelsLine();
		}
		if (Mathf.Approximately(m_RotationDegrees, 0f))
		{
			m_VehicleWheelsLine.SetActive(active: false);
		}
		else if (SandboxSelectionSet.IsSelected(m_SandboxItem))
		{
			m_VehicleWheelsLine.SetActive(active: true);
			m_VehicleWheelsLine.SyncToVehicle(this);
		}
	}

	public void TurnOffWheelsLine()
	{
		if (m_VehicleWheelsLine != null)
		{
			m_VehicleWheelsLine.SetActive(active: false);
		}
	}

	public void UpdateWheelsLineWidth()
	{
		if (m_VehicleWheelsLine != null)
		{
			m_VehicleWheelsLine.UpdateWidth();
		}
	}

	public string GetDisplayName()
	{
		return Localize.Get(m_Stub.m_DisplayNameLocID);
	}

	private bool OverlapsAnyVehicle()
	{
		return false;
	}

	private bool OverlapsVehicle(Vehicle vehicle)
	{
		return vehicle.m_StaticBoundingBox.bounds.Intersects(m_StaticBoundingBox.bounds);
	}

	private void MaybeTurnOnLights()
	{
		if (!Profiles.m_ActiveProfile.m_VehicleLights)
		{
			TurnLightsOff();
			return;
		}
		if (Theme.m_Instance.m_ThemeStub.m_ThemeTimeOfDay == ThemeTimeOfDay.NIGHT)
		{
			TurnLightsOn();
		}
		if ((bool)m_DayLights && Theme.m_Instance.m_ThemeStub.m_ThemeTimeOfDay == ThemeTimeOfDay.DAY)
		{
			Light[] componentsInChildren = m_DayLights.GetComponentsInChildren<Light>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.SetActive(value: true);
				m_LightsAreOn = true;
			}
		}
	}

	private void MaybeTurnOffLights()
	{
		if (m_LightsAreOn && m_ReachedVictoryFlag)
		{
			if (m_SecondsSinceVictoryFlagReached > GameSettings.MaxSecondsLightsOnAfterVictoryFlagReached())
			{
				TurnLightsOff();
			}
			else if (m_Speed < 0.05f)
			{
				TurnLightsOff();
			}
		}
	}

	private void SetPhysicsVehicleProperties()
	{
		SetPhysicsVehicleTargetSpeed(m_TargetSpeed, soundControl: false);
		m_Physics.isFlipped = m_Flipped;
		m_Physics.mass = m_Mass;
		m_Physics.acceleration = m_Acceleration;
		m_Physics.brakingForceMultiplier = m_BrakingForceMultiplier;
		m_Physics.idleOnDownhill = m_IdleOnDownhill;
		m_Physics.desiredAcceleration = m_DesiredAcceleration;
		m_Physics.shocksMultiplier = m_ShocksMultiplier;
		if ((bool)m_articulationAnimator)
		{
			m_articulationAnimator.isFlipped = m_Flipped;
		}
	}

	private void SetUpSync(GameObject physicsVehicle)
	{
		m_SyncTargets = GetComponentsInChildren<VehicleSyncTarget>();
		VehicleSyncSource[] componentsInChildren = physicsVehicle.GetComponentsInChildren<VehicleSyncSource>();
		VehicleSyncTarget[] syncTargets = m_SyncTargets;
		VehicleSyncSource[] array;
		foreach (VehicleSyncTarget vehicleSyncTarget in syncTargets)
		{
			vehicleSyncTarget.SaveDefaultTransform();
			array = componentsInChildren;
			foreach (VehicleSyncSource vehicleSyncSource in array)
			{
				if (vehicleSyncSource.m_VehicleSyncPart == vehicleSyncTarget.m_VehicleSyncPart)
				{
					vehicleSyncTarget.m_Source = vehicleSyncSource.transform;
					vehicleSyncTarget.transform.position = vehicleSyncSource.transform.position;
					vehicleSyncTarget.transform.rotation = vehicleSyncSource.transform.rotation;
				}
			}
		}
		array = componentsInChildren;
		foreach (VehicleSyncSource vehicleSyncSource2 in array)
		{
			if (vehicleSyncSource2.m_VehicleSyncPart == VehicleSyncPart.CHASSIS)
			{
				m_PhysicsChassis = vehicleSyncSource2.gameObject;
				break;
			}
		}
	}

	private void UpdateFloatingText()
	{
	}

	public float GetUniformScaleNormalized()
	{
		return m_ScalingTransform.localScale.x / m_OriginalScale.x;
	}

	public void SetUniformScale(float normalizedScale)
	{
		if (m_ScalingTransform != null)
		{
			m_ScalingTransform.localScale = new Vector3(normalizedScale * m_OriginalScale.x, normalizedScale * m_OriginalScale.y, normalizedScale * m_OriginalScale.z);
			if (m_Physics != null)
			{
				m_Physics.transform.localScale = new Vector3(normalizedScale * m_Physics.m_OriginalScale.x, normalizedScale * m_Physics.m_OriginalScale.y, normalizedScale * m_Physics.m_OriginalScale.z);
			}
			if (m_PhysicsPrefabInstantiated != null)
			{
				m_PhysicsPrefabInstantiated.transform.localScale = new Vector3(normalizedScale * m_PhysicsPrefabInstantiated.m_OriginalScale.x, normalizedScale * m_PhysicsPrefabInstantiated.m_OriginalScale.y, normalizedScale * m_PhysicsPrefabInstantiated.m_OriginalScale.z);
			}
		}
		UpdatePolygonShapes();
		UpdateFloatingTextWhenScaleChanges();
	}

	public void SnapToTerrainSurface()
	{
		if (UnityEngine.Physics.Raycast(new Vector3(base.transform.position.x, 1000f, 0f), Vector3.down, out var hitInfo, float.MaxValue, Utils.TERRAIN_LAYER_MASK))
		{
			base.transform.position = m_SandboxItem.SnapPosToGrid(hitInfo.point);
			m_SandboxItem.SetOutlineDirty(dirty: true);
			m_SpawnPos = base.transform.position;
		}
	}

	public void ShowCenterOfMassIcon(bool on)
	{
		if (on || (bool)m_CenterOfMassIcon)
		{
			if (on && m_CenterOfMassIcon == null)
			{
				m_CenterOfMassIcon = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_VehicleCenterOfMass, base.transform);
			}
			m_CenterOfMassIcon.SetActive(on);
			if (on)
			{
				m_CenterOfMassIcon.transform.position = ComputeCenterOfMassOfTheFirstChassisOfTheVehicleOnly();
				m_CenterOfMassIcon.transform.Translate(0f, 0f, -2f);
			}
		}
	}

	public bool IsUpsideDown()
	{
		return Vector3.Dot(base.transform.up, Vector3.up) < -0.9f;
	}

	public int GetCheckpointIndex(Checkpoint checkpoint)
	{
		for (int i = 0; i < m_Checkpoints.Count; i++)
		{
			if (m_Checkpoints[i].m_Guid == checkpoint.m_Guid)
			{
				return i;
			}
		}
		return -1;
	}

	private void UpdateFloatingTextWhenScaleChanges()
	{
		m_SandboxItem.SetFloatingTextToDefaultPosition();
	}

	private void UpdateNoProgressTimer()
	{
		if ((bool)m_Physics && m_Physics.targetVelocity > 0.01f && m_Speed < 0.09f)
		{
			m_NumSecondsNoProgressWithMotorOn += (Mathf.Approximately(Time.timeScale, 0f) ? 0f : Time.unscaledDeltaTime);
		}
		else
		{
			m_NumSecondsNoProgressWithMotorOn = 0f;
		}
	}

	private void MaybePlaySplashFX()
	{
		if (m_PlayedSplashFX)
		{
			return;
		}
		WaterBlock waterBlock = WheelsUnderWater();
		if (waterBlock != null)
		{
			if (!m_WheelsUnderWaterAtSimulationStart)
			{
				PlaySplashFX(waterBlock);
			}
			if (m_VehicleAudio != null)
			{
				m_VehicleAudio.StopEngineSound(playStopSound: false, stopImmediately: true);
			}
			m_PlayedSplashFX = true;
		}
	}

	private void PlaySplashFX(WaterBlock waterBlock)
	{
		WaterSplash.Play(new Vector3((m_StaticBoundingBox.transform.position + base.transform.up * m_StaticBoundingBox.size.y / 2f).x, waterBlock.m_Height + 0.1f, 0f), (m_SplashSize != VehicleSplashSize.LARGE) ? WaterSplashSize.SMALL : WaterSplashSize.BIG, waterBlock);
	}

	private void ScaleLightIntensity(List<VehicleNightLight> lights, float pointScale, float spotScale)
	{
		foreach (VehicleNightLight light in lights)
		{
			if (light.m_Light.type == LightType.Spot)
			{
				light.m_Light.intensity = light.m_OriginalIntensity * spotScale;
			}
			else if (light.m_Light.type == LightType.Point)
			{
				light.m_Light.intensity = light.m_OriginalIntensity * pointScale;
			}
		}
	}

	private void MaybePlaySpeedAnimations()
	{
		if (!(m_Physics == null))
		{
			if (m_Speed > 0.1f && !Mathf.Approximately(m_Physics.targetVelocity, 0f) && m_SpeedAnimState == SpeedAnimState.STOPPED)
			{
				PlayStartAnimations();
				m_SpeedAnimState = SpeedAnimState.STARTED;
			}
			if (m_Speed < 0.05f && Mathf.Approximately(m_Physics.targetVelocity, 0f) && m_SpeedAnimState == SpeedAnimState.STARTED)
			{
				PlayStopAnimations();
				m_SpeedAnimState = SpeedAnimState.STOPPED;
			}
		}
	}

	private void PlayStartAnimations()
	{
		Animator[] speedBasedAnimations = m_SpeedBasedAnimations;
		for (int i = 0; i < speedBasedAnimations.Length; i++)
		{
			speedBasedAnimations[i].SetTrigger("start");
		}
	}

	private void PlayStopAnimations()
	{
		Animator[] speedBasedAnimations = m_SpeedBasedAnimations;
		for (int i = 0; i < speedBasedAnimations.Length; i++)
		{
			speedBasedAnimations[i].SetTrigger("stop");
		}
	}

	private void PlayStopAnimationImmediate()
	{
		Animator[] speedBasedAnimations = m_SpeedBasedAnimations;
		for (int i = 0; i < speedBasedAnimations.Length; i++)
		{
			speedBasedAnimations[i].CrossFade("stopped", 0f, 0, 1f);
		}
	}

	private bool VehicleSimulatingInStage(EventStage stage, string excludeVehicleGuid)
	{
		foreach (EventUnit unit in stage.m_Units)
		{
			Vehicle vehicle = unit.GetVehicle();
			if (vehicle != null && vehicle.m_Guid != excludeVehicleGuid && !vehicle.m_ReachedVictoryFlag && unit.HasStartedSimulation())
			{
				return true;
			}
		}
		return false;
	}

	private bool IsPreferredVehicleInStage(EventStage stage, string guid)
	{
		if (stage == null || !VehicleFollow.m_PreferredVehicleInStage.ContainsKey(stage))
		{
			return false;
		}
		foreach (EventUnit unit in stage.m_Units)
		{
			Vehicle vehicle = unit.GetVehicle();
			if (vehicle != null && vehicle.m_Guid == guid && vehicle.m_Guid == VehicleFollow.m_PreferredVehicleInStage[stage] && !vehicle.m_ReachedVictoryFlag)
			{
				return true;
			}
		}
		return false;
	}

	private bool PreferredVehicleInStage(EventStage stage, string excludeVehicleGuid)
	{
		if (stage == null || !VehicleFollow.m_PreferredVehicleInStage.ContainsKey(stage))
		{
			return false;
		}
		foreach (EventUnit unit in stage.m_Units)
		{
			Vehicle vehicle = unit.GetVehicle();
			if (vehicle != null && vehicle.m_Guid != excludeVehicleGuid && vehicle.m_Guid == VehicleFollow.m_PreferredVehicleInStage[stage] && !vehicle.m_ReachedVictoryFlag)
			{
				return true;
			}
		}
		return false;
	}
}
