using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TriggerTarget
{
	private static Dictionary<string, TriggerTargetType> targetTypeLookup = new Dictionary<string, TriggerTargetType>
	{
		{
			"Anything",
			TriggerTargetType.Anything
		},
		{
			"AnyBlock",
			TriggerTargetType.AnyBlock
		},
		{
			"AnyProjectile",
			TriggerTargetType.AnyProjectile
		},
		{
			"AnyLevelObject",
			TriggerTargetType.AnyLevelObject
		},
		{
			"Picker",
			TriggerTargetType.Picker
		}
	};

	private Dictionary<string, TriggerTargetObjectType> targetObjectTypeLookup = new Dictionary<string, TriggerTargetObjectType>
	{
		{
			"All",
			TriggerTargetObjectType.All
		},
		{
			"Entity",
			TriggerTargetObjectType.Entity
		},
		{
			"Block",
			TriggerTargetObjectType.Block
		}
	};

	public ushort ID;

	public TriggerTargetType targetType;

	public TriggerTargetObjectType type;

	public bool IsEntityType;

	public int PrefabID;

	public long EntityID = LevelPrefab.UNASSIGNED_ID;

	public BlockType TargetBlockType;

	public MPTeam Team;

	private TriggerTargetType _loadTargetType;

	private TriggerTargetObjectType _loadType;

	private bool _loadIsEntityType;

	private int _loadPrefabID;

	private long _loadEntityID;

	private BlockType _loadTargetBlockType;

	private MPTeam _loadTeam;

	public MText entityName;

	public Collider coll;

	public int targetTypeCount
	{
		get
		{
			return 5;
		}
	}

	public bool hasTeam
	{
		get
		{
			return targetType == TriggerTargetType.AnyBlock || targetType == TriggerTargetType.AnyProjectile || (targetType == TriggerTargetType.Picker && type == TriggerTargetObjectType.Block);
		}
	}

	public event TargetChangeHandler TargetChanged;

	public TriggerTarget()
	{
		ID = EntityLogic.GenerateID();
		InvokeTargetChanged();
	}

	public TriggerTarget(TriggerTargetType type)
	{
		ID = EntityLogic.GenerateID();
		targetType = type;
		InvokeTargetChanged();
	}

	public TriggerTarget(TriggerTarget target)
	{
		ID = EntityLogic.GenerateID();
		targetType = (_loadTargetType = target.targetType);
		IsEntityType = (_loadIsEntityType = target.IsEntityType);
		type = (_loadType = target.type);
		coll = target.coll;
		if (type == TriggerTargetObjectType.Entity)
		{
			PrefabID = (_loadPrefabID = target.PrefabID);
			EntityID = (_loadEntityID = target.EntityID);
		}
		else if (type == TriggerTargetObjectType.Block)
		{
			TargetBlockType = (_loadTargetBlockType = target.TargetBlockType);
		}
		InvokeTargetChanged();
	}

	public TriggerTarget(string data)
	{
		Load(data);
	}

	public void ReplaceEntityReference(long oldReference, long newReference)
	{
		if (EntityID == oldReference)
		{
			EntityID = newReference;
			ApplyValue();
		}
	}

	public void ApplyValue()
	{
		_loadTargetType = targetType;
		_loadType = type;
		_loadIsEntityType = IsEntityType;
		_loadPrefabID = PrefabID;
		_loadEntityID = EntityID;
		_loadTargetBlockType = TargetBlockType;
		_loadTeam = Team;
	}

	public void ResetValue()
	{
		targetType = _loadTargetType;
		type = _loadType;
		IsEntityType = _loadIsEntityType;
		PrefabID = _loadPrefabID;
		EntityID = _loadEntityID;
		TargetBlockType = _loadTargetBlockType;
		Team = _loadTeam;
	}

	private void Load(string data)
	{
		string[] array = data.Split('|');
		int num = 0;
		if (!ushort.TryParse(array[num++], out ID) || !targetTypeLookup.TryGetValue(array[num++], out targetType))
		{
			return;
		}
		if (targetType == TriggerTargetType.Picker)
		{
			if (!targetObjectTypeLookup.TryGetValue(array[num++], out type))
			{
				return;
			}
			if (type == TriggerTargetObjectType.Entity)
			{
				string text = array[num++];
				if (!int.TryParse(text, out PrefabID))
				{
					Debug.LogWarning("Couldn't parse prefab ID '" + text + "'!");
				}
				IsEntityType = int.Parse(array[num++]) == 1;
				EntityID = long.Parse(array[num++]);
			}
			else if (type == TriggerTargetObjectType.Block)
			{
				TargetBlockType = (BlockType)int.Parse(array[num++]);
			}
		}
		if (hasTeam)
		{
			int num2 = num++;
			if (num2 < array.Length)
			{
				Team = (MPTeam)int.Parse(array[num2]);
			}
			else
			{
				Team = MPTeam.None;
			}
		}
		ApplyValue();
	}

	public string Save()
	{
		string text = ID + "|" + targetType;
		if (targetType == TriggerTargetType.Picker)
		{
			text = text + "|" + type;
			if (type == TriggerTargetObjectType.Entity)
			{
				string text2 = text;
				text = text2 + "|" + PrefabID + "|" + (IsEntityType ? 1 : 0) + "|" + EntityID;
			}
			else if (type == TriggerTargetObjectType.Block)
			{
				text = text + "|" + (int)TargetBlockType;
			}
		}
		if (hasTeam)
		{
			int team = (int)Team;
			text = text + "|" + team;
		}
		return text;
	}

	public string SaveLoadValue()
	{
		string text = ID + "|" + _loadTargetType;
		if (_loadTargetType == TriggerTargetType.Picker)
		{
			text = text + "|" + _loadType;
			if (_loadType == TriggerTargetObjectType.Entity)
			{
				string text2 = text;
				text = text2 + "|" + _loadPrefabID + "|" + (_loadIsEntityType ? 1 : 0) + "|" + _loadEntityID;
			}
			else if (_loadType == TriggerTargetObjectType.Block)
			{
				text = text + "|" + (int)_loadTargetBlockType;
			}
		}
		if (hasTeam)
		{
			int loadTeam = (int)_loadTeam;
			text = text + "|" + loadTeam;
		}
		return text;
	}

	public byte[] Encode()
	{
		byte[] bytes = Encoding.UTF8.GetBytes(Save());
		byte[] array = new byte[1 + bytes.Length];
		array[0] = (byte)bytes.Length;
		Buffer.BlockCopy(bytes, 0, array, 1, bytes.Length);
		return array;
	}

	public int Decode(byte[] data, int offset)
	{
		int num = data[offset];
		string data2 = Encoding.UTF8.GetString(data, offset + 1, num);
		Load(data2);
		InvokeTargetChanged();
		return num + 1;
	}

	private void InvokeTargetChanged()
	{
		TargetChangeHandler targetChanged = this.TargetChanged;
		if (targetChanged != null)
		{
			targetChanged();
		}
	}
}
