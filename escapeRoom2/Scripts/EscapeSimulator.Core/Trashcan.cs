using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactive
{
	public class Pair
	{
		public GameObject trigger;

		public GameObject enter;
	}

	[DontSave]
	public Transform poofPosition;

	[DontSave]
	public Sequence trashSequence;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Trashcan;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Trashcan;
	}

	public override ItemCheck itemCheck(Item item)
	{
		if (!(item == null))
		{
			return ItemCheck.Passes;
		}
		return ItemCheck.NotRequired;
	}

	private void OnTriggerEnter(Collider other)
	{
		GameObject gameObject = ((other.attachedRigidbody != null) ? other.attachedRigidbody.gameObject : other.gameObject);
		game.trashcanEnterLastFrame.Add(new Pair
		{
			trigger = base.gameObject,
			enter = gameObject
		});
		if (game.isVR())
		{
			game.vr.vibrateController(game.getControllerIdForItem(gameObject), 0.1f, 0.1f);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (game == null)
		{
			return;
		}
		GameObject enterGameObject = ((other.attachedRigidbody != null) ? other.attachedRigidbody.gameObject : other.gameObject);
		if (game.isInAnyVRPlayerHand(enterGameObject))
		{
			Pair pair = game.trashcanEnterLastFrame.Find((Pair pair2) => pair2.trigger == base.gameObject && pair2.enter == enterGameObject);
			if (pair != null)
			{
				game.trashcanEnterLastFrame.Remove(pair);
			}
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
	}
}
