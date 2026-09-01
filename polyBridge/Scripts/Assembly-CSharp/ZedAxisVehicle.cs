using System;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Dreamteck.Splines;
using Poly.Base;
using Poly.Collide;
using Poly.Collide.Unity;
using Poly.Physics;
using Poly.Physics.Gameplay;
using UnityEngine;

public class ZedAxisVehicle : MonoBehaviour
{
	public ZedAxisVehicleStub m_Stub;

	public float m_DefaultSpeed;

	public MeshRenderer m_MeshRenderer;

	public Transform m_ScalingTransform;

	public BoxCollider m_BoxCollider;

	public GameObject m_Wake;

	[Header("Outlines")]
	public MeshRenderer m_OutlineMeshRenderer;

	public MeshFilter m_OutlineMeshFilter;

	public SplineComputer m_OutlineSplineComputer;

	[Header("Cutting")]
	public GameObject[] m_CuttingQuadsEditorOnly;

	public CuttingController_TwoPlanes[] m_CuttingPlaneControllers;

	[Header("Physics")]
	public GameObject m_PhysicsPrefab;

	public PlaceableCollisionInfo m_CollisionInfo;

	[Header("Profile Outlines")]
	public GameObject m_ProfileOutlines;

	[Header("FX")]
	public GameObject m_DayLights;

	public GameObject m_NightLights;

	[Header("Sound")]
	[SoundGroup]
	public string m_InAudioGroup = "[None]";

	[SoundGroup]
	public string m_LoopAudioGroup = "[None]";

	[SoundGroup]
	public string m_OutAudioGroup = "[None]";

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public float m_TimeDelaySeconds;

	[NonSerialized]
	public Vector3 m_SpawnPos;

	[NonSerialized]
	public float m_Speed;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public float m_RotationDegrees;

	[NonSerialized]
	public Vector3 m_OriginalScale;

	[NonSerialized]
	public string m_ModId;

	[NonSerialized]
	public bool m_Reverse;

	[NonSerialized]
	public bool m_SnapToWaterLine;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private GameObject m_Physics;

	private Vector3 m_PhysicsOriginalScale;

	private bool m_HasCreatedOutlineMesh;

	private List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	private SoundGroupVariation m_StartSound;

	private SoundGroupVariation m_MoveLoop;

	private ZedAxisVehicleUGC m_UgcComponent;

	private readonly Quaternion QUATERNION_180 = Quaternion.Euler(0f, 180f, 0f);

	private int m_NumFramesOnCurrentProfile;

	private int m_NumFramesBeforeProfileSwitchAllowed;

	private int m_CurrentProfileIndex = -1;

	private Transform[] m_GameplayProfiles;

	private Transform[] m_PhysicsProfiles;

	private void Awake()
	{
		m_BoxCollider = GetComponentInChildren<BoxCollider>(includeInactive: true);
		m_BoxCollider.transform.position = new Vector3(m_BoxCollider.transform.position.x, m_BoxCollider.transform.position.y, 0f);
		m_SandboxItem = GetComponent<SandboxItem>();
		m_SandboxItem.MaybeCreateSandboxLabel();
		m_UgcComponent = GetComponent<ZedAxisVehicleUGC>();
		if (m_UgcComponent != null)
		{
			m_UgcComponent.CreateSplines(this);
		}
		GameObject[] cuttingQuadsEditorOnly = m_CuttingQuadsEditorOnly;
		for (int i = 0; i < cuttingQuadsEditorOnly.Length; i++)
		{
			cuttingQuadsEditorOnly[i].SetActive(value: false);
		}
		if ((bool)m_Wake)
		{
			m_Wake.layer = Utils.NO_RENDER_LAYER;
		}
		if ((bool)m_ProfileOutlines)
		{
			m_ProfileOutlines.SetActive(value: false);
		}
		if (m_OutlineMeshRenderer != null)
		{
			m_OutlineMeshRenderer.gameObject.layer = Utils.BUILD_ZONE_LAYER;
		}
		m_OriginalScale = ((m_ScalingTransform != null) ? m_ScalingTransform.localScale : Vector3.one);
	}

	private void OnDestroy()
	{
		if (ZedAxisVehicles.m_Vehicles.Contains(this))
		{
			ZedAxisVehicles.m_Vehicles.Remove(this);
		}
		if ((bool)m_Physics)
		{
			UnityEngine.Object.Destroy(m_Physics);
			m_Physics = null;
		}
		DisposeProfiles();
	}

	public ZedAxisVehicleType GetVehicleType()
	{
		if (m_Stub == null)
		{
			Debug.LogWarning("Calling GetVehicleType when m_Stub is null");
			return ZedAxisVehicleType.BOAT;
		}
		return m_Stub.m_Type;
	}

