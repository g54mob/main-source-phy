using System;
using System.Collections.Generic;
using UnityEngine;

public class SandboxSelectionSet
{
	public static List<SandboxItem> m_Items = new List<SandboxItem>();

	public static bool m_DeleteNextPlacementIfOverlapsUI;

	public static bool m_CancelSelectionAfterFinalizeMovement;

	public static Vector3 m_DuplicateOffset = new Vector3(2f, 0f, 0f);

	private static SandboxItem m_SelectionSetFocus;

	private static bool m_SelectionSetHasMoved;

	private static MovementConstraint m_SelectionMovementConstraint;

	private static Vector2 m_SelectionStartMovingScreenPos;

	private static List<string> m_TempExistingGuidList = new List<string>();

	public static bool IsEmpty()
	{
		return m_Items.Count == 0;
	}

	public static bool MultipleItemsSelected()
	{
		return m_Items.Count > 1;
	}

	public static bool HasAtLeastOneMovableItem()
	{
		foreach (SandboxItem item in m_Items)
		{
			if (item.IsMoveable())
			{
				return true;
			}
		}
		return false;
	}

	public static bool AllMovableItemsAreWater()
	{
		foreach (SandboxItem item in m_Items)
		{
			if (!item.IsMoveable() || item.m_Type != SandboxItemType.WATER)
			{
				return false;
			}
		}
		return true;
	}

	public static bool AllItemsAreDecorOrCustomShapes()
	{
		foreach (SandboxItem item in m_Items)
		{
			if (item.m_Type != SandboxItemType.DECOR && item.m_Type != SandboxItemType.CUSTOM_SHAPE)
			{
				return false;
			}
		}
		return true;
	}

	public static void SelectAllInRect(Rect rect, bool invert)
	{
		foreach (SandboxItem item in SandboxItems.m_Items)
		{
			if (item.m_Type != SandboxItemType.DECOR && item.OverlapsRect(rect))
			{
				SelectItem(item, invert);
			}
		}
		GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
	}

	public static void SelectAllDecorInRect(Rect rect, bool invert)
	{
		foreach (SandboxItem item in SandboxItems.m_Items)
		{
			if (item.m_Type == SandboxItemType.DECOR && item.DecorOverlapsRect(rect))
			{
				SelectItem(item, invert);
			}
		}
		GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
	}

	public static void SelectItemInverted(SandboxItem item)
	{
		if (m_Items.Contains(item))
		{
			DeSelectItem(item);
		}
		else
		{
			SelectItem(item);
		}
	}

	public static void SelectItem(SandboxItem item)
	{
		if (!m_Items.Contains(item))
		{
			m_Items.Add(item);
			item.SetOutlineColor(GameUI.m_Instance.m_OutlineSelectedColorSandbox);
			item.EnableMeshOutline(enable: true);
		}
	}

	public static void SelectItem(SandboxItem item, bool invert)
	{
		if (invert)
		{
			SelectItemInverted(item);
		}
		else
		{
			SelectItem(item);
		}
	}

	public static void DeSelectItem(SandboxItem item)
	{
		if (m_Items.Contains(item))
		{
			m_Items.Remove(item);
			item.SetOutlineColor(SandboxItems.GetDefaultOutlineColor(item));
			item.EnableMeshOutline(enable: false);
		}
	}

	public static void FinalizeMovement()
	{
		foreach (SandboxItem item in m_Items)
		{
			item.FinalizeMovement();
		}
		foreach (SandboxItem item2 in m_Items)
		{
			if ((bool)item2.m_Label)
			{
				SandboxItems.ResolveOverlappingFloatingText();
			}
		}
		if (SelectionSetHasMoved())
		{
			BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
			GameGrid.CenterOnTerrainEdge(TerrainIslands.GetLeftTerrain());
			SandboxUndo.SnapShot();
		}
		if (m_CancelSelectionAfterFinalizeMovement)
		{
			CancelSelection();
			m_CancelSelectionAfterFinalizeMovement = false;
		}
		if ((bool)m_SelectionSetFocus)
		{
			if (SelectionSetHasMoved())
			{
				Game.ForceIgnoreNextSelection();
			}
			CancelSelectionFollowingMouse();
		}
	}

