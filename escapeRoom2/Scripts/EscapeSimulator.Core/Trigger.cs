using System;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour, ISaveable
{
	[NonSerialized]
	[DontSave]
	public SoundTrigger soundTrigger;

	[DontSave]
	public List<GameObject> keys = new List<GameObject>();

	[DontSave]
	public bool anyObjectCanTrigger;

	[DontSave]
	public bool canPlayerTrigger;

	[DontSave]
	public bool triggerWhenAllPlayersEnter;

	[DontSave]
	public bool isSticky;

	[DontSave]
	public bool isLocalOnly;

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks startData;

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks enterData;

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks exitData;

	[HideInInspector]
	[DontSave]
	public RoomEditorLinks endData;

	[NonSerialized]
	[DontSave]
	public CollisionData collisionData;

	[NonSerialized]
	public HashSet<NetPlayerId> playersInTriggerLastEvent = new HashSet<NetPlayerId>();

	[NonSerialized]
	public HashSet<Interactive> interactivesInTriggerLastEvent = new HashSet<Interactive>();

	public bool state
	{
		get
		{
			if (playersInTriggerLastEvent.Count <= 0)
			{
				return interactivesInTriggerLastEvent.Count > 0;
			}
			return true;
		}
	}

	public void init(Game game)
	{
		collisionData = game.addCollisionData(base.gameObject);
		soundTrigger = GetComponent<SoundTrigger>();
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteHashSet(playersInTriggerLastEvent, delegate(FastBinaryWriter w, NetPlayerId e)
		{
			w.WriteNetPlayerId(e);
		});
		writer.WriteHashSet(interactivesInTriggerLastEvent, delegate(FastBinaryWriter w, Interactive e)
		{
			w.WriteComponent(e);
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		playersInTriggerLastEvent = reader.ReadHashSet((FastBinaryReader r) => r.ReadNetPlayerId());
		interactivesInTriggerLastEvent = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Interactive>());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		HashSet<NetPlayerId> hashSet = reader.ReadHashSet((FastBinaryReader r) => r.ReadNetPlayerId());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playersInTriggerLastEvent[" + ((hashSet == null) ? string.Empty : hashSet.Count.ToString()) + "]",
			fieldValue = (((hashSet == null) ? "null" : string.Join(", ", hashSet)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<Interactive> hashSet2 = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Interactive>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "interactivesInTriggerLastEvent[" + ((hashSet2 == null) ? string.Empty : hashSet2.Count.ToString()) + "]",
			fieldValue = (((hashSet2 == null) ? "null" : string.Join(", ", hashSet2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
