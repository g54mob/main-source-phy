using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Prop
{
	[Tooltip("Unique identifier assigned to this prop.")]
	public PropID ID;

	[Tooltip("Name shown when you hover over prop or search for props.")]
	public string name;

	[Tooltip("All of the categories that this prop is a part of.")]
	public List<PropTag> tags;

	[Tooltip("All of the complex props that this prop is a part of.")]
	public List<PropID> roots = new List<PropID>();
}
