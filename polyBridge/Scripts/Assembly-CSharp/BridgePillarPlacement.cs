using UnityEngine;

public class BridgePillarPlacement
{
	public static float DEFAULT_BRIDGE_PILLAR_HEIGHT = 5f;

	public static BridgeMaterialType m_PreviousSelectedBridgeMaterialType;

	private static ClipboardBridgePillar m_ClipboardBridgePillar;

	private static BridgePillarTooltip m_BridgePillarTooltip;

	public static void Init()
	{
		m_ClipboardBridgePillar = Object.Instantiate(Prefabs.m_Instance.m_BridgePillarClipboard, Vector3.zero, Quaternion.identity).GetComponent<ClipboardBridgePillar>();
		m_ClipboardBridgePillar.gameObject.SetActive(value: false);
		m_BridgePillarTooltip = new BridgePillarTooltip();
	}

	public static void UpdateManual(Vector2 screenPos)
	{
		if (m_ClipboardBridgePillar == null || !m_ClipboardBridgePillar.gameObject.activeInHierarchy)
		{
			m_BridgePillarTooltip.Hide();
			return;
		}
		UpdatePosition(screenPos);
		if (!GameUI.IsPointerOverGameObject())
		{
			Vector3 vector = GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(screenPos));
			m_ClipboardBridgePillar.UpdateHeight(vector.y);
		}
		m_ClipboardBridgePillar.UpdateAnchorIcon();
		if (BridgePillars.AllowedToPlaceClipboardBridgePillar(m_ClipboardBridgePillar, Budget.m_PillarLeft) == PlacementReturnValue.SUCCESS)
		{
			m_ClipboardBridgePillar.SetPlacementColor();
		}
		else
		{
			m_ClipboardBridgePillar.SetErrorColor();
		}
		m_ClipboardBridgePillar.UpdatePolygonShapes();
		if (GameInput.GetMouseButtonJustPressed(1))
		{
			CancelPlacementAndSelectPreviousMaterial();
		}
		BridgeJointPlacement.UpdatePlacementCrosshairs(m_ClipboardBridgePillar.m_Joint.transform.position);
		float totalHeight = m_ClipboardBridgePillar.GetTotalHeight();
		m_BridgePillarTooltip.UpdateManual(m_ClipboardBridgePillar.transform.position, totalHeight, BridgePillars.CalculateCostFromHeight(totalHeight), moving: true);
	}

	public static void ProcessButtonUp(Vector2 screenPos)
	{
		if (m_ClipboardBridgePillar == null)
		{
			return;
		}
		if (BridgePillarMovement.m_IgnoreNextPlacement)
		{
			BridgePillarMovement.m_IgnoreNextPlacement = false;
			return;
		}
		if (m_ClipboardBridgePillar.m_Joint.IsBad())
		{
			BridgePlacement.DisplayPlacementFailureMessage(PlacementReturnValue.FAIL_NODE_ILLEGAL_POSITION);
			BridgePlacement.PlayFailPlacement(PlacementReturnValue.FAIL_NODE_ILLEGAL_POSITION);
			return;
		}
		PlacementReturnValue placementReturnValue = BridgePillars.AllowedToPlaceClipboardBridgePillar(m_ClipboardBridgePillar, Budget.m_PillarLeft);
		if (placementReturnValue != PlacementReturnValue.SUCCESS)
		{
			BridgePlacement.DisplayPlacementFailureMessage(placementReturnValue);
			BridgePlacement.PlayFailPlacement(placementReturnValue);
			return;
		}
		string anchorGuid = (m_ClipboardBridgePillar.m_Joint.m_MergeIcon.activeInHierarchy ? m_ClipboardBridgePillar.m_Joint.m_MergeBridgeJoint.m_Guid : string.Empty);
		Vector3 vector = (m_ClipboardBridgePillar.m_Joint.m_MergeIcon.activeInHierarchy ? m_ClipboardBridgePillar.m_Joint.m_MergeBridgeJoint.transform.position : m_ClipboardBridgePillar.transform.position);
		CancelPlacement();
		BridgePillar bridgePillar = BridgePillars.Create(Prefabs.m_Instance.m_BridgePillar, m_ClipboardBridgePillar.GetTotalHeight(), new Vector3(vector.x, 0f, 0f), Quaternion.identity, Utils.GenerateUniqueId(), anchorGuid);
		if (!(bridgePillar == null))
		{
			bridgePillar.SetColor(BridgePillars.m_NormalColor);
			bridgePillar.EnableOutline(enable: true);
			RecordPlacementForUndo(bridgePillar, anchorGuid);
			InterfaceAudio.Play("ui_build_terrain_place");
		}
	}

	public static void ShowClipboardPillar(Vector2 screenPos)
	{
		if (!m_ClipboardBridgePillar.gameObject.activeInHierarchy)
		{
			m_ClipboardBridgePillar.gameObject.SetActive(value: true);
			m_ClipboardBridgePillar.m_Outline.SetActive(active: false);
			UpdatePosition(screenPos);
			m_ClipboardBridgePillar.SetTopHeightBasedOnTotalHeight(GetPlacementHeight(screenPos));
			m_ClipboardBridgePillar.m_Outline.UpdateForGameState(GameStateManager.GetState(), GameUI.m_Instance.GetOutlineWidth(GameStateManager.GetState()));
			UpdateManual(screenPos);
		}
	}

	public static float GetPlacementHeight(Vector2 screenPos)
	{
		return Mathf.Clamp(GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(screenPos)).y, BridgePillars.MIN_HEIGHT, BridgePillars.GetMaxHeight());
	}

	public static bool InPlacementMode()
	{
		if (m_ClipboardBridgePillar != null)
		{
			return m_ClipboardBridgePillar.gameObject.activeInHierarchy;
		}
		return false;
	}

	public static Vector3 GetPlacementPos()
	{
		if (!(m_ClipboardBridgePillar != null))
		{
			return Vector3.zero;
		}
		return m_ClipboardBridgePillar.transform.position;
	}

	public static float GetPlacementCost()
	{
		return BridgePillars.CalculateCostFromHeight(m_ClipboardBridgePillar.GetTotalHeight());
	}

	public static BridgePillar SelectedBridgePillarAtScreenPos(Vector2 screenPos)
	{
		BridgePillar bridgePillar = null;
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(screenPos), out var hitInfo, float.MaxValue, Utils.BRIDGE_PILLAR_LAYER_MASK))
		{
			bridgePillar = hitInfo.transform.GetComponentInParent<BridgePillar>();
		}
		if ((bool)bridgePillar && bridgePillar.IsSelected() && !bridgePillar.IsLocked())
		{
			return bridgePillar;
		}
		return null;
	}

	public static void CancelPlacementAndSelectPreviousMaterialSilent()
	{
		if (m_ClipboardBridgePillar.gameObject.activeInHierarchy)
		{
			CancelPlacement();
			if (m_PreviousSelectedBridgeMaterialType != BridgeMaterialType.INVALID)
			{
				GameUI.m_Instance.m_BottomBar.SelectMaterial(m_PreviousSelectedBridgeMaterialType, animateTransition: true);
			}
		}
	}

	public static void CancelPlacementAndSelectPreviousMaterial()
	{
		if (m_ClipboardBridgePillar.gameObject.activeInHierarchy)
		{
			CancelPlacement();
			if (m_PreviousSelectedBridgeMaterialType != BridgeMaterialType.INVALID)
			{
				GameUI.m_Instance.m_BottomBar.OnMaterial(m_PreviousSelectedBridgeMaterialType);
			}
		}
	}

	public static void CancelPlacement()
	{
		m_ClipboardBridgePillar.gameObject.SetActive(value: false);
		m_BridgePillarTooltip.Hide();
	}

	private static void UpdatePosition(Vector2 screenPos)
	{
		Vector3 vector = GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(screenPos));
		m_ClipboardBridgePillar.transform.position = new Vector3(vector.x, 0f, 0f);
		m_ClipboardBridgePillar.transform.rotation = Quaternion.identity;
	}

	private static void RecordPlacementForUndo(BridgePillar bridgePillar, string anchorGuid)
	{
		BridgeActions.StartRecording();
		BridgeActions.Create(bridgePillar);
		if (anchorGuid == string.Empty)
		{
			BridgeJoint anchor = bridgePillar.GetAnchor();
			if (anchor != null)
			{
				BridgeActions.Create(anchor);
			}
		}
		else
		{
			BridgeJoint anchor2 = bridgePillar.GetAnchor();
			if (!anchor2.m_IsAnchor)
			{
				anchor2.MakeAnchor();
				BridgeActions.MakeAnchor(anchor2);
			}
		}
		BridgeActions.FlushRecording();
	}
}
