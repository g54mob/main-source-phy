using System;
using UnityEngine;

[Serializable]
public class VRFinger
{
	public Transform source;

	public float radius;

	[NonSerialized]
	public int controllerId;
}
