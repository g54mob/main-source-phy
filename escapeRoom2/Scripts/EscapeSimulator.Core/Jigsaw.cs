using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Jigsaw : Interactive
{
	[Serializable]
	public class JigsawConnectable
	{
		public JigsawPiece piece1;

		public JigsawPiece piece2;
	}

	public const float jigsawOffset = 0.0005f;

	[Header("Jigsaw")]
	[DontSave]
	public float pieceRotationSpeed = 1f;

	[DontSave]
	public Vector3 localNormal = Vector3.up;

	[DontSave]
	public Vector3 localUpPiece;

	[DontSave]
	public float clearancePosition = 0.01f;

	[DontSave]
	public float clearanceRotation = 5f;

	[DontSave]
	public float pieceLift = 0.01f;

	[DontSave]
	public bool allowRotation = true;

	[DontSave]
	public bool allowSnap = true;

	[DontSave]
	public bool allowConnectingPieces;

	[DontSave]
	public float translateRadius = 0.025f;

	[DontSave]
	public Vector3 snapPosition;

	[DontSave]
	public float rotationSnaps = 8f;

	[NonSerialized]
	[DontSave]
	public List<Item> applicableItems = new List<Item>(16);

	[DontSave]
	public JigsawPiece[] pieces;

	[DontSave]
	public JigsawPiece deferedPiece;

	[Header("Area Limits")]
	[DontSave]
	public Transform areaLimitsPlaneOrigin;

	[DontSave]
	public Vector2 areaLimits;

	[DontSave]
	public JigsawConnectable[] connectablePieces;

	[DontSave]
	[HideInInspector]
	public EventReference soundStart;

	[DontSave]
	[HideInInspector]
	public EventReference soundMoveLoop;

	[DontSave]
	[HideInInspector]
	public EventReference soundEnd;

	[NonSerialized]
	public JigsawPiece selectedPiece;

	[NonSerialized]
	public Vector3 jigsawPieceOriginalPosition = Vector3.zero;

	[NonSerialized]
	public Vector3 jigsawPieceRotationOffset = Vector3.zero;

	[NonSerialized]
	[DontSave]
	public Plane plane;

	public Vector3 worldNormal => base.transform.TransformDirection(localNormal).normalized;

	public Vector3 worldUp => Vector3.Cross(worldNormal, Vector3.Cross(Vector3.up, worldNormal).normalized).normalized;

	protected override bool doesBackgroundUpdateForbidInteraction => false;

	public bool areAllPiecesSnapped()
	{
		JigsawPiece[] array = pieces;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].isSnapped)
			{
				return false;
			}
		}
		return true;
	}

	public bool arePiecesConnected(JigsawPiece piece1, JigsawPiece piece2)
	{
		if (piece1 == null || piece2 == null || piece1 == piece2)
		{
			return false;
		}
		Vector3 position = piece1.transform.position;
		Quaternion rotation = piece1.transform.rotation;
		piece1.transform.position = piece1.originalPosition;
		piece1.transform.rotation = piece1.originalRotation;
		Vector3 vector = piece1.transform.InverseTransformPoint(piece2.originalPosition);
		piece1.transform.position = position;
		piece1.transform.rotation = rotation;
		Vector3 vector2 = piece1.transform.InverseTransformPoint(piece2.transform.position);
		if (!((vector - vector2).sqrMagnitude < clearancePosition * clearancePosition))
		{
			return false;
		}
		Quaternion a = Quaternion.Inverse(piece1.originalRotation) * piece2.originalRotation;
		Quaternion b = Quaternion.Inverse(piece1.transform.rotation) * piece2.transform.rotation;
		if (!(Quaternion.Angle(a, b) < clearanceRotation))
		{
			return false;
		}
		return true;
	}

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

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Jigsaw;
	}

	public override void init()
	{
		base.transform.position += base.transform.TransformDirection(localNormal * 0.0005f);
		pieces = GetComponentsInChildren<JigsawPiece>(includeInactive: true);
		JigsawPiece[] array = pieces;
		foreach (JigsawPiece jigsawPiece in array)
		{
			jigsawPiece.initPiece(this);
			if (jigsawPiece.linkedItem != null)
			{
				applicableItems.Add(jigsawPiece.linkedItem);
			}
			linkedInteractives.Add(jigsawPiece);
		}
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundMoveLoop);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteComponent(selectedPiece);
		writer.WriteVector3(in jigsawPieceOriginalPosition);
		writer.WriteVector3(in jigsawPieceRotationOffset);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		selectedPiece = reader.ReadComponent<JigsawPiece>();
		jigsawPieceOriginalPosition = reader.ReadVector3();
		jigsawPieceRotationOffset = reader.ReadVector3();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		JigsawPiece arg = reader.ReadComponent<JigsawPiece>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "selectedPiece",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "jigsawPieceOriginalPosition",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "jigsawPieceRotationOffset",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