	public static void SetMovementConstraint(MovementConstraint constraint)
	{
		if (m_SelectionMovementConstraint == constraint)
		{
			return;
		}
		m_SelectionMovementConstraint = constraint;
		if (m_SelectionMovementConstraint == MovementConstraint.NONE)
		{
			return;
		}
		foreach (SandboxItem item in m_Items)
		{
			item.m_PosWhenConstraintApplied = item.transform.position;
		}
	}

	public static void CancelSelection()
	{
		foreach (SandboxItem item in m_Items)
		{
			item.SetOutlineColor(SandboxItems.GetDefaultOutlineColor(item));
			item.EnableMeshOutline(enable: false);
		}
		CancelSelectionFollowingMouse();
		m_Items.Clear();
	}

	public static void Delete()
	{
		List<BridgeJoint> list = new List<BridgeJoint>();
		bool flag = false;
		foreach (SandboxItem item in m_Items)
		{
			switch (item.m_Type)
			{
			case SandboxItemType.ANCHOR:
				list.Add(item.GetComponent<BridgeJoint>());
				break;
			case SandboxItemType.ZED_AXIS_VEHICLE:
				ZedAxisVehicles.DestroyVehicle(item.GetComponent<ZedAxisVehicle>());
				break;
			case SandboxItemType.VEHICLE:
				Vehicles.DestroyVehicle(item.GetComponent<Vehicle>());
				flag = true;
				break;
			case SandboxItemType.VEHICLE_STOP_TRIGGER:
				flag = true;
				break;
			case SandboxItemType.CHECKPOINT:
				Checkpoints.DestroyCheckpoint(item.GetComponent<Checkpoint>());
				flag = true;
				break;
			case SandboxItemType.BUILD_ZONE:
				BuildZones.DestroyBuildZone(item.GetComponent<BuildZone>());
				break;
			case SandboxItemType.CUSTOM_SHAPE:
				CustomShapes.DestroyCustomShape(item.GetComponent<CustomShape>());
				break;
			case SandboxItemType.FLYING_OBJECT:
				FlyingObjects.DestroyFlyingObject(item.GetComponent<FlyingObject>());
				break;
			case SandboxItemType.ROCK:
				Rocks.DestroyRock(item.GetComponent<Rock>());
				break;
			case SandboxItemType.PILLAR:
				Pillars.DestroyPillar(item.GetComponent<Pillar>());
				break;
			case SandboxItemType.DECOR:
				Decors.DestroyDecor(item.GetComponent<Decor>());
				break;
			case SandboxItemType.TERRAIN:
				if (item.GetComponent<TerrainIsland>().m_TerrainIslandType == TerrainIslandType.Middle)
				{
					TerrainIslands.DestroyTerrain(item.GetComponent<TerrainIsland>());
				}
				break;
			case SandboxItemType.HYDRAULICS_PHASE:
				HydraulicsPhases.DestroyPhase(item.GetComponent<HydraulicsPhase>());
				break;
			default:
				UnityEngine.Object.Destroy(item.gameObject);
				break;
			case SandboxItemType.WATER:
			case SandboxItemType.VEHICLE_RESTART_PHASE:
				break;
			}
		}
		if (flag)
		{
			SandboxItems.ResolveOverlappingFloatingText();
		}
		BridgeJoints.DestroyAnchors(list);
		CancelSelection();
		SandboxUndo.SnapShot();
	}

	public static bool SelectionFollowsMouse()
	{
		return m_SelectionSetFocus != null;
	}

	public static void CancelSelectionFollowingMouse()
	{
		m_SelectionSetFocus = null;
		m_SelectionSetHasMoved = false;
	}

