using System;
using UnityEngine;

[Serializable]
public class VisualEffect
{
	public string EffectName = "";

	public Transform effectRoot;

	public ParticleEffect[] particleEffects;
}
