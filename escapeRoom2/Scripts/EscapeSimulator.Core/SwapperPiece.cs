using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapperPiece : Interactive
{
	[Header("SwapperPiece")]
	[DontSave]
	public Item linkedItem;

	[DontSave]
	public Vector3 pivot;

	[DontSave]
	public int pieceGenericType = -1;

	[DontSave]
	public TweenState tweenState;

	[DontSave]
	public MaterialState materialState;

	[DontSave]
	public float hoverSpeed = 1f;

	[NonSerialized]
	[DontSave]
	public Swapper swapper;

	[NonSerialized]
	[DontSave]
	public Vector3 startingLocalPosition;

	[NonSerialized]
	public SwapperPiece swappedPiece;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.SwapperPiece;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Pickup;
	}

	public void initPiece(Swapper swapper)
	{
		startingLocalPosition = base.transform.localPosition;
		this.swapper = swapper;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteComponent(swappedPiece);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		swappedPiece = reader.ReadComponent<SwapperPiece>();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		SwapperPiece arg = reader.ReadComponent<SwapperPiece>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "swappedPiece",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
