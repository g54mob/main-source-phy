using System;
using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class BridgeJoint : MonoBehaviour
{
	[Header("Icon")]
	public GameObject m_Icon;

	public GameObject m_IconLeft;

	public GameObject m_IconLeftNumber;

	public GameObject m_IconRight;

	public GameObject m_IconRightNumber;

	public SpriteRenderer m_IconFillLeft;

	public SpriteRenderer m_IconFillRight;

	public SpriteRenderer m_IconLeftDivider;

	public SpriteRenderer m_IconRightDivider;

	public SpriteRenderer m_IconOutlineLeft;

	public SpriteRenderer m_IconOutlineRight;

	[Header("Anchor Icon")]
	public GameObject m_StaticIcon;

	public GameObject m_StaticIconLeft;

	public GameObject m_StaticIconLeftNumber;

	public GameObject m_StaticIconRight;

	public GameObject m_StaticIconRightSplit;

	public GameObject m_StaticIconRightSplitNumber;

	public SpriteRenderer m_StaticIconFillLeft;

	public SpriteRenderer m_StaticIconFillRight;

	public SpriteRenderer m_StaticIconFillRightSplit;

	public SpriteRenderer m_StaticIconLeftDivider;

	public SpriteRenderer m_StaticIconRightSplitDivider;

	public SpriteRenderer m_StaticIconOutlineLeft;

	public SpriteRenderer m_StaticIconOutlineRight;

	public SpriteRenderer m_StaticIconOutlineRightSplit;

	[Header("Split Joints")]
	public GameObject m_Split2;

	public SpriteRenderer m_Split2_SpriteRenderer;

	public GameObject m_Split3;

	public SpriteRenderer m_Split3_A;

	public SpriteRenderer m_Split3_B;

	public SpriteRenderer m_Split3_C;

	public SpriteRenderer m_Split3_A_Number;

	public SpriteRenderer m_Split3_A_Lock;

	public SpriteRenderer m_Split3_B_Number;

	public SpriteRenderer m_Split3_C_Number;

	public Sprite m_Split3_A_Sprite;

	public Sprite m_Split3_A_AnchorSprite;

	[Header("FX")]
	public BridgeJointFlash m_BridgeJointFlash;

	public GameObject m_FX;

	public GameObject m_HoverFX;

	public GameObject m_SnapToFX;

	public GameObject m_SnapToArrowFX;

	public GameObject m_SelectedInnerFX;

	public GameObject m_InSelectionSetFX;

	public SpriteRenderer m_InSelectionSetSpriteRenderer;

	public float m_HoverFXRotateDegreesPerSecond;

	public float m_SelectedInnerFXRotateDegreesPerSecond;

	[Header("Meshes")]
	public GameObject m_Cap;

	public GameObject m_CapMeshSingle;

	public GameObject m_CapMeshDouble;

	public GameObject m_CapMeshTriple;

	[Header("Collision")]
	public Collider m_Collider;

	public SphereCollider m_HotspotCollider;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public bool m_IsAnchor;

	[NonSerialized]
	public bool m_IsDebris;

	[NonSerialized]
	public bool m_IsSplit;

	[NonSerialized]
	public bool m_CreatedBySim;

	[NonSerialized]
	public bool m_NoBuild;

	[NonSerialized]
	public Vector3 m_MoveStartPos;

	[NonSerialized]
	public Vector3 m_BuildPos;

	[NonSerialized]
	public Node m_PhysicsNode;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public SplitJointState m_SplitJointState;

	[NonSerialized]
	public Vector3 m_StartMovementWorldPos;

	[NonSerialized]
	public float m_RestoreZ;

	[NonSerialized]
	public bool m_IsHighlighted;

	[NonSerialized]
	internal List<BridgeEdge> m_ConnectedEdgesCache = new List<BridgeEdge>();

	private bool m_Hover;

	private bool m_SnapTo;

	private bool m_Selected;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private float HOTSPOT_RADIUS = 0.25f;

	private float HOTSPOT_RADIUS_IN_SELECTION_SET = 0.28f;

	private static HashSet<BridgeEdge> tempEdgesSet = new HashSet<BridgeEdge>();

	private static List<BridgeEdge> tempEdgesList = new List<BridgeEdge>();

	public Transform m_Transform { get; private set; }

	private void Awake()
	{
		m_Split2.SetActive(value: false);
		m_Split3.SetActive(value: false);
		m_StaticIcon.SetActive(value: false);
		m_Icon.SetActive(value: true);
		m_Transform = base.transform;
	}

	private void OnDestroy()
	{
		if (BridgeJoints.m_Joints.Contains(this))
		{
			BridgeJoints.m_Joints.Remove(this);
		}
		if (BridgeSelectionSet.m_Joints.Contains(this))
		{
			BridgeSelectionSet.m_Joints.Remove(this);
		}
		HydraulicsController.RemoveJointFromAllPhases(this);
		if (m_BridgeJointFlash != null)
		{
			m_BridgeJointFlash.StopFlashing();
		}
		BridgeJoints.RemoveFromDictionary(this);
	}

	public void Destroy()
	{
		BridgeJoints.RemoveFromDictionary(this);
		base.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void UpdateManualOutsideSim()
	{
		m_HoverFX.SetActive(m_Hover && !m_Selected);
		m_SnapToFX.SetActive(m_SnapTo);
		m_SelectedInnerFX.SetActive(value: false);
		m_InSelectionSetFX.SetActive(m_Selected && BridgeSelectionSet.ContainsJoint(this));
		m_InSelectionSetSpriteRenderer.color = ((GameStateManager.GetState() == GameState.BUILD) ? GameUI.m_Instance.m_EdgeSelectColor : Color.yellow);
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && m_IsSplit)
		{
			HydraulicsPhase selectedHydraulicsPhase = GameUI.m_Instance.m_HydraulicsController.GetSelectedHydraulicsPhase();
			if ((bool)selectedHydraulicsPhase)
			{
				m_InSelectionSetFX.SetActive(value: true);
				bool flag = HydraulicsController.PhaseAffectsSplitJoint(selectedHydraulicsPhase, this);
				m_InSelectionSetSpriteRenderer.color = (flag ? GameUI.m_Instance.m_GoldColor : HydraulicsController.m_DisabledColor);
				SplitJointState splitJointState = ((!flag) ? SplitJointState.NONE_SPLIT : HydraulicsController.GetSplitJointStateForPhase(selectedHydraulicsPhase, this));
				SetSplitJointState(splitJointState);
			}
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			m_HotspotCollider.radius = (m_IsSplit ? HOTSPOT_RADIUS_IN_SELECTION_SET : 0.01f);
		}
		else
		{
			m_HotspotCollider.radius = (m_InSelectionSetFX.activeInHierarchy ? HOTSPOT_RADIUS_IN_SELECTION_SET : HOTSPOT_RADIUS);
		}
		if (m_HoverFX.activeInHierarchy)
		{
			RotateHoverFX();
		}
		if (m_SelectedInnerFX.activeInHierarchy)
		{
			RotateSelectedFX();
		}
		if (IsThreeWaySplitJoint() || TwoWayShouldFunctionAsThreeWay())
		{
			UpdateThreeWaySplitUI();
		}
		else
		{
			HideThreeWaySplitUI();
		}
		UpdateHydraulicControllerTwoWaySplitUI();
	}

	public void DisableOutline()
	{
		m_FX.gameObject.SetActive(GameStateManager.GetState() == GameState.BUILD || Game.InDecorModeFrontView());
		if ((bool)m_SandboxItem)
		{
			m_SandboxItem.m_OutlineGroup.DisableOutline();
		}
	}

	public void UpdateOutline()
	{
		bool flag = GameStateManager.GetState() == GameState.BUILD || Game.InDecorModeFrontView();
		m_FX.gameObject.SetActive(flag);
		if (flag)
		{
			if (m_Outline != null)
			{
				m_Outline.SetActive(active: false);
			}
		}
		else if ((bool)m_SandboxItem)
		{
			if (!m_HasCreatedOutline)
			{
				m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
				m_Outline.SetLayer(Utils.RENDER_LAST_LAYER);
				m_HasCreatedOutline = true;
				m_SandboxItem.SetOutlineDirty(dirty: true);
			}
			if (m_SandboxItem.IsOutlineDirty())
			{
				m_SandboxItem.UpdateOutlineFromBounds(m_Outline, base.transform, m_SandboxItem.m_Colliders[0].bounds);
				m_SandboxItem.SetOutlineDirty(dirty: false);
			}
		}
	}

	public void MakeAnchor()
	{
		m_IsAnchor = true;
		m_Icon.SetActive(value: false);
		m_StaticIcon.SetActive(value: true);
		m_SandboxItem = SandboxItems.AddSandboxItemComponent(base.gameObject, SandboxItemType.ANCHOR);
	}

	public void RevertAnchor()
	{
		m_IsAnchor = false;
		m_Icon.SetActive(value: true);
		m_StaticIcon.SetActive(value: false);
		m_SandboxItem.enabled = false;
		SandboxItems.RemoveItem(base.gameObject);
	}

	public void SetPhysicsNode(Node node)
	{
		m_PhysicsNode = node;
	}

	public Node GetPhysicsNode()
	{
		return m_PhysicsNode;
	}

	public void StartHover()
	{
		m_Hover = true;
	}

	public void EndHover()
	{
		m_Hover = false;
	}

	public void StartSnapTo()
	{
		m_SnapTo = true;
	}

	public void EndSnapTo()
	{
		m_SnapTo = false;
	}

	public bool IsSnapTo()
	{
		return m_SnapTo;
	}

	public void PointSnapToArrowAt(Vector3 pos)
	{
		Vector2 vector = (pos - base.transform.position).normalized;
		m_SnapToArrowFX.transform.localPosition = Utils.V2toV3(vector * 0.2f / m_SnapToArrowFX.transform.lossyScale.x);
		float num = Mathf.Acos(Vector2.Dot(Vector2.up, vector)) * 57.29578f;
		m_SnapToArrowFX.transform.rotation = Quaternion.Euler(0f, 0f, (pos.x > base.transform.position.x) ? (-1f * num) : num);
	}

	public void Select()
	{
		if (!m_Selected && m_Hover)
		{
			m_SelectedInnerFX.transform.rotation = m_HoverFX.transform.rotation;
		}
		m_Selected = true;
	}

	public void DeSelect()
	{
		if (m_Selected && m_Hover)
		{
			m_HoverFX.transform.rotation = m_SelectedInnerFX.transform.rotation;
		}
		m_HoverFX.SetActive(value: false);
		m_SnapToFX.SetActive(value: false);
		m_SelectedInnerFX.SetActive(value: false);
		m_InSelectionSetFX.SetActive(value: false);
		m_Selected = false;
	}

	public bool IsMouseOver()
	{
		return m_Hover;
	}

	public void SetColor(Color color, Color splitColor)
	{
		if (m_IsAnchor)
		{
			m_StaticIconFillLeft.color = color;
			m_StaticIconFillRight.color = color;
			m_StaticIconFillRightSplit.color = splitColor;
		}
		else
		{
			m_IconFillLeft.color = color;
			m_IconFillRight.color = (m_IsSplit ? splitColor : color);
		}
	}

	public void SetOutlineColor(Color color)
	{
		if (m_IsAnchor)
		{
			m_StaticIconOutlineLeft.color = color;
			m_StaticIconOutlineRight.color = color;
			m_StaticIconOutlineRightSplit.color = color;
		}
		else
		{
			m_IconOutlineLeft.color = color;
			m_IconOutlineRight.color = color;
		}
	}

	public bool HasEdgesConnected()
	{
		return BridgeEdges.EdgeIsConnectedToJoint(this);
	}

	public void HideCapIfNoConnectedEdges()
	{
		if ((!m_IsAnchor || !m_IsSplit) && BridgeEdges.GetEdgesConnectedToJoint(this).Count == 0)
		{
			m_Cap.gameObject.SetActive(value: false);
		}
	}

	public void Split()
	{
		if (m_IsAnchor)
		{
			m_StaticIconRight.gameObject.SetActive(value: false);
			m_StaticIconRightSplit.gameObject.SetActive(value: true);
		}
		else
		{
			m_IconFillRight.color = GetSplitJointColor();
		}
		m_IsSplit = true;
		if (IsThreeWaySplitJoint() || TwoWayShouldFunctionAsThreeWay())
		{
			ShowThreeWaySplitUI();
		}
		else
		{
			m_Split2.SetActive(value: true);
		}
		SetSplitJointState(SplitJointState.ALL_SPLIT);
		SetThreeWaySplitJointNumberVisibility(visible: false);
	}

	public void ResetJointSelectors()
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(this))
		{
			if (item.m_JointA == this)
			{
				item.m_JointAPart = item.CalculateJointPart(this);
			}
			else
			{
				item.m_JointBPart = item.CalculateJointPart(this);
			}
			item.EnableJointSelectorForJoint(this);
		}
	}

	public void UnSplit()
	{
		if (m_IsAnchor)
		{
			m_StaticIconRight.gameObject.SetActive(value: true);
			m_StaticIconRightSplit.gameObject.SetActive(value: false);
			m_IconFillRight.color = GetAnchorColor();
		}
		else
		{
			m_IconFillRight.color = GetJointColor();
		}
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(this))
		{
			item.DisableJointSelectorForJoint(this);
		}
		HydraulicsController.RemoveSplitJointFromAllPhases(this);
		m_Split2.SetActive(value: false);
		m_Split3.SetActive(value: false);
		m_IsSplit = false;
		SetSplitJointState(SplitJointState.NONE_SPLIT);
	}

	public void SetSplitJointState(SplitJointState splitJointState)
	{
		m_SplitJointState = splitJointState;
		RefreshCap();
	}

	public bool IsThreeWaySplitJoint()
	{
		if (m_IsSplit)
		{
			return HasConnectedEdgeUsingSplitJointPart(SplitJointPart.C);
		}
		return false;
	}

	public void ForceNonSplitCap()
	{
		m_CapMeshSingle.SetActive(value: true);
		m_CapMeshDouble.SetActive(value: false);
		m_CapMeshTriple.SetActive(value: false);
	}

	public void RefreshCap()
	{
		m_CapMeshSingle.SetActive(value: true);
		m_CapMeshTriple.SetActive(m_IsSplit && (IsThreeWaySplitJoint() || TwoWayShouldFunctionAsThreeWay()));
		m_CapMeshDouble.SetActive(m_IsSplit && !m_CapMeshTriple.gameObject.activeSelf);
	}

	public bool IsCustomShapeDynamicAnchor()
	{
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			if (shape.IsDynamic() && shape.ContainsAnchorGuid(m_Guid))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsDynamicAnchor()
	{
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			if (shape.IsDynamic() && shape.ContainsAnchorGuid(m_Guid))
			{
				return true;
			}
		}
		if (BridgePillars.IsBridgePillarAnchor(m_Guid))
		{
			return true;
		}
		return false;
	}

	public bool IsChildOfCustomShape()
	{
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			if (shape.ContainsAnchorGuid(m_Guid))
			{
				return true;
			}
		}
		return false;
	}

	public bool HasConnectedEdgeUsingSplitJointPart(SplitJointPart splitJointPart)
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(this))
		{
			if (item.m_JointA == this && item.m_JointAPart == splitJointPart)
			{
				return true;
			}
			if (item.m_JointB == this && item.m_JointBPart == splitJointPart)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsConnectedToPiston()
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(this))
		{
			if (item.gameObject.activeInHierarchy && item.IsPiston())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsCloserToRightSideTerrain()
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!rightTerrain || !leftTerrain)
		{
			return false;
		}
		float num = Vector3.Distance(base.transform.position, rightTerrain.transform.position);
		float num2 = Vector3.Distance(base.transform.position, leftTerrain.transform.position);
		return num < num2;
	}

	public bool isCustomShapeAnchor()
	{
		return CustomShapes.AnchorUsedByShape(m_Guid);
	}

	public void MakeGreyScale()
	{
		m_IconFillLeft.color = GameUI.JointGreyScaleColor();
		m_IconFillRight.color = GameUI.JointGreyScaleColor();
		m_IconOutlineLeft.gameObject.SetActive(value: false);
		m_IconOutlineRight.gameObject.SetActive(value: false);
	}

	public void MakeDefaultColor()
	{
		m_IconFillLeft.color = GetJointColor();
		m_IconFillRight.color = (m_IsSplit ? GetSplitJointColor() : GetJointColor());
		Color anchorColor = GetAnchorColor();
		m_StaticIconFillLeft.color = anchorColor;
		m_StaticIconFillRight.color = anchorColor;
		m_IconOutlineLeft.gameObject.SetActive(value: true);
		m_IconOutlineRight.gameObject.SetActive(value: true);
	}

	public void SelectSplitPart(SplitJointPart part)
	{
		switch (m_SplitJointState)
		{
		case SplitJointState.ALL_SPLIT:
			SetSplitJointState(SplitJointState.NONE_SPLIT);
			break;
		case SplitJointState.NONE_SPLIT:
			switch (part)
			{
			case SplitJointPart.A:
				SetSplitJointState(SplitJointState.A_SPLIT_ONLY);
				break;
			case SplitJointPart.B:
				SetSplitJointState(SplitJointState.B_SPLIT_ONLY);
				break;
			case SplitJointPart.C:
				SetSplitJointState(SplitJointState.C_SPLIT_ONLY);
				break;
			}
			break;
		case SplitJointState.A_SPLIT_ONLY:
			switch (part)
			{
			case SplitJointPart.A:
				SetSplitJointState(SplitJointState.NONE_SPLIT);
				break;
			case SplitJointPart.B:
			case SplitJointPart.C:
				SetSplitJointState(SplitJointState.ALL_SPLIT);
				break;
			}
			break;
		case SplitJointState.B_SPLIT_ONLY:
			switch (part)
			{
			case SplitJointPart.B:
				SetSplitJointState(SplitJointState.NONE_SPLIT);
				break;
			case SplitJointPart.A:
			case SplitJointPart.C:
				SetSplitJointState(SplitJointState.ALL_SPLIT);
				break;
			}
			break;
		case SplitJointState.C_SPLIT_ONLY:
			switch (part)
			{
			case SplitJointPart.C:
				SetSplitJointState(SplitJointState.NONE_SPLIT);
				break;
			case SplitJointPart.A:
			case SplitJointPart.B:
				SetSplitJointState(SplitJointState.ALL_SPLIT);
				break;
			}
			break;
		}
	}

	public void ShowHydraulicControllerTwoWaySplitUI()
	{
		m_Split2.SetActive(value: false);
		if (m_IsAnchor)
		{
			m_StaticIconLeftDivider.gameObject.SetActive(value: true);
			m_StaticIconRightSplitDivider.gameObject.SetActive(value: true);
		}
		else
		{
			m_IconLeftDivider.gameObject.SetActive(value: true);
			m_IconRightDivider.gameObject.SetActive(value: true);
		}
	}

	public void HideHydraulicControllerTwoWaySplitUI()
	{
		if (m_IsSplit && !m_Split3.activeInHierarchy)
		{
			m_Split2.SetActive(value: true);
		}
		if (m_IsAnchor)
		{
			m_StaticIconLeftDivider.gameObject.SetActive(value: false);
			m_StaticIconLeftNumber.SetActive(value: false);
			m_StaticIconRightSplitDivider.gameObject.SetActive(value: false);
			m_StaticIconRightSplitNumber.SetActive(value: false);
		}
		else
		{
			m_IconLeftDivider.gameObject.SetActive(value: false);
			m_IconLeftNumber.SetActive(value: false);
			m_IconRightDivider.gameObject.SetActive(value: false);
			m_IconRightNumber.SetActive(value: false);
		}
	}

	private void UpdateHydraulicControllerTwoWaySplitUI()
	{
		float num = 0.04f;
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && m_IsSplit && !m_Split3.activeInHierarchy && m_SplitJointState != SplitJointState.NONE_SPLIT)
		{
			ShowHydraulicControllerTwoWaySplitUI();
			m_StaticIconLeft.transform.localPosition = new Vector3(0f - num, 0f, 0f);
			m_StaticIconRightSplit.transform.localPosition = new Vector3(num, 0f, 0f);
			m_IconLeft.transform.localPosition = new Vector3(0f - num, 0f, 0f);
			m_IconRight.transform.localPosition = new Vector3(num, 0f, 0f);
		}
		else
		{
			HideHydraulicControllerTwoWaySplitUI();
			m_StaticIconLeft.transform.localPosition = Vector3.zero;
			m_StaticIconRightSplit.transform.localPosition = Vector3.zero;
			m_IconLeft.transform.localPosition = Vector3.zero;
			m_IconRight.transform.localPosition = Vector3.zero;
		}
	}

	public void ShowThreeWaySplitUI()
	{
		m_Split2.SetActive(value: false);
		m_Split3.SetActive(value: true);
		m_Split3_A.sprite = (m_IsAnchor ? m_Split3_A_AnchorSprite : m_Split3_A_Sprite);
		m_Split3_A.color = (m_IsAnchor ? GetAnchorColor() : GetSplit3_Color_A());
		m_Split3_B.color = GetSplit3_Color_B();
		m_Split3_C.color = GetSplit3_Color_C();
		m_Icon.SetActive(value: false);
		m_StaticIcon.SetActive(value: false);
	}

	public void HideThreeWaySplitUI()
	{
		m_Split3.SetActive(value: false);
		if (m_IsAnchor)
		{
			m_StaticIcon.SetActive(value: true);
		}
		else
		{
			m_Icon.SetActive(value: true);
		}
		if (m_IsSplit)
		{
			m_Split2.SetActive(value: true);
		}
	}

	public void SetThreeWaySplitJointNumberVisibility(bool visible)
	{
		if (m_IsAnchor)
		{
			m_Split3_A_Lock.gameObject.SetActive(visible);
			m_Split3_A_Number.gameObject.SetActive(value: false);
		}
		else
		{
			m_Split3_A_Number.gameObject.SetActive(visible);
			m_Split3_A_Lock.gameObject.SetActive(value: false);
		}
		m_Split3_B_Number.gameObject.SetActive(visible);
		m_Split3_C_Number.gameObject.SetActive(visible);
	}

	public void SetSplitJointSortOrder(int baseSortOrder)
	{
		m_Split2_SpriteRenderer.sortingOrder = baseSortOrder + 3;
		m_Split3_A.sortingOrder = baseSortOrder;
		m_Split3_B.sortingOrder = baseSortOrder;
		m_Split3_C.sortingOrder = baseSortOrder;
		m_Split3_A_Number.sortingOrder = baseSortOrder + 1;
		m_Split3_B_Number.sortingOrder = baseSortOrder + 1;
		m_Split3_C_Number.sortingOrder = baseSortOrder + 1;
		m_IconFillLeft.sortingOrder = baseSortOrder;
		m_IconFillRight.sortingOrder = baseSortOrder;
		m_IconLeftDivider.sortingOrder = baseSortOrder + 3;
		m_IconRightDivider.sortingOrder = baseSortOrder + 3;
		m_IconOutlineLeft.sortingOrder = baseSortOrder + 4;
		m_IconOutlineRight.sortingOrder = baseSortOrder + 4;
		m_StaticIconFillLeft.sortingOrder = baseSortOrder;
		m_StaticIconFillRightSplit.sortingOrder = baseSortOrder;
		m_StaticIconLeftDivider.sortingOrder = baseSortOrder + 3;
		m_StaticIconRightSplitDivider.sortingOrder = baseSortOrder + 3;
		m_StaticIconOutlineLeft.sortingOrder = baseSortOrder + 3;
		m_StaticIconOutlineRightSplit.sortingOrder = baseSortOrder + 4;
	}

	public Color GetSplit3_Color_A()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		return GameUI.m_Instance.m_Split3_Color_A;
	}

	public Color GetSplit3_Color_B()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		return GameUI.m_Instance.m_Split3_Color_B;
	}

	public Color GetSplit3_Color_C()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		return GameUI.m_Instance.m_Split3_Color_C;
	}

	public Color GetAnchorColor()
	{
		if (m_NoBuild)
		{
			return GameUI.m_Instance.m_NoBuildAnchorColor;
		}
		BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(m_Guid);
		if (bridgePillarWithAnchor != null && bridgePillarWithAnchor.IsLocked())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		if (!IsCustomShapeDynamicAnchor())
		{
			return GameUI.m_Instance.m_StaticJointColor;
		}
		return GameUI.m_Instance.m_DynamicAnchorColor;
	}

	public Color GetJointColor()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		return GameUI.JointColor();
	}

	public Color GetJointHighlightColor()
	{
		if (m_IsAnchor)
		{
			if (IsDynamicAnchor() || IsConnectedToLockedPrebuilt())
			{
				return GetAnchorColor();
			}
			return GetStaticJointHightlightColor();
		}
		return GetJointHightlightColor();
	}

	public Color GetSplitJointColor()
	{
		return GameUI.SplitJointColor();
	}

	public Color GetStaticJointHightlightColor()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		return GameUI.StaticJointHightlightColor();
	}

	public Color GetJointHightlightColor()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.PrebuiltJointHightlightColor();
		}
		return GameUI.JointHightlightColor();
	}

	public Color GetSplitJointHighlightColor()
	{
		if (IsConnectedToLockedPrebuilt())
		{
			return GameUI.m_Instance.m_PrebuiltColor;
		}
		return GameUI.SplitJointHighlightColor();
	}

	public bool IsConnectedToPrebuilt()
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(this))
		{
			if (item.IsPrebuilt())
			{
				return true;
			}
		}
		BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(m_Guid);
		if (bridgePillarWithAnchor != null && bridgePillarWithAnchor.IsPrebuilt())
		{
			return true;
		}
		return false;
	}

	public bool IsConnectedToLockedPrebuilt()
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(this))
		{
			if (item.IsLocked())
			{
				return true;
			}
		}
		BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(m_Guid);
		if (bridgePillarWithAnchor != null && bridgePillarWithAnchor.IsLocked())
		{
			return true;
		}
		return false;
	}

	public bool HasMaxEdges()
	{
		return (float)BridgeEdges.GetNumEdgesConnectedToJoint(this) >= BridgeJoints.MAX_EDGES_PER_JOINT;
	}

	public BridgeJoint Duplicate(Vector3 offset)
	{
		BridgeJointProxy bridgeJointProxy = new BridgeJointProxy(this);
		bridgeJointProxy.m_Pos += offset;
		bridgeJointProxy.m_Guid = Utils.GenerateUniqueId();
		return BridgeJoints.CreateJointFromProxy(bridgeJointProxy);
	}

	private void UpdateThreeWaySplitUI()
	{
		ShowThreeWaySplitUI();
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			m_Split3_A.transform.localPosition = Vector3.zero;
			m_Split3_B.transform.localPosition = Vector3.zero;
			m_Split3_C.transform.localPosition = Vector3.zero;
			return;
		}
		float num = 0.08f;
		switch (m_SplitJointState)
		{
		case SplitJointState.ALL_SPLIT:
		{
			float num2 = 0.05f;
			m_Split3_A.transform.localPosition = new Vector3(0f, 0f - num2, 0f);
			m_Split3_B.transform.localPosition = new Vector3(0f - num2, num2, 0f);
			m_Split3_C.transform.localPosition = new Vector3(num2, num2, 0f);
			break;
		}
		case SplitJointState.NONE_SPLIT:
			m_Split3_A.transform.localPosition = Vector3.zero;
			m_Split3_B.transform.localPosition = Vector3.zero;
			m_Split3_C.transform.localPosition = Vector3.zero;
			break;
		case SplitJointState.A_SPLIT_ONLY:
			m_Split3_A.transform.localPosition = new Vector3(0f, 0f - num, 0f);
			m_Split3_B.transform.localPosition = Vector3.zero;
			m_Split3_C.transform.localPosition = Vector3.zero;
			break;
		case SplitJointState.B_SPLIT_ONLY:
			m_Split3_A.transform.localPosition = Vector3.zero;
			m_Split3_B.transform.localPosition = new Vector3(0f - num, num, 0f);
			m_Split3_C.transform.localPosition = Vector3.zero;
			break;
		case SplitJointState.C_SPLIT_ONLY:
			m_Split3_A.transform.localPosition = Vector3.zero;
			m_Split3_B.transform.localPosition = Vector3.zero;
			m_Split3_C.transform.localPosition = new Vector3(num, num, 0f);
			break;
		}
	}

	private void RotateHoverFX()
	{
		m_HoverFX.transform.Rotate(0f, 0f, Time.unscaledDeltaTime * (0f - m_HoverFXRotateDegreesPerSecond), Space.Self);
	}

	private void RotateSelectedFX()
	{
		m_SelectedInnerFX.transform.Rotate(0f, 0f, Time.unscaledDeltaTime * (0f - m_SelectedInnerFXRotateDegreesPerSecond), Space.Self);
	}

	public void TryRecreateSpringVisualizationForAttachedEdges()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.m_JointA == this || edge.m_JointB == this)
			{
				edge.TryRecreateSpringVisualization();
			}
		}
	}

	public void RegisterEdgeInCache(BridgeEdge edge)
	{
		if (m_ConnectedEdgesCache.IndexOf(edge) == -1)
		{
			m_ConnectedEdgesCache.Add(edge);
		}
	}

	public void UnregisterEdgeFromCache(BridgeEdge edge)
	{
		m_ConnectedEdgesCache.Remove(edge);
	}

	public bool TwoWayShouldFunctionAsThreeWay()
	{
		if (m_IsSplit)
		{
			return SandboxSettings.m_ThreeWaySplitJointsEnabled;
		}
		return false;
	}

	public int GetNumConnectedEdges()
	{
		return m_ConnectedEdgesCache.Count;
	}

	public BridgeEdge GetConnecteEdge(int index)
	{
		if (index < 0 || index >= m_ConnectedEdgesCache.Count)
		{
			return null;
		}
		return m_ConnectedEdgesCache[index];
	}

	public List<BridgeEdge> GetConnectedEdgesCopy()
	{
		return new List<BridgeEdge>(m_ConnectedEdgesCache);
	}

	public BridgeEdge GetEdgeConnectingTo(BridgeJoint other, bool validateOther = true)
	{
		BridgeEdge result = null;
		foreach (BridgeEdge item in m_ConnectedEdgesCache)
		{
			if (item.m_JointA == other || item.m_JointB == other)
			{
				result = item;
				break;
			}
		}
		return result;
	}
}
