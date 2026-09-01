using System;
using System.Runtime.CompilerServices;
using Poly;
using Poly.Extension;
using Poly.Physics;
using UnityEngine;

public class BridgeEdge : MonoBehaviour
{
	public MeshRenderer m_MeshRenderer;

	public MeshRenderer m_MeshRendererChild;

	[Header("FX")]
	public GameObject m_LockFX;

	public GameObject m_SoftLockFX;

	public GameObject m_HighlightFX;

	public SpriteRenderer m_HighlightTopSpriteRenderer;

	public SpriteRenderer m_HighlightBottomSpriteRenderer;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public BridgeJoint m_JointA;

	[NonSerialized]
	public BridgeJoint m_JointB;

	[NonSerialized]
	public BridgeJoint m_StartSimJointA;

	[NonSerialized]
	public BridgeJoint m_StartSimJointB;

	[NonSerialized]
	public SplitJointPart m_JointAPart;

	[NonSerialized]
	public SplitJointPart m_JointBPart;

	[NonSerialized]
	public BridgeJointSelector m_JointSelectorA;

	[NonSerialized]
	public BridgeJointSelector m_JointSelectorB;

	[NonSerialized]
	public BridgeMaterial m_Material;

	[NonSerialized]
	public Color[] m_OriginalColors;

	[NonSerialized]
	public Edge m_PhysicsEdge;

	[NonSerialized]
	public bool m_IsBroken;

	[NonSerialized]
	public bool m_IsDebris;

	[NonSerialized]
	public float m_TimeToShowInvalidPlacementHighlight;

	[NonSerialized]
	public bool m_MouseHoveringOverJointSelector;

	[NonSerialized]
	public PrebuiltState m_PrebuiltState;

	[NonSerialized]
	public BridgeSpring m_SpringCoilVisualization;

	[NonSerialized]
	public BridgeHydraulicEdgeVisualization m_HydraulicEdgeVisualization;

	[NonSerialized]
	public Color m_OverrideColorPermanent = Color.white;

	[NonSerialized]
	public bool m_HasOverrideColorPermanent;

	[NonSerialized]
	public Color m_OverrideColor = Color.white;

	[NonSerialized]
	public bool m_HasOverrideColor;

	[NonSerialized]
	public bool m_ExcludeFromMaxStressCalculation;

	private float m_LastLength;

	private float m_LastStressNormalized = float.MaxValue;

	private float m_ShowLockIconUntilTime;

	private SpriteRenderer m_HighlightTopRevereSpriteRenderer;

	private SpriteRenderer m_HighlightBottomReverseSpriteRenderer;

	private float m_LengthForLastTilingUpload;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	[NonSerialized]
	public float cachedTiling = 1f;

	public bool m_ForceDisabled { get; private set; }

	private Transform m_Transform { get; set; }

	private Transform m_MeshRendererTransform { get; set; }

	private float m_VisualLength { get; set; }

	public MeshFilter m_MeshFilter { get; private set; }

	public BoxCollider m_BoxCollider { get; private set; }

	private void Awake()
	{
		BridgeEdges.m_Edges.Add(this);
		m_LockFX.SetActive(value: false);
		m_SoftLockFX.SetActive(value: false);
		m_HighlightFX.SetActive(value: false);
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_Transform = base.transform;
		m_MeshRendererTransform = m_MeshRenderer.transform;
		m_VisualLength = -100f;
		m_MeshFilter = m_MeshRenderer.GetComponent<MeshFilter>();
		m_BoxCollider = m_MeshRenderer.GetComponent<BoxCollider>() ?? GetComponentInChildren<BoxCollider>();
		m_HighlightTopRevereSpriteRenderer = m_HighlightTopSpriteRenderer.transform.GetChild(0).GetComponent<SpriteRenderer>();
		m_HighlightBottomReverseSpriteRenderer = m_HighlightBottomSpriteRenderer.transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		m_TimeToShowInvalidPlacementHighlight = 0f;
		m_MouseHoveringOverJointSelector = false;
		if (IsPiston())
		{
			Pistons.EnableOnEdge(this);
		}
		if ((bool)m_JointA || (bool)m_JointB)
		{
			m_JointA.RegisterEdgeInCache(this);
			m_JointB.RegisterEdgeInCache(this);
		}
	}

