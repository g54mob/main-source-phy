using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[AddComponentMenu("Pine/SurfaceSlot")]
public class SurfaceSlot : Interactive
{
	public enum SurfaceSlotState
	{
		Idle = 0,
		PlacedWaiting = 1,
		AnimatingToInventory = 2,
		Animating = 3
	}

	[Serializable]
	public struct InterestPoint : ISerializationCallbackReceiver
	{
		[Tooltip("Position of the interest point, in local space.")]
		public Vector3 localPosition;

		[Tooltip("If item is placed within this radius, event will be raised.")]
		public float radius;

		[SerializeField]
		[HideInInspector]
		private bool isSerialized;

		public void OnBeforeSerialize()
		{
			if (!isSerialized)
			{
				radius = 0.1f;
				isSerialized = true;
			}
		}

		public void OnAfterDeserialize()
		{
		}
	}

	public class Placement
	{
		public readonly SurfaceSlot surfaceSlot;

		public readonly Item placedObject;

		public readonly int interestPointIndex;

		public Placement(SurfaceSlot surfaceSlot, Item placedObject, int interestPointIndex)
		{
			this.surfaceSlot = surfaceSlot;
			this.placedObject = placedObject;
			this.interestPointIndex = interestPointIndex;
		}

		public Placement()
		{
		}
	}

	[Tooltip("Defines which objects can be placed on this surface.")]
	[DontSave]
	public List<GameObject> placableObjects;

	[Tooltip("Defines points which trigger an event when item is placed near them within specified radius.")]
	[DontSave]
	public List<InterestPoint> interestPoints;

	[Tooltip("Defines offset for object placed on the surface.")]
	[DontSave]
	public Vector3 placementOffset;

	[Tooltip("Defines if object should be returned to inventory after placement.")]
	[DontSave]
	public bool ejectToInventory;

	[Tooltip("Defines if object should snap to the position of the nearest interest point.")]
	[DontSave]
	public bool snapToInterestPoint;

	[Tooltip("Defines if placeable objects will be animated when placed.")]
	[DontSave]
	public bool isAnimated;

	[NonSerialized]
	public SurfaceSlotState state;

	[NonSerialized]
	public Placement currentPlacement;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.SurfaceSlot;
	}

	public override ItemCheck itemCheck(Item item)
	{
		if (!(item != null) || !placableObjects.Contains(item.gameObject))
		{
			return ItemCheck.Fails;
		}
		return ItemCheck.Passes;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		int value = (int)state;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WritePlacement(currentPlacement);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		state = (SurfaceSlotState)reader.ReadInt32();
		currentPlacement = reader.ReadPlacement();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		SurfaceSlotState surfaceSlotState = (SurfaceSlotState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "state",
			fieldValue = $"{surfaceSlotState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Placement arg = reader.ReadPlacement();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentPlacement",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