	public static void RevertSelectionSetToStartPositions()
	{
		bool flag = false;
		if (m_SelectionSetHasMoved)
		{
			foreach (SandboxItem item in m_Items)
			{
				item.transform.position = item.m_PosWhenStartMoving;
				if (item.m_Type == SandboxItemType.TERRAIN)
				{
					item.GetComponent<TerrainIsland>().SetHeight(item.m_HeightWhenStartMoving);
					flag = true;
				}
				else if (item.m_Type == SandboxItemType.WATER)
				{
					item.GetComponent<WaterBlock>().UpdateHeight(item.m_HeightWhenStartMoving);
					flag = true;
				}
				item.UpdatePolygonShapes();
				item.SetOutlineDirty(dirty: true);
			}
		}
		if (flag)
		{
			WaterBlocks.UpdateManual();
		}
	}

	public static void StartMoving(SandboxItem focusItem)
	{
		m_SelectionSetHasMoved = false;
		m_SelectionSetFocus = focusItem;
		m_SelectionStartMovingScreenPos = GameInput.GetMousePosition();
		m_SelectionMovementConstraint = MovementConstraint.NONE;
		foreach (SandboxItem item in m_Items)
		{
			item.m_PosWhenStartMoving = item.transform.position;
			if (item.m_Type == SandboxItemType.WATER)
			{
				item.m_PosWhenStartMoving = new Vector3(item.m_PosWhenStartMoving.x, WaterBlocks.GetHeight(), item.m_PosWhenStartMoving.z);
				item.m_HeightWhenStartMoving = WaterBlocks.GetHeight();
			}
			if (item.m_Type == SandboxItemType.TERRAIN)
			{
				TerrainIsland component = item.GetComponent<TerrainIsland>();
				item.m_HeightWhenStartMoving = component.m_HeightAdded;
			}
		}
		GroupSelect.Cancel();
	}

	private static bool MoveItem(SandboxItem item, Vector3 newPos)
	{
		if (Mathf.Approximately((item.transform.position - newPos).magnitude, 0f))
		{
			return false;
		}
		Vector3 position = item.transform.position;
		if (item.m_Type != SandboxItemType.WATER)
		{
			item.transform.position = newPos;
		}
		if (item.m_Type == SandboxItemType.ANCHOR || item.m_Type == SandboxItemType.CUSTOM_SHAPE)
		{
			BridgeEdges.UpdateTransforms();
		}
		if (m_SelectionMovementConstraint != MovementConstraint.X_AXIS)
		{
			if (item.m_Type == SandboxItemType.TERRAIN)
			{
				GameUI.m_Instance.m_SandboxEditTerrain.m_SliderStretch.m_SandboxInputField.m_ExternalContinuousHoldActive = true;
				item.GetComponent<TerrainIsland>().StretchToGround(item.transform.position.y - position.y);
			}
			else if (item.m_Type == SandboxItemType.WATER)
			{
				item.GetComponent<WaterBlock>().UpdateHeight(newPos.y);
			}
		}
		if (item.m_Type == SandboxItemType.TERRAIN)
		{
			TerrainIsland component = item.GetComponent<TerrainIsland>();
			if (component.m_TerrainIslandType == TerrainIslandType.Bookend)
			{
				WorldBounds.Calculate(GameSettings.WorldWidth(), GameSettings.WorldMinY(), GameSettings.WorldMaxY());
			}
			if (!Mathf.Approximately(component.transform.position.x - item.m_PosWhenStartMoving.x, 0f))
			{
				component.UpdatePolygonShapes();
			}
		}
		item.SetOutlineDirty(dirty: true);
		return true;
	}

