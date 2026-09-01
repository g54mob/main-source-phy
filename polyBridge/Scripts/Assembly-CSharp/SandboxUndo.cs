using System.Collections.Generic;
using UnityEngine;

public class SandboxUndo
{
	public static List<SandboxUndoState> m_States = new List<SandboxUndoState>();

	private static SandboxUndoState m_CurrentState;

	private static List<Vehicle> m_VehicleDestroyList = new List<Vehicle>();

	public static void Clear()
	{
		m_States.Clear();
	}

	public static string GetBridgeHashForCurrentState()
	{
		return string.Empty;
	}

	public static void SetBridgeForCurrentState(BridgeSaveData bridgeSaveData, string bridgeHash)
	{
		if (m_CurrentState != null)
		{
			m_CurrentState.m_State.m_Bridge = bridgeSaveData;
			RemoveAllStatesAfterCurrent();
		}
	}

	public static void SnapShot()
	{
		SandboxUndoState sandboxUndoState = new SandboxUndoState();
		if (m_States.Count == 0)
		{
			m_States.Add(sandboxUndoState);
			m_CurrentState = sandboxUndoState;
			return;
		}
		Sandbox.m_UnsavedChanges = true;
		RemoveAllStatesAfterCurrent();
		m_States.Add(sandboxUndoState);
		m_CurrentState = sandboxUndoState;
	}

	public static void PrevSnapShot()
	{
		int num = m_States.IndexOf(m_CurrentState);
		if (num > 0 && num <= m_States.Count)
		{
			LoadState(m_States[num - 1]);
		}
	}

	public static void NextSnapShot()
	{
		int num = m_States.IndexOf(m_CurrentState);
		if (num < m_States.Count - 1)
		{
			LoadState(m_States[num + 1]);
		}
	}

	public static bool CanUndo()
	{
		return m_States.IndexOf(m_CurrentState) > 0;
	}

	public static bool CanRedo()
	{
		return m_States.IndexOf(m_CurrentState) < m_States.Count - 1;
	}

