using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Breakable : MonoBehaviour, ISaveable
{
	public class Pair
	{
		public Breakable breakable;

		public Rigidbody rigidbody;

		public Vector3 relativeVelocity;
	}

	[DontSave]
	public GameObject originalPiece;

	[DontSave]
	public List<GameObject> additionalOriginalRenderers;

	[DontSave]
	public List<GameObject> pieces;

	[DontSave]
	public Lock lockBreak;

	[DontSave]
	public float breakingImpulse = 3f;

	[DontSave]
	public float health = -1f;

	[NonSerialized]
	public bool isUsed;

	[NonSerialized]
	public float damage;

	[DontSave]
	private Game localGame;

	[DontSave]
	[HideInInspector]
	public EventReference soundBreak;

	public bool isBrokenIntoPieces => !wholePiece.activeSelf;

	public GameObject wholePiece
	{
		get
		{
			if (!(originalPiece != null))
			{
				return base.gameObject;
			}
			return originalPiece;
		}
	}

	public void init(Game game)
	{
		localGame = game;
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (localGame != null && !base.transform.IsChildOf(collision.transform) && !collision.transform.IsChildOf(base.transform))
		{
			localGame.breakablesEnterLastFrame.Add(new Pair
			{
				breakable = this,
				rigidbody = collision.rigidbody,
				relativeVelocity = collision.relativeVelocity
			});
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in isUsed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in damage, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		isUsed = reader.ReadBoolean();
		damage = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isUsed",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "damage",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
