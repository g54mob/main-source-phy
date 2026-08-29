using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[ExecuteInEditMode]
public class Slot : Interactive
{
	[Header("Configuration")]
	[Space(5f)]
	public Item[] acceptItems = Array.Empty<Item>();

	public Item[] rejectItems = Array.Empty<Item>();

	[DontSave]
	public SlotPlaceBehaviour onAcceptItemPlaced;

	[DontSave]
	public SlotPlaceBehaviour onRejectItemPlaced = SlotPlaceBehaviour.ItemEjectsToInventory;

	[DontSave]
	public bool disableSlotOnUnlock;

	[DontSave]
	public bool canSwap;

	[Header("State")]
	[DontSave]
	public Item initialInsertedItem;

	[ReadOnly]
	public Item insertedItem;

	[ReadOnly]
	public bool isUnlocked;

	[ReadOnly]
	[DontSave]
	public Transform pivot;

	[NonSerialized]
	[DontSave]
	public RectTransform hotspotIndicator;

	[Header("Animations")]
	[Space(5f)]
	[DontSave]
	public SlotAnimationType animationType = SlotAnimationType.None;

	[DontSave]
	public float rotateKeyTurnDuration = 0.5f;

	[DontSave]
	public float rotateKeyTurnCount = 1f;

	[DontSave]
	public Vector3 rotateKeyTurnAxis;

	[DontSave]
	public Vector3 ejectDirection = Vector3.up;

	[DontSave]
	public float ejectDuration = 0.15f;

	[DontSave]
	[HideInInspector]
	public EventReference soundOnAcceptItemPlaced;

	[DontSave]
	[HideInInspector]
	public EventReference soundOnRejectItemPlaced;

	[DontSave]
	[HideInInspector]
	public EventReference soundOnRemove;

	[Header("Room Editor")]
	[HideInInspector]
	[DontSave]
	public RoomEditorLinks onPlace = new RoomEditorLinks();

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks onRemove = new RoomEditorLinks();

	internal const float HAND_TO_SLOT_ANIMATION_DURATION = 0.25f;

	[NonSerialized]
	internal SlotState currentSlotState;

	[NonSerialized]
	internal Item itemForCurrentInteraction;

	[NonSerialized]
	internal Vector3 itemTravellingStartPosition;

	[NonSerialized]
	internal Vector3 originalItemInZoomScale;

	[NonSerialized]
	internal Vector3 originalZoomScale;

	[NonSerialized]
	internal Quaternion itemTravellingStartRotation;

	[NonSerialized]
	internal bool isSilentPlacement;

	[NonSerialized]
	[DontSave]
	internal readonly HashSet<GameObject> triggerEnterLastFrame = new HashSet<GameObject>(16);

	public override ItemCheck itemCheck(Item item)
	{
		if (item != null && (insertedItem == null || canSwap) && (Array.IndexOf(acceptItems, item) != -1 || Array.IndexOf(rejectItems, item) != -1))
		{
			return ItemCheck.Passes;
		}
		if (item == null && currentSlotState == SlotState.Idle)
		{
			if (game.topZoomContextSafe(out var _) && Controller.isActive())
			{
				return ItemCheck.NotRequired;
			}
			if (insertedItem != null)
			{
				return ItemCheck.NotRequired;
			}
		}
		return ItemCheck.Fails;
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Slot;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Regular;
	}

	public override Interactive overrideSelection()
	{
		return insertedItem;
	}

	public bool canPlaceItem(Item itemObject)
	{
		if (itemObject != null)
		{
			if (!UnityUtils.contains(acceptItems, itemObject))
			{
				return UnityUtils.contains(rejectItems, itemObject);
			}
			return true;
		}
		return false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.isTargetableAndIdle)
		{
			triggerEnterLastFrame.Add((other.attachedRigidbody != null) ? other.attachedRigidbody.gameObject : other.gameObject);
		}
	}

	private void OnValidate()
	{
		if (insertedItem != null && insertedItem.slot != this)
		{
			insertedItem.slot = this;
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.WriteArray(acceptItems, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
		writer.WriteArray(rejectItems, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
		writer.WriteComponent(insertedItem);
		writer.Write(in isUnlocked, default(FastBinaryWriter.ForPrimitives));
		int value = (int)currentSlotState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(itemForCurrentInteraction);
		writer.WriteVector3(in itemTravellingStartPosition);
		writer.WriteVector3(in originalItemInZoomScale);
		writer.WriteVector3(in originalZoomScale);
		writer.WriteQuaternion(in itemTravellingStartRotation);
		writer.Write(in isSilentPlacement, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		acceptItems = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>());
		rejectItems = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>());
		insertedItem = reader.ReadComponent<Item>();
		isUnlocked = reader.ReadBoolean();
		currentSlotState = (SlotState)reader.ReadInt32();
		itemForCurrentInteraction = reader.ReadComponent<Item>();
		itemTravellingStartPosition = reader.ReadVector3();
		originalItemInZoomScale = reader.ReadVector3();
		originalZoomScale = reader.ReadVector3();
		itemTravellingStartRotation = reader.ReadQuaternion();
		isSilentPlacement = reader.ReadBoolean();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		Item[] array = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "acceptItems[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", (IEnumerable<Item>)array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Item[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rejectItems[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", (IEnumerable<Item>)array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Item arg = reader.ReadComponent<Item>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "insertedItem",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isUnlocked",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		SlotState slotState = (SlotState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSlotState",
			fieldValue = $"{slotState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Item arg2 = reader.ReadComponent<Item>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "itemForCurrentInteraction",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "itemTravellingStartPosition",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "originalItemInZoomScale",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector3 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "originalZoomScale",
			fieldValue = $"{vector3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion quaternion = reader.ReadQuaternion();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "itemTravellingStartRotation",
			fieldValue = $"{quaternion}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSilentPlacement",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
