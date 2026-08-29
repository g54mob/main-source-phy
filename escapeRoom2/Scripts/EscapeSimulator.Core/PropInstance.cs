using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PropInstance : MonoBehaviour
{
	[HideInInspector]
	public InstanceID ID;

	[NonSerialized]
	public PropInstance parent;

	[NonSerialized]
	public readonly List<PropInstance> children = new List<PropInstance>();

	[NonSerialized]
	public TransformData originalPivot;

	[NonSerialized]
	public bool isExpandedInHierarchy;

	public string displayName;

	[ReadOnly]
	public string scriptName;

	[NonSerialized]
	public string exportName;
}
