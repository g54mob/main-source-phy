using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveFilePlayer : IReadWrite
{
	public NetPlayerId id;

	public Vector3 position;

	public Vector2 rotation;

	public bool isCrouching;

	public List<GameObject> inventory;

	public HashSet<GameObject> interactingObjects;

	public CustomMode customMode;

	public int spawnPointIndex;

	public CharacterPoseContext characterPoseContext;

	public Game.RoomEditorPostProcessingData postProcessingData;

	public Game.RoomEditorSkyData skyData;

	public Game.RoomEditorFogData fogData;

	public Game.RoomEditorCloudsData cloudData;

	public Game.RoomEditorOceanData oceanData;

	public float speedRunning;

	public float speedWalking;

	public Game.LadderMovementRuntimeData ladderData;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteNetPlayerId(id);
		writer.WriteVector3(in position);
		writer.WriteVector2(in rotation);
		writer.Write(in isCrouching, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(inventory, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteHashSet(interactingObjects, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteComponent(customMode);
		writer.Write(in spawnPointIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(characterPoseContext);
		writer.WriteIReadWrite(postProcessingData);
		writer.WriteIReadWrite(skyData);
		writer.WriteIReadWrite(fogData);
		writer.WriteIReadWrite(cloudData);
		writer.WriteIReadWrite(oceanData);
		writer.Write(in speedRunning, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in speedWalking, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(ladderData);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		id = reader.ReadNetPlayerId();
		position = reader.ReadVector3();
		rotation = reader.ReadVector2();
		isCrouching = reader.ReadBoolean();
		inventory = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		interactingObjects = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		customMode = reader.ReadComponent<CustomMode>();
		spawnPointIndex = reader.ReadInt32();
		characterPoseContext = reader.ReadIReadWrite<CharacterPoseContext>();
		postProcessingData = reader.ReadIReadWrite<Game.RoomEditorPostProcessingData>();
		skyData = reader.ReadIReadWrite<Game.RoomEditorSkyData>();
		fogData = reader.ReadIReadWrite<Game.RoomEditorFogData>();
		cloudData = reader.ReadIReadWrite<Game.RoomEditorCloudsData>();
		oceanData = reader.ReadIReadWrite<Game.RoomEditorOceanData>();
		speedRunning = reader.ReadSingle();
		speedWalking = reader.ReadSingle();
		ladderData = reader.ReadIReadWrite<Game.LadderMovementRuntimeData>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("id: " + $"{id}");
		stringBuilder.AppendLine("position: " + $"{position}");
		stringBuilder.AppendLine("rotation: " + $"{rotation}");
		stringBuilder.AppendLine("isCrouching: " + $"{isCrouching}");
		stringBuilder.AppendLine("inventory: " + ToStringHelper.Stringify(inventory, (GameObject e) => $"{e}"));
		stringBuilder.AppendLine("interactingObjects: " + ToStringHelper.Stringify(interactingObjects, (GameObject e) => $"{e}"));
		stringBuilder.AppendLine("customMode: " + $"{customMode}");
		stringBuilder.AppendLine("spawnPointIndex: " + $"{spawnPointIndex}");
		stringBuilder.AppendLine("characterPoseContext: " + ToStringHelper.Stringify(characterPoseContext));
		stringBuilder.AppendLine("postProcessingData: " + ToStringHelper.Stringify(postProcessingData));
		stringBuilder.AppendLine("skyData: " + ToStringHelper.Stringify(skyData));
		stringBuilder.AppendLine("fogData: " + ToStringHelper.Stringify(fogData));
		stringBuilder.AppendLine("cloudData: " + ToStringHelper.Stringify(cloudData));
		stringBuilder.AppendLine("oceanData: " + ToStringHelper.Stringify(oceanData));
		stringBuilder.AppendLine("speedRunning: " + $"{speedRunning}");
		stringBuilder.AppendLine("speedWalking: " + $"{speedWalking}");
		stringBuilder.Append("ladderData: " + ToStringHelper.Stringify(ladderData));
		return stringBuilder.ToString();
	}
}
