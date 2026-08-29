using System;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPiece : Interactive
{
	[Header("JigsawPiece")]
	[DontSave]
	public Item linkedItem;

	[DontSave]
	public Vector3 pivot;

	[DontSave]
	public Vector3 sharedPivot;

	[DontSave]
	public Vector3 startingLocalPosition;

	[DontSave]
	public Quaternion startingLocalRotation;

	[NonSerialized]
	[DontSave]
	public Vector3 originalPosition;

	[NonSerialized]
	[DontSave]
	public Quaternion originalRotation;

	[NonSerialized]
	[DontSave]
	public Jigsaw jigsaw;

	[NonSerialized]
	public float remainingRotation;

	public List<JigsawPiece> connectedPieces;

	public bool isSnapped
	{
		get
		{
			if (base.transform.position == originalPosition)
			{
				return base.transform.rotation == originalRotation;
			}
			return false;
		}
	}

	protected override bool doesBackgroundUpdateForbidInteraction => false;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.JigsawPiece;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Draggable;
	}

	public void initPiece(Jigsaw jigsaw)
	{
		this.jigsaw = jigsaw;
		connectedPieces.Add(this);
		Vector3 vector = jigsaw.transform.TransformDirection(jigsaw.localNormal * 0.0005f);
		originalPosition = base.transform.position - vector;
		originalRotation = base.transform.rotation;
		base.transform.localPosition = startingLocalPosition;
		base.transform.localRotation = startingLocalRotation;
		recalculateSharedPivot();
	}

	internal void connectTo(JigsawPiece parentPiece)
	{
		Vector3 vector = originalPosition - parentPiece.originalPosition;
		Quaternion quaternion = Quaternion.Inverse(parentPiece.originalRotation) * originalRotation;
		base.transform.position = parentPiece.transform.position + parentPiece.transform.rotation * vector;
		base.transform.rotation = parentPiece.transform.rotation * quaternion;
	}

	public void recalculateSharedPivot()
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		foreach (JigsawPiece connectedPiece in connectedPieces)
		{
			zero2 += connectedPiece.transform.TransformPoint(connectedPiece.pivot);
		}
		Vector3 position = zero2 / connectedPieces.Count;
		zero = base.transform.InverseTransformPoint(position);
		sharedPivot = zero;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in remainingRotation, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(connectedPieces, delegate(FastBinaryWriter w, JigsawPiece e)
		{
			w.WriteComponent(e);
		});
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		remainingRotation = reader.ReadSingle();
		connectedPieces = reader.ReadList((FastBinaryReader r) => r.ReadComponent<JigsawPiece>());
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "remainingRotation",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<JigsawPiece> list = reader.ReadList((FastBinaryReader r) => r.ReadComponent<JigsawPiece>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "connectedPieces[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
