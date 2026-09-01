using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EntityEvent
{
	public EventContainer eventData;

	public EventContainer.EventType eventType;

	public ushort ID;

	private EventContainer.EventType _loadEventType;

	private EventContainer _loadEventData;

	public List<long> entityList = new List<long>();

	public List<long> _loadEntityList = new List<long>();

	public MPTeam team;

	public MPTeam _loadTeam;

	public int LoadEntityCount
	{
		get
		{
			return _loadEntityList.Count;
		}
	}

	public EventContainer.EventType LoadEventType
	{
		get
		{
			return _loadEventType;
		}
	}

	public static int LoadOffset
	{
		get
		{
			return 2;
		}
	}

	public event LogicChangeHandler EventChanged;

	public EntityEvent()
	{
		ID = EntityLogic.GenerateID();
		InvokeEventChanged();
	}

	public EntityEvent(EventContainer.EventType e)
	{
		ID = EntityLogic.GenerateID();
		Init(e);
		InvokeEventChanged();
	}

	public EntityEvent(string data)
	{
		Load(data);
	}

	public void ResetValue()
	{
		eventType = _loadEventType;
		eventData = _loadEventData.Clone();
		eventData.entityEvent = this;
		team = _loadTeam;
		entityList.Clear();
		entityList.AddRange(_loadEntityList);
	}

	public void ReplaceEntityReference(long oldReference, long newReference)
	{
		bool flag = false;
		for (int i = 0; i < entityList.Count; i++)
		{
			long num = entityList[i];
			if (num == oldReference)
			{
				entityList[i] = newReference;
				flag = true;
			}
		}
		if (flag)
		{
			ApplyValue();
		}
	}

	public void ApplyValue()
	{
		string text = eventData.Save();
		string text2 = string.Empty;
		for (int i = 0; i < LoadOffset; i++)
		{
			text2 += " |";
		}
		text2 += text;
		string[] stringData = text2.Split('|');
		_loadEventData.Load(stringData);
		_loadEventType = eventType;
		_loadTeam = team;
		_loadEntityList.Clear();
		_loadEntityList.AddRange(entityList);
	}

	public void ChangeEvent(EventContainer.EventType newEvent)
	{
		eventType = newEvent;
		eventData = EventContainer.GetDefault(newEvent);
		eventData.entityEvent = this;
	}

	public void Load(string data)
	{
		string[] array = data.Split('|');
		if (ushort.TryParse(array[0], out ID))
		{
			if (array.Length < 2)
			{
				Debug.LogError("Expected 2 arguments when loading EntityEvent, data='" + data + "'!");
				return;
			}
			Init(EventContainer.GetEvent(array[1]));
			eventData.Load(array);
			ApplyValue();
		}
	}

	public string Save()
	{
		return string.Concat(ID, "|", eventType, "|", eventData.Save());
	}

	public string SaveLoadValue()
	{
		return string.Concat(ID, "|", _loadEventType, "|", _loadEventData.SaveLoadValue());
	}

	private void Init(EventContainer.EventType evt)
	{
		ChangeType(evt);
		_loadEventData = EventContainer.GetDefault(evt);
		_loadEventData.entityEvent = this;
		ApplyValue();
	}

	public void ChangeType(EventContainer.EventType e)
	{
		EventContainer eventContainer = EventContainer.GetDefault(e);
		eventContainer.entityEvent = this;
		if (EventContainer.IsBehaviourEvent(eventType) && EventContainer.IsBehaviourEvent(e))
		{
			eventContainer = eventData.Clone();
		}
		eventType = e;
		eventData = eventContainer;
	}

	public virtual void UpdateEvent(float delta)
	{
		eventData.UpdateEvent(delta);
	}

	public void Reset()
	{
		eventData.Reset();
	}

	public byte[] Encode()
	{
		string s = Save();
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		int num = NetworkCompression.PackedUIntLength(bytes.Length, false);
		byte[] array = new byte[num + bytes.Length];
		NetworkCompression.PackUInt(bytes.Length, array, 0, false, num);
		Buffer.BlockCopy(bytes, 0, array, num, bytes.Length);
		return array;
	}

	public int Decode(byte[] data, int offset)
	{
		int count;
		int num = NetworkCompression.UnpackUInt(data, offset, false, out count);
		string data2 = Encoding.UTF8.GetString(data, offset + num, count);
		Load(data2);
		InvokeEventChanged();
		return count + num;
	}

	private void InvokeEventChanged()
	{
		LogicChangeHandler eventChanged = this.EventChanged;
		if (eventChanged != null)
		{
			eventChanged();
		}
	}
}
