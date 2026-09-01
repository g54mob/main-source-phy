using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;
using UnityEngine.Rendering;

public class TerrainIsland : MonoBehaviour
{
	public TerrainIslandType m_TerrainIslandType;

	public TerrainIslandSpawnPoint m_SpawnPoint;

	public float m_MeshHeight;

	public bool m_Flipped;

	public bool m_Legacy;

	public MeshFilter m_MeshFilter;

	public MeshFilter m_MeshFilterSecondPass;

	public MeshRenderer m_MeshRenderer;

	public SplineComputer m_OutlineSplineComputer;

	public BoxCollider m_BoxCollider;

	public TerrainCollisionInfo m_CollisionInfo;

	public PlaceableCollisionInfo m_CollisionInfoNew;

	[Header("Windows")]
	public Vector3 m_WindowsOrigin;

	public Vector3 m_WindowsStep;

	public Vector3 m_WindowsNumSteps;

	[Header("Windows Alt")]
	public Vector3 m_AltThesholds;

	public Vector3 m_WindowsAltOrigin;

	public Vector3 m_WindowsAltStep;

	public Vector3 m_WindowsAltNumSteps;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public float m_HeightAdded;

	[NonSerialized]
	public float m_DisplayVariantSeconds;

	[NonSerialized]
	public float m_RightEdgeWaterHeight;

	[NonSerialized]
	public bool m_LockPosition;

	[NonSerialized]
	public bool m_Hidden;

	[NonSerialized]
	public HashSet<BridgeJoint> m_OverlappingAnchors = new HashSet<BridgeJoint>();

	[NonSerialized]
	public List<TerrainParticleSystem> m_TerrainParticleSystems = new List<TerrainParticleSystem>();

	[NonSerialized]
	public List<TerrainWaterFall> m_TerrainWaterFalls = new List<TerrainWaterFall>();

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private float m_BoxColliderOriginalY;

	private float m_BoxColliderOriginalCenterY;

	internal List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	private List<int> m_OutlineBottomVertIndicies = new List<int>();

	private List<int> m_MeshBottomVertIndicies = new List<int>();

	private List<Vector2> m_OriginalOutlineVerts = new List<Vector2>();

	private List<SplinePoint> m_ControlPointsTemp = new List<SplinePoint>();

	private static float LEGACY_MESH_HEIGHT = 5.1f;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private MaterialPropertyBlock m_StencilMaterialPropertyBlock;

	private MeshRenderer[] m_AllMeshRenderers;

	private MeshFilter m_MainPassStencilMeshFilter;

	private MeshRenderer m_MainPassStencilMeshRenderer;

	private MeshFilter m_ForegroundPassStencilMeshFilter;

