using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class ClipboardBridgePillar : MonoBehaviour
{
	public ClipboardJoint m_Joint;

	public GameObject m_Cap;

	public GameObject m_Top;

	public PlaceableCollisionInfo m_CollisionInfo;

	public PlaceableCollisionInfo m_CollisionInfoNoMiddle;

	public SplineComputer m_CollisionSpline;

	public SplineComputer m_CollisionSplineNoMiddle;

	[NonSerialized]
	public BridgePillar m_SourceBridgePillar;

	[NonSerialized]
	public BridgePillar m_PastedBridgePillar;

	[NonSerialized]
	public Vector3 m_StartMovementWorldPos;

	[NonSerialized]
	public float m_StartMovementHeight;

	[NonSerialized]
	public bool m_LockedToMouse;

	[NonSerialized]
	public Outline m_Outline;

	private MeshRenderer[] m_MeshRenderers;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private List<Vector3> m_OriginalCollisionSplinePoints = new List<Vector3>();

	internal List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	private void Awake()
	{
		m_MeshRenderers = base.gameObject.GetComponentsInChildren<MeshRenderer>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_Outline = Outlines.Create(GameUI.m_Instance.m_OutlineTextureBuildMode, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthBuildMode, GameUI.m_Instance.m_OutlineColorBuildMode);
		for (int i = 0; i < m_CollisionSpline.pointCount; i++)
		{
			m_OriginalCollisionSplinePoints.Add(m_CollisionSpline.GetPoint(i, SplineComputer.Space.Local).position);
		}
	}

	private void Update()
	{
		UpdateCollisionOutline();
	}

	private void OnDestroy()
	{
		if (m_Outline != null)
		{
			m_Outline.Destroy();
		}
	}

	private void OnEnable()
	{
		if (m_Outline != null)
		{
			m_Outline.SetActive(active: true);
		}
	}

	private void OnDisable()
	{
		if (m_Outline != null)
		{
			m_Outline.SetActive(active: false);
		}
	}

	public void UpdateAnchorIcon()
	{
		if (BridgePillars.AllowedToPlaceClipboardBridgePillar(this, Budget.m_PillarLeft) != PlacementReturnValue.SUCCESS)
		{
			m_Joint.SetBad();
			return;
		}
		Vector3 vector = base.transform.position + new Vector3(0f, GetTotalHeight(), 0f);
		BridgeJoint bridgeJoint = BridgeJoints.FindClosestJoint(vector);
		if ((bool)bridgeJoint && Vector2.Distance(bridgeJoint.transform.position, vector) < GameSettings.NodeRadius())
		{
			if (bridgeJoint.m_IsAnchor || bridgeJoint.transform.position.y < BridgePillars.MIN_HEIGHT || bridgeJoint.transform.position.y > BridgePillars.GetMaxHeight())
			{
				m_Joint.SetBad();
				return;
			}
			Vector3 pos = base.transform.position + new Vector3(0f, GetTotalHeight(), 0f);
			SnapAnchorToPos(bridgeJoint.transform.position);
			if (HasIllegalPlacement())
			{
				SnapAnchorToPos(pos);
				m_Joint.SetBad();
			}
			else
			{
				m_Joint.SetMerge(bridgeJoint);
			}
		}
		else
		{
			m_Joint.SetNormal();
		}
	}

	public void SnapAnchorToPos(Vector3 pos)
	{
		Vector3 vector = base.transform.position + new Vector3(0f, GetTotalHeight(), 0f);
		Vector3 vector2 = pos - vector;
		base.transform.Translate(new Vector3(vector2.x, 0f, 0f));
		SetTopHeightBasedOnTotalHeight(pos.y);
	}

	public void SetPlacementColor()
	{
		m_MaterialPropertyBlock.SetColor(BridgePillars.BASE_COLOR_SHADER_ID, BridgePillars.m_PlacementColor);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void SetErrorColor()
	{
		m_MaterialPropertyBlock.SetColor(BridgePillars.BASE_COLOR_SHADER_ID, BridgePillars.m_ErrorColor);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void SetTopHeightBasedOnTotalHeight(float height)
	{
		float num = height - BridgePillars.MESH_BASE_HEIGHT;
		m_Top.transform.localScale = new Vector3(m_Top.transform.localScale.x, num / BridgePillars.MESH_TOP_HEIGHT - BridgePillars.TOP_SCALE_ADJUSTMENT, m_Top.transform.localScale.z);
		m_Joint.transform.position = new Vector3(base.transform.position.x, height, base.transform.position.z);
		AdjustSplineComputerForHeight(height);
		UpdateCapPosition(height);
		UpdateCollisionOutline();
		UpdatePolygonShapes();
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

	public float GetTotalHeight()
	{
		return BridgePillars.MESH_BASE_HEIGHT + GetTopHeight();
	}

	public float GetTopHeight()
	{
		return BridgePillars.MESH_TOP_HEIGHT * (m_Top.transform.localScale.y + BridgePillars.TOP_SCALE_ADJUSTMENT);
	}

	public void StickToGround()
	{
		base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
	}

	public void UpdateHeight(float height)
	{
		float max = GameGrid.RoundToNearestGridSquareForced(BridgePillars.GetMaxHeight());
		float mIN_HEIGHT = BridgePillars.MIN_HEIGHT;
		float topHeightBasedOnTotalHeight = Mathf.Clamp(height, mIN_HEIGHT, max);
		SetTopHeightBasedOnTotalHeight(topHeightBasedOnTotalHeight);
	}

	public bool HasIllegalPlacement()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (BridgePillars.CollidesWithOtherBridgePillar(null, m_PolygonShapes))
			{
				return true;
			}
			if (BridgePillars.AllowedToPlace(base.transform.position, null, GetTotalHeight(), m_PolygonShapes, m_Outline) != PlacementReturnValue.SUCCESS)
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
		}
	}
}
