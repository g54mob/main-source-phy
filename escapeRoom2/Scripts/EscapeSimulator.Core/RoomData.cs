using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class RoomData : IReadWrite
{
	public string name;

	public string description;

	public int pointVersion;

	public List<string> tags;

	public List<string> walkthrough;

	public Vector3 cameraScreenshotPosition;

	public Vector3 cameraScreenshotRotation;

	public Vector3 cameraEditPosition;

	public Vector3 cameraEditRotation;

	public bool useNonLegacyLights;

	public bool useNonLegacyFloorColliders;

	public bool hidePlayerNameplates;

	public bool hideItemNameplates;

	public bool useProximityChat;

	public bool useSkyboxPreview;

	public bool usePostProcessingPreview;

	public bool useWaterPreview;

	public bool camToLinkedProp;

	public List<PropData> props;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(name);
		writer.Write(description);
		writer.Write(in pointVersion, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(tags, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.WriteList(walkthrough, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.WriteVector3(in cameraScreenshotPosition);
		writer.WriteVector3(in cameraScreenshotRotation);
		writer.WriteVector3(in cameraEditPosition);
		writer.WriteVector3(in cameraEditRotation);
		writer.Write(in useNonLegacyLights, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useNonLegacyFloorColliders, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hidePlayerNameplates, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hideItemNameplates, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useProximityChat, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useSkyboxPreview, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in usePostProcessingPreview, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useWaterPreview, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in camToLinkedProp, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(props, delegate(FastBinaryWriter w, PropData e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		name = reader.ReadString();
		description = reader.ReadString();
		pointVersion = reader.ReadInt32();
		tags = reader.ReadList((FastBinaryReader r) => r.ReadString());
		walkthrough = reader.ReadList((FastBinaryReader r) => r.ReadString());
		cameraScreenshotPosition = reader.ReadVector3();
		cameraScreenshotRotation = reader.ReadVector3();
		cameraEditPosition = reader.ReadVector3();
		cameraEditRotation = reader.ReadVector3();
		useNonLegacyLights = reader.ReadBoolean();
		useNonLegacyFloorColliders = reader.ReadBoolean();
		hidePlayerNameplates = reader.ReadBoolean();
		hideItemNameplates = reader.ReadBoolean();
		useProximityChat = reader.ReadBoolean();
		useSkyboxPreview = reader.ReadBoolean();
		usePostProcessingPreview = reader.ReadBoolean();
		useWaterPreview = reader.ReadBoolean();
		camToLinkedProp = reader.ReadBoolean();
		props = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<PropData>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("name: " + ToStringHelper.Stringify(name));
		stringBuilder.AppendLine("description: " + ToStringHelper.Stringify(description));
		stringBuilder.AppendLine("pointVersion: " + $"{pointVersion}");
		stringBuilder.AppendLine("tags: " + ToStringHelper.Stringify(tags, (string e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("walkthrough: " + ToStringHelper.Stringify(walkthrough, (string e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("cameraScreenshotPosition: " + $"{cameraScreenshotPosition}");
		stringBuilder.AppendLine("cameraScreenshotRotation: " + $"{cameraScreenshotRotation}");
		stringBuilder.AppendLine("cameraEditPosition: " + $"{cameraEditPosition}");
		stringBuilder.AppendLine("cameraEditRotation: " + $"{cameraEditRotation}");
		stringBuilder.AppendLine("useNonLegacyLights: " + $"{useNonLegacyLights}");
		stringBuilder.AppendLine("useNonLegacyFloorColliders: " + $"{useNonLegacyFloorColliders}");
		stringBuilder.AppendLine("hidePlayerNameplates: " + $"{hidePlayerNameplates}");
		stringBuilder.AppendLine("hideItemNameplates: " + $"{hideItemNameplates}");
		stringBuilder.AppendLine("useProximityChat: " + $"{useProximityChat}");
		stringBuilder.AppendLine("useSkyboxPreview: " + $"{useSkyboxPreview}");
		stringBuilder.AppendLine("usePostProcessingPreview: " + $"{usePostProcessingPreview}");
		stringBuilder.AppendLine("useWaterPreview: " + $"{useWaterPreview}");
		stringBuilder.AppendLine("camToLinkedProp: " + $"{camToLinkedProp}");
		stringBuilder.Append("props: " + ToStringHelper.Stringify(props, (PropData e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
