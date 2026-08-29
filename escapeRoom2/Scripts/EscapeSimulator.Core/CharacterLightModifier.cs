using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Light))]
public class CharacterLightModifier : MonoBehaviour
{
	[Range(0f, 16f)]
	public float lightMultiplier = 0.1f;

	[NonSerialized]
	public Light originalLight;

	[NonSerialized]
	public Light targetLight;

	[NonSerialized]
	public HDAdditionalLightData originalLightHD;

	[NonSerialized]
	public HDAdditionalLightData targetLightHD;
}
