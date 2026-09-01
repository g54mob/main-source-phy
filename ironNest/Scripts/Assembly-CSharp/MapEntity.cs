using System;
using System.Collections.Generic;
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

	public bool IsAlive => false;
}
