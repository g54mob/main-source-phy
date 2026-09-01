using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SandboxItem : MonoBehaviour
{
	public SandboxItemType m_Type;

	public Collider[] m_Colliders;

	public SandboxItemLabel m_Label;

	public Transform m_LabelParent;

	public SpriteRenderer m_LoadingAddressableIcon;

	[NonSerialized]
	public string m_LoadingAddressable;

	[NonSerialized]
	public string m_LoadingAddressableId;

	[NonSerialized]
	public string m_LoadingAddressableModId;

	[NonSerialized]
	public SandboxItemType m_LoadingAddressableType;

	[NonSerialized]
	public string m_UndoGuid;

	[NonSerialized]
	public Vector3 m_OffsetFromPointer;

	[NonSerialized]
	public Transform m_OriginalParent;

	[NonSerialized]
	public int m_OriginalLayer;

	[NonSerialized]
	public bool m_Desaturated;

	[NonSerialized]
	public Vector3 m_PosWhenStartMoving;

	[NonSerialized]
	public Vector3 m_PosWhenConstraintApplied;

	[NonSerialized]
	public float m_HeightWhenStartMoving;

	private bool m_OutlineDirty;

	private bool m_ForceOutlineColorUpdate;

	private Color m_LastOutlineColor;

	[NonSerialized]
	public OutlineGroup m_OutlineGroup;

	private void Awake()
	{
		if (m_Colliders == null)
		{
			m_Colliders = GetComponentsInChildren<Collider>();
		}
		m_OutlineGroup = new OutlineGroup();
		SetFloatingTextToDefaultPosition();
		m_UndoGuid = Utils.GenerateUniqueId();
		m_OriginalLayer = base.gameObject.layer;
		if (m_Label != null)
		{
			m_Label.gameObject.SetActive(value: false);
		}
	}

	private void Start()
	{
		m_OffsetFromPointer = Vector3.zero;
		if (base.transform.parent == null)
		{
			SetParent();
		}
	}

	private void OnDestroy()
	{
		if (SandboxSelectionSet.m_Items.Contains(this))
		{
			SandboxSelectionSet.m_Items.Remove(this);
		}
		if (SandboxItems.m_Items.Contains(this))
		{
			SandboxItems.m_Items.Remove(this);
		}
		if (SandboxItems.m_Imposters.Contains(this))
		{
			SandboxItems.m_Imposters.Remove(this);
		}
		m_OutlineGroup.DestroyOutline();
	}

	private void OnEnable()
	{
		if (!SandboxItems.m_Items.Contains(this))
		{
			SandboxItems.m_Items.Add(this);
		}
	}

	private void OnDisable()
	{
		m_OutlineGroup.DisableOutline();
	}

	private void LateUpdate()
	{
		if (GameStateManager.GetState() != GameState.SANDBOX)
		{
			return;
		}
		if (SandboxSelectionSet.IsSelected(this))
		{
			MaybeSetOutlineColor(GameUI.m_Instance.m_OutlineSelectedColorSandbox);
			return;
		}
		bool num = SandboxItems.m_Hover == this;
		bool flag = GroupSelect.IsActive() && OverlapsRect(GroupSelect.GetRect());
		if (num || flag)
		{
			MaybeSetOutlineColor(GameUI.m_Instance.m_OutlineHoverColorSandbox);
		}
		else
		{
			MaybeSetOutlineColor(SandboxItems.GetDefaultOutlineColor(this));
		}
	}

	private void MaybeSetOutlineColor(Color color)
	{
		if (m_LastOutlineColor != color || m_ForceOutlineColorUpdate)
		{
			SetOutlineColor(color);
			m_LastOutlineColor = color;
			m_ForceOutlineColorUpdate = false;
		}
	}

	public void UpdateFloatingText()
	{
		if (!m_Label)
		{
			return;
		}
		string text = null;
		switch (m_Type)
		{
		case SandboxItemType.VEHICLE:
		case SandboxItemType.ZED_AXIS_VEHICLE:
			if (!SandboxItems.IsNewUnplacedItem(this))
			{
				text = GetTextMeshString();
			}
			break;
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
		case SandboxItemType.CHECKPOINT:
			text = GetTextMeshString();
			break;
		}
		m_Label.UpdateManual(text);
		m_Label.m_Text.color = (m_Desaturated ? GameUI.m_Instance.m_LabelTextColorDucked : GameUI.m_Instance.m_LabelTextColor);
		m_Label.m_Background.color = (m_Desaturated ? GameUI.m_Instance.m_LabelBackgroundColorDucked : GameUI.m_Instance.m_LabelBackgroundColor);
		if (m_Label.m_BackgroundOutline != null)
		{
			m_Label.m_BackgroundOutline.color = (m_Desaturated ? GameUI.m_Instance.m_LabelOutlineColorDucked : GameUI.m_Instance.m_LabelOutlineColor);
		}
	}

	public void SetOffsetFromPointer(Vector2 mouseScreenPos)
	{
		Vector3 position = base.transform.position;
		if (m_Type == SandboxItemType.WATER)
		{
			position = new Vector3(position.x, WaterBlocks.GetHeight(), position.z);
		}
		Vector2 vector = (Vector2)Cameras.MainCamera().WorldToScreenPoint(position) - mouseScreenPos;
		m_OffsetFromPointer = new Vector3(vector.x, vector.y, 0f);
	}

	public void SetFloatingTextToDefaultPosition()
	{
		if ((bool)m_Label && (bool)m_Colliders[0])
		{
			Vector3 vector = GameGrid.SnapPosToGridForced(m_Colliders[0].bounds.center + Vector3.up * (m_Colliders[0].bounds.size.y / 2f + SandboxItems.DEFAULT_FLOATING_TEXT_YOFFSET));
			m_Label.transform.position = new Vector3(vector.x, vector.y, SandboxItems.DEFAULT_FLOATING_TEXT_Z);
		}
	}

	public string GetTextMeshString()
	{
		return m_Type switch
		{
			SandboxItemType.ZED_AXIS_VEHICLE => GetComponent<ZedAxisVehicle>().GetTextMeshString(), 
			SandboxItemType.CHECKPOINT => GetComponent<Checkpoint>().GetTextMeshString(), 
			SandboxItemType.VEHICLE => GetComponent<Vehicle>().GetTextMeshString(), 
			SandboxItemType.VEHICLE_STOP_TRIGGER => GetComponent<VehicleStopTrigger>().GetTextMeshString(), 
			_ => string.Empty, 
		};
	}

	public void DisableFloatingText()
	{
		if ((bool)m_Label)
		{
			m_Label.gameObject.SetActive(value: false);
		}
	}

	public void FinalizeMovement()
	{
		BridgeJoint component = GetComponent<BridgeJoint>();
		if ((bool)component)
		{
			FinalizeAnchorMovement(component);
		}
		CustomShape component2 = GetComponent<CustomShape>();
		if ((bool)component2)
		{
			foreach (CustomShapeAnchor anchor in component2.m_Anchors)
			{
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(anchor.m_BridgeJointGuid);
				if ((bool)bridgeJoint)
				{
					FinalizeAnchorMovement(bridgeJoint);
				}
			}
		}
		Ramp component3 = GetComponent<Ramp>();
		if ((bool)component3)
		{
			component3.RefreshCollider();
		}
		UpdatePolygonShapes();
		if (m_Type == SandboxItemType.TERRAIN)
		{
			GameUI.m_Instance.m_SandboxEditTerrain.m_SliderStretch.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
		}
		m_OutlineDirty = true;
	}

	public void UpdatePolygonShapes()
	{
		switch (m_Type)
		{
		case SandboxItemType.TERRAIN:
		{
			TerrainIsland component4 = GetComponent<TerrainIsland>();
			if ((bool)component4)
			{
				component4.UpdatePolygonShapes();
			}
			break;
		}
		case SandboxItemType.VEHICLE:
		{
			Vehicle component6 = GetComponent<Vehicle>();
			if ((bool)component6)
			{
				component6.UpdatePolygonShapes();
			}
			break;
		}
		case SandboxItemType.ZED_AXIS_VEHICLE:
		{
			ZedAxisVehicle component2 = GetComponent<ZedAxisVehicle>();
			if ((bool)component2)
			{
				component2.UpdatePolygonShapes();
			}
			break;
		}
		case SandboxItemType.ROCK:
		{
			Rock component5 = GetComponent<Rock>();
			if ((bool)component5)
			{
				component5.UpdatePolygonShapes();
			}
			break;
		}
		case SandboxItemType.FLYING_OBJECT:
		{
			FlyingObject component3 = GetComponent<FlyingObject>();
			if ((bool)component3)
			{
				component3.UpdatePolygonShapes();
			}
			break;
		}
		case SandboxItemType.CUSTOM_SHAPE:
		{
			CustomShape component = GetComponent<CustomShape>();
			if ((bool)component)
			{
				component.UpdatePolygonShapes();
			}
			break;
		}
		}
	}

	public Vector3 SnapPosToGrid(Vector3 worldPos)
	{
		Vector3 vector = GameGrid.SnapPosToGridForced(worldPos);
		if (m_Type == SandboxItemType.VEHICLE || m_Type == SandboxItemType.VEHICLE_STOP_TRIGGER)
		{
			return vector + new Vector3(0f, BridgeMaterials.GetRoadCollisionOffset(), 0f);
		}
		return vector;
	}

	public float RoundToNearestGridSquare(float f)
	{
		if (!GameGrid.m_Grid.activeInHierarchy)
		{
			return f;
		}
		return Utils.RoundToNearestMultipleOf(f, GameGrid.m_Spacing);
	}

	public bool IsLocked()
	{
		return m_Type switch
		{
			SandboxItemType.TERRAIN => GetComponent<TerrainIsland>().m_LockPosition, 
			SandboxItemType.WATER => GetComponent<WaterBlock>().m_LockPosition, 
			SandboxItemType.BUILD_ZONE => GetComponent<BuildZone>().m_LockPosition, 
			_ => false, 
		};
	}

	public bool DecorOverlapsRect(Rect rect)
	{
		bool flag = false;
		Collider[] colliders = m_Colliders;
		foreach (Collider collider in colliders)
		{
			flag = ((!Game.InDecorModeTopView()) ? (collider != null && GroupSelect.OverlapsSelectionRect(collider.bounds)) : (collider != null && GroupSelect.OverlapsSelectionRectXZ(collider.bounds)));
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	public bool OverlapsRect(Rect rect)
	{
		if (m_Type == SandboxItemType.ANCHOR)
		{
			BridgeJoint component = GetComponent<BridgeJoint>();
			if ((bool)component && (component.isCustomShapeAnchor() || BridgePillars.IsBridgePillarAnchor(component.m_Guid)))
			{
				return false;
			}
		}
		if (m_Type == SandboxItemType.BUILD_ZONE)
		{
			BuildZone component2 = GetComponent<BuildZone>();
			if ((bool)component2)
			{
				return GroupSelect.OverlapsSelectionRect(new Bounds(component2.GetPosition(), component2.GetSize()));
			}
			return false;
		}
		if (m_Type == SandboxItemType.CUSTOM_SHAPE)
		{
			CustomShape component3 = GetComponent<CustomShape>();
			if ((bool)component3)
			{
				return GroupSelect.OverlapsSelectionRect(component3.m_PolygonCollider2D.bounds);
			}
			return false;
		}
		if (m_Type == SandboxItemType.TERRAIN)
		{
			TerrainIsland component4 = GetComponent<TerrainIsland>();
			if ((bool)component4)
			{
				return component4.OverlapsRect(rect);
			}
			return false;
		}
		if (m_Type == SandboxItemType.VEHICLE)
		{
			Vehicle component5 = GetComponent<Vehicle>();
			if ((bool)component5)
			{
				return component5.OverlapsRect(rect);
			}
			return false;
		}
		if (m_Type == SandboxItemType.ROCK)
		{
			Rock component6 = GetComponent<Rock>();
			if ((bool)component6)
			{
				return component6.OverlapsRect(rect);
			}
			return false;
		}
		if (m_Type == SandboxItemType.FLYING_OBJECT)
		{
			FlyingObject component7 = GetComponent<FlyingObject>();
			if ((bool)component7)
			{
				return component7.OverlapsRect(rect);
			}
			return false;
		}
		Collider[] colliders = m_Colliders;
		foreach (Collider collider in colliders)
		{
			if (collider != null && GroupSelect.OverlapsSelectionRect(collider.bounds))
			{
				if (m_Type == SandboxItemType.RAMP && !GetComponent<Ramp>().OverlapsRect(rect))
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	public void Desaturate(bool on)
	{
		switch (m_Type)
		{
		case SandboxItemType.VEHICLE:
			base.gameObject.GetComponent<Vehicle>()?.Desaturate(on);
			break;
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
			base.gameObject.GetComponent<VehicleStopTrigger>()?.Desaturate(on);
			break;
		case SandboxItemType.CHECKPOINT:
			base.gameObject.GetComponent<Checkpoint>()?.Desaturate(on);
			break;
		}
		m_Desaturated = on;
	}

	public SandboxItem TryDuplicate(Vector3 offset)
	{
		switch (m_Type)
		{
		case SandboxItemType.VEHICLE:
		{
			Vehicle component5 = base.gameObject.GetComponent<Vehicle>();
			if (component5 == null)
			{
				return null;
			}
			Vehicle vehicle = component5.Duplicate(offset);
			if (!(vehicle != null))
			{
				return null;
			}
			return vehicle.m_SandboxItem;
		}
		case SandboxItemType.ZED_AXIS_VEHICLE:
		{
			ZedAxisVehicle component2 = base.gameObject.GetComponent<ZedAxisVehicle>();
			if (component2 == null)
			{
				return null;
			}
			ZedAxisVehicle zedAxisVehicle = component2.Duplicate(offset);
			if (zedAxisVehicle != null)
			{
				zedAxisVehicle.OnlyDrawOutline();
			}
			if (!(zedAxisVehicle != null))
			{
				return null;
			}
			return zedAxisVehicle.m_SandboxItem;
		}
		case SandboxItemType.ANCHOR:
		{
			BridgeJoint bridgeJoint = base.gameObject.GetComponent<BridgeJoint>().Duplicate(offset);
			if (!(bridgeJoint != null))
			{
				return null;
			}
			return bridgeJoint.m_SandboxItem;
		}
		case SandboxItemType.BUILD_ZONE:
		{
			BuildZone component8 = base.gameObject.GetComponent<BuildZone>();
			BuildZone buildZone = component8.Duplicate(BuildZones.GetPrefabForType(component8.m_Type), offset);
			if (!(buildZone != null))
			{
				return null;
			}
			return buildZone.m_SandboxItem;
		}
		case SandboxItemType.CUSTOM_SHAPE:
		{
			CustomShape customShape = base.gameObject.GetComponent<CustomShape>().Duplicate(Prefabs.m_Instance.m_CustomShape, offset);
			if (!(customShape != null))
			{
				return null;
			}
			return customShape.m_SandboxItem;
		}
		case SandboxItemType.PLATFORM:
		{
			Platform platform = base.gameObject.GetComponent<Platform>().Duplicate(offset);
			if (!(platform != null))
			{
				return null;
			}
			return platform.m_SandboxItem;
		}
		case SandboxItemType.RAMP:
		{
			Ramp ramp = base.gameObject.GetComponent<Ramp>().Duplicate(offset);
			if (!(ramp != null))
			{
				return null;
			}
			return ramp.m_SandboxItem;
		}
		case SandboxItemType.FLYING_OBJECT:
		{
			FlyingObject component4 = base.gameObject.GetComponent<FlyingObject>();
			if (component4 == null)
			{
				return null;
			}
			FlyingObject flyingObject = component4.Duplicate(Prefabs.m_PrefabsDict[component4.name], offset);
			if (!(flyingObject != null))
			{
				return null;
			}
			return flyingObject?.m_SandboxItem;
		}
		case SandboxItemType.ROCK:
		{
			Rock component7 = base.gameObject.GetComponent<Rock>();
			if (component7 == null)
			{
				return null;
			}
			Rock rock = component7.Duplicate(Prefabs.m_PrefabsDict[component7.name], offset);
			if (!(rock != null))
			{
				return null;
			}
			return rock.m_SandboxItem;
		}
		case SandboxItemType.TERRAIN:
		{
			TerrainIsland component3 = base.gameObject.GetComponent<TerrainIsland>();
			if ((bool)component3 && component3.m_TerrainIslandType == TerrainIslandType.Middle)
			{
				int terrainPrefabIndex = Theme.m_Instance.GetTerrainPrefabIndex(component3.m_TerrainIslandType, component3.name);
				TerrainIsland terrainIsland = base.gameObject.GetComponent<TerrainIsland>().Duplicate(Theme.m_Instance.GetTerrainIslandPrefab(component3.m_TerrainIslandType, terrainPrefabIndex), offset);
				if (!(terrainIsland != null))
				{
					return null;
				}
				return terrainIsland.m_SandboxItem;
			}
			return null;
		}
		case SandboxItemType.PILLAR:
		{
			Pillar component6 = base.gameObject.GetComponent<Pillar>();
			if (component6 == null)
			{
				return null;
			}
			Pillar pillar = component6.Duplicate(Prefabs.m_PrefabsDict[component6.name], offset);
			if (!(pillar != null))
			{
				return null;
			}
			return pillar.m_SandboxItem;
		}
		case SandboxItemType.DECOR:
		{
			Decor component = base.gameObject.GetComponent<Decor>();
			if (component == null)
			{
				return null;
			}
			DecorStub stub = component.GetStub();
			if (stub == null)
			{
				return null;
			}
			GameObject asyncPrefab = Prefabs.GetAsyncPrefab(stub.m_PrefabAddress);
			if (asyncPrefab == null)
			{
				Debug.LogWarningFormat("Could not find preloaded decor prefab with address " + stub.m_PrefabAddress);
				return null;
			}
			Decor decor = component.Duplicate(asyncPrefab, stub.m_PrefabAddress, stub.m_ModId, offset);
			if (!(decor != null))
			{
				return null;
			}
			return decor.m_SandboxItem;
		}
		default:
			return null;
		}
	}

	public void MaybeCreateSandboxLabel()
	{
		if (m_Label == null)
		{
			GameObject gameObject = null;
			gameObject = ((!(m_LabelParent != null)) ? UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SandboxItemLabel, Vector3.zero, Quaternion.identity, base.transform) : UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SandboxItemLabel, Vector3.zero, Quaternion.identity, m_LabelParent));
			m_Label = gameObject.GetComponent<SandboxItemLabel>();
		}
	}

	public void SetOutlineDirty(bool dirty)
	{
		m_OutlineDirty = dirty;
		if (m_Type == SandboxItemType.CUSTOM_SHAPE)
		{
			GetComponent<CustomShape>().MarkAllAnchorOutlinesDirty();
		}
		if (dirty && SandboxSelectionSet.IsSelected(this))
		{
			m_ForceOutlineColorUpdate = true;
		}
	}

	public bool IsOutlineDirty()
	{
		return m_OutlineDirty;
	}

	public bool IsMoveable()
	{
		if (IsLocked())
		{
			return false;
		}
		switch (m_Type)
		{
		case SandboxItemType.TERRAIN:
		case SandboxItemType.ANCHOR:
		case SandboxItemType.VEHICLE:
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
		case SandboxItemType.WATER:
		case SandboxItemType.CHECKPOINT:
		case SandboxItemType.PLATFORM:
		case SandboxItemType.RAMP:
		case SandboxItemType.FLYING_OBJECT:
		case SandboxItemType.ROCK:
		case SandboxItemType.ZED_AXIS_VEHICLE:
		case SandboxItemType.CUSTOM_SHAPE:
		case SandboxItemType.BUILD_ZONE:
		case SandboxItemType.PILLAR:
		case SandboxItemType.DECOR:
			return true;
		default:
			return false;
		}
	}

	public Sprite GetSpriteForEventViewer()
	{
		return m_Type switch
		{
			SandboxItemType.VEHICLE => base.gameObject.GetComponent<Vehicle>().GetIcon(), 
			SandboxItemType.ZED_AXIS_VEHICLE => base.gameObject.GetComponent<ZedAxisVehicle>().GetIcon(), 
			SandboxItemType.VEHICLE_RESTART_PHASE => base.gameObject.GetComponent<VehicleRestartPhase>().m_Sprite, 
			SandboxItemType.HYDRAULICS_PHASE => base.gameObject.GetComponent<HydraulicsPhase>().m_Sprite, 
			_ => null, 
		};
	}

	public Vehicle GetLinkedVehicle()
	{
		return m_Type switch
		{
			SandboxItemType.VEHICLE => GameStateBuild.m_HoverSandboxItem.GetComponent<Vehicle>(), 
			SandboxItemType.CHECKPOINT => Vehicles.FindByGuid(GameStateBuild.m_HoverSandboxItem.GetComponent<Checkpoint>().m_VehicleGuid), 
			SandboxItemType.VEHICLE_STOP_TRIGGER => Vehicles.FindByGuid(GameStateBuild.m_HoverSandboxItem.GetComponent<VehicleStopTrigger>().m_VehicleGuid), 
			_ => null, 
		};
	}

	private void FinalizeAnchorMovement(BridgeJoint anchor)
	{
		Vector3 translation = anchor.transform.position - anchor.m_BuildPos;
		if (!Mathf.Approximately(translation.magnitude, 0f))
		{
			anchor.m_BuildPos = anchor.transform.position;
			UpdateBridgeAfterAnchorTranslation(anchor, translation);
		}
	}

	private void UpdateBridgeAfterAnchorTranslation(BridgeJoint anchor, Vector3 translation)
	{
		BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
		BridgeJoints.DeleteInvalidAnchorEdges(anchor);
		MaybeReplaceJointWithAnchor();
	}

	private void MaybeReplaceJointWithAnchor()
	{
		BridgeJoint component = GetComponent<BridgeJoint>();
		if (!component)
		{
			return;
		}
		HashSet<BridgeEdge> hashSet = new HashSet<BridgeEdge>();
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (!joint.gameObject.activeInHierarchy || joint.m_IsAnchor || !BridgeJoints.AtSameLocation(component, joint))
			{
				continue;
			}
			foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(component))
			{
				if (item.m_JointA == component)
				{
					foreach (BridgeEdge item2 in BridgeEdges.GetEdgesConnectedToJoint(joint))
					{
						if (item2.m_JointA == joint && item2.m_JointB == item.m_JointB)
						{
							hashSet.Add(item2);
						}
						if (item2.m_JointB == joint && item2.m_JointA == item.m_JointB)
						{
							hashSet.Add(item2);
						}
					}
					component.UnregisterEdgeFromCache(item);
					item.m_JointA = joint;
					joint.RegisterEdgeInCache(item);
					joint.MakeDefaultColor();
				}
				if (!(item.m_JointB == component))
				{
					continue;
				}
				foreach (BridgeEdge item3 in BridgeEdges.GetEdgesConnectedToJoint(joint))
				{
					if (item3.m_JointA == joint && item3.m_JointB == item.m_JointA)
					{
						hashSet.Add(item3);
					}
					if (item3.m_JointB == joint && item3.m_JointA == item.m_JointA)
					{
						hashSet.Add(item3);
					}
				}
				component.UnregisterEdgeFromCache(item);
				item.m_JointB = joint;
				joint.RegisterEdgeInCache(item);
				joint.MakeDefaultColor();
			}
			BridgeEdges.UpdateManual();
			component.gameObject.SetActive(value: false);
			component.Destroy();
			BridgeEdges.UpdateManual();
			joint.MakeAnchor();
			foreach (BridgeEdge item4 in hashSet)
			{
				item4.ForceDisable();
			}
			if (hashSet.Count > 0)
			{
				BridgeEdges.UpdateManual();
				BridgeJoints.DeleteOrphanedJoints();
				BridgeUndo.Reset();
				BridgeRedo.Reset();
			}
			foreach (BridgeEdge item5 in BridgeEdges.GetEdgesConnectedToJoint(joint))
			{
				if ((bool)item5.m_JointSelectorA)
				{
					item5.m_JointSelectorA.RefreshNumber();
				}
				if ((bool)item5.m_JointSelectorB)
				{
					item5.m_JointSelectorB.RefreshNumber();
				}
			}
			SandboxSelectionSet.m_CancelSelectionAfterFinalizeMovement = true;
			break;
		}
	}

	private void SetParent()
	{
		base.transform.parent = SandboxItems.GetSandboxContainerTransform();
	}

	public void UpdateOutlineFromBounds(Outline outline, Transform transform, Bounds bounds)
	{
		outline.SetActive(base.gameObject.activeInHierarchy);
		outline.UpdateFromBounds(transform, bounds, GetOutlineZ());
	}

	public void UpdateOutlineFromBounds(Outline outline, Bounds bounds)
	{
		outline.SetActive(base.gameObject.activeInHierarchy);
		outline.UpdateFromBounds(bounds, GetOutlineZ());
	}

	public void UpdateOutlineFromSpline(Outline outline, SplineComputer spline)
	{
		outline.SetActive(base.gameObject.activeInHierarchy);
		outline.UpdateFromSpline(spline, GetOutlineZ());
	}

	public void UpdateOutlineFromSpline(Outline outline, SplineComputer spline, float yOffset, float yThreshold)
	{
		outline.SetActive(base.gameObject.activeInHierarchy);
		outline.UpdateOutlineFromSpline(spline, yOffset, yThreshold, GetOutlineZ());
	}

	public void UpdateOutlinePoints(Outline outline, List<Vector3> points)
	{
		outline.SetActive(base.gameObject.activeInHierarchy);
		outline.UpdateOutlinePointsInWorldSpace(points);
	}

	public void UpdateOutlineFromPolygonCollider2D(Outline outline, PolygonCollider2D collider)
	{
		outline.SetActive(base.gameObject.activeInHierarchy);
		outline.UpdateFromPolygonCollider2D(collider);
	}

	public float GetOutlineZ()
	{
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			if (!SandboxSelectionSet.IsSelected(this))
			{
				if (!(SandboxItems.m_Hover == this))
				{
					return 0f;
				}
				return -0.5f;
			}
			return -1f;
		}
		if (m_Type != SandboxItemType.ROCK)
		{
			return 0f;
		}
		return -3f;
	}

	public void EnableMeshOutline(bool enable)
	{
		if (m_Type == SandboxItemType.DECOR)
		{
			Decor component = GetComponent<Decor>();
			if ((bool)component.m_Outline)
			{
				component.m_Outline.enabled = enable;
			}
		}
	}

	public void SetOutlineColor(Color color)
	{
		if (m_OutlineGroup != null)
		{
			m_OutlineGroup.SetColor(color);
		}
		if (m_Type == SandboxItemType.VEHICLE)
		{
			base.gameObject.GetComponent<Vehicle>().SetSpriteOutlineColor(color);
		}
	}
}
