using System.Collections.Generic;

public class SandboxLayoutData
{
	public int m_Version;

	public int m_BridgeVersion;

	public string m_ThemeStubKey;

	public string m_ThemeStubId;

	public List<BridgeJointProxy> m_Anchors = new List<BridgeJointProxy>();

	public List<HydraulicsPhaseProxy> m_HydraulicsPhases = new List<HydraulicsPhaseProxy>();

	public BridgeSaveData m_Bridge = new BridgeSaveData();

	public List<ZedAxisVehicleProxy> m_ZedAxisVehicles = new List<ZedAxisVehicleProxy>();

	public List<VehicleProxy> m_Vehicles = new List<VehicleProxy>();

	public List<VehicleStopTriggerProxy> m_VehicleStopTriggers = new List<VehicleStopTriggerProxy>();

	public List<EventTimelineProxy> m_EventTimelines = new List<EventTimelineProxy>();

	public List<CheckpointProxy> m_Checkpoints = new List<CheckpointProxy>();

	public List<TerrainIslandProxy> m_TerrainStretches = new List<TerrainIslandProxy>();

	public List<PillarProxy> m_Pillars = new List<PillarProxy>();

	public List<DecorProxy> m_Decors = new List<DecorProxy>();

	public List<PlatformProxy> m_Platforms = new List<PlatformProxy>();

	public List<RampProxy> m_Ramps = new List<RampProxy>();

	public List<VehicleRestartPhaseProxy> m_VehicleRestartPhases = new List<VehicleRestartPhaseProxy>();

	public List<FlyingObjectProxy> m_FlyingObjects = new List<FlyingObjectProxy>();

	public List<RockProxy> m_Rocks = new List<RockProxy>();

	public List<WaterBlockProxy> m_WaterBlocks = new List<WaterBlockProxy>();

	public List<BuildZoneProxy> m_BuildZones = new List<BuildZoneProxy>();

	public List<CustomShapeProxy> m_CustomShapes = new List<CustomShapeProxy>();

	public BudgetProxy m_Budget = new BudgetProxy();

	public SandboxSettingsProxy m_Settings = new SandboxSettingsProxy();

	public WorkshopProxy m_Workshop = new WorkshopProxy();

	private byte[] m_RawBytes;

	public string GenerateChecksum()
	{
		if (m_RawBytes == null || m_RawBytes.Length == 0)
		{
			return string.Empty;
		}
		return Checksum.Generate(m_RawBytes);
	}

	public SandboxLayoutData()
	{
	}

	public SandboxLayoutData(byte[] bytes, ref int offset)
	{
		m_RawBytes = bytes;
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		SerializePreBridgeBinary(list);
		SerializeBridgeBinary(list);
		SerializePostBridgeBinary(list);
		return list.ToArray();
	}

