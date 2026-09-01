using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundBankCategory
{
	public string categoryName = "";

	public SoundEffectInstance[] soundEffects;

	public AudioMixerGroup categoryMixerGroup;

	[Range(0f, 1f)]
	public float volumeMultiplier = 1f;

	[Range(0f, 1f)]
	public float pitchMultiplier = 1f;

	[Range(0f, 1f)]
	public float minDistanceMultiplier = 1f;

	[Range(0f, 1f)]
	public float maxDistanceMultiplier = 1f;
}
