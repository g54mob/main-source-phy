using System;
using UnityEngine;

namespace Shapes;

[Serializable]
public class Decayer
{
	public float decaySpeed;

	public float magnitude;

	public AnimationCurve curve;

	[NonSerialized]
	public float value;

	[NonSerialized]
	public float valueInv;

	[NonSerialized]
	public float t;

	public void SetT(float v)
	{
		t = v;
	}

	public void Update()
	{
		//IL_0034: Invalid comparison between I4 and F4
		//IL_0046: Expected F4, but got I4
		float deltaTime = Time.deltaTime;
		float num = deltaTime * decaySpeed;
		float num2 = t - num;
		bool flag = !(0f < num2);
		float num3 = 0f;
		if (!flag)
		{
			num3 = num2;
		}
		t = num3;
		Keyframe[] keys = curve.keys;
		float num5;
		if (keys.Length != 0)
		{
			float time = 1f - t;
			float num4 = curve.Evaluate(time);
			num5 = num4;
		}
		else
		{
			num5 = t;
		}
		float num6 = 1f - num5;
		float num7 = num5 * magnitude;
		float num8 = num6 * magnitude;
		value = num7;
		valueInv = num8;
	}
}
