using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class PropData : IReadWrite
{
	public PropID propID;

	public InstanceID ID;

	public InstanceID parentID;

	public TransformData transform;

	public string displayName;

	public string scriptName;

	public RoomEditorTargetPriotity targetPriority;

	public bool isExpandedInHierarchy;

	public bool isObstacle;

	public List<ActivatorComponentData> activator;

	public List<CloudsData> clouds;

	public List<CustomModelData> customModel;

	public List<DelayData> delay;

	public List<DialData> dial;

	public List<EditorDisplayData> display;

	public List<DraggableData> draggable;

	public List<FinishData> finish;

	public List<FloorData> floor;

	public List<FogData> fog;

	public List<ItemData> item;

	public List<ItemRespawnerData> itemRespawner;

	public List<LadderData> ladder;

	public List<EditorLightData> light;

	public List<LockData> @lock;

	public List<LookableData> lookable;

	public List<SwapMaterialData> materials;

	public List<MaterialSwapData> materialSwaps;

	public List<OceanData> ocean;

	public List<OpenLinkData> openLink;

	public List<EditorPostProcessingData> postProcessing;

	public List<PuzzleData> puzzle;

	public List<RotatableData> rotatable;

	public List<RouletteData> roulette;

	public List<ScriptComponentData> script;

	public List<EditorSetupData> setup;

	public List<SkyboxData> skybox;

	public List<SlidableData> slidable;

	public List<SlotData> slot;

	public List<SoundData> sound;

	public List<SpawnPointData> spawnPoint;

	public List<StairsData> stairs;

	public List<Switch3DData> switch3D;

	public List<TeleportData> teleport;

	public List<TestData> test;

	public List<TextData> text;

	public List<TokenData> token;

	public List<TriggerData> trigger;

	public List<TurnableData> turnable;

	public List<TweenStateDataRoomEditor> tweenState;

	public List<WallData> wall;

	public List<WaterData> water;

	public List<ZoomableData> zoomable;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteIReadWrite(propID);
		writer.WriteIReadWrite(ID);
		writer.WriteIReadWrite(parentID);
		writer.WriteIReadWrite(transform);
		writer.Write(displayName);
		writer.Write(scriptName);
		int value = (int)targetPriority;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isExpandedInHierarchy, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isObstacle, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(activator, delegate(FastBinaryWriter w, ActivatorComponentData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(clouds, delegate(FastBinaryWriter w, CloudsData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(customModel, delegate(FastBinaryWriter w, CustomModelData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(delay, delegate(FastBinaryWriter w, DelayData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(dial, delegate(FastBinaryWriter w, DialData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(display, delegate(FastBinaryWriter w, EditorDisplayData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(draggable, delegate(FastBinaryWriter w, DraggableData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(finish, delegate(FastBinaryWriter w, FinishData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(floor, delegate(FastBinaryWriter w, FloorData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(fog, delegate(FastBinaryWriter w, FogData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(item, delegate(FastBinaryWriter w, ItemData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(itemRespawner, delegate(FastBinaryWriter w, ItemRespawnerData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(ladder, delegate(FastBinaryWriter w, LadderData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(light, delegate(FastBinaryWriter w, EditorLightData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(@lock, delegate(FastBinaryWriter w, LockData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(lookable, delegate(FastBinaryWriter w, LookableData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(materials, delegate(FastBinaryWriter w, SwapMaterialData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(materialSwaps, delegate(FastBinaryWriter w, MaterialSwapData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(ocean, delegate(FastBinaryWriter w, OceanData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(openLink, delegate(FastBinaryWriter w, OpenLinkData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(postProcessing, delegate(FastBinaryWriter w, EditorPostProcessingData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(puzzle, delegate(FastBinaryWriter w, PuzzleData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(rotatable, delegate(FastBinaryWriter w, RotatableData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(roulette, delegate(FastBinaryWriter w, RouletteData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(script, delegate(FastBinaryWriter w, ScriptComponentData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(setup, delegate(FastBinaryWriter w, EditorSetupData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(skybox, delegate(FastBinaryWriter w, SkyboxData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(slidable, delegate(FastBinaryWriter w, SlidableData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(slot, delegate(FastBinaryWriter w, SlotData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(sound, delegate(FastBinaryWriter w, SoundData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(spawnPoint, delegate(FastBinaryWriter w, SpawnPointData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(stairs, delegate(FastBinaryWriter w, StairsData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(switch3D, delegate(FastBinaryWriter w, Switch3DData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(teleport, delegate(FastBinaryWriter w, TeleportData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(test, delegate(FastBinaryWriter w, TestData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(text, delegate(FastBinaryWriter w, TextData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(token, delegate(FastBinaryWriter w, TokenData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(trigger, delegate(FastBinaryWriter w, TriggerData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(turnable, delegate(FastBinaryWriter w, TurnableData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(tweenState, delegate(FastBinaryWriter w, TweenStateDataRoomEditor e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(wall, delegate(FastBinaryWriter w, WallData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(water, delegate(FastBinaryWriter w, WaterData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(zoomable, delegate(FastBinaryWriter w, ZoomableData e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		propID = reader.ReadIReadWrite<PropID>();
		ID = reader.ReadIReadWrite<InstanceID>();
		parentID = reader.ReadIReadWrite<InstanceID>();
		transform = reader.ReadIReadWrite<TransformData>();
		displayName = reader.ReadString();
		scriptName = reader.ReadString();
		targetPriority = (RoomEditorTargetPriotity)reader.ReadInt32();
		isExpandedInHierarchy = reader.ReadBoolean();
		isObstacle = reader.ReadBoolean();
		activator = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ActivatorComponentData>());
		clouds = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<CloudsData>());
		customModel = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<CustomModelData>());
		delay = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<DelayData>());
		dial = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<DialData>());
		display = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<EditorDisplayData>());
		draggable = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<DraggableData>());
		finish = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<FinishData>());
		floor = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<FloorData>());
		fog = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<FogData>());
		item = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ItemData>());
		itemRespawner = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ItemRespawnerData>());
		ladder = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LadderData>());
		light = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<EditorLightData>());
		@lock = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LockData>());
		lookable = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LookableData>());
		materials = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<SwapMaterialData>());
		materialSwaps = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<MaterialSwapData>());
		ocean = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<OceanData>());
		openLink = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<OpenLinkData>());
		postProcessing = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<EditorPostProcessingData>());
		puzzle = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<PuzzleData>());
		rotatable = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<RotatableData>());
		roulette = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<RouletteData>());
		script = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ScriptComponentData>());
		setup = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<EditorSetupData>());
		skybox = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<SkyboxData>());
		slidable = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<SlidableData>());
		slot = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<SlotData>());
		sound = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<SoundData>());
		spawnPoint = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<SpawnPointData>());
		stairs = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<StairsData>());
		switch3D = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<Switch3DData>());
		teleport = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TeleportData>());
		test = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TestData>());
		text = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TextData>());
		token = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TokenData>());
		trigger = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TriggerData>());
		turnable = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TurnableData>());
		tweenState = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TweenStateDataRoomEditor>());
		wall = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<WallData>());
		water = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<WaterData>());
		zoomable = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ZoomableData>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("propID: " + ToStringHelper.Stringify(propID));
		stringBuilder.AppendLine("ID: " + ToStringHelper.Stringify(ID));
		stringBuilder.AppendLine("parentID: " + ToStringHelper.Stringify(parentID));
		stringBuilder.AppendLine("transform: " + ToStringHelper.Stringify(transform));
		stringBuilder.AppendLine("displayName: " + ToStringHelper.Stringify(displayName));
		stringBuilder.AppendLine("scriptName: " + ToStringHelper.Stringify(scriptName));
		stringBuilder.AppendLine("targetPriority: " + $"{targetPriority}");
		stringBuilder.AppendLine("isExpandedInHierarchy: " + $"{isExpandedInHierarchy}");
		stringBuilder.AppendLine("isObstacle: " + $"{isObstacle}");
		stringBuilder.AppendLine("activator: " + ToStringHelper.Stringify(activator, (ActivatorComponentData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("clouds: " + ToStringHelper.Stringify(clouds, (CloudsData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("customModel: " + ToStringHelper.Stringify(customModel, (CustomModelData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("delay: " + ToStringHelper.Stringify(delay, (DelayData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("dial: " + ToStringHelper.Stringify(dial, (DialData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("display: " + ToStringHelper.Stringify(display, (EditorDisplayData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("draggable: " + ToStringHelper.Stringify(draggable, (DraggableData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("finish: " + ToStringHelper.Stringify(finish, (FinishData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("floor: " + ToStringHelper.Stringify(floor, (FloorData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("fog: " + ToStringHelper.Stringify(fog, (FogData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("item: " + ToStringHelper.Stringify(item, (ItemData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("itemRespawner: " + ToStringHelper.Stringify(itemRespawner, (ItemRespawnerData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("ladder: " + ToStringHelper.Stringify(ladder, (LadderData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("light: " + ToStringHelper.Stringify(light, (EditorLightData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("lock: " + ToStringHelper.Stringify(@lock, (LockData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("lookable: " + ToStringHelper.Stringify(lookable, (LookableData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("materials: " + ToStringHelper.Stringify(materials, (SwapMaterialData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("materialSwaps: " + ToStringHelper.Stringify(materialSwaps, (MaterialSwapData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("ocean: " + ToStringHelper.Stringify(ocean, (OceanData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("openLink: " + ToStringHelper.Stringify(openLink, (OpenLinkData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("postProcessing: " + ToStringHelper.Stringify(postProcessing, (EditorPostProcessingData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("puzzle: " + ToStringHelper.Stringify(puzzle, (PuzzleData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("rotatable: " + ToStringHelper.Stringify(rotatable, (RotatableData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("roulette: " + ToStringHelper.Stringify(roulette, (RouletteData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("script: " + ToStringHelper.Stringify(script, (ScriptComponentData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("setup: " + ToStringHelper.Stringify(setup, (EditorSetupData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("skybox: " + ToStringHelper.Stringify(skybox, (SkyboxData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("slidable: " + ToStringHelper.Stringify(slidable, (SlidableData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("slot: " + ToStringHelper.Stringify(slot, (SlotData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("sound: " + ToStringHelper.Stringify(sound, (SoundData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("spawnPoint: " + ToStringHelper.Stringify(spawnPoint, (SpawnPointData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("stairs: " + ToStringHelper.Stringify(stairs, (StairsData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("switch3D: " + ToStringHelper.Stringify(switch3D, (Switch3DData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("teleport: " + ToStringHelper.Stringify(teleport, (TeleportData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("test: " + ToStringHelper.Stringify(test, (TestData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("text: " + ToStringHelper.Stringify(text, (TextData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("token: " + ToStringHelper.Stringify(token, (TokenData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("trigger: " + ToStringHelper.Stringify(trigger, (TriggerData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("turnable: " + ToStringHelper.Stringify(turnable, (TurnableData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("tweenState: " + ToStringHelper.Stringify(tweenState, (TweenStateDataRoomEditor e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("wall: " + ToStringHelper.Stringify(wall, (WallData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("water: " + ToStringHelper.Stringify(water, (WaterData e) => ToStringHelper.Stringify(e)));
		stringBuilder.Append("zoomable: " + ToStringHelper.Stringify(zoomable, (ZoomableData e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