	public void UpdateManual()
	{
		if ((bool)SingletonBehaviour<World>.instance)
		{
			float currentFractionOfFixedFrame = SingletonBehaviour<World>.instance.currentFractionOfFixedFrame;
			m_MeshRenderer.transform.localPosition = Vector3.back / m_ScalingTransform.localScale.z * currentFractionOfFixedFrame * m_Speed * Time.fixedDeltaTime;
		}
		else
		{
			m_MeshRenderer.transform.localPosition = Vector3.zero;
		}
		if (TravelledCompletelyOutOfWorld() && m_ScalingTransform.gameObject.activeInHierarchy)
		{
			m_ScalingTransform.gameObject.SetActive(value: false);
			FadeOutLoopSound();
			if (m_MoveLoop != null && m_OutAudioGroup != "[None]")
			{
				SimAudio.Play(m_OutAudioGroup, base.transform.position);
			}
			if (LightsAreOn())
			{
				TurnLightsOff();
			}
		}
		if ((bool)m_Wake)
		{
			float num = m_MeshRenderer.bounds.size.z / 2f;
			m_Wake.transform.rotation = Quaternion.identity;
			if (m_Reverse)
			{
				m_Wake.transform.position = m_MeshRenderer.transform.position + new Vector3(0f, -250f, num * 0.7f);
				m_Wake.transform.rotation = QUATERNION_180;
			}
			else
			{
				m_Wake.transform.position = m_MeshRenderer.transform.position + new Vector3(0f, -250f, 0f - num * 0.7f);
			}
		}
	}

	public void FixedUpdateManual()
	{
		float num = m_Speed * Time.fixedDeltaTime;
		base.transform.Translate(new Vector3(0f, 0f, 0f - num));
		UpdateProfiles();
	}

	public void StartSimulation()
	{
		if (m_Physics == null)
		{
			Debug.LogWarningFormat("Tried to start zed axis vehicle simulation without physics enabled");
			return;
		}
		base.gameObject.SetActive(value: true);
		m_ScalingTransform.gameObject.SetActive(value: true);
		if (GameStateManager.GetState() == GameState.SIM)
		{
			if (m_InAudioGroup != "[None]")
			{
				m_StartSound = SimAudio.PlaySound3DFollowTransform(m_InAudioGroup, base.transform);
			}
			if (m_LoopAudioGroup != "[None]")
			{
				m_MoveLoop = SimAudio.PlaySound3DFollowTransform(m_LoopAudioGroup, base.transform);
			}
		}
		MaybeTurnOnLights();
		MaybeTurnOnWake();
	}

	public void EndSimulation()
	{
		if ((bool)m_Physics)
		{
			UnityEngine.Object.Destroy(m_Physics);
			m_Physics = null;
		}
		base.gameObject.SetActive(value: false);
	}

	public bool IsSimulating()
	{
		return m_Physics != null;
	}

	public void EnablePhysics()
	{
		if (!m_Physics)
		{
			if (m_UgcComponent != null)
			{
				m_Physics = m_UgcComponent.CreatePhysicsObject(this);
			}
			else
			{
				m_Physics = UnityEngine.Object.Instantiate(m_PhysicsPrefab, m_SpawnPos, base.transform.rotation);
			}
			m_PhysicsOriginalScale = m_Physics.transform.localScale;
			if (m_ScalingTransform != null)
			{
				float num = m_ScalingTransform.localScale.x / m_OriginalScale.x;
				m_Physics.transform.localScale = new Vector3(num * m_PhysicsOriginalScale.x, num * m_PhysicsOriginalScale.y, num * m_PhysicsOriginalScale.z);
			}
			InitProfiles(m_ProfileOutlines, m_Physics);
		}
	}

	public void Restore()
	{
		base.transform.position = m_SpawnPos;
		m_ScalingTransform.gameObject.SetActive(value: true);
	}

	public string GetTextMeshString()
	{
		return EventTimelines.GetStageLabelForUnit(base.gameObject);
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		m_OutlineMeshRenderer.gameObject.SetActive(value: false);
		m_MeshRenderer.gameObject.SetActive(value: true);
		m_ScalingTransform.gameObject.SetActive(value: true);
		if ((bool)m_Wake)
		{
			m_Wake.SetActive(value: true);
		}
	}

	public void EnableOutlineMeshRendering()
	{
		m_OutlineMeshRenderer.gameObject.SetActive(value: true);
		m_MeshRenderer.gameObject.SetActive(value: false);
		m_ScalingTransform.gameObject.SetActive(value: true);
		if ((bool)m_Wake)
		{
			m_Wake.SetActive(value: false);
		}
	}