	public static void MaybeMoveSelectionWithPointer()
	{
		if (IsEmpty() || !m_SelectionSetFocus || !SelectionSetHasMoved())
		{
			return;
		}
		if (!m_SelectionSetHasMoved)
		{
			InitializeSelectionSetMovement();
			m_SelectionSetHasMoved = true;
		}
		Vector3 mousePosition = GameInput.GetMousePosition();
		Vector3 vector = Vector3.zero;
		if (m_SelectionSetFocus != null && !m_SelectionSetFocus.IsLocked())
		{
			Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(mousePosition + m_SelectionSetFocus.m_OffsetFromPointer);
			worldPointFromScreenPos = ConstrainTargetPos(m_SelectionSetFocus, m_SelectionSetFocus.m_PosWhenConstraintApplied, worldPointFromScreenPos);
			if (!SandboxInput.m_ForceIgnoreGrid)
			{
				worldPointFromScreenPos = m_SelectionSetFocus.SnapPosToGrid(worldPointFromScreenPos);
			}
			if (MoveItem(m_SelectionSetFocus, worldPointFromScreenPos))
			{
				if (!SandboxInput.m_ForceIgnoreGrid && m_SelectionSetFocus.m_Type == SandboxItemType.CUSTOM_SHAPE)
				{
					m_SelectionSetFocus.transform.position += m_SelectionSetFocus.GetComponent<CustomShape>().m_GridOffset;
				}
				if (!SandboxInput.m_ForceIgnoreGrid && m_SelectionSetFocus.m_Type == SandboxItemType.BUILD_ZONE)
				{
					m_SelectionSetFocus.transform.position += m_SelectionSetFocus.GetComponent<BuildZone>().m_GridOffset;
				}
			}
			vector = m_SelectionSetFocus.transform.position - m_SelectionSetFocus.m_PosWhenStartMoving;
		}
		foreach (SandboxItem item in m_Items)
		{
			if (!item.IsLocked() && item != m_SelectionSetFocus)
			{
				Vector3 targetPos = item.m_PosWhenStartMoving + vector;
				targetPos = ConstrainTargetPos(item, item.m_PosWhenConstraintApplied, targetPos);
				if (item.m_Type == SandboxItemType.TERRAIN)
				{
					targetPos = ClampTerrainTargetPosToValidValue(item.GetComponent<TerrainIsland>(), targetPos);
				}
				MoveItem(item, targetPos);
			}
		}
		RefreshPostionUI();
	}

	public static void RefreshPostionUI()
	{
		foreach (SandboxItem item in m_Items)
		{
			UpdateSandboxPositionUI(item);
		}
	}

	public static bool IsSelected(SandboxItem item)
	{
		if ((bool)item)
		{
			return m_Items.Contains(item);
		}
		return false;
	}

