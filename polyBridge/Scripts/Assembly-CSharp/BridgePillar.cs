using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class BridgePillar : MonoBehaviour
{
	public ClipboardJoint m_ClipboardJoint;

	public PlaceableCollisionInfo m_CollisionInfo;

	public PlaceableCollisionInfo m_CollisionInfoNoMiddle;

	public SplineComputer m_CollisionSpline;

	public SplineComputer m_CollisionSplineNoMiddle;

	[Header("Meshes")]
	public GameObject m_Cap;

	public GameObject m_Top;

	public GameObject m_Base;

	[Header("FX")]
	public GameObject m_FX;

	public GameObject m_LockIcon;

	public GameObject m_SoftLockIcon;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public string m_AnchorGuid;

	[NonSerialized]
	public Vector3 m_StartMovementWorldPos;

	[NonSerialized]
	public float m_StartMovementHeight;

	[NonSerialized]
	public Outline m_Outline;

	[NonSerialized]
	public PrebuiltState m_PrebuiltState;

	[NonSerialized]
	public bool m_ForceDisabled;

	private float m_PricePerMeter;

	private bool m_Selected;

	internal MeshRenderer[] m_MeshRenderers;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private BridgePillarTooltip m_BridgePillarTooltip;

	private const float OUTLINE_SCALE_WHEN_SELECTED = 2f;

	private List<Vector3> m_OriginalCollisionSplinePoints = new List<Vector3>();

	internal List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	private void Awake()
	{
		m_PricePerMeter = BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.PILLAR).m_PricePerMeter;
		m_Outline = Outlines.Create(GameUI.m_Instance.m_OutlineTextureBuildMode, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthBuildMode, GameUI.m_Instance.m_OutlineColorBuildMode);
		m_Outline.SetLayer(Utils.RENDER_LAST_LAYER);
		m_Outline.m_BuildModeWidthMultiplier = 0.7f;
		m_LockIcon.SetActive(value: false);
		m_SoftLockIcon.SetActive(value: false);
		m_MeshRenderers = base.gameObject.GetComponentsInChildren<MeshRenderer>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		for (int i = 0; i < m_CollisionSpline.pointCount; i++)
		{
			m_OriginalCollisionSplinePoints.Add(m_CollisionSpline.GetPoint(i, SplineComputer.Space.Local).position);
		}
	}

	private void Start()
	{
		if (!BridgePillars.m_BridgePillars.Contains(this))
		{
			BridgePillars.m_BridgePillars.Add(this);
		}
		m_BridgePillarTooltip = new BridgePillarTooltip();
	}

	private void Update()
	{
		m_Top.SetActive(!Mathf.Approximately(m_Top.transform.localScale.y, 0f));
		m_Cap.SetActive(value: true);
		m_Base.SetActive(value: true);
		bool moving = BridgePillarMovement.IsMovingSelectionSet() && BridgeSelectionSet.m_BridgePillars.Contains(this);
		if (m_BridgePillarTooltip != null)
		{
			m_BridgePillarTooltip.UpdateManual(base.transform.position, GetTotalHeight(), Cost(), moving);
		}
		UpdateLockIcon();
		m_Outline.SetActive((GameStateManager.GetState() == GameState.BUILD && !GameStateBuild.m_CameraInTransition) || (GameStateManager.GetState() == GameState.SANDBOX && BridgeSelectionSet.ContainsPillar(this) && !GameStateSandbox.m_CameraInTransition));
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		if (m_BridgePillarTooltip != null)
		{
			m_BridgePillarTooltip.Hide();
		}
		if (m_Outline != null)
		{
			m_Outline.SetActive(active: false);
		}
	}

	private void OnDestroy()
	{
		if (BridgePillars.m_BridgePillars.Contains(this))
		{
			BridgePillars.m_BridgePillars.Remove(this);
		}
		if (m_BridgePillarTooltip != null)
		{
			m_BridgePillarTooltip.Destroy();
		}
		if (m_Outline != null)
		{
			m_Outline.Destroy();
		}
	}

	public void Destroy()
	{
		BridgeJoint anchor = GetAnchor();
		if (anchor != null)
		{
			if (anchor.IsConnectedToLockedPrebuilt())
			{
				anchor.RevertAnchor();
			}
			else
			{
				anchor.gameObject.SetActive(value: false);
				UnityEngine.Object.Destroy(anchor.gameObject);
			}
		}
		base.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public float Cost()
	{
		return BridgePillars.BASE_COST + m_PricePerMeter * (GetTopHeight() - BridgePillars.MESH_CAP_HEIGHT);
	}

	public void UpdatePolygonShapes()
	{
		m_PolygonShapes.Clear();
		if (Mathf.Approximately(GetTotalHeight(), BridgePillars.MIN_HEIGHT))
		{
			m_PolygonShapes.AddRange(m_CollisionInfoNoMiddle.CreatePolygonShapes_ForBuildMode());
		}
		else
		{
			m_PolygonShapes.AddRange(m_CollisionInfo.CreatePolygonShapes_ForBuildMode());
		}
	}

	public bool OverlapsPolygonShape(PolygonShape shape)
	{
		return Utils.PolygonShapeOverlapsShapes(shape, m_PolygonShapes);
	}

	public bool OverlapsRect(Rect rect)
	{
		PolygonShape polygonShape = PolygonShape.FromRect(rect.center, rect.size);
		polygonShape.radius = 0f;
		return Utils.PolygonShapeOverlapsShapes(polygonShape, m_PolygonShapes);
	}

	public void SetTopHeightBasedOnTotalHeight(float height)
	{
		float num = height - BridgePillars.MESH_BASE_HEIGHT;
		m_Top.transform.localScale = new Vector3(m_Top.transform.localScale.x, num / BridgePillars.MESH_TOP_HEIGHT - BridgePillars.TOP_SCALE_ADJUSTMENT, m_Top.transform.localScale.z);
		BridgeJoint anchor = GetAnchor();
		if (anchor != null)
		{
			anchor.transform.position = new Vector3(base.transform.position.x, height, base.transform.position.z);
		}
		UpdateAfterTransformChange(height);
	}

	public void UpdateAfterTransformChange(float height)
	{
		AdjustSplineComputerForHeight(height);
		UpdateCapPosition(height);
		UpdateCollisionOutline();
		UpdatePolygonShapes();
	}

	public BridgeJoint CreateAnchor()
	{
		return BridgeJoints.CreateAnchor(base.transform.position + new Vector3(0f, GetTotalHeight(), 0f), Utils.GenerateUniqueId());
	}

	public BridgeJoint GetAnchor()
	{
		return BridgeJoints.FindByGuid(m_AnchorGuid);
	}

	public float GetTotalHeight()
	{
		return BridgePillars.MESH_BASE_HEIGHT + GetTopHeight();
	}

	public float GetTopHeight()
	{
		return BridgePillars.MESH_TOP_HEIGHT * (m_Top.transform.localScale.y + BridgePillars.TOP_SCALE_ADJUSTMENT);
	}

	public void EnableOutline(bool enable)
	{
		m_Outline.SetActive(enable);
	}

	public void Select()
	{
		m_Selected = true;
		if (m_Outline != null)
		{
			if (GameStateManager.GetState() == GameState.BUILD)
			{
				m_Outline.SetColor(GameUI.m_Instance.m_PillarSelectGoldColor);
			}
			else if (GameStateManager.GetState() == GameState.SANDBOX)
			{
				m_Outline.SetColor(GameUI.m_Instance.m_OutlineSelectedColorSandbox);
			}
		}
	}

	public void DeSelect()
	{
		m_Selected = false;
		if (m_Outline != null && GameStateManager.GetState() == GameState.BUILD)
		{
			m_Outline.SetColor(GameUI.m_Instance.m_OutlineColorBuildMode);
		}
		else if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			m_Outline.SetColor(GameUI.m_Instance.m_OutlineColorSandbox);
		}
	}

	public bool IsSelected()
	{
		return m_Selected;
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

	public void SetPrebuiltState(PrebuiltState prebuiltState)
	{
		m_PrebuiltState = prebuiltState;
		m_LockIcon.SetActive(value: false);
	}

	public void SetColor(Color color)
	{
		m_MaterialPropertyBlock.SetColor(BridgePillars.BASE_COLOR_SHADER_ID, color);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void SetMeshLayer(int layer)
	{
		m_Top.gameObject.layer = layer;
		m_Base.gameObject.layer = layer;
		m_Cap.gameObject.layer = layer;
	}

	public void RevertToMovementStart()
	{
		base.gameObject.transform.position = m_StartMovementWorldPos;
		BridgeJoint anchor = GetAnchor();
		anchor.transform.position = anchor.m_StartMovementWorldPos;
		SetTopHeightBasedOnTotalHeight(m_StartMovementHeight);
	}

	public void UpdateClipboardJoint(bool active)
	{
		m_ClipboardJoint.gameObject.SetActive(active);
		HideAnchorSprites(active);
		m_ClipboardJoint.transform.position = base.gameObject.transform.position + new Vector3(0f, GetTotalHeight(), 0f);
	}

	public void SnapAnchorToPos(Vector3 pos)
	{
		Vector3 vector = base.transform.position + new Vector3(0f, GetTotalHeight(), 0f);
		Vector3 vector2 = pos - vector;
		base.transform.Translate(new Vector3(vector2.x, 0f, 0f));
		SetTopHeightBasedOnTotalHeight(pos.y);
	}

	public bool IsValidHeight()
	{
		float totalHeight = GetTotalHeight();
		if (totalHeight < BridgePillars.MIN_HEIGHT - Mathf.Epsilon)
		{
			return false;
		}
		if (totalHeight > BridgePillars.GetMaxHeight() + Mathf.Epsilon)
		{
			return false;
		}
		return true;
	}

	public void HideAnchorSprites(bool hide)
	{
		SpriteRenderer[] componentsInChildren = GetAnchor().GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = !hide;
		}
	}

	public bool ConnectedToLockedEdges()
	{
		BridgeJoint anchor = GetAnchor();
		if (anchor == null)
		{
			return false;
		}
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(anchor))
		{
			if (item.IsLocked())
			{
				return true;
			}
		}
		return false;
	}

	public void DisconnectFromLockedEdges()
	{
		BridgeJoint anchor = GetAnchor();
		if (anchor == null)
		{
			return;
		}
		BridgeJoint bridgeJoint = null;
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(anchor))
		{
			if (!item.IsLocked())
			{
				continue;
			}
			if (item.m_JointA.m_Guid == anchor.m_Guid)
			{
				if (bridgeJoint == null)
				{
					bridgeJoint = BridgeJoints.CreateJoint(item.m_JointA.transform.position, Utils.GenerateUniqueId());
				}
				if (bridgeJoint != null)
				{
					anchor.UnregisterEdgeFromCache(item);
					item.m_JointA = bridgeJoint;
					bridgeJoint.RegisterEdgeInCache(item);
					bridgeJoint.MakeDefaultColor();
				}
			}
			else
			{
				if (bridgeJoint == null)
				{
					bridgeJoint = BridgeJoints.CreateJoint(item.m_JointB.transform.position, Utils.GenerateUniqueId());
				}
				if (bridgeJoint != null)
				{
					anchor.UnregisterEdgeFromCache(item);
					item.m_JointB = bridgeJoint;
					bridgeJoint.RegisterEdgeInCache(item);
					bridgeJoint.MakeDefaultColor();
				}
			}
		}
	}

	public bool HasIllegalPlacement()
	{
		if (base.gameObject.activeInHierarchy && !IsLocked())
		{
			if (BridgePillars.CollidesWithOtherBridgePillar(this, m_PolygonShapes))
			{
				return true;
			}
			if (BridgePillars.AllowedToPlace(base.transform.position, GetAnchor(), GetTotalHeight(), m_PolygonShapes, m_Outline) != PlacementReturnValue.SUCCESS)
			{
				return true;
			}
		}
		return false;
	}

	private void AdjustSplineComputerForHeight(float height)
	{
		for (int i = 0; i < m_CollisionSpline.pointCount; i++)
		{
			if (m_OriginalCollisionSplinePoints[i].y > BridgePillars.Y_THRESHOLD_FOR_ADJUSTABLE_SPLINE_POINTS)
			{
				SplinePoint point = m_CollisionSpline.GetPoint(i, SplineComputer.Space.Local);
				float y = m_OriginalCollisionSplinePoints[i].y + GetTopHeight() - BridgePillars.MESH_CAP_HEIGHT - 1f;
				m_CollisionSpline.SetPointPosition(i, new Vector3(point.position.x, y, point.position.z), SplineComputer.Space.Local);
			}
		}
		m_CollisionSpline.RebuildImmediate();
	}

	private void UpdateCapPosition(float height)
	{
		m_Cap.transform.position = new Vector3(m_Cap.transform.position.x, height - BridgePillars.MESH_CAP_HEIGHT, m_Cap.transform.position.z);
	}

	private void UpdateCollisionOutline()
	{
		if (m_Outline != null)
		{
			m_Outline.ClearCachedSplinePoints();
			if (Mathf.Approximately(GetTotalHeight(), BridgePillars.MIN_HEIGHT))
			{
				m_Outline.UpdateFromSpline(m_CollisionSplineNoMiddle.GetComponent<SplineComputer>(), BridgePillars.BRIDGE_PILLAR_OUTLINE_Z);
			}
			else
			{
				m_Outline.UpdateFromSpline(m_CollisionSpline.GetComponent<SplineComputer>(), BridgePillars.BRIDGE_PILLAR_OUTLINE_Z);
			}
			float outlineWidth = GameUI.m_Instance.GetOutlineWidth(GameStateManager.GetState());
			m_Outline.UpdateForGameState(GameStateManager.GetState(), outlineWidth);
		}
	}

	private void UpdateLockIcon()
	{
		m_FX.transform.position = new Vector3(m_FX.transform.position.x, GetTotalHeight(), m_FX.transform.position.z);
		if (IsLocked())
		{
			m_LockIcon.SetActive(GameStateManager.GetState() == GameState.SANDBOX || m_Selected);
			m_SoftLockIcon.SetActive(value: false);
		}
		else if (IsSoftLocked())
		{
			m_SoftLockIcon.SetActive(GameStateManager.GetState() == GameState.SANDBOX);
			m_LockIcon.SetActive(value: false);
		}
		else
		{
			m_SoftLockIcon.SetActive(value: false);
			m_LockIcon.SetActive(value: false);
		}
	}
}
