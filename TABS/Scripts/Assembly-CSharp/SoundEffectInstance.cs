using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundEffectInstance
{
	public enum MaxInstanceBehaviour
	{
		Steal = 0,
		Cap = 1
	}

	public string soundRef = "";

	public SoundEffectVariations[] clipTypes;

	public Vector2 volume = new Vector2(1f, 1f);

	public Vector2 pitch = new Vector2(1f, 1f);

	[Range(0f, 256f)]
	public int priority = 128;

	public MaxInstanceBehaviour maxInstanceBehaviour;

	public int maxInstances = 10;

	public float cooldown = 0.1f;

	public AudioMixerGroup overrideMixerGroup;

	[Range(0f, 1f)]
	public float spatialBlend = 1f;

	[Range(0f, 1f)]
	public float reverbZoneMix = 1f;

	public float minDistance = 1f;

	public float maxDistance = 500f;

	public int lengthInMeasures;

	public int[] transitionMeasures;

	[HideInInspector]
	public SoundBackSettings category;

	public AudioMixerGroup categoryMixerGroup;

	public int GetIDFromMeterialType(SoundEffectVariations.MaterialType materialType)
	{
		for (int i = 0; i < clipTypes.Length; i++)
		{
			if (clipTypes[i].materialType == materialType)
			{
				return i;
			}
		}
		return 0;
	}
}
