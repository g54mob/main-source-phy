using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SlidableGraphPiece : Interactive
{
	[NonSerialized]
	[Tooltip("Reference to the graph that this piece is a part of.")]
	[DontSave]
	public SlidableGraph graph;

	[Tooltip("If true, this piece will collide with other pieces that also use collision.")]
	public bool useCollision = true;

	[Tooltip("Amount by which default collider size (calculated by renderer bounds) is scaled. If renderer does not exist, this value is used as collider size.")]
	[Min(0f)]
	public float colliderScale = 1f;

	[NonSerialized]
	internal bool isBackgroundUpdatePiece;

	[NonSerialized]
	internal bool wasReleasedInNode;

	[NonSerialized]
	internal float currentSnapStartPercent;

	[NonSerialized]
	internal short currentSnapStartNodeIndex;

	[NonSerialized]
	internal short currentSnapEndNodeIndex;

	[NonSerialized]
	[DontSave]
	public GameObject initialEdgeStartNode;

	[NonSerialized]
	[DontSave]
	public GameObject initialEdgeEndNode;

	[NonSerialized]
	[DontSave]
	public float initialEdgePercent;

	[NonSerialized]
	public GameObject currentEdgeStartNode;

	[NonSerialized]
	public GameObject currentEdgeEndNode;

	[NonSerialized]
	public float currentEdgePercent;

	[NonSerialized]
	[DontSave]
	internal Transform anchor;

	[NonSerialized]
	[DontSave]
	internal Vector3 initialLocalHitPosition;

	[NonSerialized]
	[DontSave]
	internal Vector3 initialPosition;

	[NonSerialized]
	[DontSave]
	internal Vector3 initialAnchorWorldPoint;

	[NonSerialized]
	[DontSave]
	internal Vector3 anchorSpaceDragPoint;

	public Vector3 currentLinearPosition => Vector3.Lerp(currentEdgeStartNode.transform.position, currentEdgeEndNode.transform.position, currentEdgePercent);

	public float colliderRadius
	{
		get
		{
			if (!base.gameObject.TryGetComponent<Renderer>(out var component))
			{
				return colliderScale;
			}
			Vector3 vector = Vector3.Scale(component.localBounds.extents, component.transform.lossyScale);
			return Mathf.Max(vector.x, Mathf.Max(vector.y, vector.z)) * colliderScale;
		}
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.SlidableGraphPiece;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Draggable;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in useCollision, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in colliderScale, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isBackgroundUpdatePiece, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wasReleasedInNode, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentSnapStartPercent, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentSnapStartNodeIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentSnapEndNodeIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteGameObject(currentEdgeStartNode);
		writer.WriteGameObject(currentEdgeEndNode);
		writer.Write(in currentEdgePercent, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		useCollision = reader.ReadBoolean();
		colliderScale = reader.ReadSingle();
		isBackgroundUpdatePiece = reader.ReadBoolean();
		wasReleasedInNode = reader.ReadBoolean();
		currentSnapStartPercent = reader.ReadSingle();
		currentSnapStartNodeIndex = reader.ReadInt16();
		currentSnapEndNodeIndex = reader.ReadInt16();
		currentEdgeStartNode = reader.ReadGameObject();
		currentEdgeEndNode = reader.ReadGameObject();
		currentEdgePercent = reader.ReadSingle();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "useCollision",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "colliderScale",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isBackgroundUpdatePiece",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasReleasedInNode",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSnapStartPercent",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		short num3 = reader.ReadInt16();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSnapStartNodeIndex",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		short num4 = reader.ReadInt16();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSnapEndNodeIndex",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentEdgeStartNode",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg2 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentEdgeEndNode",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentEdgePercent",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
