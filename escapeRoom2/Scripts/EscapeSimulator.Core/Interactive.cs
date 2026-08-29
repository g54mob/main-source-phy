using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactive : MonoBehaviour, ISaveable
{
	[NonSerialized]
	[DontSave]
	public Game game;

	[DontSave]
	public Game.PCCrosshair Cursor = Game.PCCrosshair.Default;

	[DontSave]
	public bool unlockCursor;

	public bool blocksRaycasts = true;

	public bool targetable = true;

	public int targetPriority = -1;

	[HideInInspector]
	public RoomEditorTargetPriotity roomEditorTargetPriotity;

	[DontSave]
	public string displayName;

	public float interactionMaxDistance = -1f;

	public float distanceExtender;

	[DontSave]
	public bool zoomInInteraction;

	[DontSave]
	[HideInInspector]
	public Vector3 eyePosition;

	[DontSave]
	[HideInInspector]
	public Vector3 eyeRotation;

	[DontSave]
	[HideInInspector]
	public float zoomInTimer;

	[NonSerialized]
	[DontSave]
	public int zoomCounter;

	[NonSerialized]
	[ResolveChangesIgnore]
	public int hostSyncedInteractionId;

	[NonSerialized]
	[ResolveChangesIgnore]
	public int sessionId = -1;

	[NonSerialized]
	[ResolveChangesIgnore]
	public bool backgroundUpdate;

	[NonSerialized]
	[ResolveChangesIgnore]
	public float backgroundTime;

	[NonSerialized]
	[DontSave]
	public NetPlayerId authorityPlayerId;

	[NonSerialized]
	[DontSave]
	public StudioEventEmitter soundEmitter;

	[NonSerialized]
	[DontSave]
	public readonly List<Interactive> linkedInteractives = new List<Interactive>();

	public bool isTargetableAndIdle
	{
		get
		{
			if (targetable)
			{
				if (backgroundUpdate)
				{
					return !doesBackgroundUpdateForbidInteraction;
				}
				return true;
			}
			return false;
		}
	}

	protected virtual bool doesBackgroundUpdateForbidInteraction => true;

	public virtual bool isInternallyTargetable => true;

	public new GameObject gameObject => base.gameObject;

	public new Transform transform => base.transform;

	public virtual Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.None;
	}

	public virtual ItemCheck itemCheck(Item item)
	{
		if (item == null)
		{
			return ItemCheck.NotRequired;
		}
		if (item.toolType == ItemTool.AlwaysActive || Array.IndexOf(item.toolTargets, this) != -1)
		{
			return ItemCheck.Passes;
		}
		return ItemCheck.Fails;
	}

	protected virtual Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Regular;
	}

	public Game.PCCrosshair getCursor()
	{
		if (Cursor == Game.PCCrosshair.Default)
		{
			return getBaseCursor();
		}
		return Cursor;
	}

	public virtual Interactive overrideSelection()
	{
		return null;
	}

	public virtual void init()
	{
	}

	public static void linkInteractives(params Interactive[] interactives)
	{
		if (interactives == null)
		{
			Debug.LogError("Cannot link null interactives array.");
			return;
		}
		List<Interactive> list = new List<Interactive>();
		for (int i = 0; i < interactives.Length; i++)
		{
			Interactive interactive = interactives[i];
			if (interactive == null)
			{
				Debug.LogError($"Null interactive found at index {i}.");
			}
			else if (list.Contains(interactive))
			{
				Debug.LogError($"Duplicate interactive found at index {i} ({interactive.name}).", interactive);
			}
			else
			{
				list.Add(interactive);
			}
		}
		Debug.Log($"Linking {list.Count} interactives:");
		for (int j = 0; j < list.Count; j++)
		{
			Interactive interactive2 = interactives[j];
			Debug.Log($"{j + 1}. {interactive2}", interactive2);
			for (int k = 0; k < list.Count; k++)
			{
				if (j != k)
				{
					Interactive item = interactives[k];
					if (!interactive2.linkedInteractives.Contains(item))
					{
						interactive2.linkedInteractives.Add(item);
					}
				}
			}
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in blocksRaycasts, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetable, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetPriority, default(FastBinaryWriter.ForPrimitives));
		int value = (int)roomEditorTargetPriotity;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in interactionMaxDistance, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in distanceExtender, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hostSyncedInteractionId, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sessionId, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in backgroundUpdate, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in backgroundTime, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		blocksRaycasts = reader.ReadBoolean();
		targetable = reader.ReadBoolean();
		targetPriority = reader.ReadInt32();
		roomEditorTargetPriotity = (RoomEditorTargetPriotity)reader.ReadInt32();
		interactionMaxDistance = reader.ReadSingle();
		distanceExtender = reader.ReadSingle();
		hostSyncedInteractionId = reader.ReadInt32();
		sessionId = reader.ReadInt32();
		backgroundUpdate = reader.ReadBoolean();
		backgroundTime = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "blocksRaycasts",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "targetable",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "targetPriority",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		RoomEditorTargetPriotity roomEditorTargetPriotity = (RoomEditorTargetPriotity)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "roomEditorTargetPriotity",
			fieldValue = $"{roomEditorTargetPriotity}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "interactionMaxDistance",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distanceExtender",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hostSyncedInteractionId",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num5 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sessionId",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "backgroundUpdate",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num6 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "backgroundTime",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