	private MeshRenderer m_ForegroundPassStencilMeshRenderer;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		if ((bool)m_BoxCollider)
		{
			m_BoxColliderOriginalY = m_BoxCollider.size.y;
			m_BoxColliderOriginalCenterY = m_BoxCollider.center.y;
		}
		if (Mathf.Approximately(m_MeshHeight, 0f))
		{
			m_MeshHeight = LEGACY_MESH_HEIGHT;
		}
		SplinePoint[] points = m_OutlineSplineComputer.GetPoints(SplineComputer.Space.Local);
		for (int i = 0; i < points.Length; i++)
		{
			SplinePoint splinePoint = points[i];
			m_OriginalOutlineVerts.Add(new Vector2(splinePoint.position.x, splinePoint.position.y));
		}
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_StencilMaterialPropertyBlock = new MaterialPropertyBlock();
		m_AllMeshRenderers = base.gameObject.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		CreateMainPassStencilObjects();
		CreateForegroundPassStencilObjects();
		if (m_SandboxItem.m_Colliders != null && m_SandboxItem.m_Colliders.Length != 0 && m_SandboxItem.m_Colliders[0] == null)
		{
			m_SandboxItem.m_Colliders = m_BoxCollider.GetComponentsInChildren<Collider>();
		}
		if (m_MeshFilterSecondPass != null)
		{
			Utils.SetLayerOnAllRecursive(m_MeshFilterSecondPass.gameObject, Utils.FOREGROUND_LAYER);
		}
	}

	private void CreateMainPassStencilObjects()
	{
		GameObject gameObject = new GameObject("MainPassStencil");
		gameObject.transform.parent = m_MeshRenderer.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 50f);
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.layer = Utils.TERRAIN_LAYER;
		m_MainPassStencilMeshFilter = gameObject.AddComponent<MeshFilter>();
		m_MainPassStencilMeshRenderer = gameObject.AddComponent<MeshRenderer>();
		m_MainPassStencilMeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		m_MainPassStencilMeshRenderer.material = GameSettings.m_Instance.m_TerrainCollisionMaterial;
		m_MainPassStencilMeshRenderer.gameObject.SetActive(value: false);
	}

	private void CreateForegroundPassStencilObjects()
	{
		GameObject gameObject = new GameObject("ForegroundPassStencil");
		gameObject.transform.parent = m_MeshRenderer.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 50f);
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.layer = Utils.FOREGROUND_LAYER;
		m_ForegroundPassStencilMeshFilter = gameObject.AddComponent<MeshFilter>();
		m_ForegroundPassStencilMeshRenderer = gameObject.AddComponent<MeshRenderer>();
		m_ForegroundPassStencilMeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		m_ForegroundPassStencilMeshRenderer.material = GameSettings.m_Instance.m_TerrainCollisionSolidMaterial;
		m_ForegroundPassStencilMeshRenderer.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		if (!TerrainIslands.m_Terrains.Contains(this))
		{
			TerrainIslands.m_Terrains.Add(this);
		}
		CuttingController_OnePlane component = base.gameObject.GetComponent<CuttingController_OnePlane>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(m_MeshFilter.mesh);
		if (m_MeshFilterSecondPass != null)
		{
			UnityEngine.Object.Destroy(m_MeshFilterSecondPass.mesh);
		}
		if (m_MainPassStencilMeshFilter != null)
		{
			UnityEngine.Object.Destroy(m_MainPassStencilMeshFilter.sharedMesh);
		}
		if (m_ForegroundPassStencilMeshFilter != null)
		{
			UnityEngine.Object.Destroy(m_ForegroundPassStencilMeshFilter.sharedMesh);
		}
		if (TerrainIslands.m_Terrains.Contains(this))
		{
			TerrainIslands.m_Terrains.Remove(this);
		}
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		m_MeshRenderer.gameObject.SetActive(value: true);
	}

	public void UpdateOutline()
	{
		if (!m_HasCreatedOutline)
		{
			m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_Outline.SetLayer(Utils.OUTLINE_LAYER);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (m_Outline != null && (bool)m_OutlineSplineComputer && m_SandboxItem.IsOutlineDirty())
		{
			m_SandboxItem.UpdateOutlineFromSpline(m_Outline, m_OutlineSplineComputer);
			m_SandboxItem.SetOutlineDirty(dirty: false);
			m_Outline.m_VectorLine.Draw3DAuto();
		}
		if (m_DisplayVariantSeconds > 0f)
		{
			m_DisplayVariantSeconds -= Time.unscaledDeltaTime;
			bool flag = m_DisplayVariantSeconds > 0f;
			HideAllMeshRenderers(!flag);
			EnableCollisionMeshRenderer(!flag);
		}
		else
		{
			HideAllMeshRenderers(GameStateManager.GetState() == GameState.SANDBOX);
			EnableCollisionMeshRenderer(GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.BUILD);
		}
	}

	public void HideAllMeshRenderers(bool hide)
	{
		MeshRenderer[] allMeshRenderers = m_AllMeshRenderers;
		for (int i = 0; i < allMeshRenderers.Length; i++)
		{
			allMeshRenderers[i].enabled = !hide;
		}
	}

	public void HideSecondPassMeshRenderers(bool hide)
	{
		if (m_MeshFilterSecondPass != null)
		{
			m_MeshFilterSecondPass.gameObject.SetActive(!hide);
		}
	}

	public void StretchToGround(float delta)
	{
		if (!Mathf.Approximately(delta, 0f))
		{
			base.transform.Translate(0f, 0f - delta, 0f);
			GameUI.m_Instance.m_SandboxEditTerrain.m_SliderStretch.m_SandboxInputField.SetHeight(base.gameObject, GameGrid.RoundToNearestGridSquare(GetHeight() + delta) + 0.1f);
			if (GameUI.m_Instance.m_SandboxEditTerrain.m_SliderStretch.m_SandboxInputField.m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditTerrain.SkipInputFieldUpdateFromSlider();
				GameUI.m_Instance.m_SandboxEditTerrain.m_SliderStretch.m_SandboxInputField.m_LinkedSlider.SetValue(GetHeight());
			}
		}
	}

	public void RefreshAfterHeightChange()
	{
		float num = m_MeshHeight + m_HeightAdded - m_MeshHeight;
		if (!m_Legacy && num < 0f)
		{
			base.transform.position = new Vector3(base.transform.position.x, num, base.transform.position.z);
		}
		else
		{
			base.transform.position = new Vector3(base.transform.position.x, m_HeightAdded, base.transform.position.z);
			TranslateMeshVerts(m_MeshFilter.mesh, m_HeightAdded);
			TranslateMeshVerts(m_MeshFilterSecondPass.mesh, m_HeightAdded);
			foreach (TerrainWaterFall terrainWaterFall in m_TerrainWaterFalls)
			{
				terrainWaterFall.TranslateMeshVerts(m_HeightAdded + terrainWaterFall.transform.localPosition.y);
			}
		}
		UpdateOutlineSpline();
		UpdatePolygonShapes();
		UpdateBoxCollider();
		m_SandboxItem.m_OutlineGroup.ClearCachedSplinePoints();
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void UpdatePolygonShapes()
	{
		if ((bool)m_CollisionInfo)
		{
			m_PolygonShapes.Clear();
			m_PolygonShapes.AddRange(m_CollisionInfo.CreatePolygonShapes_ForBuildMode(m_Flipped));
		}
		if ((bool)m_CollisionInfoNew)
		{
			m_PolygonShapes.Clear();
			m_PolygonShapes.AddRange(m_CollisionInfoNew.CreatePolygonShapes_ForBuildMode());
		}
		if (m_MainPassStencilMeshFilter != null)
		{
			if (m_MainPassStencilMeshFilter.sharedMesh != null)
			{
				UnityEngine.Object.Destroy(m_MainPassStencilMeshFilter.sharedMesh);
			}
			m_MainPassStencilMeshFilter.sharedMesh = TerrainIslandCollisionMesh.BuildCollisionMesh(m_MeshRenderer.transform, m_PolygonShapes, m_BoxCollider);
		}
		if (m_ForegroundPassStencilMeshFilter != null)
		{
			if (m_ForegroundPassStencilMeshFilter.sharedMesh != null)
			{
				UnityEngine.Object.Destroy(m_ForegroundPassStencilMeshFilter.sharedMesh);
			}
			m_ForegroundPassStencilMeshFilter.sharedMesh = m_MainPassStencilMeshFilter.sharedMesh;
		}
		if (GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.BUILD)
		{
			UpdateOutline();
		}
	}

	public void UpdateWaterfallsInverseTimeScale(float timescale)
	{
		foreach (TerrainWaterFall terrainWaterFall in m_TerrainWaterFalls)
		{
			terrainWaterFall.m_MeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
			m_MaterialPropertyBlock.SetFloat(ShaderVariables_Common.INVERSE_TIME_SCALE_SHADER_ID, Mathf.Approximately(timescale, 0f) ? 0f : (1f / timescale));
			terrainWaterFall.m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void UpdateShaderProperties(bool buildMode, MeshRenderer cuttingPlane)
	{
		m_MaterialPropertyBlock.Clear();
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_NORMAL_1, (cuttingPlane != null) ? cuttingPlane.transform.up : Vector3.up);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_POSITION_1, (cuttingPlane != null) ? cuttingPlane.transform.position : new Vector3(0f, 0f, -1000f));
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_Common.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_BuildingGlass.NOISE_THRESHOLD_SHADER_ID, Theme.m_Instance.m_ThemeStub.m_WindowsNoiseThreshold);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.ORIGIN_SHADER_ID, m_WindowsOrigin);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.STEP_SHADER_ID, m_WindowsStep);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.NUMSTEPS_SHADER_ID, m_WindowsNumSteps);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.ALT_THRESHOLDS, m_AltThesholds);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.ALT_ORIGIN_SHADER_ID, m_WindowsAltOrigin);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.ALT_STEP_SHADER_ID, m_WindowsAltStep);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_BuildingGlass.ALT_NUMSTEPS_SHADER_ID, m_WindowsAltNumSteps);
		MeshRenderer[] allMeshRenderers = m_AllMeshRenderers;
		foreach (MeshRenderer meshRenderer in allMeshRenderers)
		{
			m_MaterialPropertyBlock.SetColor(ShaderVariables_Common.BUILD_MODE_TINT_SHADER_ID, (meshRenderer.gameObject.layer == Utils.FOREGROUND_LAYER) ? PostFX.m_Instance.m_BuildModeCollideTint : PostFX.m_Instance.m_BuildModeNoCollideTint);
			meshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		}
		foreach (TerrainWaterFall terrainWaterFall in m_TerrainWaterFalls)
		{
			if (!SandboxSettings.m_NoWater)
			{
				m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_POSITION_1, new Vector3(0f, WaterBlocks.GetHeight() - 0.1f, 0f));
				terrainWaterFall.m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
			}
		}
	}

	public void UpdateStencilShaderProperties(bool buildMode, Color color)
	{
		if (buildMode)
		{
			m_StencilMaterialPropertyBlock.SetColor(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, color);
			m_ForegroundPassStencilMeshRenderer.SetPropertyBlock(m_StencilMaterialPropertyBlock);
		}
	}

	public float GetBoxColliderWidth()
	{
		return m_BoxCollider.size.x;
	}

	public void DisplayFullMesh(float durationSeconds)
	{
		m_DisplayVariantSeconds = durationSeconds;
		EnableCollisionMeshRenderer(on: false);
	}

	public void ClearDisplayVariantTimer()
	{
		m_DisplayVariantSeconds = 0f;
		HideAllMeshRenderers(hide: true);
	}

	private void UpdateBoxCollider()
	{
		m_BoxCollider.size = new Vector3(m_BoxCollider.size.x, m_BoxColliderOriginalY + m_HeightAdded, m_BoxCollider.size.z);
		m_BoxCollider.center = new Vector3(m_BoxCollider.center.x, m_BoxColliderOriginalCenterY - m_HeightAdded / 2f, m_BoxCollider.center.z);
	}

	private void UpdateOutlineSpline()
	{
		float num = m_MeshHeight + m_HeightAdded;
		float num2 = num - m_MeshHeight;
		if (!m_Legacy && num2 < 0f)
		{
			float num3 = m_MeshHeight - num;
			m_ControlPointsTemp.Clear();
			for (int i = 0; i < m_OriginalOutlineVerts.Count; i++)
			{
				Vector2 vector = m_OriginalOutlineVerts[i];
				SplinePoint item = new SplinePoint(vector);
				item.normal = Vector3.up;
				if (vector.y >= num3)
				{
					m_ControlPointsTemp.Add(item);
				}
				if (i < m_OriginalOutlineVerts.Count - 1)
				{
					Vector2 positiveInfinity = Vector2.positiveInfinity;
					if (ShouldComputeIntercept(m_OriginalOutlineVerts[i], m_OriginalOutlineVerts[i + 1], num3))
					{
						positiveInfinity = ComputeIntercept(m_OriginalOutlineVerts[i], m_OriginalOutlineVerts[i + 1], num3);
						SplinePoint item2 = new SplinePoint(positiveInfinity);
						item2.normal = Vector3.up;
						m_ControlPointsTemp.Add(item2);
					}
				}
			}
			if (!Mathf.Approximately((m_ControlPointsTemp[m_ControlPointsTemp.Count - 1].position - m_ControlPointsTemp[0].position).magnitude, 0f))
			{
				SplinePoint item3 = new SplinePoint(m_ControlPointsTemp[0].position);
				item3.normal = Vector3.up;
				m_ControlPointsTemp.Add(item3);
			}
			m_OutlineSplineComputer.SetPoints(m_ControlPointsTemp.ToArray(), SplineComputer.Space.Local);
			return;
		}
		if (m_OutlineBottomVertIndicies.Count == 0)
		{
			m_OutlineBottomVertIndicies.AddRange(GetOutlineBottomVertIndicies());
		}
		m_ControlPointsTemp.Clear();
		for (int j = 0; j < m_OriginalOutlineVerts.Count; j++)
		{
			Vector2 vector2 = m_OriginalOutlineVerts[j];
			SplinePoint item4 = new SplinePoint(vector2);
			item4.normal = Vector3.up;
			if (m_OriginalOutlineVerts[j].y < 0.1f)
			{
				item4.position.y -= m_HeightAdded;
			}
			m_ControlPointsTemp.Add(item4);
		}
		m_OutlineSplineComputer.SetPoints(m_ControlPointsTemp.ToArray(), SplineComputer.Space.Local);
	}

	private bool ShouldComputeIntercept(Vector2 start, Vector2 end, float yCutoff)
	{
		if (!(start.y > yCutoff) || !(end.y < yCutoff))
		{
			if (start.y < yCutoff)
			{
				return end.y > yCutoff;
			}
			return false;
		}
		return true;
	}

	private Vector2 ComputeIntercept(Vector2 start, Vector2 end, float yCutoff)
	{
		Vector2 normalized = (end - start).normalized;
		float num = Vector2.Dot(normalized, Vector2.down);
		float num2 = Mathf.Abs((start.y - yCutoff) / num);
		return start + normalized * num2;
	}

	public void TranslateMeshVerts(Mesh mesh, float height)
	{
		m_MeshBottomVertIndicies.Clear();
		m_MeshBottomVertIndicies.AddRange(TerrainIslands.GetMeshBottomVertIndicies(mesh));
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < m_MeshBottomVertIndicies.Count; i++)
		{
			vertices[m_MeshBottomVertIndicies[i]].y = 0f - height;
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
	}

	public void Flip()
	{
		FlipTransform(m_MeshRenderer.transform);
		if (Mathf.Approximately(m_BoxCollider.transform.localEulerAngles.y, 0f))
		{
			m_BoxCollider.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			m_BoxCollider.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		m_BoxCollider.transform.localPosition = new Vector3(0f - Mathf.Abs(m_BoxCollider.transform.localPosition.x), m_BoxCollider.transform.localPosition.y, m_BoxCollider.transform.localPosition.z);
		m_Flipped = true;
	}

	public bool OverlapsCircle(PolygonShape circleShape)
	{
		return Utils.PolygonShapeOverlapsShapes(circleShape, m_PolygonShapes);
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

	public TerrainIsland Duplicate(GameObject prefab, Vector3 offset)
	{
		TerrainIsland terrainIsland = TerrainIslands.CreateTerrain(prefab, base.transform.position, Quaternion.identity);
		if ((bool)terrainIsland)
		{
			terrainIsland.m_HeightAdded = m_HeightAdded;
			terrainIsland.RefreshAfterHeightChange();
			if (m_Flipped)
			{
				terrainIsland.Flip();
			}
			if ((bool)base.transform.parent)
			{
				terrainIsland.transform.SetParent(base.transform.parent.transform);
			}
			terrainIsland.transform.position += offset;
			terrainIsland.m_LockPosition = m_LockPosition;
			terrainIsland.m_Hidden = m_Hidden;
			terrainIsland.ShrinkForSandboxMode(shrink: true);
			terrainIsland.UpdatePolygonShapes();
			terrainIsland.EnableCollisionMeshRenderer(on: true);
			terrainIsland.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
		}
		return terrainIsland;
	}

	public float GetNorthEdgeZ()
	{
		return m_BoxCollider.center.z + m_BoxCollider.size.z / 2f;
	}

	public float GetSouthEdgeZ()
	{
		return m_BoxCollider.center.z - m_BoxCollider.size.z / 2f;
	}

	public float GetMinHeight()
	{
		if (!m_Legacy)
		{
			return TerrainIslands.MIN_HEIGHT;
		}
		return m_MeshHeight;
	}

	public float GetHeight()
	{
		return m_MeshHeight + m_HeightAdded;
	}

	public void SetHeight(float height)
	{
		height = Mathf.Clamp(height, TerrainIslands.MIN_HEIGHT, TerrainIslands.MAX_HEIGHT);
		m_HeightAdded = height - m_MeshHeight;
		RefreshAfterHeightChange();
	}

	public void SetStretch(float stretch)
	{
		float height = TerrainIslands.DEFAULT_HEIGHT + stretch;
		SetHeight(height);
	}

	public float GetScaleAdjustmentForOverhang()
	{
		float num = m_BoxCollider.size.x / 2f;
		return (num + GameSettings.TerrainOverhang()) / num;
	}

	public void EnableCollisionMeshRenderer(bool on)
	{
		if (!m_Legacy)
		{
			m_MainPassStencilMeshRenderer.gameObject.SetActive(on);
			m_ForegroundPassStencilMeshRenderer.gameObject.SetActive(on && GameStateManager.GetState() != GameState.SANDBOX);
		}
	}

	public void StoreOverlappingAnchors()
	{
		m_OverlappingAnchors.Clear();
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && !joint.IsDynamicAnchor() && !SandboxSelectionSet.IsSelected(joint.m_SandboxItem) && Utils.PolygonShapeOverlapsShapes(PolygonShape.FromCircle((Vec2)joint.transform.position, GameSettings.NodeRadius()), m_PolygonShapes))
			{
				m_OverlappingAnchors.Add(joint);
			}
		}
	}

	public void StartParticleSystems()
	{
		foreach (TerrainParticleSystem terrainParticleSystem in m_TerrainParticleSystems)
		{
			if (!terrainParticleSystem.IntersectsWater() && !terrainParticleSystem.BelowTerrain())
			{
				terrainParticleSystem.Play();
			}
		}
	}

	public void StopParticleSystems()
	{
		foreach (TerrainParticleSystem terrainParticleSystem in m_TerrainParticleSystems)
		{
			terrainParticleSystem.Stop();
		}
	}

	public void PauseParticleSystems(bool pause)
	{
		foreach (TerrainParticleSystem terrainParticleSystem in m_TerrainParticleSystems)
		{
			terrainParticleSystem.Pause(pause);
		}
	}

	public void StartWaterfalls()
	{
		foreach (TerrainWaterFall terrainWaterFall in m_TerrainWaterFalls)
		{
			terrainWaterFall.Play();
		}
	}

	public void StopWaterfalls()
	{
		foreach (TerrainWaterFall terrainWaterFall in m_TerrainWaterFalls)
		{
			terrainWaterFall.Stop();
		}
	}

	public void PauseWaterfalls(bool pause)
	{
		foreach (TerrainWaterFall terrainWaterFall in m_TerrainWaterFalls)
		{
			terrainWaterFall.Pause(pause);
		}
	}

	public void ShrinkForSandboxMode(bool shrink)
	{
		if (m_TerrainIslandType == TerrainIslandType.Bookend)
		{
			float x = (shrink ? 0f : (m_Flipped ? (0f - GameSettings.TerrainOverhang()) : GameSettings.TerrainOverhang()));
			m_MeshRenderer.transform.localPosition = new Vector3(x, m_MeshRenderer.transform.localPosition.y, 0f);
		}
		else
		{
			float scaleAdjustmentForOverhang = GetScaleAdjustmentForOverhang();
			m_MeshRenderer.transform.localScale = new Vector3(shrink ? 1f : scaleAdjustmentForOverhang, 1f, 1f);
		}
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public string FormatHeight()
	{
		float height = GetHeight();
		float num = height - 0.1f;
		if (GameGrid.IsGridAligned(num))
		{
			return Utils.FormatDistance(num);
		}
		return Utils.FormatDistance(height);
	}

	private List<int> GetOutlineBottomVertIndicies()
	{
		List<int> list = new List<int>();
		SplinePoint[] points = m_OutlineSplineComputer.GetPoints(SplineComputer.Space.Local);
		float num = float.MaxValue;
		for (int i = 0; i < points.Length; i++)
		{
			if (points[i].position.y < num)
			{
				num = points[i].position.y;
			}
		}
		for (int j = 0; j < points.Length; j++)
		{
			if (points[j].position.y < num + 0.01f)
			{
				list.Add(j);
			}
		}
		return list;
	}

	private void FlipTransform(Transform transform)
	{
		transform.localScale = new Vector3(0f - transform.localScale.x, transform.localScale.y, transform.localScale.z);
		transform.localPosition = new Vector3(0f - transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
	}
}