	private void OnDisable()
	{
		if ((bool)m_JointA)
		{
			m_JointA.UnregisterEdgeFromCache(this);
		}
		if ((bool)m_JointB)
		{
			m_JointB.UnregisterEdgeFromCache(this);
		}
		if (IsPiston())
		{
			Pistons.DisableOnEdge(this);
		}
	}

	private void OnDestroy()
	{
		if (BridgeEdges.m_Edges.Contains(this))
		{
			BridgeEdges.m_Edges.Remove(this);
		}
		if (BridgeSelectionSet.m_Edges.Contains(this))
		{
			BridgeSelectionSet.m_Edges.Remove(this);
		}
		if (IsPiston())
		{
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(this);
			if ((bool)pistonOnEdge)
			{
				Pistons.DestroyPiston(pistonOnEdge);
			}
		}
		if ((bool)m_JointSelectorA)
		{
			UnityEngine.Object.Destroy(m_JointSelectorA.gameObject);
		}
		if ((bool)m_JointSelectorB)
		{
			UnityEngine.Object.Destroy(m_JointSelectorB.gameObject);
		}
		if ((bool)m_SpringCoilVisualization)
		{
			BridgeSprings.Remove(this);
		}
		if ((bool)m_HydraulicEdgeVisualization)
		{
			m_HydraulicEdgeVisualization = null;
		}
		BridgeEdges.RemoveFromDictionary(this);
	}