	public byte[] SerializeWithoutBridgeBinary()
	{
		List<byte> list = new List<byte>();
		SerializePreBridgeBinary(list);
		SerializePostBridgeBinary(list);
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_Version = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_BridgeVersion = ((m_Version >= 38) ? ByteSerializer.DeserializeInt(bytes, ref offset) : 0);
		DeserializeTheme(m_Version, bytes, ref offset);
		if (m_Version >= 19)
		{
			DeserializeAnchorsBinary(m_BridgeVersion, bytes, ref offset);
		}
		if (m_Version == 37)
		{
			DeserializeNoBuildAnchorListBinary_OBSOLETE(bytes, ref offset);
		}
		if (m_Version >= 5)
		{
			DeserializeHydraulicsPhasesBinary(bytes, ref offset);
		}
		DeserializeBridgeBinary(bytes, ref offset);
		if (m_Version >= 7)
		{
			DeserializeZedAxisVehiclesBinary(bytes, ref offset);
		}
		DeserializeVehiclesBinary(bytes, ref offset);
		DeserializeVehicleStopTriggersBinary(bytes, ref offset);
		if (m_Version < 20)
		{
			DeserializeThemeObjectsBinary_OBSOLETE(bytes, ref offset);
		}
		DeserializeEventTimelinesBinary(bytes, ref offset);
		DeserializeCheckpointsBinary(bytes, ref offset);
		DeserializeTerrainStretchesBinary(bytes, ref offset);
		DeserializePlatformsBinary(bytes, ref offset);
		DeserializeRampsBinary(bytes, ref offset);
		if (m_Version < 5)
		{
			DeserializeHydraulicsPhasesBinary(bytes, ref offset);
		}
		DeserializeVehicleRestartPhasesBinary(bytes, ref offset);
		DeserializeFlyingObjectsBinary(bytes, ref offset);
		DeserializeRocksBinary(bytes, ref offset);
		DeserializeWaterBlocksBinary(bytes, ref offset);
		if (m_Version < 5)
		{
			int num = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int i = 0; i < num; i++)
			{
				ByteSerializer.DeserializeString(bytes, ref offset);
				int num2 = ByteSerializer.DeserializeInt(bytes, ref offset);
				for (int j = 0; j < num2; j++)
				{
					ByteSerializer.DeserializeString(bytes, ref offset);
				}
			}
		}
		m_Budget.DeserializeBinary(m_Version, bytes, ref offset);
		m_Settings.DeserializeBinary(m_Version, (m_WaterBlocks.Count > 0) ? m_WaterBlocks[0].m_Height : WaterBlocks.DEFAULT_HEIGHT, bytes, ref offset);
		if (m_Version >= 9)
		{
			DeserializeCustomShapesBinary(bytes, ref offset);
		}
		if (m_Version >= 15)
		{
			m_Workshop.DeserializeBinary(m_Version, bytes, ref offset);
		}
		if (m_Version >= 17 && m_Version <= 28)
		{
			DeserializeSupportPillarsBinary_OBSOLETE(bytes, ref offset);
		}
		if (m_Version >= 18)
		{
			DeserializePillarsBinary(bytes, ref offset);
		}
		if (m_Version >= 32)
		{
			DeserializeBuildZonesBinary(bytes, ref offset);
		}
		if (m_Version >= 33)
		{
			DeserializeTrainTracksBinary_OBSOLETE(bytes, ref offset);
		}
		if (offset < bytes.Length && m_Version >= 35)
		{
			DeserializeDecorsBinary(bytes, ref offset);
		}
	}

	private void SerializePreBridgeBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Version));
		bytes.AddRange(ByteSerializer.SerializeInt(m_BridgeVersion));
		bytes.AddRange(ByteSerializer.SerializeString(m_ThemeStubId));
		SerializeAnchorsBinary(bytes);
		SerializeHydraulicsPhasesBinary(bytes);
	}

	private void SerializeBridgeBinary(List<byte> bytes)
	{
		if (m_Bridge != null)
		{
			bytes.AddRange(m_Bridge.SerializeBinary());
		}
	}

	private void SerializePostBridgeBinary(List<byte> bytes)
	{
		SerializeZedAxisVehiclesBinary(bytes);
		SerializeVehiclesBinary(bytes);
		SerializeVehicleStopTriggersBinary(bytes);
		SerializeEventTimelinesBinary(bytes);
		SerializeCheckpointsBinary(bytes);
		SerializeTerrainStretchesBinary(bytes);
		SerializePlatformsBinary(bytes);
		SerializeRampsBinary(bytes);
		SerializeVehicleRestartPhasesBinary(bytes);
		SerializeFlyingObjectsBinary(bytes);
		SerializeRocksBinary(bytes);
		SerializeWaterBlocksBinary(bytes);
		bytes.AddRange(m_Budget.SerializeBinary());
		bytes.AddRange(m_Settings.SerializeBinary());
		SerializeCustomShapesBinary(bytes);
		bytes.AddRange(m_Workshop.SerializeBinary());
		SerializePillarsBinary(bytes);
		SerializeBuildZonesBinary(bytes);
		SerializeTrainTracksBinary_OBSOLETE(bytes);
		SerializeDecorsBinary(bytes);
	}

	private void SerializeAnchorsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Anchors.Count));
		foreach (BridgeJointProxy anchor in m_Anchors)
		{
			bytes.AddRange(anchor.SerializeBinary());
		}
	}

	private void SerializeZedAxisVehiclesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_ZedAxisVehicles.Count));
		foreach (ZedAxisVehicleProxy zedAxisVehicle in m_ZedAxisVehicles)
		{
			bytes.AddRange(zedAxisVehicle.SerializeBinary());
		}
	}

	private void SerializeVehiclesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Vehicles.Count));
		foreach (VehicleProxy vehicle in m_Vehicles)
		{
			bytes.AddRange(vehicle.SerializeBinary());
		}
	}

	private void SerializeVehicleStopTriggersBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_VehicleStopTriggers.Count));
		foreach (VehicleStopTriggerProxy vehicleStopTrigger in m_VehicleStopTriggers)
		{
			bytes.AddRange(vehicleStopTrigger.SerializeBinary());
		}
	}

	private void SerializeEventTimelinesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_EventTimelines.Count));
		foreach (EventTimelineProxy eventTimeline in m_EventTimelines)
		{
			bytes.AddRange(eventTimeline.SerializeBinary());
		}
	}

	private void SerializeCheckpointsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Checkpoints.Count));
		foreach (CheckpointProxy checkpoint in m_Checkpoints)
		{
			bytes.AddRange(checkpoint.SerializeBinary());
		}
	}

	private void SerializeTerrainStretchesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_TerrainStretches.Count));
		foreach (TerrainIslandProxy terrainStretch in m_TerrainStretches)
		{
			bytes.AddRange(terrainStretch.SerializeBinary());
		}
	}

	private void SerializePlatformsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Platforms.Count));
		foreach (PlatformProxy platform in m_Platforms)
		{
			bytes.AddRange(platform.SerializeBinary());
		}
	}

	private void SerializeRampsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Ramps.Count));
		foreach (RampProxy ramp in m_Ramps)
		{
			ramp.MaybeUpdateLinePoints();
			bytes.AddRange(ramp.SerializeBinary());
		}
	}

	private void SerializeHydraulicsPhasesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_HydraulicsPhases.Count));
		foreach (HydraulicsPhaseProxy hydraulicsPhase in m_HydraulicsPhases)
		{
			bytes.AddRange(hydraulicsPhase.SerializeBinary());
		}
	}

	private void SerializeVehicleRestartPhasesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_VehicleRestartPhases.Count));
		foreach (VehicleRestartPhaseProxy vehicleRestartPhase in m_VehicleRestartPhases)
		{
			bytes.AddRange(vehicleRestartPhase.SerializeBinary());
		}
	}

	private void SerializeFlyingObjectsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_FlyingObjects.Count));
		foreach (FlyingObjectProxy flyingObject in m_FlyingObjects)
		{
			bytes.AddRange(flyingObject.SerializeBinary());
		}
	}

	private void SerializeRocksBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Rocks.Count));
		foreach (RockProxy rock in m_Rocks)
		{
			bytes.AddRange(rock.SerializeBinary());
		}
	}

	private void SerializeWaterBlocksBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_WaterBlocks.Count));
		foreach (WaterBlockProxy waterBlock in m_WaterBlocks)
		{
			bytes.AddRange(waterBlock.SerializeBinary());
		}
	}

	private void SerializeBuildZonesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_BuildZones.Count));
		foreach (BuildZoneProxy buildZone in m_BuildZones)
		{
			bytes.AddRange(buildZone.SerializeBinary());
		}
	}

	private void SerializeCustomShapesBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_CustomShapes.Count));
		foreach (CustomShapeProxy customShape in m_CustomShapes)
		{
			bytes.AddRange(customShape.SerializeBinary());
		}
	}

	private void SerializePillarsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Pillars.Count));
		foreach (PillarProxy pillar in m_Pillars)
		{
			bytes.AddRange(pillar.SerializeBinary());
		}
	}

	private void SerializeTrainTracksBinary_OBSOLETE(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(0));
	}

	private void SerializeDecorsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_Decors.Count));
		foreach (DecorProxy decor in m_Decors)
		{
			bytes.AddRange(decor.SerializeBinary());
		}
	}

	private void DeserializeBridgeBinary(byte[] bytes, ref int offset)
	{
		m_Bridge.DeserializeBinary(bytes, ref offset);
	}

	private void DeserializeTheme(int version, byte[] bytes, ref int offset)
	{
		string name = string.Empty;
		if (m_Version < 35)
		{
			name = ByteSerializer.DeserializeString(bytes, ref offset);
		}
		m_ThemeStubId = ((m_Version >= 35) ? ByteSerializer.DeserializeString(bytes, ref offset) : ThemeStubs.m_Instance.GetIdFromName(name));
		if (ThemeStubs.m_Instance.GetPreloadStubFromId(m_ThemeStubId) == null)
		{
			m_ThemeStubId = ThemeStubs.m_Instance.GetDefaultPreloadStub().m_ID;
		}
	}

	private void DeserializeAnchorsBinary(int version, byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Anchors.Add(new BridgeJointProxy(version, bytes, ref offset));
		}
	}

	private void DeserializeNoBuildAnchorListBinary_OBSOLETE(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			string text = ByteSerializer.DeserializeString(bytes, ref offset);
			foreach (BridgeJointProxy anchor in m_Anchors)
			{
				if (anchor.m_Guid == text)
				{
					anchor.m_NoBuild = true;
				}
			}
		}
	}

	private void DeserializeZedAxisVehiclesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_ZedAxisVehicles.Add(new ZedAxisVehicleProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeVehiclesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Vehicles.Add(new VehicleProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeVehicleStopTriggersBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_VehicleStopTriggers.Add(new VehicleStopTriggerProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeThemeObjectsBinary_OBSOLETE(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			ByteSerializer.DeserializeVector2(bytes, ref offset);
			ByteSerializer.DeserializeString(bytes, ref offset);
			ByteSerializer.DeserializeBool(bytes, ref offset);
		}
	}

	private void DeserializeEventTimelinesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_EventTimelines.Add(new EventTimelineProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeCheckpointsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Checkpoints.Add(new CheckpointProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeTerrainStretchesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_TerrainStretches.Add(new TerrainIslandProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializePlatformsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Platforms.Add(new PlatformProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeRampsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Ramps.Add(new RampProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeHydraulicsPhasesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_HydraulicsPhases.Add(new HydraulicsPhaseProxy(bytes, ref offset));
		}
	}

	private void DeserializeVehicleRestartPhasesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_VehicleRestartPhases.Add(new VehicleRestartPhaseProxy(bytes, ref offset));
		}
	}

	private void DeserializeFlyingObjectsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_FlyingObjects.Add(new FlyingObjectProxy(bytes, ref offset));
		}
	}

	private void DeserializeRocksBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Rocks.Add(new RockProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeWaterBlocksBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_WaterBlocks.Add(new WaterBlockProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeBuildZonesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_BuildZones.Add(new BuildZoneProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeCustomShapesBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_CustomShapes.Add(new CustomShapeProxy(m_Version, bytes, ref offset));
		}
	}

	private void DeserializeTrainTracksBinary_OBSOLETE(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			ByteSerializer.DeserializeVector3(bytes, ref offset);
			ByteSerializer.DeserializeFloat(bytes, ref offset);
			ByteSerializer.DeserializeString(bytes, ref offset);
		}
	}

	private void DeserializeSupportPillarsBinary_OBSOLETE(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			ByteSerializer.DeserializeVector3(bytes, ref offset);
			ByteSerializer.DeserializeVector3(bytes, ref offset);
			ByteSerializer.DeserializeString(bytes, ref offset);
		}
	}

	private void DeserializePillarsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Pillars.Add(new PillarProxy(bytes, ref offset));
		}
	}

	private void DeserializeDecorsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Decors.Add(new DecorProxy(m_Version, bytes, ref offset));
		}
	}
}
