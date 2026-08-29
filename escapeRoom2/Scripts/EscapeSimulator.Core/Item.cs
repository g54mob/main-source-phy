using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/Item")]
public class Item : Interactive
{
	public ItemType itemType;

	[DontSave]
	public ItemRespawn itemRespawn;

	[DontSave]
	public int collectionId;

	[DontSave]
	public ItemSparkCalculation sparkCalculation;

	[DontSave]
	public bool isPinningDisabled;

	[HideInInspector]
	[DontSave]
	public Vector3 initialPosition;

	[HideInInspector]
	[DontSave]
	public Quaternion initialRotation;

	public Slot slot;

	public SurfaceSlot surfaceSlot;

	[NonSerialized]
	public bool inSlotAnimation;

	[DontSave]
	public bool carriable;

	public bool hasRigidbody;

	[DontSave]
	public float mass = 1f;

	[DontSave]
	public float angularDrag = 0.05f;

	[DontSave]
	public float drag;

	[DontSave]
	public CollisionDetectionMode collisionDetection = CollisionDetectionMode.Continuous;

	[DontSave]
	public RigidbodyOverrides rbOverrides;

	[DontSave]
	public bool resetTargetPriorityOnInventoryAdd = true;

	[DontSave]
	public bool retainZoomRotation;

	[DontSave]
	public Vector2 retainedZoomRotation = Vector2.zero;

	[DontSave]
	public Vector3 examinePivotOffset;

	[DontSave]
	public Vector3 examineBaseRotation;

	[DontSave]
	public float examineScaleModifier = 1f;

	[DontSave]
	public Vector3 handRotationOffset;

	[DontSave]
	public float handScale = 1f;

	[DontSave]
	public Vector3 handPivotOffset = Vector3.zero;

	[DontSave]
	public Vector3 toolZoomRotationOffset;

	[DontSave]
	public float toolZoomScale = 1f;

	[DontSave]
	public Vector3 toolZoomPivotOffset = Vector3.zero;

	[DontSave]
	public Vector3 inventoryPositionOffset;

	[DontSave]
	public Vector3 inventoryRotationOffset;

	[DontSave]
	public float inventoryScale = 1f;

	[DontSave]
	public Vector3 groundRotation;

	[HideInInspector]
	public bool playedPickupStinger;

	[DontSave]
	[HideInInspector]
	public EventReference soundPickup;

	[DontSave]
	[HideInInspector]
	public EventReference soundHit;

	[DontSave]
	[HideInInspector]
	public EventReference soundPlace;

	[DontSave]
	[HideInInspector]
	public EventReference soundOrbitStart;

	[DontSave]
	[HideInInspector]
	public EventReference soundOrbitLoop;

	[DontSave]
	[HideInInspector]
	public EventReference soundOrbitEnd;

	[NonSerialized]
	[DontSave]
	public GameObject sparkle;

	[NonSerialized]
	public bool sparkleActive;

	[NonSerialized]
	public float timeSparklesActivated;

	[DontSave]
	public bool disableDragInZoom;

	[DontSave]
	public ItemTool toolType;

	[DontSave]
	public ToolInteractionType toolInteraction;

	[DontSave]
	public Interactive[] toolTargets;

	[DontSave]
	public Game.PCCrosshair toolCrosshair = Game.PCCrosshair.Default;

	[DontSave]
	public string toolActionDescription = "";

	[DontSave]
	public ToolZoomType toolZoomType;

	[DontSave]
	public float toolMaxRange = float.PositiveInfinity;

	[DontSave]
	public float toolZoomFov = 40f;

	[DontSave]
	public float toolUseDuration = -1f;

	[DontSave]
	public bool disableLookDownFasterPlace;

	[HideInInspector]
	public float toolUseTimer;

	[DontSave]
	public bool inZoomAnimation;

	[DontSave]
	public bool lockItemInteractions;

	[DontSave]
	public bool blockMovement;

	[HideInInspector]
	public bool hideItemInHand;

	[HideInInspector]
	public bool unlockItemInteractions;

	[NonSerialized]
	[DontSave]
	internal Rigidbody rigidbody;

	[NonSerialized]
	[DontSave]
	internal Renderer renderer;

	[NonSerialized]
	[DontSave]
	internal TransformSync transformSync;

	[NonSerialized]
	[DontSave]
	internal int defaultLayer;

	public override void init()
	{
		defaultLayer = base.gameObject.layer;
		rigidbody = base.gameObject.GetComponent<Rigidbody>();
		renderer = base.gameObject.GetComponentInChildren<Renderer>();
		initialPosition = base.transform.position;
		initialRotation = base.transform.rotation;
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Item;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Pickup;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!soundHit.IsNull && !collision.collider.isTrigger && hasRigidbody && rigidbody != null && collision.relativeVelocity.sqrMagnitude > 4f)
		{
			PineFmod.playOneShotSoundAttached(soundHit, base.gameObject);
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		int value = (int)itemType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(slot);
		writer.WriteComponent(surfaceSlot);
		writer.Write(in inSlotAnimation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hasRigidbody, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playedPickupStinger, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sparkleActive, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in timeSparklesActivated, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in toolUseTimer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hideItemInHand, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in unlockItemInteractions, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		itemType = (ItemType)reader.ReadInt32();
		slot = reader.ReadComponent<Slot>();
		surfaceSlot = reader.ReadComponent<SurfaceSlot>();
		inSlotAnimation = reader.ReadBoolean();
		hasRigidbody = reader.ReadBoolean();
		playedPickupStinger = reader.ReadBoolean();
		sparkleActive = reader.ReadBoolean();
		timeSparklesActivated = reader.ReadSingle();
		toolUseTimer = reader.ReadSingle();
		hideItemInHand = reader.ReadBoolean();
		unlockItemInteractions = reader.ReadBoolean();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		ItemType itemType = (ItemType)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "itemType",
			fieldValue = $"{itemType}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Slot arg = reader.ReadComponent<Slot>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "slot",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		SurfaceSlot arg2 = reader.ReadComponent<SurfaceSlot>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "surfaceSlot",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "inSlotAnimation",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hasRigidbody",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playedPickupStinger",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sparkleActive",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "timeSparklesActivated",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "toolUseTimer",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hideItemInHand",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "unlockItemInteractions",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
