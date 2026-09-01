using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Localisation;
using UnityEngine;

[Serializable]
public class MapEntity
{
	public string ID;

	public string RawID;

	public TextIdentifier Name;

	public int IDIndex;

	public string Icon;

	public Sprite IconRaw;

	public EntityRoles Role;

	public Vector3 Position;

	public MapEntityStates State;

	public int MaxHealth;

	public int Armour;

	public int Stars;

	public int Scale;

	public List<string> ImmuneShells;

	[NonSerialized]
	public EntityLocation Location;

	public int Health;

	public unsafe bool IsAlive
	{
		get
		{
			object obj = default(object);
			object obj2 = default(object);
			bool flag = FlagExtensions.Has((MapEntityStates)(int)(&obj), (MapEntityStates)(int)(&obj2));
			return (byte)((flag ? 1u : 0u) ^ 1u) != 0;
		}
	}

	public MapEntity()
	{
		List<string> immuneShells = new List<string>();
		ImmuneShells = immuneShells;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
