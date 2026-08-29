using System;
using UnityEngine;

[DisallowMultipleComponent]
public class Occludee : MonoBehaviour
{
	[NonSerialized]
	public GameObject cachedGameObject;

	[NonSerialized]
	public Transform cachedTransform;

	[NonSerialized]
	public Renderer cachedRenderer;

	[NonSerialized]
	public Light cachedLight;

	[NonSerialized]
	public bool wasEnabledLastFrame = true;
}