	public void Hide(bool hide)
	{
		m_MeshRenderer.gameObject.SetActive(!hide);
		m_OutlineMeshRenderer.gameObject.SetActive(!hide);
	}

	public void UpdateOutline()
	{
		if (!m_HasCreatedOutline)
		{
			m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (!m_HasCreatedOutlineMesh)
		{
			m_OutlineMeshFilter.mesh = CreateOutlineMeshFromSpline(m_OutlineSplineComputer);
			m_OutlineMeshRenderer.transform.localPosition = m_OutlineSplineComputer.transform.localPosition;
			m_HasCreatedOutlineMesh = true;
		}
		if (m_SandboxItem.IsOutlineDirty())
		{
			m_SandboxItem.UpdateOutlineFromSpline(m_Outline, m_OutlineSplineComputer);
			m_SandboxItem.SetOutlineDirty(dirty: false);
			m_Outline.SetActive(active: true);
			m_Outline.m_VectorLine.Draw3DAuto();
		}
	}

	public void OnlyDrawOutline()
	{
		m_OutlineMeshRenderer.gameObject.SetActive(value: false);
		m_MeshRenderer.gameObject.SetActive(value: false);
	}

	public void UpdatePolygonShapes()
	{
		m_PolygonShapes.Clear();
		if (m_CollisionInfo != null)
		{
			m_PolygonShapes.AddRange(m_CollisionInfo.CreatePolygonShapes_ForBuildMode());
		}
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void UpdateFloatingTextWhenScaleChanges()
	{
		m_SandboxItem.SetFloatingTextToDefaultPosition();
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
		}
		if (m_Physics != null)
		{
			m_Physics.transform.localScale = new Vector3(normalizedScale * m_PhysicsOriginalScale.x, normalizedScale * m_PhysicsOriginalScale.y, normalizedScale * m_PhysicsOriginalScale.z);
		}
		UpdatePolygonShapes();
		UpdateFloatingTextWhenScaleChanges();
	}

	public bool TravelledCompletelyOutOfWorld()
	{
		float num = m_MeshRenderer.bounds.size.z / 2f;
		if (m_Reverse)
		{
			return base.transform.position.z > ZedAxisVehicles.DEFAULT_SPAWN_IN_Z + num;
		}
		return base.transform.position.z < ZedAxisVehicles.DEFAULT_SPAWN_OUT_Z - num;
	}

	public void LinkToCuttingPlane(GameObject plane1, GameObject plane2)
	{
		CuttingController_TwoPlanes[] cuttingPlaneControllers = m_CuttingPlaneControllers;
		foreach (CuttingController_TwoPlanes obj in cuttingPlaneControllers)
		{
			obj.plane1 = plane1;
			obj.plane2 = plane2;
			obj.UpdateShaderProperties();
		}
	}

	public void UnLinkFromCuttingPlane()
	{
		CuttingController_TwoPlanes[] cuttingPlaneControllers = m_CuttingPlaneControllers;
		foreach (CuttingController_TwoPlanes obj in cuttingPlaneControllers)
		{
			obj.plane1 = null;
			obj.plane2 = null;
			obj.UpdateShaderProperties();
		}
	}

	public void FadeOutLoopSound()
	{
		if (m_MoveLoop != null)
		{
			m_MoveLoop.FadeOutNowAndStop();
		}
		if (m_StartSound != null)
		{
			m_StartSound.FadeOutNowAndStop();
		}
		m_MoveLoop = null;
		m_StartSound = null;
	}

	public void StopLoopSoundImmediate()
	{
		if (m_MoveLoop != null)
		{
			m_MoveLoop.Stop();
		}
		if (m_StartSound != null)
		{
			m_StartSound.Stop();
		}
		m_MoveLoop = null;
		m_StartSound = null;
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
	}

	public void TurnLightsOff()
	{
		if ((bool)m_DayLights)
		{
			m_DayLights.SetActive(value: true);
		}
		if ((bool)m_NightLights)
		{
			m_NightLights.SetActive(value: false);
		}
	}

	public bool LightsAreOn()
	{
		if (!m_NightLights)
		{
			return false;
		}
		return m_NightLights.activeInHierarchy;
	}

	public ZedAxisVehicle Duplicate(Vector3 offset)
	{
		ZedAxisVehicleProxy zedAxisVehicleProxy = new ZedAxisVehicleProxy(this);
		zedAxisVehicleProxy.m_Pos += Utils.V3toV2(offset);
		zedAxisVehicleProxy.m_Guid = Utils.GenerateUniqueId();
		ZedAxisVehicle zedAxisVehicle = ZedAxisVehicles.CreateVehicleFromProxy(zedAxisVehicleProxy, SandboxLayout.CURRENT_VERSION);
		if (zedAxisVehicle == null)
		{
			return null;
		}
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(base.gameObject);
		if (stageWithUnit != null)
		{
			stageWithUnit.AddUnit(zedAxisVehicle.gameObject, EventUnitType.ZED_AXIS_VEHICLE);
		}
		return zedAxisVehicle;
	}

	public bool OverlapsPolygonShape(PolygonShape shape)
	{
		return Utils.PolygonShapeOverlapsShapes(shape, m_PolygonShapes);
	}

	public Sprite GetIcon()
	{
		return m_Stub.m_Icon;
	}

	public void SnapToWaterLine()
	{
		if (GetVehicleType() == ZedAxisVehicleType.BOAT && m_SnapToWaterLine && !SandboxSettings.m_NoWater && !Mathf.Approximately(WaterBlocks.GetHeight(), base.transform.position.y))
		{
			base.transform.position = new Vector3(base.transform.position.x, WaterBlocks.GetHeight(), base.transform.position.y);
			UpdatePolygonShapes();
		}
	}

	private void MaybeTurnOnLights()
	{
		if (Theme.m_Instance.m_ThemeStub.m_ThemeTimeOfDay == ThemeTimeOfDay.NIGHT)
		{
			TurnLightsOn();
		}
	}

	private void MaybeTurnOnWake()
	{
		if ((bool)m_Wake)
		{
			m_Wake.SetActive(BoundsIntersectsWaterSurface());
		}
	}

	private bool BoundsIntersectsWaterSurface()
	{
		if (SandboxSettings.m_NoWater)
		{
			return false;
		}
		PolygonShape shape = PolygonShape.FromCircle((Vector2)new Vector3(base.transform.position.x, WaterBlocks.GetHeight()), 0.1f);
		return OverlapsPolygonShape(shape);
	}

	private Mesh CreateOutlineMeshFromSpline(SplineComputer spline)
	{
		SplinePoint[] points = spline.GetPoints(SplineComputer.Space.Local);
		Vector2[] array = new Vector2[points.Length - 1];
		for (int i = 0; i < points.Length - 1; i++)
		{
			array[i] = new Vector2(points[i].position.x, points[i].position.y);
		}
		int[] triangles = new TriangulatorBridges(array).Triangulate();
		Vector3[] array2 = new Vector3[array.Length];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = new Vector3(array[j].x, array[j].y, 0f);
		}
		Mesh mesh = new Mesh();
		mesh.vertices = array2;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	private void InitProfiles(GameObject gameProfileContainer, GameObject physicsProfileContainer)
	{
		int childCount = gameProfileContainer.transform.childCount;
		m_GameplayProfiles = new Transform[childCount];
		m_PhysicsProfiles = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			m_GameplayProfiles[i] = gameProfileContainer.transform.GetChild(i);
			m_PhysicsProfiles[i] = physicsProfileContainer.transform.GetChild(i);
		}
		m_CurrentProfileIndex = -1;
		m_NumFramesOnCurrentProfile = 0;
		m_NumFramesBeforeProfileSwitchAllowed = 1;
	}