	private static void LoadState(SandboxUndoState state)
	{
		ApplyState(state.m_State);
		m_CurrentState = state;
		SandboxSelectionSet.SelectItemsMatchingGuids(state.m_SelectedItemGuids);
		RefreshSandboxPanels();
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp && GameUI.m_Instance.m_SandboxEditRamp.IsEditingSplinePoints())
		{
			selectedRamp.ActivateControlPoints();
		}
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.m_IsAnchor && joint.gameObject.activeInHierarchy)
			{
				BridgeJoints.DeleteInvalidAnchorEdges(joint);
			}
		}
		m_VehicleDestroyList.Clear();
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (EventTimelines.GetStageWithUnit(vehicle.gameObject) == null)
			{
				m_VehicleDestroyList.Add(vehicle);
			}
		}
		foreach (Vehicle vehicleDestroy in m_VehicleDestroyList)
		{
			Vehicles.DestroyVehicle(vehicleDestroy);
		}
	}

	private static void ApplyState(SandboxLayoutData state)
	{
		EventTimelines.Clear();
		Bridge.Clear();
		ApplyAnchors(state.m_Anchors);
		ApplyHydraulicPhases(state.m_HydraulicsPhases);
		ApplyZedAxisVehicles(state.m_ZedAxisVehicles);
		ApplyVehicles(state.m_Vehicles);
		ApplyCheckpoints(state.m_Checkpoints);
		ApplyStopTriggers(state.m_VehicleStopTriggers);
		ApplyVehicleRestartPhases(state.m_VehicleRestartPhases);
		EventTimelines.Deserialize(state.m_EventTimelines);
		ApplyTerrain(state.m_TerrainStretches);
		ApplyPillars(state.m_Pillars);
		ApplyDecors(state.m_Decors);
		ApplyPlatforms(state.m_Platforms);
		ApplyRamps(state.m_Ramps);
		ApplyFlyingObjects(state.m_FlyingObjects);
		ApplyRocks(state.m_Rocks);
		ApplyWaterBlocks(state.m_WaterBlocks);
		ApplyBuildZones(state.m_BuildZones);
		ApplyCustomShapes(state.m_CustomShapes);
		Budget.Deserialize(state.m_Budget);
		SandboxSettings.Deserialize(state.m_Settings);
		WorkshopSubmit.Deserialize(state.m_Workshop);
		SandboxLayout.DeserializeBridge(state.m_Bridge);
		Vehicles.ResolveCheckpointGuids();
		Checkpoints.UpdateFloatingText();
		BridgeEdges.UpdateTransforms();
		Pistons.DisablePinions();
		WorldBounds.Calculate(GameSettings.WorldWidth(), GameSettings.WorldMinY(), GameSettings.WorldMaxY());
		RefreshEditInfoForSelectedItem();
		if (GameStateManager.GetState() == GameState.DECOR)
		{
			SandboxItems.DisableFloatingText();
		}
		else
		{
			SandboxItems.ResolveOverlappingFloatingText();
		}
	}

	private static void ApplyAnchors(List<BridgeJointProxy> proxies)
	{
		List<BridgeJoint> list = new List<BridgeJoint>();
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (!joint.gameObject.activeInHierarchy || !joint.m_IsAnchor || BridgePillars.IsBridgePillarAnchor(joint.m_Guid))
			{
				continue;
			}
			bool flag = false;
			foreach (BridgeJointProxy proxy in proxies)
			{
				if (proxy.m_Guid == joint.m_Guid)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(joint);
			}
		}
		if (list.Count > 0)
		{
			BridgeJoints.DestroyAnchors(list);
		}
		foreach (BridgeJointProxy proxy2 in proxies)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(proxy2.m_Guid);
			if ((bool)bridgeJoint)
			{
				bridgeJoint.transform.position = proxy2.m_Pos;
				bridgeJoint.m_NoBuild = proxy2.m_NoBuild;
				bridgeJoint.m_SandboxItem.SetOutlineDirty(dirty: true);
				continue;
			}
			BridgeJoint bridgeJoint2 = BridgeJoints.CreateJointFromProxy(proxy2);
			if ((bool)bridgeJoint2)
			{
				bridgeJoint2.m_Guid = proxy2.m_Guid;
			}
		}
		BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditAnchor.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditAnchor.ForceRefresh();
		}
	}

	private static void ApplyHydraulicPhases(List<HydraulicsPhaseProxy> proxies)
	{
		for (int num = HydraulicsPhases.m_Phases.Count - 1; num >= 0; num--)
		{
			HydraulicsPhase hydraulicsPhase = HydraulicsPhases.m_Phases[num];
			if (hydraulicsPhase.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (HydraulicsPhaseProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == hydraulicsPhase.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					HydraulicsPhases.DestroyPhase(hydraulicsPhase);
				}
			}
		}
		foreach (HydraulicsPhaseProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				HydraulicsPhases.ApplyProxyToPhase(sandboxItem.GetComponent<HydraulicsPhase>(), proxy2);
				continue;
			}
			HydraulicsPhase hydraulicsPhase2 = HydraulicsPhases.CreatePhaseFromProxy(proxy2);
			if ((bool)hydraulicsPhase2)
			{
				hydraulicsPhase2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditHydraulicsPhase.ForceRefresh();
		}
	}

	private static void ApplyZedAxisVehicles(List<ZedAxisVehicleProxy> proxies)
	{
		for (int num = ZedAxisVehicles.m_Vehicles.Count - 1; num >= 0; num--)
		{
			ZedAxisVehicle zedAxisVehicle = ZedAxisVehicles.m_Vehicles[num];
			if (zedAxisVehicle.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (ZedAxisVehicleProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == zedAxisVehicle.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ZedAxisVehicles.DestroyVehicle(zedAxisVehicle);
				}
			}
		}
		foreach (ZedAxisVehicleProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				ZedAxisVehicle component = sandboxItem.GetComponent<ZedAxisVehicle>();
				component.transform.position = proxy2.m_Pos;
				component.transform.rotation = proxy2.m_Rot;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				ZedAxisVehicles.ApplyProxyToVehicle(component, proxy2, SandboxLayout.CURRENT_VERSION);
			}
			else
			{
				ZedAxisVehicle zedAxisVehicle2 = ZedAxisVehicles.CreateVehicleFromProxy(proxy2, SandboxLayout.CURRENT_VERSION);
				if ((bool)zedAxisVehicle2)
				{
					zedAxisVehicle2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		ZedAxisVehicles.EnterSandboxMode();
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditZedAxisVehicle.ForceRefresh();
		}
	}

	private static void ApplyVehicles(List<VehicleProxy> proxies)
	{
		for (int num = Vehicles.m_Vehicles.Count - 1; num >= 0; num--)
		{
			Vehicle vehicle = Vehicles.m_Vehicles[num];
			if (vehicle.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (VehicleProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == vehicle.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Vehicles.DestroyVehicle(vehicle);
				}
			}
		}
		foreach (VehicleProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Vehicle component = sandboxItem.GetComponent<Vehicle>();
				component.transform.position = proxy2.m_Pos;
				component.transform.rotation = proxy2.m_Rot;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				Vehicles.ApplyProxyToVehicle(component, proxy2, SandboxLayout.CURRENT_VERSION);
			}
			else
			{
				Vehicle vehicle2 = Vehicles.CreateVehicleFromProxy(proxy2, SandboxLayout.CURRENT_VERSION);
				if ((bool)vehicle2)
				{
					vehicle2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditVehicle.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditVehicle.ForceRefresh();
		}
	}

	private static void ApplyCheckpoints(List<CheckpointProxy> proxies)
	{
		for (int num = Checkpoints.m_Checkpoints.Count - 1; num >= 0; num--)
		{
			Checkpoint checkpoint = Checkpoints.m_Checkpoints[num];
			if (checkpoint.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (CheckpointProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == checkpoint.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Checkpoints.DestroyCheckpoint(checkpoint);
				}
			}
		}
		foreach (CheckpointProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Checkpoint component = sandboxItem.GetComponent<Checkpoint>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				Checkpoints.ApplyProxyToCheckpoint(component, proxy2);
			}
			else
			{
				Checkpoint checkpoint2 = Checkpoints.CreateCheckpointFromProxy(proxy2);
				if ((bool)checkpoint2)
				{
					checkpoint2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditCheckpoint.ForceRefresh();
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditVehicle.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditVehicle.ForceRefresh();
		}
	}

	private static void ApplyStopTriggers(List<VehicleStopTriggerProxy> proxies)
	{
		for (int num = VehicleStopTriggers.m_Triggers.Count - 1; num >= 0; num--)
		{
			VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.m_Triggers[num];
			if (vehicleStopTrigger.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (VehicleStopTriggerProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == vehicleStopTrigger.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					VehicleStopTriggers.DestroyTrigger(vehicleStopTrigger);
				}
			}
		}
		foreach (VehicleStopTriggerProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				VehicleStopTrigger component = sandboxItem.GetComponent<VehicleStopTrigger>();
				component.transform.position = proxy2.m_Pos;
				component.transform.rotation = proxy2.m_Rot;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				VehicleStopTriggers.ApplyProxyToTrigger(component, proxy2);
			}
			else
			{
				VehicleStopTrigger vehicleStopTrigger2 = VehicleStopTriggers.CreateTriggerFromProxy(proxy2);
				if ((bool)vehicleStopTrigger2)
				{
					vehicleStopTrigger2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.ForceRefresh();
		}
	}

	private static void ApplyVehicleRestartPhases(List<VehicleRestartPhaseProxy> proxies)
	{
		for (int num = VehicleRestartPhases.m_Phases.Count - 1; num >= 0; num--)
		{
			VehicleRestartPhase vehicleRestartPhase = VehicleRestartPhases.m_Phases[num];
			if (vehicleRestartPhase.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (VehicleRestartPhaseProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == vehicleRestartPhase.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					VehicleRestartPhases.DestroyPhase(vehicleRestartPhase);
				}
			}
		}
		foreach (VehicleRestartPhaseProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				VehicleRestartPhases.ApplyProxyToPhase(sandboxItem.GetComponent<VehicleRestartPhase>(), proxy2);
				continue;
			}
			VehicleRestartPhase vehicleRestartPhase2 = VehicleRestartPhases.CreatePhaseFromProxy(proxy2);
			if ((bool)vehicleRestartPhase2)
			{
				vehicleRestartPhase2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.ForceRefresh();
		}
	}

	private static void ApplyTerrain(List<TerrainIslandProxy> proxies)
	{
		for (int num = TerrainIslands.m_Terrains.Count - 1; num >= 0; num--)
		{
			TerrainIsland terrainIsland = TerrainIslands.m_Terrains[num];
			if (terrainIsland.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (TerrainIslandProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == terrainIsland.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					TerrainIslands.DestroyTerrain(terrainIsland);
				}
			}
		}
		foreach (TerrainIslandProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				TerrainIsland component = sandboxItem.GetComponent<TerrainIsland>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				TerrainIslands.ApplyProxyToTerrain(component, proxy2);
			}
			else
			{
				TerrainIsland terrainIsland2 = TerrainIslands.CreateTerrainFromProxy(proxy2);
				if ((bool)terrainIsland2)
				{
					terrainIsland2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditTerrain.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditTerrain.ForceRefresh();
		}
	}

	private static void ApplyPillars(List<PillarProxy> proxies)
	{
		for (int num = Pillars.m_Pillars.Count - 1; num >= 0; num--)
		{
			Pillar pillar = Pillars.m_Pillars[num];
			if (pillar.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (PillarProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == pillar.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Pillars.DestroyPillar(pillar);
				}
			}
		}
		foreach (PillarProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Pillar component = sandboxItem.GetComponent<Pillar>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				Pillars.ApplyProxyToPillar(component, proxy2);
			}
			else
			{
				Pillar pillar2 = Pillars.CreatePillarFromProxy(proxy2);
				if ((bool)pillar2)
				{
					pillar2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditPillar.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditPillar.ForceRefresh();
		}
	}

	private static void ApplyDecors(List<DecorProxy> proxies)
	{
		for (int num = Decors.m_Decors.Count - 1; num >= 0; num--)
		{
			Decor decor = Decors.m_Decors[num];
			if (decor.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (DecorProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == decor.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Decors.DestroyDecor(decor);
				}
			}
		}
		foreach (DecorProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Decor component = sandboxItem.GetComponent<Decor>();
				Decors.ApplyProxyToDecor(component, proxy2);
				component.Hide((GameStateManager.GetState() == GameState.SANDBOX && !Profiles.m_ActiveProfile.m_ShowDecor) || (GameStateManager.GetState() == GameState.BUILD && !component.m_ShowInBuildMode));
				continue;
			}
			Decor decor2 = Decors.CreateDecorFromProxy(proxy2);
			if ((bool)decor2)
			{
				decor2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				decor2.Hide(GameStateManager.GetState() == GameState.SANDBOX || (GameStateManager.GetState() == GameState.BUILD && !decor2.m_ShowInBuildMode));
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditDecor.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditDecor.ForceRefresh();
		}
	}

	private static void ApplyPlatforms(List<PlatformProxy> proxies)
	{
		for (int num = Platforms.m_Platforms.Count - 1; num >= 0; num--)
		{
			Platform platform = Platforms.m_Platforms[num];
			if (platform.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (PlatformProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == platform.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Platforms.DestroyPlatform(platform);
				}
			}
		}
		foreach (PlatformProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Platform component = sandboxItem.GetComponent<Platform>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				Platforms.ApplyProxyToPlatform(component, proxy2);
			}
			else
			{
				Platform platform2 = Platforms.CreatePlatformFromProxy(proxy2);
				if ((bool)platform2)
				{
					platform2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditPlatform.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditPlatform.ForceRefresh();
		}
	}

	private static void ApplyRamps(List<RampProxy> proxies)
	{
		for (int num = Ramps.m_Ramps.Count - 1; num >= 0; num--)
		{
			Ramp ramp = Ramps.m_Ramps[num];
			if (ramp.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (RampProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == ramp.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Ramps.DestroyRamp(ramp);
				}
			}
		}
		foreach (RampProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Ramp component = sandboxItem.GetComponent<Ramp>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				Ramps.ApplyProxyToRamp(component, proxy2);
			}
			else
			{
				Ramp ramp2 = Ramps.CreateRampFromProxy(proxy2);
				if ((bool)ramp2)
				{
					ramp2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditRamp.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditRamp.ForceRefresh();
		}
	}

	private static void ApplyFlyingObjects(List<FlyingObjectProxy> proxies)
	{
		for (int num = FlyingObjects.m_FlyingObjects.Count - 1; num >= 0; num--)
		{
			FlyingObject flyingObject = FlyingObjects.m_FlyingObjects[num];
			if (flyingObject.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (FlyingObjectProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == flyingObject.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					FlyingObjects.DestroyFlyingObject(flyingObject);
				}
			}
		}
		foreach (FlyingObjectProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				FlyingObject component = sandboxItem.GetComponent<FlyingObject>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				FlyingObjects.ApplyProxyToFlyingObject(component, proxy2);
			}
			else
			{
				FlyingObject flyingObject2 = FlyingObjects.CreateFlyingObjectFromProxy(proxy2);
				if ((bool)flyingObject2)
				{
					flyingObject2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditFlyingObject.ForceRefresh();
		}
	}

	private static void ApplyRocks(List<RockProxy> proxies)
	{
		for (int num = Rocks.m_Rocks.Count - 1; num >= 0; num--)
		{
			Rock rock = Rocks.m_Rocks[num];
			if (rock.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (RockProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == rock.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Rocks.DestroyRock(rock);
				}
			}
		}
		foreach (RockProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				Rock component = sandboxItem.GetComponent<Rock>();
				component.transform.position = proxy2.m_Pos;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				Rocks.ApplyProxyToRock(component, proxy2);
			}
			else
			{
				Rock rock2 = Rocks.CreateRockFromProxy(proxy2);
				if ((bool)rock2)
				{
					rock2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditRock.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditRock.ForceRefresh();
		}
	}

	private static void ApplyWaterBlocks(List<WaterBlockProxy> proxies)
	{
		for (int num = WaterBlocks.m_WaterBlocks.Count - 1; num >= 0; num--)
		{
			WaterBlock waterBlock = WaterBlocks.m_WaterBlocks[num];
			if (waterBlock.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (WaterBlockProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == waterBlock.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					WaterBlocks.DestroyWaterBlock(waterBlock);
				}
			}
		}
		foreach (WaterBlockProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				WaterBlock component = sandboxItem.GetComponent<WaterBlock>();
				component.transform.position = proxy2.m_Pos;
				WaterBlocks.ApplyProxyToWaterBlock(component, proxy2);
				continue;
			}
			WaterBlock waterBlock2 = WaterBlocks.CreateWaterBlockFromProxy(proxy2);
			if ((bool)waterBlock2)
			{
				waterBlock2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditWater.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditWater.ForceRefresh();
		}
	}

	private static void ApplyBuildZones(List<BuildZoneProxy> proxies)
	{
		for (int num = BuildZones.m_BuildZones.Count - 1; num >= 0; num--)
		{
			BuildZone buildZone = BuildZones.m_BuildZones[num];
			if (buildZone.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (BuildZoneProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == buildZone.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					BuildZones.DestroyBuildZone(buildZone);
				}
			}
		}
		foreach (BuildZoneProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				BuildZone component = sandboxItem.GetComponent<BuildZone>();
				component.transform.position = proxy2.m_Pos;
				component.transform.rotation = Quaternion.identity;
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				BuildZones.ApplyProxyToBuildZone(component, proxy2);
				component.DestroyControlPoints();
				component.CreateControlPoints();
				component.PositionControlPoints();
				component.UpdateBuildZoneFromControlPoints();
			}
			else
			{
				BuildZone buildZone2 = BuildZones.CreateBuildZoneFromProxy(proxy2);
				if ((bool)buildZone2)
				{
					buildZone2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
				}
			}
		}
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditBuildZone.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditBuildZone.ForceRefresh();
		}
	}

	private static void ApplyCustomShapes(List<CustomShapeProxy> proxies)
	{
		for (int num = CustomShapes.m_Shapes.Count - 1; num >= 0; num--)
		{
			CustomShape customShape = CustomShapes.m_Shapes[num];
			if (customShape.gameObject.activeInHierarchy)
			{
				bool flag = false;
				foreach (CustomShapeProxy proxy in proxies)
				{
					if (proxy.m_UndoGuid == customShape.m_SandboxItem.m_UndoGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					CustomShapes.DestroyCustomShape(customShape);
				}
			}
		}
		foreach (CustomShapeProxy proxy2 in proxies)
		{
			SandboxItem sandboxItem = SandboxItems.FindByGuid(proxy2.m_UndoGuid);
			if ((bool)sandboxItem)
			{
				CustomShape component = sandboxItem.GetComponent<CustomShape>();
				CustomShapes.UnParentDynamicAnchors(proxy2.m_DynamicAnchorGuids);
				component.transform.position = proxy2.m_Pos;
				component.transform.rotation = proxy2.m_Rot;
				CustomShapes.ApplyProxyToCustomShape(component, proxy2);
				if (component.m_Pins.Count == 1)
				{
					component.RecalculatePivot();
				}
				component.RebuildCollider();
				if (component.m_Dirty)
				{
					component.RebuildMesh();
				}
				component.m_SandboxItem.SetOutlineDirty(dirty: true);
				component.MarkAllAnchorOutlinesDirty();
			}
			else
			{
				CustomShape customShape2 = CustomShapes.CreateCustomShapeFromProxy(proxy2);
				if ((bool)customShape2)
				{
					customShape2.m_SandboxItem.m_UndoGuid = proxy2.m_UndoGuid;
					customShape2.m_SandboxItem.SetOutlineDirty(dirty: true);
					customShape2.MarkAllAnchorOutlinesDirty();
				}
			}
		}
		CustomShapes.ShowPinMeshes(on: false);
		if (proxies.Count > 0 && GameUI.m_Instance.m_SandboxEditCustomShape.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditCustomShape.ForceRefresh();
		}
	}

	private static void RefreshSandboxPanels()
	{
		if (GameUI.m_Instance.m_SandboxResources.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxResources.RefreshProperties();
		}
		if (GameUI.m_Instance.m_SandboxModifiers.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxModifiers.RefreshProperties();
		}
		if (GameUI.m_Instance.m_SandboxMultiSelect.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxMultiSelect.RefreshProperties();
		}
	}

	private static void RefreshEditInfoForSelectedItem()
	{
		if (SandboxSelectionSet.m_Items.Count != 1)
		{
			return;
		}
		SandboxItem sandboxItem = SandboxSelectionSet.m_Items[0];
		switch (sandboxItem.m_Type)
		{
		case SandboxItemType.ANCHOR:
			GameUI.m_Instance.m_SandboxEditAnchor.RefreshProperties(sandboxItem.GetComponent<BridgeJoint>());
			break;
		case SandboxItemType.ZED_AXIS_VEHICLE:
			GameUI.m_Instance.m_SandboxEditZedAxisVehicle.RefreshProperties(sandboxItem.GetComponent<ZedAxisVehicle>());
			break;
		case SandboxItemType.VEHICLE:
		{
			Vehicle component = sandboxItem.GetComponent<Vehicle>();
			GameUI.m_Instance.m_SandboxEditVehicle.RefreshProperties(component);
			break;
		}
		case SandboxItemType.CHECKPOINT:
			GameUI.m_Instance.m_SandboxEditCheckpoint.RefreshProperties(sandboxItem.GetComponent<Checkpoint>());
			break;
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
			GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.RefreshProperties(sandboxItem.GetComponent<VehicleStopTrigger>());
			break;
		case SandboxItemType.PLATFORM:
			GameUI.m_Instance.m_SandboxEditPlatform.RefreshProperties(sandboxItem.GetComponent<Platform>());
			break;
		case SandboxItemType.RAMP:
			GameUI.m_Instance.m_SandboxEditRamp.RefreshProperties(sandboxItem.GetComponent<Ramp>());
			break;
		case SandboxItemType.HYDRAULICS_PHASE:
			GameUI.m_Instance.m_SandboxEditHydraulicsPhase.RefreshProperties(sandboxItem.GetComponent<HydraulicsPhase>());
			break;
		case SandboxItemType.VEHICLE_RESTART_PHASE:
			GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.RefreshProperties(sandboxItem.GetComponent<VehicleRestartPhase>());
			break;
		case SandboxItemType.TERRAIN:
			GameUI.m_Instance.m_SandboxEditTerrain.RefreshProperties(sandboxItem.GetComponent<TerrainIsland>());
			break;
		case SandboxItemType.FLYING_OBJECT:
			GameUI.m_Instance.m_SandboxEditFlyingObject.RefreshProperties(sandboxItem.GetComponent<FlyingObject>());
			break;
		case SandboxItemType.ROCK:
			GameUI.m_Instance.m_SandboxEditRock.RefreshProperties(sandboxItem.GetComponent<Rock>());
			break;
		case SandboxItemType.BUILD_ZONE:
			GameUI.m_Instance.m_SandboxEditBuildZone.RefreshProperties(sandboxItem.GetComponent<BuildZone>());
			break;
		case SandboxItemType.CUSTOM_SHAPE:
			GameUI.m_Instance.m_SandboxEditCustomShape.RefreshProperties(sandboxItem.GetComponent<CustomShape>());
			if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_SandboxEditCustomShapeTools.RefreshProperties(sandboxItem.GetComponent<CustomShape>());
			}
			break;
		case SandboxItemType.PILLAR:
			GameUI.m_Instance.m_SandboxEditPillar.RefreshProperties(sandboxItem.GetComponent<Pillar>());
			break;
		case SandboxItemType.DECOR:
			GameUI.m_Instance.m_SandboxEditDecor.RefreshProperties(sandboxItem.GetComponent<Decor>());
			break;
		case SandboxItemType.WATER:
			GameUI.m_Instance.m_SandboxEditWater.RefreshProperties(sandboxItem.GetComponent<WaterBlock>());
			break;
		case SandboxItemType.IMPOSTER:
			break;
		}
	}

	private static void RemoveAllStatesAfterCurrent()
	{
		int num = m_States.IndexOf(m_CurrentState);
		for (int num2 = m_States.Count - 1; num2 > num; num2--)
		{
			m_States.RemoveAt(num2);
		}
	}
}
