using System;
using UnityEngine;

[Serializable]
public class ObjectParticle
{
	public float size = 1f;

	public AnimationCurve sizeOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	public float lifetime = 1f;

	public float rotation;

	public float randomRotation;

	public Color color = Color.magenta;

	public Color randomColor = Color.magenta;

	public Color randomAddedColor = Color.black;

	public float randomAddedSaturation;

	public bool singleRandomValueColor = true;

	public AnimationCurve alphaOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);
}
