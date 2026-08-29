using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Swapper : Interactive
{
	public class SwapperEvent
	{
		public SwapperPiece first;

		public SwapperPiece second;

		public bool isSolved;

		public bool isReset;

		public SwapperPiece getCurrentSelected
		{
			get
			{
				if (!(first != null))
				{
					return second;
				}
				return first;
			}
		}

		public bool didSwap => second != null;

		public SwapperEvent(SwapperPiece first, SwapperPiece second, bool isSolved, bool isReset)
		{
			this.first = first;
			this.second = second;
			this.isSolved = isSolved;
			this.isReset = isReset;
		}
	}

	[Header("Swapper")]
	[DontSave]
	public Vector3 localNormal = Vector3.up;

	[DontSave]
	public float pieceLift = 0.01f;

	[DontSave]
	public SwapperPiece[] startingPieces = new SwapperPiece[0];

	[NonSerialized]
	[DontSave]
	public List<Item> applicableItems = new List<Item>(16);

	[NonSerialized]
	public Vector3 swapperPieceOriginalPosition = Vector3.zero;

	[NonSerialized]
	public SwapperPiece firstPiece;

	[DontSave]
	[HideInInspector]
	public EventReference soundStart;

	[DontSave]
	[HideInInspector]
	public EventReference soundSwap;

	[DontSave]
	[HideInInspector]
	public EventReference soundCancel;

	public override ItemCheck itemCheck(Item item)
	{
		if (!(item == null))
		{
			if (!applicableItems.Contains(item))
			{
				return ItemCheck.Fails;
			}
			return ItemCheck.Passes;
		}
		return ItemCheck.Fails;
	}

	public override void init()
	{
		SwapperPiece[] componentsInChildren = GetComponentsInChildren<SwapperPiece>(includeInactive: true);
		SwapperPiece[] array = componentsInChildren;
		foreach (SwapperPiece swapperPiece in array)
		{
			swapperPiece.initPiece(this);
			if (swapperPiece.linkedItem != null)
			{
				applicableItems.Add(swapperPiece.linkedItem);
			}
			linkedInteractives.Add(swapperPiece);
		}
		if (startingPieces.Length != componentsInChildren.Length)
		{
			return;
		}
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].swappedPiece = startingPieces[j];
			if (componentsInChildren[j].swappedPiece != null)
			{
				componentsInChildren[j].transform.localPosition = componentsInChildren[j].swappedPiece.startingLocalPosition;
			}
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteVector3(in swapperPieceOriginalPosition);
		writer.WriteComponent(firstPiece);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		swapperPieceOriginalPosition = reader.ReadVector3();
		firstPiece = reader.ReadComponent<SwapperPiece>();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "swapperPieceOriginalPosition",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		SwapperPiece arg = reader.ReadComponent<SwapperPiece>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstPiece",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