	public void Destroy()
	{
		ForceDisable();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public float GetLength()
	{
		if ((object)m_JointA == null || (object)m_JointB == null)
		{
			return 0f;
		}
		return Vector3.Distance(m_JointA.m_Transform.position, m_JointB.m_Transform.position);
	}

	public void UpdateManual()
	{
		if (!m_ForceDisabled)
		{
			_UpdateTransform();
		}
		if (!m_IsBroken)
		{
			UpdateVisibility();
		}
	}

	public void UpdateManualOutsideSim()
	{
		_UpdateTransform();
		MaybeCreateJointSelectors();
		UpdateHighlight();
		UpdateLockIcon();
	}

	public void ShowLockIconForSeconds(float seconds)
	{
		m_ShowLockIconUntilTime = Time.realtimeSinceStartup + seconds;
	}

	public void ForceDisable()
	{
		base.gameObject.SetActive(value: false);
		m_ForceDisabled = true;
	}

	public void ForceEnable()
	{
		base.gameObject.SetActive(value: true);
		m_ForceDisabled = false;
	}

	public void Highlight(Color c)
	{
		SetHighlightColor(c);
		m_HighlightFX.SetActive(value: true);
	}

	public void UnHighlight()
	{
		m_HighlightFX.SetActive(value: false);
	}

	public void SetHighlightColor(Color c)
	{
		m_HighlightTopSpriteRenderer.color = c;
		m_HighlightBottomSpriteRenderer.color = c;
		m_HighlightTopRevereSpriteRenderer.color = c;
		m_HighlightBottomReverseSpriteRenderer.color = c;
	}

	public bool IsSelected()
	{
		return m_HighlightFX.activeInHierarchy;
	}

	public void UpdateTransform()
	{
		_UpdateTransform();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void _UpdateTransform()
	{
		if ((bool)m_PhysicsEdge)
		{
			EdgeHandle handle = m_PhysicsEdge.handle;
			Vec2 cachedSmoothPos = m_PhysicsEdge.node0.cachedSmoothPos;
			Vec2 cachedSmoothPos2 = m_PhysicsEdge.node1.cachedSmoothPos;
			Vec2 vec = cachedSmoothPos2 - cachedSmoothPos;
			if (m_PhysicsEdge.areNodesReversedInPhysics)
			{
				vec *= -1f;
			}
			float num = 0.5f * (float)Math.Atan2(vec.y, vec.x);
			float w = (float)Math.Cos(num);
			float z = (float)Math.Sin(num);
			m_Transform.SetPositionAndRotation(0.5f * (cachedSmoothPos + cachedSmoothPos2), new Quaternion(0f, 0f, z, w));
			float magnitude = vec.magnitude;
			float num2 = (m_HydraulicEdgeVisualization ? 0.002f : 0.05f);
			if (Mathf.Abs(m_VisualLength - magnitude) > num2)
			{
				m_VisualLength = magnitude;
				float num3 = ((m_Material.m_MaterialType == BridgeMaterialType.WOOD) ? 0.5f : 1f);
				m_MeshRendererTransform.localScale = new Vector3(m_VisualLength * num3, 1f, m_MeshRendererTransform.localScale.z);
			}
			if (!handle.isEnabled && !m_ForceDisabled)
			{
				if ((m_Material.m_MaterialType == BridgeMaterialType.ROPE || m_Material.m_MaterialType == BridgeMaterialType.CABLE) && BridgeRopes.m_BridgeRopes.Count > 0)
				{
					BridgeRopes.DisableRopeForEdge(this);
				}
				ForceDisable();
			}
		}
		else
		{
			Vector3 position = m_JointA.m_Transform.position;
			Vector3 position2 = m_JointB.m_Transform.position;
			Vector3 toDirection = position2 - position;
			float magnitude2 = toDirection.magnitude;
			if (!float.IsInfinity(magnitude2))
			{
				m_Transform.position = 0.5f * (position + position2);
				m_Transform.rotation = Quaternion.FromToRotation(Vector3.right, toDirection);
				if (Mathf.Abs(m_VisualLength - magnitude2) > 0.01f)
				{
					m_VisualLength = magnitude2;
					float num4 = ((m_Material.m_MaterialType == BridgeMaterialType.WOOD) ? 0.5f : 1f);
					m_MeshRendererTransform.localScale = new Vector3(m_VisualLength * num4, 1f, m_MeshRendererTransform.localScale.z);
					m_HighlightFX.transform.SetLocalScaleX(magnitude2);
				}
			}
		}
		if (m_SpringCoilVisualization != null)
		{
			m_SpringCoilVisualization.UpdateLinks();
		}
		if (m_HydraulicEdgeVisualization != null)
		{
			m_HydraulicEdgeVisualization.UpdateTransform_Manual(this);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void UpdateVisibility()
	{
		base.gameObject.SetActive(!m_ForceDisabled && NodesAreActive());
	}

	public void UpdateSpringVisibility()
	{
		bool num = m_MeshRenderer.enabled;
		m_MeshRenderer.enabled = Cameras.GetOrthographicSize() > BridgeEdges.m_SpringsDisappearOrthographicSize;
		if (num != m_MeshRenderer.enabled)
		{
			m_SpringCoilVisualization.m_FrontLink.m_MeshRenderer.enabled = !m_MeshRenderer.enabled;
			m_SpringCoilVisualization.m_BackLink.m_MeshRenderer.enabled = !m_MeshRenderer.enabled;
		}
	}

	public bool ShouldShowJointCaps()
	{
		BridgeMaterialType materialType = m_Material.m_MaterialType;
		if ((uint)(materialType - 3) <= 6u)
		{
			return true;
		}
		return false;
	}

	public bool IsValidLength()
	{
		float length = GetLength();
		float min = GameSettings.NodeDiameter();
		float maxLength = GetMaxLength();
		return BridgeEdges.IsValidEdgeLength(length, min, maxLength);
	}

	public float GetMaxLength()
	{
		return BridgeMaterials.GetMaxEdgeLength(m_Material.m_MaterialType);
	}

	public bool IsPiston()
	{
		if ((bool)m_Material)
		{
			return m_Material.m_MaterialType == BridgeMaterialType.HYDRAULICS;
		}
		return false;
	}

	public bool IsSpring()
	{
		if ((bool)m_Material)
		{
			return m_Material.m_MaterialType == BridgeMaterialType.SPRING;
		}
		return false;
	}

	public bool IsRoad()
	{
		if (m_Material.m_MaterialType != BridgeMaterialType.ROAD)
		{
			return m_Material.m_MaterialType == BridgeMaterialType.REINFORCED_ROAD;
		}
		return true;
	}

	public void SetStressColor(float stressNormalized)
	{
		if (m_HasOverrideColor || m_HasOverrideColorPermanent)
		{
			UpdateOverrideColor();
		}
		else if (!(Mathf.Abs(m_LastStressNormalized - stressNormalized) < 0.001f))
		{
			Color edgeColor = Color.black;
			if (!Mathf.Approximately(stressNormalized, 0f))
			{
				edgeColor = GetColorForStress(stressNormalized);
			}
			SetEdgeColor(edgeColor);
			m_LastStressNormalized = stressNormalized;
		}
	}

	public void SetOverrideColor(string colorHexCode)
	{
		m_HasOverrideColor = !string.IsNullOrEmpty(colorHexCode);
		m_OverrideColor = Utils.GetColorFromHexCode(colorHexCode, Color.white);
		m_OverrideColorPermanent.a = 0.2f;
		UpdateOverrideColor();
	}

	public void SetOverrideColorPermanent(string colorHexCode)
	{
		m_HasOverrideColorPermanent = !string.IsNullOrEmpty(colorHexCode);
		m_OverrideColorPermanent = Utils.GetColorFromHexCode(colorHexCode, Color.white);
		UpdateOverrideColor();
		if (m_HasOverrideColorPermanent)
		{
			BridgeEdges.SetPermanentEdgeColor(this, colorHexCode);
		}
		else
		{
			BridgeEdges.RemovePermanentEdgeColor(this);
		}
	}

	public static Color GetColorForStress(float stressNormalized)
	{
		if (Profiles.m_ActiveProfile.m_ColorBlindModeOn)
		{
			return Utils.HSVToRGB((1f - stressNormalized) * 0.35f, 0f, (1f - stressNormalized) * 0.9f);
		}
		return Utils.HSVToRGB((1f - stressNormalized) * 0.30556f, 0.6f, 0.6f);
	}

	public void MaybeCreateJointSelectors()
	{
		if (!Game.IsCurrentLevelTutorial())
		{
			if (m_JointA.m_IsSplit && !m_JointSelectorA)
			{
				m_JointSelectorA = BridgeJointSelectors.Create(this, BridgeJointSelectorSide.A);
				m_JointSelectorA.UpdateTransform();
				m_JointSelectorA.RefreshNumber();
				m_JointSelectorA.gameObject.SetActive(value: false);
				m_JointSelectorA.RefreshVisibility();
			}
			if (m_JointB.m_IsSplit && !m_JointSelectorB)
			{
				m_JointSelectorB = BridgeJointSelectors.Create(this, BridgeJointSelectorSide.B);
				m_JointSelectorB.UpdateTransform();
				m_JointSelectorB.RefreshNumber();
				m_JointSelectorB.gameObject.SetActive(value: false);
				m_JointSelectorB.RefreshVisibility();
			}
		}
	}

	public void EnableJointSelectorForJoint(BridgeJoint joint)
	{
		if ((bool)m_JointSelectorA && m_JointSelectorA.GetAssociatedJoint() == joint && m_JointSelectorA.IsVisible())
		{
			m_JointSelectorA.gameObject.SetActive(value: true);
			m_JointSelectorA.RefreshVisibility();
		}
		if ((bool)m_JointSelectorB && m_JointSelectorB.GetAssociatedJoint() == joint && m_JointSelectorB.IsVisible())
		{
			m_JointSelectorB.gameObject.SetActive(value: true);
			m_JointSelectorB.RefreshVisibility();
		}
		RefreshJointSelectorNumbers();
	}

	public void ResolveJointSelectorOverlap()
	{
		if ((bool)m_JointSelectorA && m_JointSelectorA.IsVisible())
		{
			m_JointSelectorA.ResolveOverlap();
		}
		if ((bool)m_JointSelectorB && m_JointSelectorB.IsVisible())
		{
			m_JointSelectorB.ResolveOverlap();
		}
	}

	public void DisableJointSelectorForJoint(BridgeJoint joint)
	{
		if ((bool)m_JointSelectorA && m_JointSelectorA.GetAssociatedJoint() == joint)
		{
			m_JointSelectorA.gameObject.SetActive(value: false);
		}
		if ((bool)m_JointSelectorB && m_JointSelectorB.GetAssociatedJoint() == joint)
		{
			m_JointSelectorB.gameObject.SetActive(value: false);
		}
	}

	public void RefreshJointSelectorNumbers()
	{
		if ((bool)m_JointSelectorA)
		{
			m_JointSelectorA.RefreshNumber();
		}
		if ((bool)m_JointSelectorB)
		{
			m_JointSelectorB.RefreshNumber();
		}
	}

	public void RefreshJointSelectorVisibility()
	{
		if ((bool)m_JointSelectorA)
		{
			m_JointSelectorA.RefreshVisibility();
		}
		if ((bool)m_JointSelectorB)
		{
			m_JointSelectorB.RefreshVisibility();
		}
	}

	public void ClampJointSelectorsToTwoWay()
	{
		if (m_JointAPart == SplitJointPart.C)
		{
			m_JointAPart = SplitJointPart.B;
		}
		if (m_JointBPart == SplitJointPart.C)
		{
			m_JointBPart = SplitJointPart.B;
		}
	}

	public SplitJointPart CalculateJointPart(BridgeJoint joint)
	{
		if (joint.m_IsAnchor)
		{
			return SplitJointPart.B;
		}
		if (!(base.transform.position.x > joint.transform.position.x))
		{
			return SplitJointPart.A;
		}
		return SplitJointPart.B;
	}

	public void UpdateJointSelectors()
	{
		if ((bool)m_JointSelectorA)
		{
			m_JointSelectorA.UpdateTransform();
		}
		if ((bool)m_JointSelectorB)
		{
			m_JointSelectorB.UpdateTransform();
		}
	}

	public int GetNumActiveJointSelectors()
	{
		int num = 0;
		if ((bool)m_JointSelectorA && m_JointSelectorA.gameObject.activeInHierarchy)
		{
			num++;
		}
		if ((bool)m_JointSelectorB && m_JointSelectorB.gameObject.activeInHierarchy)
		{
			num++;
		}
		return num;
	}

	public bool MatchesJoints(BridgeJoint A, BridgeJoint B)
	{
		if (!(m_JointA == A) || !(m_JointB == B))
		{
			if (m_JointA == B)
			{
				return m_JointB == A;
			}
			return false;
		}
		return true;
	}

	public bool HasJoint(BridgeJoint A)
	{
		if (!(m_JointA == A))
		{
			return m_JointB == A;
		}
		return true;
	}

	public void MatchTilingWithLength(BridgeMaterialType materialType, float length)
	{
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
		case BridgeMaterialType.REINFORCED_ROAD:
			BridgeMaterials.GetMaterial(BridgeMaterialType.ROAD);
			if (Mathf.Abs(length - m_LengthForLastTilingUpload) > 0.01f)
			{
				m_MaterialPropertyBlock.SetVector("_BaseMap_ST", new Vector4(1f, length, 0f, 0f));
				m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
				m_LengthForLastTilingUpload = length;
			}
			break;
		case BridgeMaterialType.WOOD:
			BridgeMaterials.GetMaterial(BridgeMaterialType.WOOD);
			if (Mathf.Abs(length - m_LengthForLastTilingUpload) > 0.01f)
			{
				cachedTiling = Mathf.Clamp01(length / BridgeMaterials.WOOD_REFERENCE_LENGTH);
				m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.TILING_SHADER_ID, new Vector2(cachedTiling, 1f));
				m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
				m_LengthForLastTilingUpload = length;
			}
			break;
		}
	}

	public void MaybeSetRopeCableTiling()
	{
		if (m_Material.m_MaterialType == BridgeMaterialType.ROPE || m_Material.m_MaterialType == BridgeMaterialType.CABLE)
		{
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.TILING_SHADER_ID, new Vector2(GetLength(), 1f));
			m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void CreateSpringVisualization()
	{
		BridgeSprings.CreateSpring(this, 0.5f, Utils.GenerateUniqueId());
	}

	public void CreateHydraulicVisualization()
	{
		m_HydraulicEdgeVisualization = GetComponentInChildren<BridgeHydraulicEdgeVisualization>();
		if ((bool)m_HydraulicEdgeVisualization)
		{
			m_HydraulicEdgeVisualization.Init(this);
		}
	}

	public void ReinitHydraulicVisualization()
	{
		if ((bool)m_HydraulicEdgeVisualization)
		{
			m_HydraulicEdgeVisualization.Init(this);
			m_HydraulicEdgeVisualization.UpdateTransform_Manual(this);
		}
	}

	public void TryRecreateSpringVisualization()
	{
		if ((bool)m_SpringCoilVisualization)
		{
			m_SpringCoilVisualization.MaybeRecreateLinks();
		}
	}

	public float CalculateAngle()
	{
		Vector3 normalized = (m_JointB.transform.position - m_JointA.transform.position).normalized;
		float num = Vector3.Angle(Vector3.right, normalized);
		if (Vector3.Dot(Vector3.up, normalized) < 0f)
		{
			num *= -1f;
		}
		return num;
	}

	public void ForceStressVisualizationRefresh()
	{
		m_LastStressNormalized = float.MaxValue;
	}

	public void SetPrebuiltState(PrebuiltState prebuiltState)
	{
		m_PrebuiltState = prebuiltState;
		m_LockFX.SetActive(value: false);
		m_SoftLockFX.SetActive(value: false);
	}

	public bool IsLocked()
	{
		return m_PrebuiltState == PrebuiltState.HARD_LOCKED;
	}

	public bool IsSoftLocked()
	{
		return m_PrebuiltState == PrebuiltState.SOFT_LOCKED;
	}

	public bool IsPrebuilt()
	{
		return m_PrebuiltState != PrebuiltState.NONE;
	}

	public bool ShouldDesaturatePrebuilt()
	{
		if (!IsLocked())
		{
			if (IsPrebuilt())
			{
				return GameStateManager.GetState() == GameState.SANDBOX;
			}
			return false;
		}
		return true;
	}

	public Vector3 GetCenterPos()
	{
		return (m_JointA.transform.position + m_JointB.transform.position) / 2f;
	}

	public float MassFilteredByAnchors()
	{
		float num = Mass();
		if (m_JointA.m_IsAnchor && m_JointB.m_IsAnchor)
		{
			return 0f;
		}
		if (!m_JointA.m_IsAnchor && !m_JointB.m_IsAnchor)
		{
			return num;
		}
		return num / 2f;
	}

	public float Mass()
	{
		return m_Material.m_EdgeMaterial.baseMass + m_Material.m_EdgeMaterial.massPerMeter * GetLength();
	}

	public bool NodeExistsAtPosition(Vector3 pos)
	{
		if (Mathf.Approximately(Vector2.Distance(m_JointA.transform.position, pos), 0f))
		{
			return true;
		}
		if (Mathf.Approximately(Vector2.Distance(m_JointB.transform.position, pos), 0f))
		{
			return true;
		}
		return false;
	}

	public void RefreshPistonJointRefs()
	{
		Piston pistonOnEdge = Pistons.GetPistonOnEdge(this);
		if ((bool)pistonOnEdge)
		{
			pistonOnEdge.m_JointA = pistonOnEdge.m_Edge.m_JointA;
			pistonOnEdge.m_JointB = pistonOnEdge.m_Edge.m_JointB;
		}
	}

	private void UpdateHighlight()
	{
		float length = GetLength();
		if (m_LastLength - length > 0.05f && IsValidLength())
		{
			m_TimeToShowInvalidPlacementHighlight = 0f;
		}
		m_LastLength = length;
		m_TimeToShowInvalidPlacementHighlight -= Time.unscaledDeltaTime;
		if (m_TimeToShowInvalidPlacementHighlight > 0f || !IsValidLength())
		{
			Highlight(GameUI.PlacementLineErrorColor());
			return;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && IsPiston())
		{
			HydraulicsPhase selectedHydraulicsPhase = GameUI.m_Instance.m_HydraulicsController.GetSelectedHydraulicsPhase();
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(this);
			if ((bool)selectedHydraulicsPhase && (bool)pistonOnEdge)
			{
				Highlight(HydraulicsController.PhaseAffectsPiston(selectedHydraulicsPhase, pistonOnEdge) ? GameUI.m_Instance.m_GoldColor : HydraulicsController.m_DisabledColor);
				return;
			}
		}
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && m_MouseHoveringOverJointSelector && !BridgeJointPlacement.InPlacementMode())
		{
			Highlight(GameUI.EdgeJointSelectorHoverColor());
			m_MouseHoveringOverJointSelector = false;
		}
		else if (BridgeSelectionSet.ContainsEdge(this))
		{
			Highlight((GameStateManager.GetState() == GameState.SANDBOX) ? Color.yellow : GameUI.EdgeSelectColor());
		}
		else
		{
			UnHighlight();
		}
	}

	private void UpdateLockIcon()
	{
		if (IsLocked())
		{
			m_LockFX.SetActive(GameStateManager.GetState() == GameState.SANDBOX || m_HighlightFX.activeInHierarchy || SliderDotVisible() || Time.realtimeSinceStartup < m_ShowLockIconUntilTime);
			if (m_LockFX.gameObject.activeInHierarchy)
			{
				SetLockFXTransform();
			}
		}
		else if (IsSoftLocked())
		{
			m_SoftLockFX.SetActive(GameStateManager.GetState() == GameState.SANDBOX);
			if (m_SoftLockFX.gameObject.activeInHierarchy)
			{
				m_SoftLockFX.transform.rotation = Quaternion.Euler(base.transform.rotation.eulerAngles.x, base.transform.rotation.eulerAngles.y, 0f);
			}
		}
	}

	private void SetLockFXTransform()
	{
		if (SliderDotVisible())
		{
			if (IsPiston())
			{
				Piston pistonOnEdge = Pistons.GetPistonOnEdge(this);
				m_LockFX.transform.position = pistonOnEdge.m_Slider.m_Handle.transform.position;
			}
			else if (IsSpring())
			{
				m_LockFX.transform.position = m_SpringCoilVisualization.m_Slider.m_Handle.transform.position;
			}
		}
		else
		{
			m_LockFX.transform.localPosition = Vector3.zero;
		}
		m_LockFX.transform.rotation = Quaternion.Euler(base.transform.rotation.eulerAngles.x, base.transform.rotation.eulerAngles.y, 0f);
	}

	private bool SliderDotVisible()
	{
		if (IsPiston())
		{
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(this);
			if ((bool)pistonOnEdge)
			{
				return pistonOnEdge.m_Slider.m_Handle.gameObject.activeInHierarchy;
			}
			return false;
		}
		if (IsSpring())
		{
			if ((bool)m_SpringCoilVisualization)
			{
				return m_SpringCoilVisualization.m_Slider.m_Handle.gameObject.activeInHierarchy;
			}
			return false;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool NodesAreActive()
	{
		if ((object)m_JointA == null || !m_JointA.isActiveAndEnabled)
		{
			return false;
		}
		if ((object)m_JointB == null || !m_JointB.isActiveAndEnabled)
		{
			return false;
		}
		return true;
	}

	private void UpdateOverrideColor()
	{
		if (m_HasOverrideColor)
		{
			SetEdgeColor(m_OverrideColor);
			return;
		}
		if (m_HasOverrideColorPermanent)
		{
			SetEdgeColor(m_OverrideColorPermanent);
			return;
		}
		m_LastStressNormalized = -100f;
		SetStressColor(0f);
	}

	private void SetEdgeColor(Color edgeColor)
	{
		if ((m_Material.m_MaterialType == BridgeMaterialType.ROPE || m_Material.m_MaterialType == BridgeMaterialType.CABLE) && BridgeRopes.m_BridgeRopes.Count > 0)
		{
			BridgeRopes.SetStressColorForEdge(this, edgeColor);
			return;
		}
		if (m_Material.m_MaterialType == BridgeMaterialType.SPRING && BridgeSprings.m_BridgeSprings.Count > 0)
		{
			BridgeSprings.SetStressColorForEdge(this, edgeColor);
			return;
		}
		m_MaterialPropertyBlock.SetColor(BridgeEdges.STRESS_COLOR_SHADER_ID, edgeColor);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		if (m_MeshRendererChild != null)
		{
			m_MeshRendererChild.SetPropertyBlock(m_MaterialPropertyBlock);
		}
		if ((bool)m_HydraulicEdgeVisualization)
		{
			m_HydraulicEdgeVisualization.SetStressColorForEdge(this, edgeColor);
		}
	}
}