	private void UpdateProfiles()
	{
		if (m_CurrentProfileIndex >= 0)
		{
			m_NumFramesOnCurrentProfile++;
		}
		if (m_CurrentProfileIndex >= m_GameplayProfiles.Length - 1)
		{
			return;
		}
		Transform transform = m_GameplayProfiles[m_CurrentProfileIndex + 1];
		bool flag = false;
		flag = ((!m_Reverse) ? (transform.position.z < GameSettings.BridgeWidth() / 2f) : (transform.position.z > GameSettings.BridgeWidth() / 2f));
		if (flag && (m_CurrentProfileIndex == -1 || m_NumFramesOnCurrentProfile >= m_NumFramesBeforeProfileSwitchAllowed))
		{
			if (m_CurrentProfileIndex >= 0)
			{
				m_PhysicsProfiles[m_CurrentProfileIndex].gameObject.SetActive(value: false);
			}
			m_CurrentProfileIndex++;
			m_PhysicsProfiles[m_CurrentProfileIndex].gameObject.SetActive(value: true);
			m_NumFramesBeforeProfileSwitchAllowed = (int)Mathf.Ceil((float)BridgeEdges.m_Edges.Count / (float)Singleton<TriggerManager, int>.instance.edgesPerUpdate);
			m_NumFramesOnCurrentProfile = 0;
		}
	}

	private void DisposeProfiles()
	{
		m_CurrentProfileIndex = -1;
		m_GameplayProfiles = null;
		m_PhysicsProfiles = null;
	}
}
