using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class SlotData : IReadWrite
{
	public SlotPlaceBehaviour unlockSuccessKey;

	public SlotPlaceBehaviour unlockFailedKey;

	public List<InstanceID> keys;

	public List<InstanceID> rejectKeys;

	public SlotAnimationType animationType;

	public InstanceID initialItemID;

	public float turnDuration;

	public float turnCount;

	public Vector3 turnAxis;

	public Vector3 ejectDirection;

	public TransformData pivot;

	public bool onlyUsableInZoom;

	public RoomEditorLinksData onPlace;

	public RoomEditorLinksData onRemove;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)unlockSuccessKey;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)unlockFailedKey;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(keys, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(rejectKeys, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		value = (int)animationType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(initialItemID);
		writer.Write(in turnDuration, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in turnCount, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in turnAxis);
		writer.WriteVector3(in ejectDirection);
		writer.WriteIReadWrite(pivot);
		writer.Write(in onlyUsableInZoom, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(onPlace);
		writer.WriteIReadWrite(onRemove);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		unlockSuccessKey = (SlotPlaceBehaviour)reader.ReadInt32();
		unlockFailedKey = (SlotPlaceBehaviour)reader.ReadInt32();
		keys = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		rejectKeys = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		animationType = (SlotAnimationType)reader.ReadInt32();
		initialItemID = reader.ReadIReadWrite<InstanceID>();
		turnDuration = reader.ReadSingle();
		turnCount = reader.ReadSingle();
		turnAxis = reader.ReadVector3();
		ejectDirection = reader.ReadVector3();
		pivot = reader.ReadIReadWrite<TransformData>();
		onlyUsableInZoom = reader.ReadBoolean();
		onPlace = reader.ReadIReadWrite<RoomEditorLinksData>();
		onRemove = reader.ReadIReadWrite<RoomEditorLinksData>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("unlockSuccessKey: " + $"{unlockSuccessKey}");
		stringBuilder.AppendLine("unlockFailedKey: " + $"{unlockFailedKey}");
		stringBuilder.AppendLine("keys: " + ToStringHelper.Stringify(keys, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("rejectKeys: " + ToStringHelper.Stringify(rejectKeys, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("animationType: " + $"{animationType}");
		stringBuilder.AppendLine("initialItemID: " + ToStringHelper.Stringify(initialItemID));
		stringBuilder.AppendLine("turnDuration: " + $"{turnDuration}");
		stringBuilder.AppendLine("turnCount: " + $"{turnCount}");
		stringBuilder.AppendLine("turnAxis: " + $"{turnAxis}");
		stringBuilder.AppendLine("ejectDirection: " + $"{ejectDirection}");
		stringBuilder.AppendLine("pivot: " + ToStringHelper.Stringify(pivot));
		stringBuilder.AppendLine("onlyUsableInZoom: " + $"{onlyUsableInZoom}");
		stringBuilder.AppendLine("onPlace: " + ToStringHelper.Stringify(onPlace));
		stringBuilder.Append("onRemove: " + ToStringHelper.Stringify(onRemove));
		return stringBuilder.ToString();
	}
}