	public static SandboxItem GetSelectedItem()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0];
	}

	public static BridgeJoint GetSelectedAnchor()
	{
		if (m_Items.Count != 1 || m_Items[0].m_Type != SandboxItemType.ANCHOR)
		{
			return null;
		}
		return m_Items[0].GetComponent<BridgeJoint>();
	}

	public static ZedAxisVehicle GetSelectedZedAxisVehicle()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<ZedAxisVehicle>();
	}

	public static Checkpoint GetSelectedCheckpoint()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<Checkpoint>();
	}

	public static BuildZone GetSelectedBuildZone()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<BuildZone>();
	}

	public static CustomShape GetSelectedCustomShape()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<CustomShape>();
	}

	public static FlyingObject GetSelectedFlyingObject()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<FlyingObject>();
	}

	public static Rock GetSelectedRock()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<Rock>();
	}

	public static Pillar GetSelectedPillar()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<Pillar>();
	}

	public static Decor GetSelectedDecor()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<Decor>();
	}

	public static HydraulicsPhase GetSelectedHydraulicsPhase()
	{
		if (m_Items.Count != 1 || m_Items[0].m_Type != SandboxItemType.HYDRAULICS_PHASE)
		{
			return null;
		}
		return m_Items[0].GetComponent<HydraulicsPhase>();
	}

	public static VehicleRestartPhase GetSelectedVehicleRestartPhase()
	{
		if (m_Items.Count != 1 || m_Items[0].m_Type != SandboxItemType.VEHICLE_RESTART_PHASE)
		{
			return null;
		}
		return m_Items[0].GetComponent<VehicleRestartPhase>();
	}

	public static GameObject GetSelectedGameObject()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].gameObject;
	}

	public static Platform GetSelectedPlatform()
	{
		if (m_Items.Count != 1 || m_Items[0].m_Type != SandboxItemType.PLATFORM)
		{
			return null;
		}
		return m_Items[0].GetComponent<Platform>();
	}

	public static Ramp GetSelectedRamp()
	{
		if (m_Items.Count != 1 || m_Items[0].m_Type != SandboxItemType.RAMP)
		{
			return null;
		}
		return m_Items[0].GetComponent<Ramp>();
	}

	public static TerrainIsland GetSelectedTerrain()
	{
		if (m_Items.Count != 1 || m_Items[0].m_Type != SandboxItemType.TERRAIN)
		{
			return null;
		}
		return m_Items[0].gameObject.GetComponent<TerrainIsland>();
	}

	public static Vehicle GetSelectedVehicle()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<Vehicle>();
	}

	public static VehicleStopTrigger GetSelectedVehicleStopTrigger()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<VehicleStopTrigger>();
	}

	public static WaterBlock GetSelectedWaterBlock()
	{
		if (m_Items.Count != 1)
		{
			return null;
		}
		return m_Items[0].GetComponent<WaterBlock>();
	}

	public static bool ContainsType(Type type)
	{
		foreach (SandboxItem item in m_Items)
		{
			if ((bool)item.gameObject.GetComponent(type))
			{
				return true;
			}
		}
		return false;
	}

	public static int GetNumberOfType(Type type)
	{
		int num = 0;
		foreach (SandboxItem item in m_Items)
		{
			if ((bool)item.gameObject.GetComponent(type))
			{
				num++;
			}
		}
		return num;
	}

	public static void ForceSelection(SandboxItem pendingSelection)
	{
		if ((bool)pendingSelection)
		{
			CancelSelection();
			SelectItem(pendingSelection);
		}
	}

	public static void OnLayoutLoaded()
	{
		m_TempExistingGuidList.Clear();
	}

	public static void StoreSelectionSetGuids()
	{
		m_TempExistingGuidList.Clear();
		foreach (SandboxItem item in m_Items)
		{
			m_TempExistingGuidList.Add(item.m_UndoGuid);
		}
	}

	public static bool SelectionSetMatchesStoredGuids()
	{
		if (m_TempExistingGuidList.Count != m_Items.Count)
		{
			return false;
		}
		foreach (string tempExistingGuid in m_TempExistingGuidList)
		{
			bool flag = true;
			foreach (SandboxItem item in m_Items)
			{
				if (tempExistingGuid != item.m_UndoGuid)
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	public static void SelectItemsMatchingGuids(List<string> guids)
	{
		if (guids.Count == 0)
		{
			return;
		}
		m_Items.Clear();
		foreach (string guid in guids)
		{
			foreach (SandboxItem item in SandboxItems.m_Items)
			{
				if (item != null && item.gameObject.activeInHierarchy && item.m_UndoGuid == guid)
				{
					SelectItem(item);
					item.SetOutlineDirty(dirty: true);
				}
			}
		}
		GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
	}

	public static void ExportSelectedCustomShapes(string customShapeName)
	{
		if (string.IsNullOrEmpty(customShapeName))
		{
			return;
		}
		List<CustomShape> list = new List<CustomShape>();
		foreach (SandboxItem item in m_Items)
		{
			if (item.m_Type == SandboxItemType.CUSTOM_SHAPE)
			{
				list.Add(item.GetComponent<CustomShape>());
			}
		}
		if (list.Count == 0)
		{
			Debug.LogFormat("No custom shapes in the selection set");
			return;
		}
		CustomShapesLibrary.Add(customShapeName, list);
		GameUI.m_Instance.m_SandboxCreateObjects.PopulateMyCustomShapes();
		GameUI.m_Instance.m_SandboxCreateObjects.m_MyCustomShapesRollout.SetState(RolloutState.EXPANDED);
	}

	public static void DoNudge(Vector3 offset, bool continuousHold)
	{
		if (Mathf.Approximately(offset.magnitude, 0f))
		{
			return;
		}
		int num = 0;
		bool flag = false;
		bool flag2 = false;
		int num2 = 0;
		foreach (SandboxItem item in m_Items)
		{
			if (!item.IsLocked() && item.m_Type == SandboxItemType.TERRAIN && item.GetComponent<TerrainIsland>().m_TerrainIslandType == TerrainIslandType.Bookend)
			{
				num2++;
			}
		}
		if (num2 == 1)
		{
			foreach (SandboxItem item2 in m_Items)
			{
				if (!item2.IsLocked() && item2.m_Type == SandboxItemType.TERRAIN && item2.GetComponent<TerrainIsland>().m_TerrainIslandType == TerrainIslandType.Bookend)
				{
					offset = GetModifiedOffsetForTerrain(item2.GetComponent<TerrainIsland>(), offset);
				}
			}
		}
		foreach (SandboxItem item3 in m_Items)
		{
			if (item3.IsLocked())
			{
				continue;
			}
			if (item3.m_Type == SandboxItemType.ZED_AXIS_VEHICLE)
			{
				ZedAxisVehicle component = item3.GetComponent<ZedAxisVehicle>();
				if (component.GetVehicleType() == ZedAxisVehicleType.BOAT && component.m_SnapToWaterLine && !Mathf.Approximately(offset.y, 0f))
				{
					continue;
				}
			}
			if (item3.m_Type == SandboxItemType.TERRAIN)
			{
				item3.transform.Translate(offset.x, 0f, 0f);
			}
			else
			{
				item3.transform.Translate(offset.x, offset.y, offset.z, Space.World);
			}
			if (AllItemsAreVehiclesOrFlags() && Mathf.Approximately(Mathf.Abs(offset.y), GameGrid.m_Spacing) && GameGrid.IsGridAligned(item3.transform.position.y))
			{
				item3.transform.Translate(0f, BridgeMaterials.GetRoadCollisionOffset(), 0f);
			}
			item3.SetOutlineDirty(dirty: true);
			if (item3.m_Type == SandboxItemType.TERRAIN && !Mathf.Approximately(offset.y, 0f))
			{
				TerrainIsland component2 = item3.GetComponent<TerrainIsland>();
				component2.SetHeight(component2.GetHeight() + offset.y);
			}
			else
			{
				item3.UpdatePolygonShapes();
			}
			if (item3.m_Type == SandboxItemType.ANCHOR)
			{
				flag = true;
			}
			if (item3.m_Type == SandboxItemType.TERRAIN && item3.GetComponent<TerrainIsland>().m_TerrainIslandType == TerrainIslandType.Bookend)
			{
				flag2 = true;
			}
			if (item3.m_Type == SandboxItemType.WATER)
			{
				WaterBlock component3 = item3.GetComponent<WaterBlock>();
				float goalHeight = component3.m_Height + offset.y;
				component3.UpdateHeight(goalHeight);
			}
			num++;
		}
		if (flag)
		{
			BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
		}
		if (flag2)
		{
			WorldBounds.Calculate(GameSettings.WorldWidth(), GameSettings.WorldMinY(), GameSettings.WorldMaxY());
		}
		if (num > 0)
		{
			RefreshPostionUI();
		}
		if (num > 0 && !continuousHold)
		{
			SandboxUndo.SnapShot();
		}
	}

	public static Vector3 GetNudgeVector(NudgeDirection direction, float increment)
	{
		return direction switch
		{
			NudgeDirection.UP => new Vector3(0f, increment, 0f), 
			NudgeDirection.DOWN => new Vector3(0f, 0f - increment, 0f), 
			NudgeDirection.LEFT => new Vector3(0f - increment, 0f, 0f), 
			NudgeDirection.RIGHT => new Vector3(increment, 0f, 0f), 
			NudgeDirection.FORWARD => new Vector3(0f, 0f, increment), 
			NudgeDirection.BACK => new Vector3(0f, 0f, 0f - increment), 
			_ => Vector2.zero, 
		};
	}

	private static void UpdateSandboxPositionUI(SandboxItem item)
	{
		item.SetOutlineDirty(dirty: true);
		switch (item.m_Type)
		{
		case SandboxItemType.ANCHOR:
			GameUI.m_Instance.m_SandboxEditAnchor.RefreshPosition(item.GetComponent<BridgeJoint>());
			break;
		case SandboxItemType.ZED_AXIS_VEHICLE:
			GameUI.m_Instance.m_SandboxEditZedAxisVehicle.RefreshPosition(item.GetComponent<ZedAxisVehicle>());
			break;
		case SandboxItemType.CHECKPOINT:
			GameUI.m_Instance.m_SandboxEditCheckpoint.RefreshPosition(item.GetComponent<Checkpoint>());
			break;
		case SandboxItemType.BUILD_ZONE:
			GameUI.m_Instance.m_SandboxEditBuildZone.RefreshPosition(item.GetComponent<BuildZone>());
			break;
		case SandboxItemType.CUSTOM_SHAPE:
		{
			CustomShape component = item.GetComponent<CustomShape>();
			GameUI.m_Instance.m_SandboxEditCustomShape.RefreshPosition(component);
			component.MarkAllAnchorOutlinesDirty();
			break;
		}
		case SandboxItemType.FLYING_OBJECT:
			GameUI.m_Instance.m_SandboxEditFlyingObject.RefreshPosition(item.GetComponent<FlyingObject>());
			break;
		case SandboxItemType.PLATFORM:
			GameUI.m_Instance.m_SandboxEditPlatform.RefreshPosition(item.GetComponent<Platform>());
			break;
		case SandboxItemType.RAMP:
			GameUI.m_Instance.m_SandboxEditRamp.RefreshPosition(item.GetComponent<Ramp>());
			break;
		case SandboxItemType.ROCK:
			GameUI.m_Instance.m_SandboxEditRock.RefreshPosition(item.GetComponent<Rock>());
			break;
		case SandboxItemType.PILLAR:
			GameUI.m_Instance.m_SandboxEditPillar.RefreshPosition(item.GetComponent<Pillar>());
			break;
		case SandboxItemType.DECOR:
			GameUI.m_Instance.m_SandboxEditDecor.RefreshPosition(item.GetComponent<Decor>());
			break;
		case SandboxItemType.TERRAIN:
			GameUI.m_Instance.m_SandboxEditTerrain.RefreshPosition(item.GetComponent<TerrainIsland>());
			break;
		case SandboxItemType.VEHICLE:
			GameUI.m_Instance.m_SandboxEditVehicle.RefreshPosition(item.GetComponent<Vehicle>());
			break;
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
			GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.RefreshPosition(item.GetComponent<VehicleStopTrigger>());
			break;
		default:
			Debug.LogWarningFormat("UpdateSandboxPositionUI called with unsupported SandboxItemType {0}", item.m_Type);
			break;
		case SandboxItemType.WATER:
		case SandboxItemType.HYDRAULICS_PHASE:
		case SandboxItemType.VEHICLE_RESTART_PHASE:
			break;
		}
	}

	private static Vector3 ConstrainTargetPos(SandboxItem item, Vector3 constraint, Vector3 targetPos)
	{
		if (item.m_Type == SandboxItemType.WATER)
		{
			return targetPos;
		}
		switch (m_SelectionMovementConstraint)
		{
		case MovementConstraint.X_AXIS:
			if (Game.InDecorModeTopView())
			{
				targetPos.z = constraint.z;
			}
			else
			{
				targetPos.y = constraint.y;
			}
			break;
		case MovementConstraint.Y_AXIS:
			targetPos.x = constraint.x;
			break;
		}
		if (Game.InDecorModeTopView())
		{
			targetPos.y = item.transform.position.y;
		}
		else
		{
			targetPos.z = item.transform.position.z;
		}
		if (item.m_Type == SandboxItemType.TERRAIN)
		{
			targetPos = ClampTerrainTargetPosToValidValue(item.GetComponent<TerrainIsland>(), targetPos);
		}
		return targetPos;
	}

	private static Vector3 ClampTerrainTargetPosToValidValue(TerrainIsland terrain, Vector3 targetPos)
	{
		if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend && !terrain.m_Flipped)
		{
			TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
			if (targetPos.x > rightTerrain.transform.position.x)
			{
				targetPos.x = rightTerrain.transform.position.x;
			}
			if (Mathf.Abs(rightTerrain.transform.position.x - targetPos.x) >= TerrainIslands.MAX_SEPARATION_X)
			{
				targetPos.x = rightTerrain.transform.position.x - TerrainIslands.MAX_SEPARATION_X;
			}
		}
		if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend && terrain.m_Flipped)
		{
			TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
			if ((bool)leftTerrain && targetPos.x < leftTerrain.transform.position.x)
			{
				targetPos.x = leftTerrain.transform.position.x;
			}
			if (Mathf.Abs(leftTerrain.transform.position.x - targetPos.x) >= TerrainIslands.MAX_SEPARATION_X)
			{
				targetPos.x = leftTerrain.transform.position.x + TerrainIslands.MAX_SEPARATION_X;
			}
		}
		return targetPos;
	}

	private static bool SelectionSetHasMoved()
	{
		Vector2 vector = Utils.V3toV2(GameInput.GetMousePosition()) - m_SelectionStartMovingScreenPos;
		if (Mathf.FloorToInt(Mathf.Abs(vector.x)) <= 0)
		{
			return Mathf.FloorToInt(Mathf.Abs(vector.y)) > 0;
		}
		return true;
	}

	private static void InitializeSelectionSetMovement()
	{
		if (m_SelectionSetFocus != null)
		{
			m_SelectionSetFocus.SetOffsetFromPointer(m_SelectionStartMovingScreenPos);
		}
	}

	private static Vector2 GetModifiedOffsetForTerrain(TerrainIsland terrain, Vector2 offset)
	{
		if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend)
		{
			TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
			TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
			if (terrain == leftTerrain && leftTerrain.transform.position.x + offset.x > rightTerrain.transform.position.x)
			{
				return new Vector2(0f, offset.y);
			}
			if (terrain == rightTerrain && rightTerrain.transform.position.x + offset.x < leftTerrain.transform.position.x)
			{
				return new Vector2(0f, offset.y);
			}
			if (terrain == leftTerrain)
			{
				float num = rightTerrain.transform.position.x - (leftTerrain.transform.position.x + offset.x);
				if (num > TerrainIslands.MAX_SEPARATION_X)
				{
					offset.x += num - TerrainIslands.MAX_SEPARATION_X;
				}
			}
			else
			{
				float num2 = rightTerrain.transform.position.x + offset.x - leftTerrain.transform.position.x;
				if (num2 > TerrainIslands.MAX_SEPARATION_X)
				{
					offset.x -= num2 - TerrainIslands.MAX_SEPARATION_X;
				}
			}
		}
		return offset;
	}

	private static bool AllItemsAreVehiclesOrFlags()
	{
		foreach (SandboxItem item in m_Items)
		{
			if (item.m_Type != SandboxItemType.VEHICLE && item.m_Type != SandboxItemType.VEHICLE_STOP_TRIGGER)
			{
				return false;
			}
		}
		return true;
	}
}
